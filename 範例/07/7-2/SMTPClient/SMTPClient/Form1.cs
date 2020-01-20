using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;

using System.IO;
using System.Threading;
using System.Collections;

namespace SMTPClient
{
  public partial class Form1 : Form
  {
    public Socket smtpSocket;

    public Form1()
    {
      InitializeComponent();
    }

    private void ToolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
    {
      switch (ToolBar1.Buttons.IndexOf(e.Button))
      {
        case 0:
          // Send
          Thread mailThread = new Thread(new ThreadStart(ProcessMail));
          mailThread.Start();

          break;

        case 1:
          // Clear
          txtTo.Clear();
          txtSubject.Clear();
          txtMessage.Clear();
          lstLog.Items.Clear();

          break;
      }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      txtTo.Clear();
      txtSubject.Clear();
      txtMessage.Clear();
      lstLog.Items.Clear();
    }

    private void Form1_Resize(object sender, EventArgs e)
    {
      Control control = (Control)sender;

      txtTo.Location = new System.Drawing.Point(53, 7);
      txtTo.Size = new System.Drawing.Size(control.Size.Width - 85, 22);
      txtSubject.Location = new System.Drawing.Point(53, 35);
      txtSubject.Size = new System.Drawing.Size(control.Size.Width - 85, 22);

      txtHost.Location = new System.Drawing.Point(48, 4);
      txtHost.Size = new System.Drawing.Size(control.Size.Width - 80, 22);
      txtPort.Location = new System.Drawing.Point(48, 32);
      txtPort.Size = new System.Drawing.Size(control.Size.Width - 80, 22);
      txtFrom.Location = new System.Drawing.Point(48, 60);
      txtFrom.Size = new System.Drawing.Size(control.Size.Width - 80, 22);
    }

    private void mnuExit_Click(object sender, EventArgs e)
    {
      DialogResult result = MessageBox.Show("Are you sure to quit?", "SMTP Client", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

      if (result == DialogResult.Yes)
      {
        try
        {
          // 判斷是否仍連線至郵件伺服器
          if (smtpSocket.Connected)
          {
            // 同時暫停用戶端的傳送和接收作業
            smtpSocket.Shutdown(SocketShutdown.Both);
            // 關閉用戶端Socket
            smtpSocket.Close();
          }
        }
        catch (Exception) { }

        this.Close();
      }
    }

    private void ProcessMail()
    {
      string msg;

      lstLog.Items.Clear();

      string host = txtHost.Text;       // SMTP郵件伺服器之主機名稱或IP位址
      string port = txtPort.Text;       // SMTP郵件伺服器之通訊埠，預設通訊埠為25
      string from = txtFrom.Text;       // 寄件者郵件地址
      string to = txtTo.Text;           // 收件者郵件地址
      string subject = txtSubject.Text; // 郵件主旨
      string message = txtMessage.Text; // 郵件內容

      // 連線郵件伺服器
      try
      {
        // Connect to SMTP server
        lstLog.Items.Add("C: Trying to connect to host " + host + ", port: " + port);

        IPAddress smtpServerIP = Dns.Resolve(host).AddressList[0];

        IPEndPoint smtpServerHost = new IPEndPoint(smtpServerIP, Int32.Parse(port));

        smtpSocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // 建立用戶端與郵件伺服器連線
        smtpSocket.Connect(smtpServerHost);

        // 判斷是否無法連線至郵件伺服器
        if (!smtpSocket.Connected)
          lstLog.Items.Add("無法連線至郵件伺服器: " + host + ":" + port);

        // 判斷郵件伺服器是否回應 220 Ready 訊息
        if (!SMTPResponse("220"))
          return;
      }
      catch (Exception ex)
      {
        lstLog.Items.Add(ex.ToString());
      }

      // 傳送郵件訊息
      try
      {
        string data = "";

        // 用戶端傳送 HELO <Mail Server> 訊息回應郵件伺服器
        data = "HELO " + host + "\r\n";

        // 傳送郵件訊息
        if (!SMTPRequest(data))
          return;

        // 判斷郵件伺服器是否回應 250 OK 訊息
        if (!SMTPResponse("250"))
          return;

        // 用戶端傳送 MAIL FROM：<寄件者E-Mail Address> 訊息
        // 一旦有任何錯誤或產生郵件回應時，將回傳至此郵件地址
        data = "MAIL FROM: " + from + "\r\n";
        if (!SMTPRequest(data))
          return;

        // 若寄件者郵件地址正確，郵件伺服器將回傳 250 OK 訊息
        // 否則將回傳 550 No such user 訊息
        if (!SMTPResponse("250"))
          return;

        // 當收件者有一個以上時，需以逗號（Comma）隔開
        string[] toAddress = to.Split(",".ToCharArray());

        for (int i = 0; i < toAddress.Length; i++)
        {
          if (toAddress[i].Trim().ToString() != "")
          {
            // 用戶端傳送 RCPT TO：<收件者E-Mail Address> 指令
            // 代表收件者郵件地址
            data = "RCPT TO: " + toAddress[i].Trim().ToString() + "\r\n";

            // 傳送郵件訊息
            if (!SMTPRequest(data))
              return;

            // 若收件者郵件地址正確，郵件伺服器將回傳 250 OK 訊息
            // 否則將回傳 550 No such user 訊息
            if (!SMTPResponse("250"))
              return;

          }
        }

        // 開始處理郵件標題及內容，用戶端傳送 DATA 指令
        // 告知郵件伺服器接著要開始傳送郵件標題及內容
        data = "DATA" + "\r\n";

        // 傳送郵件訊息
        if (!SMTPRequest(data))
          return;

        // 若正確，郵件伺服器將回應 354 Start mail input 訊息
        if (!SMTPResponse("354"))
          return;

        // 傳送郵件標題之日期 (Date) ，每一行須以<CR><LF>（換行歸位\r\n）結尾
        data = "Date: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "\r\n";

        // 傳送郵件訊息
        if (!SMTPRequest(data))
          return;

        // 傳送郵件標題之寄件者郵件地址 (From) 
        data = "From: " + from + "\r\n";

        // 傳送郵件訊息
        if (!SMTPRequest(data))
          return;

        // 傳送郵件標題之收件者郵件地址 (To) 
        data = "To: " + to + "\r\n";

        // 傳送郵件訊息
        if (!SMTPRequest(data))
          return;

        // 傳送郵件標題之主旨 (Subject) 
        data = "Subject: " + subject + "\r\n" + "\r\n";

        // 傳送郵件訊息
        if (!SMTPRequest(data))
          return;

        // 傳送郵件內容
        data = message + "\r\n";

        // 傳送郵件訊息
        if (!SMTPRequest(data))
          return;

        // 傳送歸位、換行、句點、歸位、換行字串，則<CR><LF>.<CR><LF>
        // 代表郵件內容傳送結束
        data = "\r\n" + "." + "\r\n";

        // 傳送郵件訊息
        if (!SMTPRequest(data))
          return;

        // 判斷郵件伺服器是否回應 250 OK 訊息，代表傳送成功
        if (!SMTPResponse("250"))
          return;

        // 用戶端傳送 QUIT 指令，要求結束通訊連結
        data = "QUIT" + "\r\n";

        // 傳送郵件訊息
        if (!SMTPRequest(data))
          return;

        // 判斷郵件伺服器是否回應 221 Service closing transmission channel 訊息，代表連線結束
        if (!SMTPResponse("221"))
          return;

        // 同時暫停用戶端的傳送和接收作業
        smtpSocket.Shutdown(SocketShutdown.Both);
        // 關閉用戶端Socket
        smtpSocket.Close();
        
      }
      catch (Exception ex)
      {
        lstLog.Items.Add(ex.ToString());
      }
    }

    // 處理用戶端傳送訊息至郵件伺服器
    private bool SMTPRequest(string dataSent)
    {
      // 設定傳送資料緩衝區
      byte[] bytes = new Byte[1024];

      try
      {
        lstLog.Items.Add("C: " + dataSent.ToString());

        bytes = Encoding.ASCII.GetBytes(dataSent.ToCharArray());

        // 傳送資料至郵件伺服器
        int bytesSent = smtpSocket.Send(bytes, 0, bytes.Length, SocketFlags.None);

        return (true);
      }
      catch (Exception ex)
      {
        lstLog.Items.Add("SMTP Request Error: " + ex.ToString());

        // 同時暫停用戶端的傳送和接收作業
        smtpSocket.Shutdown(SocketShutdown.Both);
        // 關閉用戶端Socket
        smtpSocket.Close();

        return (false);
      }
    }

    // 處理郵件伺服器回傳訊息至用戶端
    private bool SMTPResponse(string echo)
    {
      // 設定接收資料緩衝區
      byte[] bytes = new Byte[1024];

      try
      {
        // 自郵件伺服器接收資料
        int bytesReceived = smtpSocket.Receive(bytes, 0, bytes.Length, SocketFlags.None);

        string strResponse = Encoding.ASCII.GetString(bytes, 0, bytesReceived);

        lstLog.Items.Add("S: " + strResponse);

        // 判斷郵件伺服器回應訊息
        if (!strResponse.StartsWith(echo))
        {
          lstLog.Items.Add("SMTP Response Error.");

          // 同時暫停用戶端的傳送和接收作業
          smtpSocket.Shutdown(SocketShutdown.Both);
          // 關閉用戶端Socket
          smtpSocket.Close();

          return (false);
        }
        else
        {
          return (true);
        }
      }
      catch (Exception ex)
      {
        lstLog.Items.Add("SMTP Response Error: " + ex.ToString());

        // 同時暫停用戶端的傳送和接收作業
        smtpSocket.Shutdown(SocketShutdown.Both);
        // 關閉用戶端Socket
        smtpSocket.Close();

        return (false);
      }
    }
  }
}

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

namespace POP3Client
{
  public partial class Form1 : Form
  {
    public Socket pop3Socket;

    private int totalMail, currentMail, mailSize;

    public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      txtMessage.Clear();
      lstLog.Items.Clear();

      btnPrevious.Enabled = false;
      btnNext.Enabled = false;
    }

    private void Form1_Resize(object sender, EventArgs e)
    {
      Control control = (Control)sender;

      txtHost.Location = new System.Drawing.Point(62, 6);
      txtHost.Size = new System.Drawing.Size(control.Size.Width - 94, 22);
      txtPort.Location = new System.Drawing.Point(62, 33);
      txtPort.Size = new System.Drawing.Size(control.Size.Width - 94, 22);
      txtUser.Location = new System.Drawing.Point(62, 60);
      txtUser.Size = new System.Drawing.Size(control.Size.Width - 94, 22);
      txtPass.Location = new System.Drawing.Point(62, 87);
      txtPass.Size = new System.Drawing.Size(control.Size.Width - 94, 22);
    }

    private void mnuExit_Click(object sender, EventArgs e)
    {
      DialogResult result = MessageBox.Show("Are you sure to quit?", "POP3 Client", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

      if (result == DialogResult.Yes)
      {
        try
        {
          // 判斷是否仍連線至郵件伺服器
          if (pop3Socket.Connected)
          {
            // 同時暫停用戶端的傳送和接收作業
            pop3Socket.Shutdown(SocketShutdown.Both);
            // 關閉用戶端Socket
            pop3Socket.Close();
          }
        }
        catch (Exception) { }

        this.Close();
      }
    }

    private void ToolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
    {
      bool flag;

      switch (ToolBar1.Buttons.IndexOf(e.Button))
      {
        case 0:
          // 收件
          try
          {
            if (receiveMail())
            {
              if (totalMail > 0)
              {
                currentMail = 1;

                flag = showMail(currentMail);

                showObject(true);
              }
              else
                MessageBox.Show("There is no mail.", "POP3", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else
              showObject(false);
          }
          catch (Exception ex)
          {
            lstLog.Items.Add("Socket: " + ex.ToString());
          }

          break;

        case 1:
          // 顯示上一封郵件
          currentMail = currentMail - 1;

          if (currentMail <= 1)
            currentMail = 1;

          if (!showMail(currentMail))
            currentMail = currentMail + 1;

          showObject(true);

          break;

        case 2:
          // 顯示下一封郵件
          currentMail = currentMail + 1;

          if (currentMail >= totalMail)
            currentMail = totalMail;

          if (!showMail(currentMail))
            currentMail = currentMail - 1;

          showObject(true);

          break;

        case 3:
          // Clear
          txtMessage.Clear();
          lstLog.Items.Clear();
          break;
      }
    }

    private void showObject(bool flag)
    {
      if (flag)
      {
        btnReceive.Enabled = false;

        if ((totalMail > 1) && (currentMail == 1))
        {
          btnPrevious.Enabled = false;
          btnNext.Enabled = true;
        }
        else if ((currentMail < totalMail) && (currentMail > 1))
        {
          btnPrevious.Enabled = true;
          btnNext.Enabled = true;
        }
        else if ((currentMail == totalMail) && (currentMail > 1))
        {
          btnPrevious.Enabled = true;
          btnNext.Enabled = false;
        }
      }
      else
      {
        btnPrevious.Enabled = false;
        btnNext.Enabled = false;
        btnReceive.Enabled = true;

        txtMessage.Clear();
        lstLog.Items.Clear();
      }
    }

    // 收件
    private bool receiveMail()
    {
      string host = txtHost.Text; // POP3郵件伺服器之主機名稱或IP位址
      string port = txtPort.Text; // POP3郵件伺服器之通訊埠，預設通訊埠為110
      string user = txtUser.Text; // 登錄帳號
      string pass = txtPass.Text; // 密碼

      string data = "";
      byte[] bytes;
      int bytesReceived;
      string strResponse;

      lstLog.Items.Clear();

      try
      {
        lstLog.Items.Add("C: Trying to connect to host " + host + ", port: " + port);

        IPAddress pop3ServerIP = Dns.Resolve(host).AddressList[0];

        // 使用伺服端之IPEndPoint
        IPEndPoint pop3ServerHost = new IPEndPoint(pop3ServerIP, Int32.Parse(port));

        pop3Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // 建立用戶端與郵件伺服器連線
        pop3Socket.Connect(pop3ServerHost) ;

        // 判斷是否無法連線至郵件伺服器
        if (!pop3Socket.Connected)
        {
          lstLog.Items.Add("Unable to connect to " + host + ":" + port);
          return (false);
        }

        // 判斷郵件伺服器是否回傳 +OK hello from popgate 訊息?
        if (!POP3Response())
          return (false);

        // 使用者認證
        // USER (登錄帳號)
        data = "USER " + user;

        // 傳送郵件訊息
        if (!POP3Request(data))
          return (false);

        // 判斷郵件伺服器是否回應 +OK password required 訊息?
        if (!POP3Response())
          return (false);

        // PASS
        data = "PASS " + pass;

        // 傳送郵件訊息
        if (!POP3Request(data))
          return (false);

        // 判斷郵件伺服器是否回應 +OK maildrop ready, .. messages 訊息?
        if (!POP3Response())
          return (false);

        // STAT
        data = "STAT";

        // 傳送郵件訊息
        if (!POP3Request(data))
          return (false);

        // 設定接收資料緩衝區
        bytes = new Byte[1024];

        // 郵件伺服器回傳 +OK <# of Mail> <Mail Size>
        bytesReceived = pop3Socket.Receive(bytes, 0, bytes.Length, SocketFlags.None);

        strResponse = Encoding.ASCII.GetString(bytes, 0, bytesReceived);
        
        lstLog.Items.Add("S: " + strResponse.ToString());

        // 判斷郵件伺服器是否回傳 +OK <# of Mail> <Mail Size>
        if (!strResponse.StartsWith("+OK"))
        {
          MessageBox.Show(strResponse.ToString(), "POP3 Error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
          return (false);
        }

        // +OK <#> <Size>
        string[] strTemp = strResponse.Split(" ".ToCharArray());

        totalMail = Int32.Parse(strTemp[1].Trim().ToString());
        mailSize = Int32.Parse(strTemp[2].Trim().ToString());

        return (true);
      }
      catch (Exception ex)
      {
        lstLog.Items.Add(ex.ToString());

        // 同時暫停用戶端的傳送和接收作業
        pop3Socket.Shutdown(SocketShutdown.Both);
        // 關閉用戶端Socket
        pop3Socket.Close();

        return (false);
      }
    }

    private bool showMail(int mailNo)
    {
      // 設定接收資料緩衝區
      byte[] bytes = new Byte[1024];

      int bytesReceived;
      string strResponse;
      string strContent = "";
      string data = "";

      bool flag = true;

      try
      {
        // 顯示e-mail
       data = "RETR " + mailNo;

       // 傳送郵件訊息
       if (!POP3Request(data))
          return (false);

        do
        {
          bytesReceived = pop3Socket.Receive(bytes, 0, bytes.Length, SocketFlags.None);

          if (bytesReceived > 0)
          {
            strResponse = Encoding.ASCII.GetString(bytes, 0, bytesReceived);

            if (flag)
            {
              if (!strResponse.StartsWith("+OK"))
              {
                MessageBox.Show(strResponse.ToString(), "POP3 Error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return (false);
              }
              flag = false;
            }

            strContent = strContent + strResponse;

            if (strContent.Trim().EndsWith("."))
              break;
          }
          else
            break;
        }
        while (true);

        txtMessage.Text = strContent;

        Label1.Text = "Total: " + totalMail + " (Size: " + mailSize + ") Current: " + mailNo;

        return (true);
      }
      catch (Exception ex)
      {
        lstLog.Items.Add(ex.ToString());
        return (false);
      }
    }

    // 處理用戶端傳送訊息至郵件伺服器
    private bool POP3Request(string dataSent)
    {
      // 設定傳送資料緩衝區
      byte[] bytes = new Byte[1024];

      dataSent = dataSent + "\r\n";

      try
      {
        bytes = Encoding.ASCII.GetBytes(dataSent.ToCharArray());

        // 傳送資料至郵件伺服器
        int bytesSent = pop3Socket.Send(bytes, 0, bytes.Length, SocketFlags.None);

        lstLog.Items.Add("C: " + dataSent.ToString());

        return (true);
      }
      catch (Exception ex)
      {
        lstLog.Items.Add("POP3 Request Error: " + ex.ToString());

        // 同時暫停用戶端的傳送和接收作業
        pop3Socket.Shutdown(SocketShutdown.Both);
        // 關閉用戶端Socket
        pop3Socket.Close();

        return (false);
      }
    }

    // 處理郵件伺服器回傳訊息至用戶端
    private bool POP3Response()
    {
      // 設定接收資料緩衝區
      byte[] bytes = new Byte[1024];

      try
      {
        // 自郵件伺服器接收資料
        int bytesReceived = pop3Socket.Receive(bytes, 0, bytes.Length, SocketFlags.None);

        string strResponse = Encoding.ASCII.GetString(bytes, 0, bytesReceived);

        lstLog.Items.Add("S: " + strResponse.ToString());

        // 判斷郵件伺服器回應訊息
        if (!strResponse.StartsWith("+OK"))
        {
          lstLog.Items.Add("POP3 Response Error.");

          // 同時暫停用戶端的傳送和接收作業
          pop3Socket.Shutdown(SocketShutdown.Both);
          // 關閉用戶端Socket
          pop3Socket.Close();

          return (false);
        }
        else
          return (true);
      }
      catch (Exception ex)
      {
        lstLog.Items.Add("POP3 Response Error: " + ex.ToString());

        // 同時暫停用戶端的傳送和接收作業
        pop3Socket.Shutdown(SocketShutdown.Both);
        // 關閉用戶端Socket
        pop3Socket.Close();

        return (false);
      }
    }
  }
}
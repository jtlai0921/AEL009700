using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Threading;

namespace MailClient
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      lstAttachment.Items.Clear();
    }
    
    private void Form1_Resize(object sender, EventArgs e)
    {
      Control control = (Control)sender;

      txtTo.Location = new System.Drawing.Point(72, 5);
      txtTo.Size = new System.Drawing.Size(control.Size.Width - 104, 22);
      txtCc.Location = new System.Drawing.Point(72, 31);
      txtCc.Size = new System.Drawing.Size(control.Size.Width - 104, 22);
      txtBcc.Location = new System.Drawing.Point(72, 57);
      txtBcc.Size = new System.Drawing.Size(control.Size.Width - 104, 22);
      txtSubject.Location = new System.Drawing.Point(72, 83);
      txtSubject.Size = new System.Drawing.Size(control.Size.Width - 104, 22);
      lstAttachment.Location = new System.Drawing.Point(72, 109);
      lstAttachment.Size = new System.Drawing.Size(control.Size.Width - 104, 28);

      txtHost.Location = new System.Drawing.Point(67, 16);
      txtHost.Size = new System.Drawing.Size(control.Size.Width - 99, 22);
      txtPort.Location = new System.Drawing.Point(67, 44);
      txtPort.Size = new System.Drawing.Size(control.Size.Width - 99, 22);
      txtFrom.Location = new System.Drawing.Point(67, 72);
      txtFrom.Size = new System.Drawing.Size(control.Size.Width - 99, 22);
      txtUser.Location = new System.Drawing.Point(67, 100);
      txtUser.Size = new System.Drawing.Size(control.Size.Width - 99, 22);
      txtPass.Location = new System.Drawing.Point(67, 128);
      txtPass.Size = new System.Drawing.Size(control.Size.Width - 99, 22);
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
          // 處理附件
          OpenFileDialog1.Filter = "All files (*.*)|*.*";
          OpenFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
          OpenFileDialog1.Title = "Select Attachment";

          if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            lstAttachment.Items.Add(OpenFileDialog1.FileName);

          break;

        case 2:
          // Clear
          txtTo.Clear();
          txtCc.Clear();
          txtBcc.Clear();
          txtSubject.Clear();
          txtMessage.Clear();
          lstAttachment.Items.Clear();
          chkHTML.Checked = false;
          
          break;
      }
    }

    private void mnuExit_Click(object sender, EventArgs e)
    {
      DialogResult result = MessageBox.Show("Are you sure to quit?", ".Net Mail Client", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

      if (result == DialogResult.Yes)
      {
        this.Close();
      }
    }

    public void ProcessMail()
    {
      System.Net.Mail.SmtpClient mailClient = null;
      System.Net.Mail.MailMessage mailMessage = null;
      System.Net.Mail.MailAddress fromAddress = null;
      System.Net.Mail.MailAddress toAddress = null;
      System.Net.Mail.MailAddress ccAddress = null;
      System.Net.Mail.MailAddress bccAddress = null;
      System.Net.Mail.Attachment mailAttachment = null;
      System.Net.NetworkCredential credentials = null;

      try
      {
        if (txtHost.Text != "" && txtPort.Text != ""){
          // 設定SMTP郵件伺服器的DNS名稱或IP位址及通訊埠
          mailClient = new SmtpClient(txtHost.Text, Int32.Parse(txtPort.Text));
        }
        else {
          MessageBox.Show("請輸入SMTP郵件伺服器的DNS名稱或IP位址及通訊埠.", ".Net Mail Client", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
          return;
        }

        if (txtUser.Text != "" && txtPass.Text != ""){
          // 設定使用者登入SMTP郵件伺服器需使用帳號與密碼之認證
          credentials = new NetworkCredential(txtUser.Text, txtPass.Text, txtHost.Text);
        }
        else {
          MessageBox.Show("請輸入使用者帳號與密碼.", ".Net Mail Client", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
          return;
        }

        // 設定SmtpClient物件的Credentials屬性
        mailClient.Credentials = credentials;

        // 設定寄件者郵件地址
        fromAddress = new MailAddress(txtFrom.Text);

        // 設定收件者郵件地址（To）
        if (txtTo.Text != ""){
          toAddress = new MailAddress(txtTo.Text);
        }
        else {
          MessageBox.Show("請輸入收件者郵件地址.", ".Net Mail Client", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
          return;
        }

        // 設定副本收件者郵件地址（CC）
        if (txtCc.Text != "")
          ccAddress = new MailAddress(txtCc.Text);

        // 設定密件副本收件者郵件地址（BCC）
        if (txtBcc.Text != "")
          bccAddress = new MailAddress(txtBcc.Text);

        mailMessage = new MailMessage(fromAddress, toAddress);
        mailMessage.CC.Add(ccAddress);
        mailMessage.Bcc.Add(bccAddress);

        // 設定郵件主旨
        mailMessage.Subject = txtSubject.Text;

        // 設定郵件內文的字元編碼格式
        mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

        // 設定郵件內文
        mailMessage.Body = txtMessage.Text;

        // 設定郵件內文是否為HTML格式
        if (chkHTML.Checked)
          mailMessage.IsBodyHtml = true;
        else
          mailMessage.IsBodyHtml = false;

        // 處理附件
        for (int i = 0; i <= lstAttachment.Items.Count - 1; i++)
        {
          mailAttachment = new Attachment(lstAttachment.Items[i].ToString());
          mailMessage.Attachments.Add(mailAttachment);
        }

        // 處理順序
        mailMessage.Priority = System.Net.Mail.MailPriority.Normal;

        // 郵件傳送
        mailClient.Send(mailMessage);

        mailMessage = null;

        MessageBox.Show("郵件傳送成功.", ".Net Mail Client", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
      }
      catch (Exception ex)
      {
        MessageBox.Show("郵件傳送失敗: " + ex.ToString(), ".Net Mail Client", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
      }
    }
  }
}

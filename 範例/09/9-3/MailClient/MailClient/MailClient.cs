using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;

namespace MailClient
{
  class MailClient
  {
    static void Main(string[] args)
    {
      if ((args.Length < 10))
      {
        Console.WriteLine("Usage: MailClient [SMTP Server DNS/IP] [Port] [username] [password] [From] [To] [Cc] [Bcc] [Subject] [Message]");
        return;
      }

      string host = args[0];    // SMTP郵件伺服器之主機名稱或IP位址
      string port = args[1];    // SMTP郵件伺服器之通訊埠，預設通訊埠為25
      string user = args[2];    // 使用者帳號
      string pass = args[3];    // 使用者密碼
      string from = args[4];    // 寄件者郵件地址
      string to = args[5];      // 收件者郵件地址
      string cc = args[6];      // 副本收件者郵件地址
      string bcc = args[7];     // 密件副本收件者郵件地址
      string subject = args[8]; // 郵件主旨
      string message = args[9]; // 郵件內容
      
      System.Net.Mail.SmtpClient mailClient = null;
      System.Net.Mail.MailMessage mailMessage = null;
      System.Net.Mail.MailAddress fromAddress = null;
      System.Net.Mail.MailAddress toAddress = null;
      System.Net.Mail.MailAddress ccAddress = null;
      System.Net.Mail.MailAddress bccAddress = null;

      System.Net.NetworkCredential credentials = null;

      try
      {
        // 設定SMTP郵件伺服器的DNS名稱或IP位址及通訊埠
        mailClient = new SmtpClient(host, Int32.Parse(port));

        // 設定使用者登入SMTP郵件伺服器需使用帳號與密碼之認證
        credentials = new NetworkCredential(user, pass, host);

        // 設定SmtpClient物件的Credentials屬性
        mailClient.Credentials = credentials;

        // 當傳送郵件完畢時
        // 並定義所呼叫的Callback方法
        mailClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

        // 設定寄件者郵件地址
        fromAddress = new MailAddress(from);

        toAddress = new MailAddress(to);

        // 設定副本收件者郵件地址（Cc）
        ccAddress = new MailAddress(cc);

        // 設定密件副本收件者郵件地址（Bcc）
        bccAddress = new MailAddress(bcc);

        mailMessage = new MailMessage(fromAddress, toAddress);
        mailMessage.CC.Add(ccAddress);
        mailMessage.Bcc.Add(bccAddress);

        // 設定郵件主旨
        mailMessage.Subject = subject;

        // 設定郵件內文的字元編碼格式
        mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

        // 設定郵件內文
        mailMessage.Body = message;

        // 處理順序
        mailMessage.Priority = System.Net.Mail.MailPriority.Normal;

        string userToken = "Asynchronous";

        // 執行非同步傳送郵件
        mailClient.SendAsync(mailMessage, userToken);

        Console.WriteLine("傳送郵件中... 請按任意鍵結束.");

        mailMessage = null;
      }
      catch (Exception ex)
      {
        Console.WriteLine("郵件傳送失敗: " + ex.ToString());
      }
    }
        
    // 自訂Callback方法
    public static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
      // 取得非同步作業之識別碼
      string userToken = (string)e.UserState;

      // 判斷非同步作業是否已被取消  
      if (e.Cancelled)
        Console.WriteLine("取消非同步傳送郵件.");

      // 判斷非同步作業是否發生錯誤
      if (e.Error != null)
        Console.WriteLine("非同步傳送郵件發生錯誤.");
      else
        // 郵件傳送完畢
        Console.WriteLine("非同步傳送郵件成功.");
    }
  }
}


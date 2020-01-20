using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace POP3Client
{
  class POP3Client
  {
    public static TcpClient pop3Client;
    public static NetworkStream networkStream;
    public static int totalMail, mailSize;

    static void Main(string[] args)
    {
      if ((args.Length < 4))
      {
        Console.WriteLine("Usage: POP3Client [POP3 Server DNS/IP] [Port] [username] [password]");
        return;
      }

      string host = args[0];    // POP3郵件伺服器之主機名稱或IP位址
      string port = args[1];    // POP3郵件伺服器之通訊埠，預設通訊埠為25
      string user = args[2];    // 登錄帳號
      string pass = args[3];    // 密碼

      string data = "";
      byte[] bytes ;
      int bytesReceived;
      string strResponse;

      // 連線郵件伺服器
      try
      {
        IPAddress pop3ServerIP = Dns.Resolve(host).AddressList[0];

        IPEndPoint pop3ServerHost = new IPEndPoint(pop3ServerIP, Int32.Parse(port));

        pop3Client = new System.Net.Sockets.TcpClient();

        // 建立用戶端與郵件伺服器連線
        pop3Client.Connect(pop3ServerHost);

        // 判斷是否無法連線至郵件伺服器
        if (!pop3Client.Connected)
          Console.WriteLine("無法連線至郵件伺服器: " + host + ":" + port);

        // 取得用戶端的輸出入串流
        networkStream = pop3Client.GetStream(); 

        // 判斷郵件伺服器是否回應 +OK hello from popgate 訊息?
        if (!POP3Response())
          return;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }

      // 使用者認證
      try
      {
        // USER
        data = "USER " + user;

        // 傳送郵件訊息
        if (!POP3Request(data))
          return;

        // 判斷郵件伺服器是否回應 +OK password required 訊息?
        if (!POP3Response())
          return;

        // PASS
        data = "PASS " + pass;

        // 傳送郵件訊息
        if (!POP3Request(data))
          return;

        // 判斷郵件伺服器是否回應 +OK maildrop ready, .. messages 訊息?
        if (!POP3Response())
          return;

        // STAT
        data = "STAT";

        // 傳送郵件訊息
        if (!POP3Request(data))
          return;

        // 設定接收資料緩衝區
        bytes = new Byte[pop3Client.ReceiveBufferSize];

        // 郵件伺服器回傳 +OK <# of Mail> <Mail Size>
        // 自郵件伺服器接收資料
        bytesReceived = networkStream.Read(bytes, 0, (int)pop3Client.ReceiveBufferSize);

        strResponse = Encoding.ASCII.GetString(bytes, 0, bytesReceived);

        Console.WriteLine("S: " + strResponse.ToString());

        if (strResponse.StartsWith("+OK"))
        {
          // +OK <#> <Size>
          string[] strTemp = strResponse.Trim().Split(" ".ToCharArray());

          totalMail = Int32.Parse(strTemp[1].Trim().ToString());
          mailSize = Int32.Parse(strTemp[2].Trim().ToString());

          if (totalMail > 0)
          {
            // 顯示第一封e-mail
            data = "RETR 1";

            // 傳送郵件訊息
            if (!POP3Request(data))
              return;

            bool flag = true;
            string strContent = "";

            do
            {
              // 自郵件伺服器接收資料
              bytesReceived = networkStream.Read(bytes, 0, (int)pop3Client.ReceiveBufferSize);

              if (bytesReceived > 0)
              {
                strResponse = Encoding.ASCII.GetString(bytes, 0, bytesReceived);

                if (flag)
                {
                  if (!strResponse.StartsWith("+OK"))
                  {
                    Console.WriteLine("POP3 Error."); 
                    return;
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

            Console.WriteLine(strContent);
          }
          else
            Console.WriteLine("There is no mail.");

        }
        else
          Console.WriteLine("POP3 Error.");

        // 關閉用戶端的輸出入串流
        networkStream.Close();

        // 關閉用戶端與POP3郵件伺服器連結
        pop3Client.Close();

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }

    // 處理用戶端傳送訊息至郵件伺服器
    private static bool POP3Request(string dataSent)
    {
      // 設定傳送資料緩衝區
      byte[] bytes = new Byte[1024];

      dataSent = dataSent + "\r\n";

      try
      {
        bytes = Encoding.ASCII.GetBytes(dataSent.ToCharArray());

        // 傳送資料至郵件伺服器
        networkStream.Write(bytes, 0, bytes.Length);

        Console.WriteLine("C: " + dataSent.ToString());

        return (true);
      }
      catch (Exception ex)
      {
        Console.WriteLine("POP3 Request Error: " + ex.ToString());

        // 關閉用戶端的輸出入串流
        networkStream.Close();

        // 關閉用戶端與POP3郵件伺服器連結
        pop3Client.Close();

        return (false);
      }
    }

    // 處理郵件伺服器回傳訊息至用戶端
    private static bool POP3Response()
    {
      // 設定接收資料緩衝區
      byte[] bytes = new byte[pop3Client.ReceiveBufferSize];

      try
      {
        // 自郵件伺服器接收資料
        int bytesReceived = networkStream.Read(bytes, 0, (int)pop3Client.ReceiveBufferSize);

        string strResponse = Encoding.ASCII.GetString(bytes, 0, bytesReceived);

        Console.WriteLine("S: " + strResponse);

        // 判斷郵件伺服器回應訊息
        if (!strResponse.StartsWith("+OK"))
        {
          Console.WriteLine("POP3 Response Error.");

          // 關閉用戶端的輸出入串流
          networkStream.Close();

          // 關閉用戶端與POP3郵件伺服器連結
          pop3Client.Close();

          return (false);
        }
        else
        {
          return (true);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("POP3 Response Error: " + ex.ToString());

        // 關閉用戶端的輸出入串流
        networkStream.Close();

        // 關閉用戶端與POP3郵件伺服器連結
        pop3Client.Close();

        return (false);
      }
    }
  }
}

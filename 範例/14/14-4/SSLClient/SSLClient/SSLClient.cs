using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace SSLClient
{
  class SSLClient
  {
    static void Main(string[] args)
    {
      if (args.Length < 3)
      {
        Console.WriteLine("Usage: SSLClient [SSLServer DNS/IP] [Port] [Message]");
        return;
      }

      string host = args[0];
      string port = args[1];
      string message = args[2];

      SslStream sslStream = null;

      try
      {
        IPAddress serverIP = Dns.Resolve(host).AddressList[0];

        // 使用伺服端之IPEndPoint
        IPEndPoint serverhost = new IPEndPoint(serverIP, Int32.Parse(port));

        TcpClient tcpClient = new TcpClient();

        // 建立用戶端與伺服端的連線
        tcpClient.Connect(serverhost);

         // 建立支援SSL安全通訊之資料串流
        sslStream = new SslStream(tcpClient.GetStream());

        // 由用戶端呼叫驗證連接中的伺服端並選擇性驗證用戶端
        sslStream.AuthenticateAsClient(host);

        // 判斷驗證（Authentication）是否成功
        if (sslStream.IsAuthenticated)
        {
          Console.WriteLine("IsAuthenticated: {0}", sslStream.IsAuthenticated);
          Console.WriteLine("IsEncrypted: {0}", sslStream.IsEncrypted);
          Console.WriteLine("IsMutuallyAuthenticated: {0}", sslStream.IsMutuallyAuthenticated);
          Console.WriteLine("IsServer: {0}", sslStream.IsServer);
          Console.WriteLine("IsSigned: {0}", sslStream.IsSigned);
        }

        // 判斷串流是否支援寫入功能
        if (sslStream.CanWrite)
        {
          // 設定傳送資料緩衝區
          byte[] msg = Encoding.ASCII.GetBytes(message);

          // 將資料寫入資料串流中
          sslStream.Write(msg, 0, msg.Length);

          Console.WriteLine("傳送的資料內容: " + "\r\n" + "{0}", message + "\r\n");
        }
        else
        {
          Console.WriteLine("串流不支援寫入功能.");
        }

        // 判斷串流是否支援讀取功能
        if (sslStream.CanRead)
        {
          // 設定接收資料緩衝區
          byte[] bytes = new Byte[1024];

          // 自資料串流中讀取資料
          int bytesReceived = sslStream.Read(bytes, 0, bytes.Length);

          string data = Encoding.ASCII.GetString(bytes, 0, bytesReceived);

          Console.WriteLine("接收的資料內容: " + "\r\n" + "{0}", data + "\r\n");
        }
        else
        {
          Console.WriteLine("串流不支援讀取功能.");
        }
      }
      catch (AuthenticationException ex)
      {
        Console.WriteLine(ex.Message);
      }
      catch (SocketException ex)
      {
        Console.WriteLine(ex.Message);
      }
      catch (IOException ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
        if (sslStream != null)
          // 關閉串流
          sslStream.Close();
      }

      Console.WriteLine("Press Enter to exit.");
      Console.Read();
    }
  }
}

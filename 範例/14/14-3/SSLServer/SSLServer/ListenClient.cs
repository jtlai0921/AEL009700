using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SSLServer
{
  // 自訂類別
  class ListenClient
  {
    private System.Net.Sockets.TcpListener tcpListener;
    private System.Net.Sockets.TcpClient tcpClient;

    // 建構函式
    public ListenClient(TcpListener tcpListener)
    {
      this.tcpListener = tcpListener;
    }

    public void ServerThreadProc()
    {
      SslStream sslStream = null;

      while (true)
      {
        try
        {
          // 處理用戶端連線
          tcpClient = tcpListener.AcceptTcpClient();

          // 取得本機相關的網路資訊
          IPEndPoint serverInfo = (IPEndPoint)tcpListener.LocalEndpoint;

          // 以Client屬性取得用戶端之Socket物件
          Socket clientSocket = tcpClient.Client;

          // 取得連線用戶端相關的網路連線資訊
          IPEndPoint clientInfo = (IPEndPoint)clientSocket.RemoteEndPoint;

          Console.WriteLine("Client: " + clientInfo.Address.ToString() + ":" + clientInfo.Port.ToString());
          Console.WriteLine("Server: " + serverInfo.Address.ToString() + ":" + serverInfo.Port.ToString());

          // 建立支援SSL安全通訊之資料串流
          sslStream = new SslStream(tcpClient.GetStream());

          // 建立伺服端憑證，憑證檔案為test.cer
          System.Security.Cryptography.X509Certificates.X509Certificate certificate = X509Certificate.CreateFromCertFile("test.cer");

          // 由伺服端呼叫驗證連接中的伺服端，並選擇性驗證用戶端
          sslStream.AuthenticateAsServer(certificate);
          // 或以SSL 3.0或TLS 1.0通訊協定進行安全通訊
          // sslStream.AuthenticateAsServer(certificate, true, SslProtocols.Default, true);

          // 判斷驗證（Authentication）是否成功
          if (sslStream.IsAuthenticated)
          {
            Console.WriteLine("IsAuthenticated: {0}", sslStream.IsAuthenticated);
            Console.WriteLine("IsEncrypted: {0}", sslStream.IsEncrypted);
            Console.WriteLine("IsMutuallyAuthenticated: {0}", sslStream.IsMutuallyAuthenticated);
            Console.WriteLine("IsServer: {0}", sslStream.IsServer);
            Console.WriteLine("IsSigned: {0}", sslStream.IsSigned);
          }

          // 判斷串流是否支援讀取功能
          if (sslStream.CanRead)
          {
            byte[] bytes = new byte[1024];

            // 自資料串流中讀取資料
            int bytesReceived = sslStream.Read(bytes, 0, bytes.Length);

            string data = Encoding.ASCII.GetString(bytes, 0, bytesReceived);

            Console.WriteLine("接收的資料內容: " + "\r\n" + "{0}", data + "\r\n");
          }
          else
          {
            Console.WriteLine("串流不支援讀取功能.");
          }

          // 判斷串流是否支援寫入功能
          if (sslStream.CanWrite)
          {
            // 測試用
            string htmlBody = "<html><head><title>Send Test</title></head><body><font size=2 face=Verdana>Sent OK.</font></body></html>";
            string htmlHeader = "HTTP/1.0 200 OK" + "\r\n" + "Server: HTTP Server 1.0" + "\r\n" + "Content-Type: text/html" + "\r\n" + "Content-Length: " + htmlBody.Length + "\r\n" + "\r\n";

            string htmlContent = htmlHeader + htmlBody;

            // 設定傳送資料緩衝區
            byte[] msg = Encoding.ASCII.GetBytes(htmlContent);

            // 將資料寫入資料串流中
            sslStream.Write(msg, 0, msg.Length);

            Console.WriteLine("傳送的資料內容: " + "\r\n" + "{0}", Encoding.UTF8.GetString(msg, 0, msg.Length) + "\r\n");
          }
          else
          {
            Console.WriteLine("串流不支援寫入功能.");
          }
        }
        catch (AuthenticationException aex)
        {
          Console.WriteLine(aex.StackTrace.ToString());
        }
        catch (SocketException ex)
        {
          Console.WriteLine(ex.StackTrace.ToString());
        }
        catch (IOException ioex)
        {
          Console.WriteLine(ioex.StackTrace.ToString());
        }
        finally
        {
          if (sslStream != null)
            // 關閉串流
            sslStream.Close();
        }
      }
    }
  }
}
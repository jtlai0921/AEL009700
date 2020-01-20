using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace ServerSocket
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

          // 取得伺服端的輸出入串流
          NetworkStream networkStream = tcpClient.GetStream();

          // 判斷串流是否支援讀取功能
          if (networkStream.CanRead)
          {
            byte[] bytes = new byte[1024];
            int bytesReceived = 0;
            string data = null;

            do
            {
              // 自資料串流中讀取資料
              bytesReceived = networkStream.Read(bytes, 0, bytes.Length);

              data += Encoding.ASCII.GetString(bytes, 0, bytesReceived);

            } while (networkStream.DataAvailable);  // 判斷串流中的資料是否可供讀取

            Console.WriteLine("接收的資料內容: " + "\r\n" + "{0}", data + "\r\n");
          }
          else
          {
            Console.WriteLine("串流不支援讀取功能.");
          }

          // 判斷串流是否支援寫入功能
          if (networkStream.CanWrite)
          {
            // 測試用
            string htmlBody = "<html><head><title>Send Test</title></head><body><font size=2 face=Verdana>Sent OK.</font></body></html>";
            string htmlHeader = "HTTP/1.0 200 OK" + "\r\n" + "Server: HTTP Server 1.0" + "\r\n" + "Content-Type: text/html" + "\r\n" + "Content-Length: " + htmlBody.Length + "\r\n" + "\r\n";

            string htmlContent = htmlHeader + htmlBody;

            // 設定傳送資料緩衝區
            byte[] msg = Encoding.ASCII.GetBytes(htmlContent);

            // 將資料寫入資料串流中
            networkStream.Write(msg, 0, msg.Length);

            Console.WriteLine("傳送的資料內容: " + "\r\n" + "{0}", Encoding.UTF8.GetString(msg, 0, msg.Length) + "\r\n");
          }
          else
          {
            Console.WriteLine("串流不支援寫入功能.");
          }

          // 關閉串流
          networkStream.Close();
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.StackTrace.ToString());
        }
      }
    }
  }
}

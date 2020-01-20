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
    private System.Net.Sockets.Socket clientSocket;

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
          clientSocket = tcpListener.AcceptSocket();

          // 取得本機相關的網路資訊
          IPEndPoint serverInfo = (IPEndPoint)tcpListener.LocalEndpoint;

          // 取得連線用戶端相關的網路連線資訊
          IPEndPoint clientInfo = (IPEndPoint)clientSocket.RemoteEndPoint;

          Console.WriteLine("Client: " + clientInfo.Address.ToString() + ":" + clientInfo.Port.ToString());
          Console.WriteLine("Server: " + serverInfo.Address.ToString() + ":" + serverInfo.Port.ToString());

          // 設定接收資料緩衝區
          byte[] bytes = new Byte[1024];

          // 自已連線的用戶端接收資料
          int bytesReceived = clientSocket.Receive(bytes, 0, bytes.Length, SocketFlags.None);

          if (bytesReceived > 0)
          {
            Console.WriteLine("接收位元組數目: {0}", bytesReceived);
            Console.WriteLine("接收的資料內容: \r\n" + "{0}", Encoding.UTF8.GetString(bytes, 0, bytesReceived) + "\r\n");
          }

          // 測試用
          string htmlBody = "<html><head><title>Send Test</title></head><body><font size=2 face=Verdana>Sent OK.</font></body></html>";
          string htmlHeader = "HTTP/1.0 200 OK" + "\r\n" + "Server: HTTP Server 1.0" + "\r\n" + "Content-Type: text/html" + "\r\n" + "Content-Length: " + htmlBody.Length + "\r\n\r\n";

          string htmlContent = htmlHeader + htmlBody;

          // 設定傳送資料緩衝區
          byte[] msg = Encoding.ASCII.GetBytes(htmlContent);

          // 傳送資料至已連線的用戶端
          int bytesSend = clientSocket.Send(msg, 0, msg.Length, SocketFlags.None);

          Console.WriteLine("傳送位元組數目: {0}", bytesSend);
          Console.WriteLine("傳送的資料內容: " + "\r\n" + "{0}", Encoding.UTF8.GetString(msg, 0, bytesSend) + "\r\n");

          // 同時暫停用戶端的傳送和接收作業
          clientSocket.Shutdown(SocketShutdown.Both);
          // 關閉用戶端Socket
          clientSocket.Close();
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.StackTrace.ToString());
        }
      }
    }
  }
}

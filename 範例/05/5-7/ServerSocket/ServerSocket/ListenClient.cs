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
    private System.Net.Sockets.Socket serverSocket;
    private System.Net.Sockets.Socket clientSocket;

    // 建構函式
    public ListenClient(Socket serverSocket)
    {
      this.serverSocket = serverSocket;
    }

    public void ServerThreadProc()
    {
      while (true)
      {
        try
        {
          // 處理用戶端連線
          clientSocket = serverSocket.Accept();

          // 取得本機相關的網路資訊
          IPEndPoint serverInfo = (IPEndPoint)serverSocket.LocalEndPoint;

          // 取得連線用戶端相關的網路連線資訊
          IPEndPoint clientInfo = (IPEndPoint)clientSocket.RemoteEndPoint;

          Console.WriteLine("Server: " + serverInfo.Address.ToString() + ":" + serverInfo.Port.ToString());
          Console.WriteLine("Client: " + clientInfo.Address.ToString() + ":" + clientInfo.Port.ToString());

          // 設定接收資料緩衝區
          byte[] bytes = new Byte[1024];

          // 自已連線的用戶端接收資料
          int bytesReceived = clientSocket.Receive(bytes, 0, bytes.Length, SocketFlags.None);

          if (bytesReceived > 0)
          {
            Console.WriteLine("接收位元組數目: {0}", bytesReceived);
            Console.WriteLine("接收的資料內容: \r\n" + "{0}", Encoding.UTF8.GetString(bytes, 0, bytesReceived) + "\r\n");
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.StackTrace.ToString());
        }
      }
    }
  }
}

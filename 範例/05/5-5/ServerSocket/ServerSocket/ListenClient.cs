using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.StackTrace.ToString());
        }
      }
    }
  }
}

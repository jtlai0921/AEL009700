using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerSocket
{
  class ServerSocket
  {
    public static ManualResetEvent thread = new System.Threading.ManualResetEvent(false);

    /// 應用程式的主進入點
    static void Main(string[] args)
    {
      try
      {
        // 取得本機的識別名稱
        string hostname = Dns.GetHostName();

        // 取得主機的DNS資訊
        IPAddress serverIP = Dns.Resolve(hostname).AddressList[0];

        // Port = 80
        string Port = "80";

        // 建立伺服端TcpListener
        TcpListener tcpListener = new TcpListener(serverIP, Int32.Parse(Port));

        // 等候用戶端連線
        tcpListener.Start();

        Console.WriteLine("Server started at: " + serverIP.ToString() + ":" + Port);

        while (true)
        {
          thread.Reset();

          // 開始非同步作業嘗試接受用戶端連線
          // 並定義所呼叫的Callback方法為AcceptCallback
          tcpListener.BeginAcceptTcpClient(new AsyncCallback(AcceptCallback), tcpListener);

          thread.WaitOne();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
    }

    // 自訂Callback方法
    public static void AcceptCallback(IAsyncResult asyncResult)
    {
      try
      {
        TcpListener tcpListener = (System.Net.Sockets.TcpListener)asyncResult.AsyncState;

        // 以非同步作業接受用戶端連線
        TcpClient tcpClient = tcpListener.EndAcceptTcpClient(asyncResult);

        // 取得本機相關的網路資訊
        IPEndPoint serverInfo = (IPEndPoint)tcpListener.LocalEndpoint;

        // 以Client屬性取得用戶端之Socket物件
        Socket clientSocket = tcpClient.Client;

        // 取得連線用戶端相關的網路連線資訊
        IPEndPoint clientInfo = (IPEndPoint)clientSocket.RemoteEndPoint;

        Console.WriteLine("Server: " + serverInfo.Address.ToString() + ":" + serverInfo.Port.ToString());
        Console.WriteLine("Client: " + clientInfo.Address.ToString() + ":" + clientInfo.Port.ToString());

        thread.Set();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
    }
  }
}
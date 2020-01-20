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
        // 建立伺服端Socket
        Socket serverSocket = new Socket(AddressFamily.InterNetwork,
          SocketType.Stream, ProtocolType.Tcp);

        // 取得本機的識別名稱
        string hostname = Dns.GetHostName();

        // 取得主機的DNS資訊
        IPAddress serverIP = Dns.Resolve(hostname).AddressList[0];

        // Port = 80
        string Port = "80";

        // 處理主機IP位址及主機服務所需的通訊埠資訊
        IPEndPoint serverhost = new IPEndPoint(serverIP, Int32.Parse(Port));

        // 繫結設定伺服端Socket
        serverSocket.Bind(serverhost);

        // 開始接聽等候用戶端的網路連線請求
        // 設定伺服端最大用戶端連線數為int.MaxValue
        serverSocket.Listen(int.MaxValue);

        Console.WriteLine("Server started at: " + serverIP.ToString() + ":" + Port);

        while (true)
        {
          thread.Reset();

          // 開始非同步作業以嘗試接受用戶端連線
          // 並定義所呼叫的Callback方法為AcceptCallback
          serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), serverSocket);

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
        Socket serverSocket = (System.Net.Sockets.Socket)asyncResult.AsyncState;

        // 以非同步作業接受用戶端連線
        Socket clientSocket = serverSocket.EndAccept(asyncResult);

        // 取得本機相關的網路資訊
        IPEndPoint serverInfo = (IPEndPoint)serverSocket.LocalEndPoint;

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
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;

namespace ServerSocket
{
  class ServerSocket
  {
    /// 應用程式的主進入點
    static void Main(string[] args)
    {
      try
      {
        // 建立伺服端Socket
        Socket serverSocket = new Socket(AddressFamily.InterNetwork,
          SocketType.Stream, ProtocolType.Tcp);

        // 取得本機的識別名稱
        string Host = ConfigurationManager.AppSettings["host"];
        // 通訊埠
        string Port = ConfigurationManager.AppSettings["port"];

        // 取得主機的DNS資訊
        IPAddress serverIP = Dns.Resolve(Host).AddressList[0];

        // 處理主機IP位址及主機服務所需的通訊埠資訊
        IPEndPoint serverhost = new IPEndPoint(serverIP, Int32.Parse(Port));

        // 繫結設定伺服端Socket
        serverSocket.Bind(serverhost);

        // 開始接聽等候用戶端的網路連線請求
        // 設定伺服端最大用戶端連線數為int.MaxValue
        serverSocket.Listen(int.MaxValue);

        Console.WriteLine("Server started at: " + serverIP.ToString() + ":" + Port);

        ListenClient lc = new ListenClient(serverSocket);

        // 執行緒
        ThreadStart serverThreadStart = new ThreadStart(lc.ServerThreadProc);
        Thread serverthread = new Thread(serverThreadStart);

        serverthread.Start();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
      finally
      {
      }
    }
  }
}
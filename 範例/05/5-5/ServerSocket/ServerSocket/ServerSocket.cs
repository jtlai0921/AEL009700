using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerSocket
{
  class ServerSocket
  {
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

        ListenClient lc = new ListenClient(tcpListener);

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

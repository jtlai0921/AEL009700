using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace HTTPServer
{
  class HTTPServer
  {
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

        // HTTP Server Port = 80
        string Port = "80";

        // 處理主機IP位址及主機服務所需的通訊埠資訊
        IPEndPoint serverhost = new IPEndPoint(serverIP, Int32.Parse(Port));

        // 繫結設定伺服端Socket
        serverSocket.Bind(serverhost);

        // 開始接聽等候用戶端的網路連線請求
        // 設定伺服端最大用戶端連線數為int.MaxValue
        serverSocket.Listen(int.MaxValue);

        Console.WriteLine("HTTP server started at: " + serverhost.Address.ToString() + ":" + Port);

        HTTPSession httpSession = new HTTPSession(serverSocket);

        // 執行緒
        ThreadStart serverThreadStart = new ThreadStart(httpSession.HTTPSessionThread);
        Thread serverthread = new Thread(serverThreadStart);

        serverthread.Start();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
    }
  }
}

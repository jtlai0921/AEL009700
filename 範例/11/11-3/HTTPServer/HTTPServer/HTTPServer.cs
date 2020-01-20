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
        // 取得本機的識別名稱
        string hostname = Dns.GetHostName();

        // 取得主機的DNS資訊
        IPAddress serverIP = Dns.Resolve(hostname).AddressList[0];

        // HTTP Server Port = 80
        string Port = "80";

        System.Net.Sockets.TcpListener tcpListener = new TcpListener(serverIP, Int32.Parse(Port));

        // 開始接聽等候用戶端的網路連線請求
        tcpListener.Start();

        Console.WriteLine("HTTP server started at: " + serverIP.ToString() + ":" + Port);

        HTTPSession httpSession = new HTTPSession(tcpListener);

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

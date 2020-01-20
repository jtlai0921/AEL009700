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
        // 判斷作業系統是否支援
        if (!System.Net.HttpListener.IsSupported)
        {
          Console.WriteLine("作業系統不支援HttpListener類別");
          return;
        }

        // 取得本機的識別名稱
        string hostname = Dns.GetHostName();

        // 取得主機的DNS資訊
        IPAddress serverIP = Dns.Resolve(hostname).AddressList[0];

        // HTTP Server Port = 80
        string Port = "80";

        string prefix = "http://" + serverIP.ToString() + ":" + Port + "/";

        Console.WriteLine("HTTP server started at: " + prefix);

        System.Net.HttpListener httpListener = new System.Net.HttpListener();

        // 設定Prefixes屬性，以指定HTTP伺服端的IP位址及通訊埠
        httpListener.Prefixes.Add(prefix);

        // 等候自用戶端的連線請求
        httpListener.Start();

        //Console.WriteLine("HTTP server started at: " + Dns.GetHostName() + ":" + Port);

        HTTPSession httpSession = new HTTPSession(httpListener);

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

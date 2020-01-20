using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FTPServer
{
  class FTPServer
  {
    static void Main(string[] args)
    {
      try
      {
        // 取得本機的識別名稱
        string hostname = Dns.GetHostName();

        // 取得主機的DNS資訊
        IPAddress serverIP = Dns.Resolve(hostname).AddressList[0];

        // Port = 21
        string Port = "21";

        System.Net.Sockets.TcpListener tcpListener = new TcpListener(serverIP, Int32.Parse(Port));

        // 開始接聽等候用戶端的網路連線請求
        tcpListener.Start();

        Console.WriteLine("FTP server started at: " + serverIP.ToString() + ":" + Port);

        FTPSession ftpSession = new FTPSession(tcpListener);

        // 執行緒
        ThreadStart serverThreadStart = new ThreadStart(ftpSession.FTPSessionThread);
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

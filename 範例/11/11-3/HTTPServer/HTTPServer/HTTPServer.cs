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
        // ���o�������ѧO�W��
        string hostname = Dns.GetHostName();

        // ���o�D����DNS��T
        IPAddress serverIP = Dns.Resolve(hostname).AddressList[0];

        // HTTP Server Port = 80
        string Port = "80";

        System.Net.Sockets.TcpListener tcpListener = new TcpListener(serverIP, Int32.Parse(Port));

        // �}�l��ť���ԥΤ�ݪ������s�u�ШD
        tcpListener.Start();

        Console.WriteLine("HTTP server started at: " + serverIP.ToString() + ":" + Port);

        HTTPSession httpSession = new HTTPSession(tcpListener);

        // �����
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

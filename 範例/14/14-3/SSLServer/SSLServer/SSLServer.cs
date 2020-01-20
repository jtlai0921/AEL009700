using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SSLServer
{
  class SSLServer
  {
    static void Main(string[] args)
    {
      try
      {
        // ���o�������ѧO�W��
        string hostname = Dns.GetHostName();

        // ���o�D����DNS��T
        IPAddress serverIP = Dns.Resolve(hostname).AddressList[0];

        // �]�wSSL�q�T��w���q�T�q�T��443
        string Port = "443";

        // �إߦ��A��TcpListener
        TcpListener tcpListener = new TcpListener(serverIP, Int32.Parse(Port));

        // ���ԥΤ�ݳs�u
        tcpListener.Start();

        Console.WriteLine("SSL server started at: " + serverIP.ToString() + ":" + Port);

        ListenClient lc = new ListenClient(tcpListener);

        // �����
        ThreadStart serverThreadStart = new ThreadStart(lc.ServerThreadProc);
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

using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NegotiateServer
{
  class NegotiateServer
  {
    static void Main(string[] args)
    {
      try
      {
        // ���o�������ѧO�W��
        string hostname = Dns.GetHostName();

        // ���o�D����DNS��T
        IPAddress serverIP = Dns.Resolve(hostname).AddressList[0];

        // �]�w��A�]Negotiate�^�w���ʳq�T��w���q�T�q�T��8080
        string Port = "8080";

        // �إߦ��A��TcpListener
        TcpListener tcpListener = new TcpListener(serverIP, Int32.Parse(Port));

        // ���ԥΤ�ݳs�u
        tcpListener.Start();

        Console.WriteLine("Negotiate server started at: " + serverIP.ToString() + ":" + Port);

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
      finally
      {
      }
    }
  }
}

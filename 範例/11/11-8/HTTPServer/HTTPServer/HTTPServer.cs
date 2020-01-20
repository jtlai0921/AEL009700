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
        // �P�_�@�~�t�άO�_�䴩
        if (!System.Net.HttpListener.IsSupported)
        {
          Console.WriteLine("�@�~�t�Τ��䴩HttpListener���O");
          return;
        }

        // ���o�������ѧO�W��
        string hostname = Dns.GetHostName();

        // ���o�D����DNS��T
        IPAddress serverIP = Dns.Resolve(hostname).AddressList[0];

        // HTTP Server Port = 80
        string Port = "80";

        string prefix = "http://" + serverIP.ToString() + ":" + Port + "/";

        Console.WriteLine("HTTP server started at: " + prefix);

        System.Net.HttpListener httpListener = new System.Net.HttpListener();

        // �]�wPrefixes�ݩʡA�H���wHTTP���A�ݪ�IP��}�γq�T��
        httpListener.Prefixes.Add(prefix);

        // ���ԦۥΤ�ݪ��s�u�ШD
        httpListener.Start();

        //Console.WriteLine("HTTP server started at: " + Dns.GetHostName() + ":" + Port);

        HTTPSession httpSession = new HTTPSession(httpListener);

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

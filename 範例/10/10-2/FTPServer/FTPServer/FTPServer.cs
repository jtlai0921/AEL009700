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
        // ���o�������ѧO�W��
        string hostname = Dns.GetHostName();

        // ���o�D����DNS��T
        IPAddress serverIP = Dns.Resolve(hostname).AddressList[0];

        // Port = 21
        string Port = "21";

        System.Net.Sockets.TcpListener tcpListener = new TcpListener(serverIP, Int32.Parse(Port));

        // �}�l��ť���ԥΤ�ݪ������s�u�ШD
        tcpListener.Start();

        Console.WriteLine("FTP server started at: " + serverIP.ToString() + ":" + Port);

        FTPSession ftpSession = new FTPSession(tcpListener);

        // �����
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

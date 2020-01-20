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
        // �إߦ��A��Socket
        Socket serverSocket = new Socket(AddressFamily.InterNetwork,
          SocketType.Stream, ProtocolType.Tcp);

        // ���o�������ѧO�W��
        string hostname = Dns.GetHostName();

        // ���o�D����DNS��T
        IPAddress serverIP = Dns.Resolve(hostname).AddressList[0];

        // Port = 21
        string Port = "21";

        // �B�z�D��IP��}�ΥD���A�ȩһݪ��q�T���T
        IPEndPoint serverhost = new IPEndPoint(serverIP, Int32.Parse(Port));

        // ô���]�w���A��Socket
        serverSocket.Bind(serverhost);

        // �}�l��ť���ԥΤ�ݪ������s�u�ШD
        // �]�w���A�ݳ̤j�Τ�ݳs�u�Ƭ�int.MaxValue
        serverSocket.Listen(int.MaxValue);

        Console.WriteLine("FTP server started at: " + serverIP.ToString() + ":" + Port);

        FTPSession ftpSession = new FTPSession(serverSocket);

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

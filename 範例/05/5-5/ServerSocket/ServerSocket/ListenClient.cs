using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerSocket
{
  // �ۭq���O
  class ListenClient
  {
    private System.Net.Sockets.TcpListener tcpListener;
    private System.Net.Sockets.TcpClient tcpClient;

    // �غc�禡
    public ListenClient(TcpListener tcpListener)
    {
      this.tcpListener = tcpListener;
    }

    public void ServerThreadProc()
    {
      while (true)
      {
        try
        {
          // �B�z�Τ�ݳs�u
          tcpClient = tcpListener.AcceptTcpClient();

          // ���o����������������T
          IPEndPoint serverInfo = (IPEndPoint)tcpListener.LocalEndpoint;

          // �HClient�ݩʨ��o�Τ�ݤ�Socket����
          Socket clientSocket = tcpClient.Client;

          // ���o�s�u�Τ�ݬ����������s�u��T
          IPEndPoint clientInfo = (IPEndPoint)clientSocket.RemoteEndPoint;

          Console.WriteLine("Client: " + clientInfo.Address.ToString() + ":" + clientInfo.Port.ToString());
          Console.WriteLine("Server: " + serverInfo.Address.ToString() + ":" + serverInfo.Port.ToString());
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.StackTrace.ToString());
        }
      }
    }
  }
}

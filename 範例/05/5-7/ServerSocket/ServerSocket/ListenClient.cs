using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace ServerSocket
{
  // �ۭq���O
  class ListenClient
  {
    private System.Net.Sockets.Socket serverSocket;
    private System.Net.Sockets.Socket clientSocket;

    // �غc�禡
    public ListenClient(Socket serverSocket)
    {
      this.serverSocket = serverSocket;
    }

    public void ServerThreadProc()
    {
      while (true)
      {
        try
        {
          // �B�z�Τ�ݳs�u
          clientSocket = serverSocket.Accept();

          // ���o����������������T
          IPEndPoint serverInfo = (IPEndPoint)serverSocket.LocalEndPoint;

          // ���o�s�u�Τ�ݬ����������s�u��T
          IPEndPoint clientInfo = (IPEndPoint)clientSocket.RemoteEndPoint;

          Console.WriteLine("Server: " + serverInfo.Address.ToString() + ":" + serverInfo.Port.ToString());
          Console.WriteLine("Client: " + clientInfo.Address.ToString() + ":" + clientInfo.Port.ToString());

          // �]�w������ƽw�İ�
          byte[] bytes = new Byte[1024];

          // �ۤw�s�u���Τ�ݱ������
          int bytesReceived = clientSocket.Receive(bytes, 0, bytes.Length, SocketFlags.None);

          if (bytesReceived > 0)
          {
            Console.WriteLine("�����줸�ռƥ�: {0}", bytesReceived);
            Console.WriteLine("��������Ƥ��e: \r\n" + "{0}", Encoding.UTF8.GetString(bytes, 0, bytesReceived) + "\r\n");
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.StackTrace.ToString());
        }
      }
    }
  }
}

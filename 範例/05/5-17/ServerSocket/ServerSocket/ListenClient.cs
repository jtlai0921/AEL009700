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
    private System.Net.Sockets.TcpListener tcpListener;
    private System.Net.Sockets.Socket clientSocket;

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
          clientSocket = tcpListener.AcceptSocket();

          // ���o����������������T
          IPEndPoint serverInfo = (IPEndPoint)tcpListener.LocalEndpoint;

          // ���o�s�u�Τ�ݬ����������s�u��T
          IPEndPoint clientInfo = (IPEndPoint)clientSocket.RemoteEndPoint;

          Console.WriteLine("Client: " + clientInfo.Address.ToString() + ":" + clientInfo.Port.ToString());
          Console.WriteLine("Server: " + serverInfo.Address.ToString() + ":" + serverInfo.Port.ToString());

          // �]�w������ƽw�İ�
          byte[] bytes = new Byte[1024];

          // �ۤw�s�u���Τ�ݱ������
          int bytesReceived = clientSocket.Receive(bytes, 0, bytes.Length, SocketFlags.None);

          if (bytesReceived > 0)
          {
            Console.WriteLine("�����줸�ռƥ�: {0}", bytesReceived);
            Console.WriteLine("��������Ƥ��e: \r\n" + "{0}", Encoding.UTF8.GetString(bytes, 0, bytesReceived) + "\r\n");
          }

          // ���ե�
          string htmlBody = "<html><head><title>Send Test</title></head><body><font size=2 face=Verdana>Sent OK.</font></body></html>";
          string htmlHeader = "HTTP/1.0 200 OK" + "\r\n" + "Server: HTTP Server 1.0" + "\r\n" + "Content-Type: text/html" + "\r\n" + "Content-Length: " + htmlBody.Length + "\r\n\r\n";

          string htmlContent = htmlHeader + htmlBody;

          // �]�w�ǰe��ƽw�İ�
          byte[] msg = Encoding.ASCII.GetBytes(htmlContent);

          // �ǰe��Ʀܤw�s�u���Τ��
          int bytesSend = clientSocket.Send(msg, 0, msg.Length, SocketFlags.None);

          Console.WriteLine("�ǰe�줸�ռƥ�: {0}", bytesSend);
          Console.WriteLine("�ǰe����Ƥ��e: " + "\r\n" + "{0}", Encoding.UTF8.GetString(msg, 0, bytesSend) + "\r\n");

          // �P�ɼȰ��Τ�ݪ��ǰe�M�����@�~
          clientSocket.Shutdown(SocketShutdown.Both);
          // �����Τ��Socket
          clientSocket.Close();
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.StackTrace.ToString());
        }
      }
    }
  }
}

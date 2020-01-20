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

          // ���o���A�ݪ���X�J��y
          NetworkStream networkStream = tcpClient.GetStream();

          // �P�_��y�O�_�䴩Ū���\��
          if (networkStream.CanRead)
          {
            byte[] bytes = new byte[1024];
            int bytesReceived = 0;
            string data = null;

            do
            {
              // �۸�Ʀ�y��Ū�����
              bytesReceived = networkStream.Read(bytes, 0, bytes.Length);

              data += Encoding.ASCII.GetString(bytes, 0, bytesReceived);

            } while (networkStream.DataAvailable);  // �P�_��y������ƬO�_�i��Ū��

            Console.WriteLine("��������Ƥ��e: " + "\r\n" + "{0}", data + "\r\n");
          }
          else
          {
            Console.WriteLine("��y���䴩Ū���\��.");
          }

          // �P�_��y�O�_�䴩�g�J�\��
          if (networkStream.CanWrite)
          {
            // ���ե�
            string htmlBody = "<html><head><title>Send Test</title></head><body><font size=2 face=Verdana>Sent OK.</font></body></html>";
            string htmlHeader = "HTTP/1.0 200 OK" + "\r\n" + "Server: HTTP Server 1.0" + "\r\n" + "Content-Type: text/html" + "\r\n" + "Content-Length: " + htmlBody.Length + "\r\n" + "\r\n";

            string htmlContent = htmlHeader + htmlBody;

            // �]�w�ǰe��ƽw�İ�
            byte[] msg = Encoding.ASCII.GetBytes(htmlContent);

            // �N��Ƽg�J��Ʀ�y��
            networkStream.Write(msg, 0, msg.Length);

            Console.WriteLine("�ǰe����Ƥ��e: " + "\r\n" + "{0}", Encoding.UTF8.GetString(msg, 0, msg.Length) + "\r\n");
          }
          else
          {
            Console.WriteLine("��y���䴩�g�J�\��.");
          }

          // ������y
          networkStream.Close();
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.StackTrace.ToString());
        }
      }
    }
  }
}

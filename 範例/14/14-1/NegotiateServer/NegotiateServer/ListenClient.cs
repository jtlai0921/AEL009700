using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;

namespace NegotiateServer
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
      NegotiateStream negotiateStream = null;

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

          // �إߤ䴩�Τ�ݻP���A�ݶ���A�w���ʳq�T��w����Ʀ�y
          negotiateStream = new NegotiateStream(tcpClient.GetStream());

          // �Ѧ��A�ݩI�s���ҳs�������Τ�ݡA�ÿ�ܩ����Ҧ��A��
          negotiateStream.AuthenticateAsServer();

          // �P�_���ҡ]Authentication�^�O�_���\
          if (negotiateStream.IsAuthenticated)
          {
            System.Security.Principal.IIdentity remoteIdentity = negotiateStream.RemoteIdentity;

            Console.WriteLine("Client identity: {0}", remoteIdentity.Name);
            Console.WriteLine("Authentication Type: {0}", remoteIdentity.AuthenticationType);
            Console.WriteLine("IsAuthenticated: {0}", negotiateStream.IsAuthenticated);
            Console.WriteLine("IsMutuallyAuthenticated: {0}", negotiateStream.IsMutuallyAuthenticated);
            Console.WriteLine("IsEncrypted: {0}", negotiateStream.IsEncrypted);
            Console.WriteLine("IsSigned: {0}", negotiateStream.IsSigned);
            Console.WriteLine("IsServer: {0}", negotiateStream.IsServer);
          }

          // �P�_��y�O�_�䴩Ū���\��
          if (negotiateStream.CanRead)
          {
            byte[] bytes = new byte[1024];

            // �۸�Ʀ�y��Ū�����
            int bytesReceived = negotiateStream.Read(bytes, 0, bytes.Length);

            string data = Encoding.ASCII.GetString(bytes, 0, bytesReceived);

            Console.WriteLine("��������Ƥ��e: " + "\r\n" + "{0}", data + "\r\n");
          }
          else
          {
            Console.WriteLine("��y���䴩Ū���\��.");
          }

          // �P�_��y�O�_�䴩�g�J�\��
          if (negotiateStream.CanWrite)
          {
            // ���ե�
            string htmlBody = "<html><head><title>Send Test</title></head><body><font size=2 face=Verdana>Sent OK.</font></body></html>";
            string htmlHeader = "HTTP/1.0 200 OK" + "\r\n" + "Server: HTTP Server 1.0" + "\r\n" + "Content-Type: text/html" + "\r\n" + "Content-Length: " + htmlBody.Length + "\r\n" + "\r\n";

            string htmlContent = htmlHeader + htmlBody;

            // �]�w�ǰe��ƽw�İ�
            byte[] msg = Encoding.ASCII.GetBytes(htmlContent);

            // �N��Ƽg�J��Ʀ�y��
            negotiateStream.Write(msg, 0, msg.Length);

            Console.WriteLine("�ǰe����Ƥ��e: " + "\r\n" + "{0}", Encoding.UTF8.GetString(msg, 0, msg.Length) + "\r\n");
          }
          else
          {
            Console.WriteLine("��y���䴩�g�J�\��.");
          }
        }
        catch (AuthenticationException aex)
        {
          Console.WriteLine(aex.StackTrace.ToString());
        }
        catch (SocketException ex)
        {
          Console.WriteLine(ex.StackTrace.ToString());
        }
        catch (IOException ioex)
        {
          Console.WriteLine(ioex.StackTrace.ToString());
        }
        finally
        {
          if (negotiateStream != null)
            // ������y
            negotiateStream.Close();
        }
      }
    }
  }
}
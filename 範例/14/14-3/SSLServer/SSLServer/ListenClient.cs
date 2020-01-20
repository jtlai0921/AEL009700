using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SSLServer
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
      SslStream sslStream = null;

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

          // �إߤ䴩SSL�w���q�T����Ʀ�y
          sslStream = new SslStream(tcpClient.GetStream());

          // �إߦ��A�ݾ��ҡA�����ɮ׬�test.cer
          System.Security.Cryptography.X509Certificates.X509Certificate certificate = X509Certificate.CreateFromCertFile("test.cer");

          // �Ѧ��A�ݩI�s���ҳs���������A�ݡA�ÿ�ܩ����ҥΤ��
          sslStream.AuthenticateAsServer(certificate);
          // �ΥHSSL 3.0��TLS 1.0�q�T��w�i��w���q�T
          // sslStream.AuthenticateAsServer(certificate, true, SslProtocols.Default, true);

          // �P�_���ҡ]Authentication�^�O�_���\
          if (sslStream.IsAuthenticated)
          {
            Console.WriteLine("IsAuthenticated: {0}", sslStream.IsAuthenticated);
            Console.WriteLine("IsEncrypted: {0}", sslStream.IsEncrypted);
            Console.WriteLine("IsMutuallyAuthenticated: {0}", sslStream.IsMutuallyAuthenticated);
            Console.WriteLine("IsServer: {0}", sslStream.IsServer);
            Console.WriteLine("IsSigned: {0}", sslStream.IsSigned);
          }

          // �P�_��y�O�_�䴩Ū���\��
          if (sslStream.CanRead)
          {
            byte[] bytes = new byte[1024];

            // �۸�Ʀ�y��Ū�����
            int bytesReceived = sslStream.Read(bytes, 0, bytes.Length);

            string data = Encoding.ASCII.GetString(bytes, 0, bytesReceived);

            Console.WriteLine("��������Ƥ��e: " + "\r\n" + "{0}", data + "\r\n");
          }
          else
          {
            Console.WriteLine("��y���䴩Ū���\��.");
          }

          // �P�_��y�O�_�䴩�g�J�\��
          if (sslStream.CanWrite)
          {
            // ���ե�
            string htmlBody = "<html><head><title>Send Test</title></head><body><font size=2 face=Verdana>Sent OK.</font></body></html>";
            string htmlHeader = "HTTP/1.0 200 OK" + "\r\n" + "Server: HTTP Server 1.0" + "\r\n" + "Content-Type: text/html" + "\r\n" + "Content-Length: " + htmlBody.Length + "\r\n" + "\r\n";

            string htmlContent = htmlHeader + htmlBody;

            // �]�w�ǰe��ƽw�İ�
            byte[] msg = Encoding.ASCII.GetBytes(htmlContent);

            // �N��Ƽg�J��Ʀ�y��
            sslStream.Write(msg, 0, msg.Length);

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
          if (sslStream != null)
            // ������y
            sslStream.Close();
        }
      }
    }
  }
}
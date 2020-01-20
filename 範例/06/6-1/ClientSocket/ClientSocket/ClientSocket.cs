using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace ClientSocket
{
  class ClientSocket
  {
    static void Main(string[] args)
    {
      if ((args.Length < 2))
      {
        Console.WriteLine("Usage: ClientSocket [Server DNS/IP] [Port]");
        return;
      }

      string host = args[0];
      string port = args[1];

      try
      {
        IPAddress serverIP = Dns.Resolve(host).AddressList[0];

        IPEndPoint serverhost = new IPEndPoint(serverIP, Int32.Parse(port));

        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // 建立用戶端與伺服端連線
        clientSocket.Connect(serverhost);

        // 測試用
        string htmlContent = "GET / HTTP/1.1" + "\r\n" + "Host: " + serverhost.Address.ToString() + "\r\n" + "Connection: Close" + "\r\n\r\n";

        // 設定傳送資料緩衝區
        byte[] msg = Encoding.ASCII.GetBytes(htmlContent);

        // 傳送資料至已連線的伺服端
        int bytesSend = clientSocket.Send(msg, 0, msg.Length, SocketFlags.None);

        Console.WriteLine("傳送位元組數目: {0}", bytesSend);
        Console.WriteLine("傳送的資料內容: " + "\r\n" + "{0}", Encoding.UTF8.GetString(msg, 0, bytesSend) + "\r\n");

        // 設定接收資料緩衝區
        byte[] bytes = new Byte[1024];

        // 自已連線的伺服端接收資料
        int bytesReceived = clientSocket.Receive(bytes, 0, bytes.Length, SocketFlags.None);

        if (bytesReceived > 0)
        {
          Console.WriteLine("接收位元組數目: {0}", bytesReceived);
          Console.WriteLine("接收的資料內容: \r\n" + "{0}", Encoding.UTF8.GetString(bytes, 0, bytesReceived) + "\r\n");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }
  }
}

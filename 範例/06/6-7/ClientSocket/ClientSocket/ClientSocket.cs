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

        // 使用伺服端之IPEndPoint
        IPEndPoint serverhost = new IPEndPoint(serverIP, Int32.Parse(port));

        TcpClient tcpClient = new TcpClient();

        // 建立用戶端與伺服端的連線
        tcpClient.Connect(serverhost);

        // 取得用戶端的輸出入串流
        NetworkStream networkStream = tcpClient.GetStream();

        // 判斷串流是否支援寫入功能
        if (networkStream.CanWrite)
        {
          // 測試用
          string htmlContent = "GET / HTTP/1.1" + "\r\n" + "Host: " + serverhost.Address.ToString() + "\r\n" + "Connection: Close" + "\r\n\r\n";

          // 設定傳送資料緩衝區
          byte[] msg = Encoding.ASCII.GetBytes(htmlContent);

          // 將資料寫入資料串流中
          networkStream.Write(msg, 0, msg.Length);

          Console.WriteLine("傳送的資料內容: " + "\r\n" + "{0}", Encoding.UTF8.GetString(msg, 0, msg.Length) + "\r\n");
        }
        else
        {
          Console.WriteLine("串流不支援寫入功能.");
        }

        // 判斷串流是否支援讀取功能
        if (networkStream.CanRead)
        {
          // 設定接收資料緩衝區
          byte[] bytes = new Byte[1024];
          int bytesReceived = 0;
          string data = null;

          do
          {
            // 自資料串流中讀取資料
            bytesReceived = networkStream.Read(bytes, 0, bytes.Length);

            data += Encoding.ASCII.GetString(bytes, 0, bytesReceived);

          } while (networkStream.DataAvailable);  // 判斷串流中的資料是否可供讀取

          Console.WriteLine("接收的資料內容: " + "\r\n" + "{0}", data + "\r\n");
        }
        else
        {
          Console.WriteLine("串流不支援讀取功能.");
        }

        // 關閉串流
        networkStream.Close();

        // 關閉用戶端Socket
        tcpClient.Close();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }
  }
}

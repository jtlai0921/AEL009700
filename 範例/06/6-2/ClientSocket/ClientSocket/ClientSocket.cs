using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ClientSocket
{
  class ClientSocket
  {
    public static ManualResetEvent thread = new System.Threading.ManualResetEvent(false);

    // 設定接收資料緩衝區
    public static byte[] bytes = new byte[1024];

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

        thread.Reset();

        // 開始非同步作業連線至伺服端 
        // 並定義所呼叫的Callback方法為ConnectCallback
        clientSocket.BeginConnect(serverhost, new AsyncCallback(ConnectCallback), clientSocket);

        thread.WaitOne();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }

    public static void ConnectCallback(IAsyncResult asyncResult) {
      try {
        Socket clientSocket = (System.Net.Sockets.Socket)asyncResult.AsyncState;

        // 以非同步作業連線至伺服端
        clientSocket.EndConnect(asyncResult);

        // 取得伺服端相關的網路連線資訊
        IPEndPoint serverInfo = (IPEndPoint)clientSocket.RemoteEndPoint;

        // 測試用
        string htmlContent = "GET / HTTP/1.1" + "\r\n" + "Host: " + serverInfo.Address.ToString() + "\r\n" + "Connection: Close" + "\r\n\r\n";
       
        // 設定傳送資料緩衝區
        byte[] msg = Encoding.ASCII.GetBytes(htmlContent);

        Console.WriteLine("傳送的資料內容: \r\n" + htmlContent); 
        
        // 開始非同步作業傳送資料至已連線的伺服端
        // 並定義所呼叫的Callback方法為SendCallback
        clientSocket.BeginSend(msg, 0, msg.Length, SocketFlags.None, new AsyncCallback(SendCallback), clientSocket);
      }
      catch (Exception ex) {
        Console.WriteLine(ex.ToString());
      }
    }

    // 自訂Callback方法
    public static void SendCallback(IAsyncResult asyncResult) {
      try {
        Socket clientSocket = (System.Net.Sockets.Socket)asyncResult.AsyncState;

        // 結束非同步傳送資料至伺服端
        int bytesSend = clientSocket.EndSend(asyncResult);

        Console.WriteLine("傳送位元組數目: {0}", bytesSend);

        // 開始非同步作業自已連線的用戶端接收資料
        // 並定義所呼叫的Callback方法為ReceiveCallback
        clientSocket.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), clientSocket);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }

    // 自訂Callback方法
    public static void ReceiveCallback(IAsyncResult asyncResult) {
      try {
        Socket clientSocket = (System.Net.Sockets.Socket)asyncResult.AsyncState;

        // 結束非同步自已連線的用戶端接收資料
        int bytesReceived = clientSocket.EndReceive(asyncResult);

        if (bytesReceived > 0)
        {
          Console.WriteLine("接收位元組數目: {0}", bytesReceived);
          Console.WriteLine("接收的資料內容: \r\n" + "{0}", Encoding.UTF8.GetString(bytes, 0, bytesReceived) + "\r\n");
        }

        thread.Set();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
    }
  }
}

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace ServerSocket
{
  class ServerSocket
  {
    public static ManualResetEvent thread = new System.Threading.ManualResetEvent(false);

    // 設定接收資料緩衝區
    public static byte[] bytes = new byte[1024];

    /// 應用程式的主進入點
    static void Main(string[] args)
    {
      try
      {
        // 取得本機的識別名稱
        string hostname = Dns.GetHostName();

        // 取得主機的DNS資訊
        IPAddress serverIP = Dns.Resolve(hostname).AddressList[0];

        // Port = 80
        string Port = "80";

        // 建立伺服端TcpListener
        TcpListener tcpListener = new TcpListener(serverIP, Int32.Parse(Port));

        // 等候用戶端連線
        tcpListener.Start();

        Console.WriteLine("Server started at: " + serverIP.ToString() + ":" + Port);

        while (true)
        {
          thread.Reset();

          // 開始非同步作業嘗試接受用戶端連線
          // 並定義所呼叫的Callback方法為AcceptCallback
          tcpListener.BeginAcceptSocket(new AsyncCallback(AcceptCallback), tcpListener);

          thread.WaitOne();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
    }

    // 自訂Callback方法
    public static void AcceptCallback(IAsyncResult asyncResult)
    {
      try
      {
        TcpListener tcpListener = (System.Net.Sockets.TcpListener)asyncResult.AsyncState;

        // 以非同步作業接受用戶端連線
        Socket clientSocket = tcpListener.EndAcceptSocket(asyncResult);

        // 取得本機相關的網路資訊
        IPEndPoint serverInfo = (IPEndPoint)tcpListener.LocalEndpoint;

        // 取得連線用戶端相關的網路連線資訊
        IPEndPoint clientInfo = (IPEndPoint)clientSocket.RemoteEndPoint;

        Console.WriteLine("Server: " + serverInfo.Address.ToString() + ":" + serverInfo.Port.ToString());
        Console.WriteLine("Client: " + clientInfo.Address.ToString() + ":" + clientInfo.Port.ToString());

        // 開始非同步作業自已連線的用戶端接收資料
        // 並定義所呼叫的Callback方法為ReceiveCallback
        clientSocket.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None,
          new AsyncCallback(ReceiveCallback), clientSocket);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
    }

    // 自訂Callback方法
    public static void ReceiveCallback(IAsyncResult asyncResult)
    {
      try
      {
        Socket clientSocket = (System.Net.Sockets.Socket)asyncResult.AsyncState;

        // 結束非同步自已連線的用戶端接收資料
        int bytesReceived = clientSocket.EndReceive(asyncResult);

        if (bytesReceived > 0)
        {
          Console.WriteLine("接收位元組數目: {0}", bytesReceived);
          Console.WriteLine("接收的資料內容: \r\n" + "{0}", Encoding.UTF8.GetString(bytes, 0, bytesReceived) + "\r\n");
        }

        // 測試用
        string htmlBody = "<html><head><title>Send Test</title></head><body><font size=2 face=Verdana>Sent OK.</font></body></html>";
        string htmlHeader = "HTTP/1.0 200 OK" + "\r\n" + "Server: HTTP Server 1.0" + "\r\n" + "Content-Type: text/html" + "\r\n" + "Content-Length: " + htmlBody.Length + "\r\n\r\n";

        string htmlContent = htmlHeader + htmlBody;

        // 設定傳送資料緩衝區
        byte[] msg = Encoding.ASCII.GetBytes(htmlContent);

        // 開始非同步作業傳送資料至已連線的用戶端
        // 並定義所呼叫的Callback方法為SendCallback
        clientSocket.BeginSend(msg, 0, msg.Length, SocketFlags.None,
          new AsyncCallback(SendCallback), clientSocket);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
    }

    // 自訂Callback方法
    public static void SendCallback(IAsyncResult asyncResult)
    {
      try
      {
        Socket clientSocket = (System.Net.Sockets.Socket)asyncResult.AsyncState;

        // 結束非同步傳送資料至用戶端
        int bytesSend = clientSocket.EndSend(asyncResult);

        if (bytesSend > 0)
          Console.WriteLine("傳送位元組數目: {0}", bytesSend + "\r\n");

        thread.Set();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
    }
  }
}


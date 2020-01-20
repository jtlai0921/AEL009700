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
    public static byte[] bytes = new byte[1024];
    public static string data;

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
          tcpListener.BeginAcceptTcpClient(new AsyncCallback(AcceptCallback), tcpListener);

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
        TcpClient tcpClient = tcpListener.EndAcceptTcpClient(asyncResult);

        // 取得本機相關的網路資訊
        IPEndPoint serverInfo = (IPEndPoint)tcpListener.LocalEndpoint;

        // 以Client屬性取得用戶端之Socket物件
        Socket clientSocket = tcpClient.Client;

        // 取得連線用戶端相關的網路連線資訊
        IPEndPoint clientInfo = (IPEndPoint)clientSocket.RemoteEndPoint;

        Console.WriteLine("Server: " + serverInfo.Address.ToString() + ":" + serverInfo.Port.ToString());
        Console.WriteLine("Client: " + clientInfo.Address.ToString() + ":" + clientInfo.Port.ToString());

        // 取得伺服端的輸出入串流
        NetworkStream networkStream = tcpClient.GetStream();

        // 判斷串流是否支援讀取功能
        if (networkStream.CanRead)
        {
          data = "";

          // 開始非同步作業自資料串流中讀取資料
          // 並定義所呼叫的Callback方法為ReadCallBack
          networkStream.BeginRead(bytes, 0, bytes.Length, new AsyncCallback(ReadCallback), networkStream);
        }
        else
        {
          Console.WriteLine("串流不支援讀取功能.");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
    }

    // 自訂Callback方法
    public static void ReadCallback(IAsyncResult asyncResult)
    {
      try
      {
        NetworkStream networkStream = (NetworkStream)asyncResult.AsyncState;

        // 結束非同步自資料串流中讀取資料
        int bytesRead = networkStream.EndRead(asyncResult);

        data = String.Concat(data, Encoding.ASCII.GetString(bytes, 0, bytesRead));

        // 判斷串流中的資料是否可供讀取
        while (networkStream.DataAvailable)
        {
          // 開始非同步作業自資料串流中讀取資料
          // 並定義所呼叫的Callback方法為ReadCallBack
          networkStream.BeginRead(bytes, 0, bytes.Length, new AsyncCallback(ReadCallback), networkStream);
        }

        Console.WriteLine("接收的資料內容: " + "\r\n" + "{0}", data + "\r\n");

        // 判斷串流是否支援寫入功能
        if (networkStream.CanWrite)
        {
          // 測試用
          string htmlBody = "<html><head><title>Send Test</title></head><body><font size=2 face=Verdana>Sent OK.</font></body></html>";
          string htmlHeader = "HTTP/1.0 200 OK" + "\r\n" + "Server: HTTP Server 1.0" + "\r\n" + "Content-Type: text/html" + "\r\n" + "Content-Length: " + htmlBody.Length + "\r\n" + "\r\n";

          string htmlContent = htmlHeader + htmlBody;

          // 設定傳送資料緩衝區
          byte[] msg = Encoding.ASCII.GetBytes(htmlContent);

          // 開始非同步作業將資料寫入資料串流中
          // 並定義所呼叫的Callback方法為WriteCallBack
          networkStream.BeginWrite(msg, 0, msg.Length, new AsyncCallback(WriteCallBack), networkStream);
        }
        else
        {
          Console.WriteLine("串流不支援寫入功能.");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
    }

    // 自訂Callback方法
    public static void WriteCallBack(IAsyncResult asyncResult)
    {
      try
      {
        NetworkStream networkStream = (NetworkStream)asyncResult.AsyncState;

        // 結束非同步將資料寫入資料串流中
        networkStream.EndWrite(asyncResult);

        // 關閉串流
        networkStream.Close();

        thread.Set();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
    }
  }
}
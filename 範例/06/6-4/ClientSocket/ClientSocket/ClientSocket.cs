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
    public static string data;

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

        thread.Reset();

        // 開始非同步作業連線至伺服端 
        // 並定義所呼叫的Callback方法為ConnectCallback
        tcpClient.BeginConnect(host, Int32.Parse(port), new AsyncCallback(ConnectCallback), tcpClient);

        thread.WaitOne();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }

    public static void ConnectCallback(IAsyncResult asyncResult) {
      try {
        TcpClient tcpClient = (System.Net.Sockets.TcpClient)asyncResult.AsyncState;

        // 以非同步作業連線至伺服端
        tcpClient.EndConnect(asyncResult);

        // 以Client屬性取得用戶端之Socket物件
        Socket clientSocket = tcpClient.Client;

        // 取得伺服端相關的網路連線資訊
        IPEndPoint serverInfo = (IPEndPoint)clientSocket.RemoteEndPoint;

        // 取得伺服端的輸出入串流
        NetworkStream networkStream = tcpClient.GetStream();

        // 判斷串流是否支援寫入功能
        if (networkStream.CanWrite)
        {
          // 測試用
          string htmlContent = "GET / HTTP/1.1" + "\r\n" + "Host: " + serverInfo.Address.ToString() + "\r\n" + "Connection: Close" + "\r\n\r\n";

          // 設定傳送資料緩衝區
          byte[] msg = Encoding.ASCII.GetBytes(htmlContent);

          // 開始非同步作業將資料寫入資料串流中
          // 並定義所呼叫的Callback方法為WriteCallBack
          networkStream.BeginWrite(msg, 0, msg.Length, new AsyncCallback(WriteCallBack), networkStream);

          Console.WriteLine("傳送的資料內容: " + "\r\n" + "{0}", Encoding.UTF8.GetString(msg, 0, msg.Length) + "\r\n");
        }
        else
        {
          Console.WriteLine("串流不支援寫入功能.");
        }
      }
      catch (Exception ex) {
        Console.WriteLine(ex.ToString());
      }
    }

    // 自訂Callback方法
    public static void WriteCallBack(IAsyncResult asyncResult)
    {
      try {
        NetworkStream networkStream = (NetworkStream)asyncResult.AsyncState;

        // 結束非同步將資料寫入資料串流中
        networkStream.EndWrite(asyncResult);

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
        Console.WriteLine(ex.ToString());
      }
    }

    // 自訂Callback方法
    public static void ReadCallback(IAsyncResult asyncResult)
    {
      try {
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

        // 關閉串流()
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

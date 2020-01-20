using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;

namespace HTTPServer
{
  public class HTTPSession
  {
    // Server Socket
    private System.Net.Sockets.Socket serverSocket;
    // Connection Socket
    private System.Net.Sockets.Socket clientSocket;

    // 建構函式
    public HTTPSession(Socket serverSocket)
    {
      this.serverSocket = serverSocket;
    }

    public void HTTPSessionThread()
    {
      while (true)
      {
        try
        {
          // 處理用戶端連線
          clientSocket = serverSocket.Accept();

          // 取得本機相關的網路資訊
          IPEndPoint serverInfo = (IPEndPoint)serverSocket.LocalEndPoint;

          // 取得連線用戶端相關的網路連線資訊
          IPEndPoint clientInfo = (IPEndPoint)clientSocket.RemoteEndPoint;

          Console.WriteLine("Server: " + serverInfo.Address.ToString() + ":" + serverInfo.Port.ToString());
          Console.WriteLine("Client: " + clientInfo.Address.ToString() + ":" + clientInfo.Port.ToString());

          // 執行緒
          Thread clientThread = new Thread(new ThreadStart(this.processRequest));
          clientThread.Start();
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.StackTrace.ToString());

          if (clientSocket.Connected)
            clientSocket.Close();
        }
      }
    }

    private void processRequest()
    {
      // 設定接收資料緩衝區
      byte[] bytes = new byte[1024];

      string htmlRequest = "";

      try
      {
        // 自已連線的用戶端接收資料
        int bytesReceived = clientSocket.Receive(bytes, 0, bytes.Length, SocketFlags.None);

        htmlRequest = Encoding.ASCII.GetString(bytes, 0, bytesReceived);

        Console.WriteLine("HTTP Request: \r\n" + htmlRequest);

        // 指定專案所在目錄為網站的主目錄
        string rootPath = Directory.GetCurrentDirectory() + "\\WWWRoot\\";

        // Set default page
        string defaultPage = "index.html";

        string[] buffer;
        string request;

        buffer = htmlRequest.Trim().Split(" ".ToCharArray());

        // Determine the HTTP method (GET only)
        if (buffer[0].Trim().ToUpper().Equals("GET"))
        {
          request = buffer[1].Trim().ToString();

          if (request.StartsWith("/"))
            request = request.Substring(1);

          if (request.EndsWith("/") || request.Equals(""))
            request = request + defaultPage;

          request = rootPath + request;

          sendHTMLResponse(request);
        }
        else // HTTP GET method not available
        {
          request = rootPath + "Error\\" + "400.html";

          sendHTMLResponse(request);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Exception: " + ex.StackTrace.ToString());

        if (clientSocket.Connected)
          clientSocket.Close();
      }
    }

    // Send HTTP Response
    private void sendHTMLResponse(string httpRequest)
    {
      try
      {
        Console.WriteLine("HTTP Request: " + httpRequest);

        // Get the file content of HTTP Request 
        FileStream fs = new FileStream(httpRequest, FileMode.Open, FileAccess.Read, FileShare.None);
        BinaryReader br = new BinaryReader(fs);

        // The content Length of HTTP Request
        byte[] contentByte = new byte[(int)fs.Length];
        br.Read(contentByte, 0, (int)fs.Length);

        br.Close();
        fs.Close();

        // Set HTML Header
        string htmlHeader = "HTTP/1.0 200 OK" + "\r\n" +
          "Server: WebServer 1.0" + "\r\n" +
          "Content-Length: " + contentByte.Length + "\r\n" +
          "Content-Type: " + getContentType(httpRequest) +
          "\r\n" + "\r\n";

        // The content Length of HTML Header
        byte[] headerByte = Encoding.ASCII.GetBytes(htmlHeader);

        Console.WriteLine("HTML Header: " + "\r\n" + htmlHeader);

        // 回應HTML標題至用戶端瀏覽器
        clientSocket.Send(headerByte, 0, headerByte.Length, SocketFlags.None);

        // 回應網頁內容至用戶端瀏覽器
        clientSocket.Send(contentByte, 0, contentByte.Length, SocketFlags.None);

        // Close HTTP Socket connection
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
      }
      catch (Exception ex)
      {
        Console.WriteLine("Exception: " + ex.StackTrace.ToString());

        if (clientSocket.Connected)
          clientSocket.Close();
      }
    }

    // MIME: Get Content Type
    private string getContentType(string httpRequest)
    {
      if (httpRequest.EndsWith("html"))
        return "text/html";
      else if (httpRequest.EndsWith("htm"))
        return "text/html";
      else if (httpRequest.EndsWith("txt"))
        return "text/plain";
      else if (httpRequest.EndsWith("gif"))
        return "image/gif";
      else if (httpRequest.EndsWith("jpg"))
        return "image/jpeg";
      else if (httpRequest.EndsWith("jpeg"))
        return "image/jpeg";
      else if (httpRequest.EndsWith("pdf"))
        return "application/pdf";
      else if (httpRequest.EndsWith("pdf"))
        return "application/pdf";
      else if (httpRequest.EndsWith("doc"))
        return "application/msword";
      else if (httpRequest.EndsWith("xls"))
        return "application/vnd.ms-excel";
      else if (httpRequest.EndsWith("ppt"))
        return "application/vnd.ms-powerpoint";
      else
        return "text/plain";
    }
  }
}

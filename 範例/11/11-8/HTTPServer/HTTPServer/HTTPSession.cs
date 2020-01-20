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
    System.Net.HttpListener httpListener = null;
    System.Net.HttpListenerRequest httpRequest = null;
    System.Net.HttpListenerResponse httpResponse = null;
    System.Net.HttpListenerContext httpContext = null;

    // 建構函式
    public HTTPSession(HttpListener httpListener)
    {
      this.httpListener = httpListener;
    }

    public void HTTPSessionThread()
    {
      while (true)
      {
        try
        {
          // 以HttpListener類別的GetContext方法等待傳入之用戶端請求 
          httpContext = httpListener.GetContext();

          // Set Thread for each HTTP client Connection
          Thread clientThread = new Thread(new ThreadStart(this.processRequest));
          clientThread.Start();
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.StackTrace.ToString());
        }
      }
    }

    private void processRequest()
    {
      // Set WWW Root Path
      string rootPath = Directory.GetCurrentDirectory() + "\\WWWRoot\\";

      // Set default page
      string defaultPage = "index.html";

      try
      {
        // 以Request屬性取得HTTP伺服端的輸入串流，則用戶端之請求
        httpRequest = httpContext.Request;

        //
        // 顯示HttpListenerRequest類別的屬性取得HTTP請求之相關內容
        //

        // 用戶端所能接受的MIME類型
        string[] types = httpRequest.AcceptTypes;
        if (types != null)
        {
          Console.WriteLine("用戶端所能接受的MIME類型:");

          foreach (string type in types)
          {
            Console.WriteLine("   {0}", type);
          }
        }

        // Content Length
        Console.WriteLine("Content Length {0}", httpRequest.ContentLength64);

        // Content Type
        if (httpRequest.ContentType != null)
        {
          Console.WriteLine("Content Type {0}", httpRequest.ContentType);
        }

        // Cookie
        foreach (Cookie cookie in httpRequest.Cookies)
        {
          Console.WriteLine("Cookie:");
          Console.WriteLine("   {0} = {1}", cookie.Name, cookie.Value);
          Console.WriteLine("   網域屬性: {0}", cookie.Domain);
          Console.WriteLine("   有效期限: {0} (expired? {1})", cookie.Expires, cookie.Expired);
          Console.WriteLine("   URI路徑屬性: {0}", cookie.Path);
          Console.WriteLine("   通訊埠: {0}", cookie.Port);
          Console.WriteLine("   安全層級: {0}", cookie.Secure);
          Console.WriteLine("   發出的時間: {0}", cookie.TimeStamp);
          Console.WriteLine("   版本: RFC {0}", cookie.Version == 1 ? "2109" : "2965");
          Console.WriteLine("   內容: {0}", cookie.ToString());
        }

        // 用戶端所傳送資料內容的標題資訊
        System.Collections.Specialized.NameValueCollection headers = httpRequest.Headers;

        foreach (string key in headers.AllKeys)
        {
          string[] values = headers.GetValues(key);

          if (values.Length > 0)
          {
            Console.WriteLine("用戶端所傳送資料內容的標題資訊:");
            foreach (string value in values)
            {
              Console.WriteLine("   {0}", value);
            }
          }
        }

        Console.WriteLine("HTTP通訊協定方法: {0}", httpRequest.HttpMethod);
        Console.WriteLine("HTTP請求是否自本機送出? {0}", httpRequest.IsLocal);
        Console.WriteLine("是否保持持續性連結: {0}", httpRequest.KeepAlive);
        Console.WriteLine("Local End Point: {0}", httpRequest.LocalEndPoint.ToString());
        Console.WriteLine("Remote End Point: {0}", httpRequest.RemoteEndPoint.ToString());
        Console.WriteLine("HTTP通訊協定的版本: {0}", httpRequest.ProtocolVersion);
        Console.WriteLine("URL: {0}", httpRequest.Url.OriginalString);
        Console.WriteLine("Raw URL: {0}", httpRequest.RawUrl);
        Console.WriteLine("Query: {0}", httpRequest.QueryString);
        Console.WriteLine("Referred by: {0}", httpRequest.UrlReferrer);

        //
        // End of 顯示HttpListenerRequest類別的屬性取得HTTP請求之相關內容
        //

        // 取得相對URL
        string url = httpRequest.RawUrl;

        if (url.StartsWith("/"))
          url = url.Substring(1);

        if (url.EndsWith("/") || url.Equals(""))
          url = url + defaultPage;

        string request = rootPath + url;

        sendHTMLResponse(request);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Exception: " + ex.StackTrace.ToString());
      }
    }

    // Send HTTP Response
    private void sendHTMLResponse(string request)
    {
      try
      {
        // Get the file content of HTTP Request 
        FileStream fs = new FileStream(request, FileMode.Open, FileAccess.Read, FileShare.None);
        BinaryReader br = new BinaryReader(fs);

        // The content Length of HTTP Request
        byte[] respByte = new byte[(int)fs.Length];
        br.Read(respByte, 0, (int)fs.Length);

        br.Close();
        fs.Close();

        // 以Response屬性取得HTTP伺服端的輸出串流，則HTTP伺服端之回應
        httpResponse = httpContext.Response;

        // HTTP回應資料內容的大小
        httpResponse.ContentLength64 = respByte.Length;

        // HTTP回應資料內容的MIME格式
        httpResponse.ContentType = getContentType(request);

        // 取得伺服端HTTP回應之資料串流
        System.IO.Stream output = httpResponse.OutputStream;

        // 回應HTML網頁內容至用戶端瀏覽器
        output.Write(respByte, 0, respByte.Length);

        output.Close();
        httpResponse.Close();
      }
      catch (Exception ex)
      {
        Console.WriteLine("Exception: " + ex.StackTrace.ToString());

        if (httpResponse != null)
          httpResponse.Close();
      }
    }

    // MIME: Get Content Type
    private string getContentType(string request)
    {
      if (request.EndsWith("html"))
        return "text/html";
      else if (request.EndsWith("htm"))
        return "text/html";
      else if (request.EndsWith("txt"))
        return "text/plain";
      else if (request.EndsWith("gif"))
        return "image/gif";
      else if (request.EndsWith("jpg"))
        return "image/jpeg";
      else if (request.EndsWith("jpeg"))
        return "image/jpeg";
      else if (request.EndsWith("pdf"))
        return "application/pdf";
      else if (request.EndsWith("pdf"))
        return "application/pdf";
      else if (request.EndsWith("doc"))
        return "application/msword";
      else if (request.EndsWith("xls"))
        return "application/vnd.ms-excel";
      else if (request.EndsWith("ppt"))
        return "application/vnd.ms-powerpoint";
      else
        return "text/plain";
    }
  }
}
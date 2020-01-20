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

    // �غc�禡
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
          // �HHttpListener���O��GetContext��k���ݶǤJ���Τ�ݽШD 
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
        // �HRequest�ݩʨ��oHTTP���A�ݪ���J��y�A�h�Τ�ݤ��ШD
        httpRequest = httpContext.Request;

        //
        // ���HttpListenerRequest���O���ݩʨ��oHTTP�ШD���������e
        //

        // �Τ�ݩү౵����MIME����
        string[] types = httpRequest.AcceptTypes;
        if (types != null)
        {
          Console.WriteLine("�Τ�ݩү౵����MIME����:");

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
          Console.WriteLine("   �����ݩ�: {0}", cookie.Domain);
          Console.WriteLine("   ���Ĵ���: {0} (expired? {1})", cookie.Expires, cookie.Expired);
          Console.WriteLine("   URI���|�ݩ�: {0}", cookie.Path);
          Console.WriteLine("   �q�T��: {0}", cookie.Port);
          Console.WriteLine("   �w���h��: {0}", cookie.Secure);
          Console.WriteLine("   �o�X���ɶ�: {0}", cookie.TimeStamp);
          Console.WriteLine("   ����: RFC {0}", cookie.Version == 1 ? "2109" : "2965");
          Console.WriteLine("   ���e: {0}", cookie.ToString());
        }

        // �Τ�ݩҶǰe��Ƥ��e�����D��T
        System.Collections.Specialized.NameValueCollection headers = httpRequest.Headers;

        foreach (string key in headers.AllKeys)
        {
          string[] values = headers.GetValues(key);

          if (values.Length > 0)
          {
            Console.WriteLine("�Τ�ݩҶǰe��Ƥ��e�����D��T:");
            foreach (string value in values)
            {
              Console.WriteLine("   {0}", value);
            }
          }
        }

        Console.WriteLine("HTTP�q�T��w��k: {0}", httpRequest.HttpMethod);
        Console.WriteLine("HTTP�ШD�O�_�ۥ����e�X? {0}", httpRequest.IsLocal);
        Console.WriteLine("�O�_�O������ʳs��: {0}", httpRequest.KeepAlive);
        Console.WriteLine("Local End Point: {0}", httpRequest.LocalEndPoint.ToString());
        Console.WriteLine("Remote End Point: {0}", httpRequest.RemoteEndPoint.ToString());
        Console.WriteLine("HTTP�q�T��w������: {0}", httpRequest.ProtocolVersion);
        Console.WriteLine("URL: {0}", httpRequest.Url.OriginalString);
        Console.WriteLine("Raw URL: {0}", httpRequest.RawUrl);
        Console.WriteLine("Query: {0}", httpRequest.QueryString);
        Console.WriteLine("Referred by: {0}", httpRequest.UrlReferrer);

        //
        // End of ���HttpListenerRequest���O���ݩʨ��oHTTP�ШD���������e
        //

        // ���o�۹�URL
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

        // �HResponse�ݩʨ��oHTTP���A�ݪ���X��y�A�hHTTP���A�ݤ��^��
        httpResponse = httpContext.Response;

        // HTTP�^����Ƥ��e���j�p
        httpResponse.ContentLength64 = respByte.Length;

        // HTTP�^����Ƥ��e��MIME�榡
        httpResponse.ContentType = getContentType(request);

        // ���o���A��HTTP�^������Ʀ�y
        System.IO.Stream output = httpResponse.OutputStream;

        // �^��HTML�������e�ܥΤ���s����
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
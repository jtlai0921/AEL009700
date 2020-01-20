using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.IO;

namespace FTPClient
{
  class FTPClient
  {
    static void Main(string[] args)
    {
      if (args.Length == 0 || args[0].Equals("/?"))
      {
        Console.WriteLine("Usage: FTPClient [/? | <FTP 下載URL> | <Local File> <FTP 上傳URL> | /list <FTP 目錄URL>]");
        Console.WriteLine("例如: ");
        Console.WriteLine("    下載檔案: FTPClient ftp://[username:password@]ftpserver/path/downloadfile");
        Console.WriteLine("    上傳檔案: FTPClient uploadfile ftp://[username:password@]ftpserver/path");
        Console.WriteLine("    瀏覽目錄: FTPClient /list ftp://[username:password@]ftpserver/path");
      }
      else if (args.Length == 1)
      {
        DownloadFile(args[0]);
      }
      else if (args.Length == 2)
      {
        if (args[0].Equals("/list"))
          ListDirectory(args[1]);
        else
          UploadFile(args[0], args[1]);
      }
      else
        Console.WriteLine("錯誤指令.");
    }

    private static void DownloadFile(string url)
    {
      Stream respStream = null;
      FileStream fileStream = null;
      StreamReader reader = null;

      try
      {
        // 以WebRequest抽象類別的Create方法建立FtpWebRequest物件
        FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(url);

        // 以FtpWebRequest類別的GetResponse方法建立FtpWebResponse物件
        FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();

        // 取得FTP伺服端回應串流
        respStream = ftpResponse.GetResponseStream();

        string fileName = Path.GetFileName(ftpRequest.RequestUri.AbsolutePath);

        if (fileName.Length == 0)
        {
          reader = new StreamReader(respStream);

          Console.WriteLine(reader.ReadToEnd());
        }
        else
        {
          fileStream = File.Create(fileName);
          byte[] buffer = new byte[1024];
          int bytesRead;

          while (true)
          {
            bytesRead = respStream.Read(buffer, 0, buffer.Length);
            if (bytesRead == 0)
              break;
            fileStream.Write(buffer, 0, bytesRead);
          }
        }
        Console.WriteLine("完成下載檔案.");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
      finally
      {
        if (reader != null)
          reader.Close();
        if (respStream != null)
          respStream.Close();
        if (fileStream != null)
          fileStream.Close();
      }
    }

    private static void UploadFile(string fileName, string url)
    {
      FtpWebResponse ftpResponse = null;
      Stream reqStream = null;
      FileStream fileStream = null;

      try
      {
        // 以WebRequest抽象類別的Create方法建立FtpWebRequest物件
        FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(url);

        // 設定用戶端所使用的FTP指令
        ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

        // 取得用以上載資料至FTP伺服端的資料串流
        reqStream = ftpRequest.GetRequestStream();

        fileStream = File.Open(fileName, FileMode.Open);

        byte[] buffer = new byte[1024];
        int bytesRead;

        while (true)
        {
          bytesRead = fileStream.Read(buffer, 0, buffer.Length);

          if (bytesRead == 0)
            break;

          reqStream.Write(buffer, 0, bytesRead);
        }

        // 關閉串流
        reqStream.Close();

        ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();

        Console.WriteLine("完成上傳檔案.");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
      finally
      {
        if (ftpResponse != null)
          ftpResponse.Close();
        if (reqStream != null)
          reqStream.Close();
        if (fileStream != null)
          fileStream.Close();
      }
    }

    private static void ListDirectory(string url)
    {
      StreamReader reader = null;

      try
      {
        // 以WebRequest抽象類別的Create方法建立FtpWebRequest物件
        FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(url);

        // 設定用戶端所使用的FTP指令
        ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

        // 以FtpWebRequest類別的GetResponse方法建立FtpWebResponse物件
        FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();

        reader = new StreamReader(ftpResponse.GetResponseStream());

        Console.WriteLine(reader.ReadToEnd());
        Console.WriteLine("完成瀏覽FTP伺服端目錄.");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
      finally
      {
        if (reader != null)
          reader.Close();
      }
    }
  }
}

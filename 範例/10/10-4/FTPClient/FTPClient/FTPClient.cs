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
        Console.WriteLine("Usage: FTPClient [/? | <FTP �U��URL> | <Local File> <FTP �W��URL> | /list <FTP �ؿ�URL>]");
        Console.WriteLine("�Ҧp: ");
        Console.WriteLine("    �U���ɮ�: FTPClient ftp://[username:password@]ftpserver/path/downloadfile");
        Console.WriteLine("    �W���ɮ�: FTPClient uploadfile ftp://[username:password@]ftpserver/path");
        Console.WriteLine("    �s���ؿ�: FTPClient /list ftp://[username:password@]ftpserver/path");
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
        Console.WriteLine("���~���O.");
    }

    private static void DownloadFile(string url)
    {
      Stream respStream = null;
      FileStream fileStream = null;
      StreamReader reader = null;

      try
      {
        // �HWebRequest��H���O��Create��k�إ�FtpWebRequest����
        FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(url);

        // �HFtpWebRequest���O��GetResponse��k�إ�FtpWebResponse����
        FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();

        // ���oFTP���A�ݦ^����y
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
        Console.WriteLine("�����U���ɮ�.");
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
        // �HWebRequest��H���O��Create��k�إ�FtpWebRequest����
        FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(url);

        // �]�w�Τ�ݩҨϥΪ�FTP���O
        ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

        // ���o�ΥH�W����Ʀ�FTP���A�ݪ���Ʀ�y
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

        // ������y
        reqStream.Close();

        ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();

        Console.WriteLine("�����W���ɮ�.");
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
        // �HWebRequest��H���O��Create��k�إ�FtpWebRequest����
        FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(url);

        // �]�w�Τ�ݩҨϥΪ�FTP���O
        ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

        // �HFtpWebRequest���O��GetResponse��k�إ�FtpWebResponse����
        FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();

        reader = new StreamReader(ftpResponse.GetResponseStream());

        Console.WriteLine(reader.ReadToEnd());
        Console.WriteLine("�����s��FTP���A�ݥؿ�.");
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

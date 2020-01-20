using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace FTPServer
{
  // 自訂類別
  class FTPSession
  {
    // Server Socket
    private System.Net.Sockets.Socket serverSocket;
    // Connection Socket
    private System.Net.Sockets.Socket clientSocket;
    // Data Socket
    private System.Net.Sockets.Socket dataSocket;

    // FTP Root Path
    private string rootPath = Directory.GetCurrentDirectory() + "\\FTPRoot\\";
    private string currentPath;
    private string currentPathStr = "/";

    private string loginName = "";
    private bool blnBinary;

    // Data Socket IP and Port
    private string clientIP = "";
    private int dataPort;

    // 建構函式
    public FTPSession(Socket serverSocket)
    {
      this.serverSocket = serverSocket;
      this.currentPath = rootPath;
    }

    public void FTPSessionThread()
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

    private void resetDefault()
    {
      currentPath = rootPath;
      currentPathStr = "/";
      Console.WriteLine("currentPath: " + currentPath);
    }

    private void showMessage(string message)
    {
      try
      {
        // 設定傳送資料緩衝區
        byte[] sendByte = Encoding.Default.GetBytes(message + "\r\n");

        // 傳送資料至已連線的用戶端
        int bytesSend = clientSocket.Send(sendByte, 0, sendByte.Length, SocketFlags.None);

        Console.WriteLine(message);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
    }

    private void showData(string data)
    {
      try
      {
        IPAddress dataIP = Dns.Resolve(clientIP).AddressList[0];
        IPEndPoint dataHost = new IPEndPoint(dataIP, dataPort);

        byte[] sendByte = Encoding.Default.GetBytes(data);

        // 建立 Data Socket
        dataSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // 建立用戶端與伺服端連線
        dataSocket.Connect(dataHost);

        // 傳送資料至已連線的伺服端
        int bytesSend = dataSocket.Send(sendByte, 0, sendByte.Length, SocketFlags.None);
        
        Console.WriteLine(data);

        dataSocket.Close();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());

        if (dataSocket.Connected)
          dataSocket.Close();
      }
    }

    private void processRequest()
    {
      // 設定接收資料緩衝區
      byte[] bytes = new byte[1024];

      string ftpCmd = "";
      string date = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
      string message = "220 .Net FTP Server (Version 1.0.0) " + date + "\r\n" + "220 Welcome to .NET FTP Server";

      showMessage(message);

      ftpCmd = "";

      // if FTP command is not "QUIT"
      while (!(ftpCmd.ToLower().StartsWith("quit")))
      {
        try
        {
          // 自已連線的用戶端接收資料
          int bytesReceived = clientSocket.Receive(bytes, 0, bytes.Length, SocketFlags.None); 

          ftpCmd = Encoding.ASCII.GetString(bytes, 0, bytesReceived);

          Console.WriteLine("FTP Command: " + ftpCmd);

          ftpCommand(ftpCmd);
        }
        catch (Exception ex)
        {
          Console.WriteLine("Exception: " + ex.StackTrace.ToString());
          
          ftpCmd = "quit";
        }
      }

      // Close FTP Session
      try
      {
        if (clientSocket.Connected)
          clientSocket.Close();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
    }

    private void ftpCommand(string command)
    {
      string[] ftpCmdToken;
      string ftpCmd = "";
      string argument;

      if (command == null)
        command = "";

      ftpCmdToken = command.Trim().Split(" ".ToCharArray());

      ftpCmd = ftpCmdToken[0].ToLower().Trim();

      // user: Login
      // 表示使用者的登錄名稱
      if (ftpCmd.Equals("user"))
      {
        try
        {
          loginName = ftpCmdToken[1].Trim();

          if (loginName.ToLower().Trim() == "anonymous")
            showMessage("331 Anonymous access allowed, send identity (e-mail name) as password.");
          else
            showMessage("331 Password required for " + loginName + ".");
        }
        catch
        {
          showMessage("500 User syntax.");
        }
      }

      // pass: Verify password
      // 表示使用者密碼
      else if (ftpCmd.Equals("pass"))
      {
        // Add the logic of verifying password here
        showMessage("230 " + loginName + " user logged in.");
        resetDefault();
      }

      // quit
      // 結束FTP Client端與Server端的通訊連結
      else if (ftpCmd.Equals("quit"))
      {
        showMessage("221 Service closing control connection. Goodbye.");
        resetDefault();
      }

      // port
      else if (ftpCmd.Equals("port"))
      {
        string[] port;

        try
        {
          // PORT h1,h2,h3,h4,p1,p2
          port = ftpCmdToken[1].Trim().Split(",".ToCharArray());

          // h1
          clientIP = port[0].ToString() + "." + port[1].ToString() + "." + port[2].ToString() + "." + port[3].ToString();

          // Port = p1 * 256 + p2
          dataPort = Int32.Parse(port[4].ToString()) * 256 + Int32.Parse(port[5].ToString());

          // Demo only 
          showMessage("PORT " + ftpCmdToken[1].Trim() + ".");
          showMessage("200 PORT command successful.");
        }
        catch
        {
          showMessage("500 PORT number syntax.");
        }
      }

      // list: List Directory (dir)
      // 列出FTP Server端目錄與檔案的詳細內容
      // 包括建檔日期、時間、檔案大小、目錄與檔案名稱等
      else if (ftpCmd.Equals("list"))
      {
        if (ftpCmdToken.Length > 1)
          argument = ftpCmdToken[1].Trim();
        else
          argument = "";

        listDirectory(argument, true);
      }

      // NLST: Name List (ls)
      // 僅顯示目錄與檔案名稱，不顯示其詳細內容
      else if (ftpCmd.Equals("nlst"))
      {
        if (ftpCmdToken.Length > 1)
          argument = ftpCmdToken[1].Trim();
        else
          argument = "";

        listDirectory(argument, false);
      }

      // cdup: Change to Parent Directory
      // 用以改變FTP Server至使用者根目錄
      else if (ftpCmd.Equals("cdup"))
      {
        changeDirectory(".");
      }

      // cwd: Change Directory (cd)
      // 改變目前FTP Server的工作目錄
      else if (ftpCmd.Equals("cwd"))
      {
        argument = ftpCmdToken[1].Trim();
        changeDirectory(argument);
      }

      // xpwd: Current Directory (pwd)
      // 顯示FTP Server端的目錄
      else if (ftpCmd.Equals("xpwd"))
      {
        showMessage("257 \"" + currentPathStr + "\" is current directory.");
        Console.WriteLine("Physical Path: " + currentPath);
      }

      // xmkd: Make Directory (mkdir)
      // 建立FTP Server端的目錄
      else if (ftpCmd.Equals("xmkd"))
      {
        argument = ftpCmdToken[1].Trim();
        makeDirectory(argument);
      }

      // xrmd: Remove Directory (rmdir)
      // 移除FTP Server端的目錄
      else if (ftpCmd.Equals("xrmd"))
      {
        argument = ftpCmdToken[1].Trim();
        removeDirectory(argument);
      }

      // dele: Remove File (delete)
      // 刪除FTP Server端檔案
      else if (ftpCmd.Equals("dele"))
      {
        argument = ftpCmdToken[1].Trim();
        removeFile(argument);
      }

      // noop: No Operation
      else if (ftpCmd.Equals("noop"))
      {
        showMessage("200 OK.");
      }

      // syst
      else if (ftpCmd.Equals("syst"))
      {
        showMessage("215 .NET FTP Server.");
      }

      //  help: Remote Help (remotehelp)
      else if (ftpCmd.Equals("help"))
      {
        string strHelp;
        strHelp = "214-The following commands are recognized(* ==>//s unimplemented).... " + "\r\n" + "214 HELP command successful.";
        showMessage(strHelp);
      }

      // type
      // 資料型態（Data Type）
      else if (ftpCmd.Equals("type"))
      {
        try
        {
          argument = ftpCmdToken[1].Trim();

          // Binary
          if (argument.ToLower().IndexOf("i") != -1)
          {
            blnBinary = true;
            showMessage("200 TYPE set to I.");
          }
          // ASCII
          else if (argument.ToLower().IndexOf("a") != -1)
          {
            blnBinary = false;
            showMessage("200 TYPE set to A.");
          }
          else
            showMessage("500 TYPE " + argument + " syntax.");
        }
        catch
        {
          showMessage("500 TYPE syntax.");
        }
      }

      // mode
      // 傳輸模式（Transmission Mode）
      else if (ftpCmd.Equals("mode"))
      {
        try
        {
          argument = ftpCmdToken[1].Trim();

          if (argument.ToLower().Equals("s"))
            showMessage("200 MODE S.");
          else
            showMessage("500 MODE " + argument + " syntax.");
        }
        catch
        {
          showMessage("500 MODE syntax.");
        }
      }

      // stru
      // 資料結構（Data Structure）
      else if (ftpCmd.Equals("stru"))
      {
        try
        {
          argument = ftpCmdToken[1].Trim();

          if (argument.ToLower().Equals("f"))
            showMessage("200 STRU F.");
          else
            showMessage("501 STRU " + argument + " not found.");
        }
        catch
        {
          showMessage("500 STRU syntax.");
        }
      }

      else
        showMessage("502 " + ftpCmd + " not implemented. Invalid command.");
    }

    // Change Directory
    private void changeDirectory(string ftpPath)
    {
      string path = "";

      try
      {
        if (ftpPath == "." || ftpPath == "/")
        {
          path = rootPath;
        }
        else if (ftpPath.StartsWith(".."))
        {
          if (currentPath == rootPath)
          {
            path = rootPath;
          }
          else
          {
            if (currentPath.EndsWith("\\"))
            {
              path = currentPath.Substring(0, currentPath.Length - 1);
              path = path.Substring(0, path.LastIndexOf("\\") + 1);
            }
            else
            {
              path = currentPath.Substring(0, currentPath.LastIndexOf("\\") + 1);
            }
          }
        }
        else if (ftpPath.StartsWith("\\"))
        {
          path = currentPath + ftpPath.Substring(1, ftpPath.Length);
        }
        else
        {
          path = currentPath + ftpPath;
        }

        if (!path.EndsWith("\\"))
        {
          path = path + "\\";
        }

        // 判斷是否為FTP伺服端的工作目錄
        if (Path.GetFileName(path) != "")
        {
          showMessage("550 " + ftpPath + " is not a directory.");
          
          return;
        }

        DirectoryInfo dirInfo = new DirectoryInfo(path);

        // 判斷目錄的使用權限是否為唯讀
        if (dirInfo.Attributes == FileAttributes.ReadOnly)
        {
          showMessage("550 " + ftpPath + ": Access is denied.");
          
          return;
        }

        // 檢查目錄是否存在
        if (Directory.Exists(path))
        {
          // 變更目錄
          Directory.SetCurrentDirectory(path);

          currentPath = path;

          if (currentPath == rootPath)
          {
            currentPathStr = "/";
          }
          else
          {
            currentPathStr = "/" + currentPath.Replace(rootPath, "");
          }

          currentPathStr = currentPathStr.Replace("\\", "/");

          if (currentPathStr.EndsWith("/") && currentPathStr.Length > 1)
          {
            currentPathStr = currentPathStr.Substring(0, currentPathStr.Length - 1);
          }

          showMessage("250 CWD command successful. " + currentPathStr);
        }
        else
        {
          showMessage("550 " + ftpPath + " is not a subdirectory of " + currentPathStr + ".");
        }
      }
      catch (Exception ex)
      {
        showMessage("500 " + ex.StackTrace.ToString());
      }
    }

    // Create a new directory
    private void makeDirectory(string ftpPath)
    {
      string path = "";

      try
      {
        if (ftpPath.StartsWith("\\"))
        {
          ftpPath = ftpPath.Substring(1, ftpPath.Length);
        }

        path = currentPath + ftpPath;

        if (!path.EndsWith("\\"))
        {
          path = path + "\\";
        }

        Console.WriteLine("New Path: " + path);

        DirectoryInfo dirInfo = new DirectoryInfo(currentPath);

        // 判斷目錄的使用權限是否為唯讀
        if (dirInfo.Attributes == FileAttributes.ReadOnly)
        {
          showMessage("550 " + ftpPath + ": Access is denied.");

          return;
        }

        // 檢查目錄是否存在
        if (Directory.Exists(path))
        {
          showMessage("550 " + ftpPath + ": Cannot create a file/path when that file/path already exists.");
        }
        else
        {
          Directory.CreateDirectory(path);

          showMessage("257 \"" + ftpPath + "\" directory created.");
        }
      }
      catch (Exception ex)
      {
        showMessage("500 " + ex.StackTrace.ToString());
      }
    }

    // Delete a existing directory
    private void removeDirectory(string ftpPath)
    {
      string path = "";

      try
      {
        if (ftpPath.StartsWith("\\"))
        {
          ftpPath = ftpPath.Substring(1, ftpPath.Length);
        }

        path = currentPath + ftpPath;

        if (!path.EndsWith("\\"))
        {
          path = path + "\\";
        }

        Console.WriteLine("Delete Path: " + path);

        // 檢查目錄是否存在
        if (Directory.Exists(path))
        {
          DirectoryInfo dirInfo = new DirectoryInfo(currentPath);

          // 判斷目錄的使用權限是否為唯讀
          if (dirInfo.Attributes == FileAttributes.ReadOnly)
          {
            showMessage("550 " + ftpPath + ": Access is denied.");

            return;
          }

          string[] fileEntries, dirEntries;

          fileEntries = Directory.GetFiles(path);
          dirEntries = Directory.GetDirectories(path);

          // Directory is empty
          if (fileEntries.Length == 0 && dirEntries.Length == 0)
          {
            // 移除目錄 
            Directory.Delete(path);

            showMessage("250 RMD command successful.");
          }
          else
          {
            showMessage("550 " + ftpPath + ": The directory is not empty.");
          }
        }
        else
        {
          showMessage("550 " + ftpPath + " is not existed.");
        }
      }
      catch (Exception ex)
      {
        showMessage("500 " + ex.StackTrace.ToString());
      }
    }

    // Delete a existing file
    private void removeFile(string ftpFile)
    {
      string file = "";

      try
      {
        if (ftpFile.StartsWith("\\"))
        {
          ftpFile = ftpFile.Substring(1, ftpFile.Length);
        }

        file = currentPath + ftpFile;

        Console.WriteLine("Delete File: " + file);

        // 檢查檔案是否存在
        if (File.Exists(file))
        {
          FileInfo fileInfo = new FileInfo(file);

          // 判斷檔案的使用權限是否為唯讀
          if (fileInfo.Attributes == FileAttributes.ReadOnly)
          {
            showMessage("550 " + ftpFile + ": Access is denied.");
          }
          else
          {
            // 刪除檔案 
            File.Delete(file);

            showMessage("250 DELE command successful.");
          }
        }
        else
        {
          showMessage("550 " + ftpFile + ": The system cannot find the file specified.");
        }
      }
      catch (Exception ex)
      {
        showMessage("500 " + ex.StackTrace.ToString());
      }
    }

    // ls / list / nlst
    private void listDirectory(string list, bool showDetail)
    {
      string path = "";
      string buffer = "";

      if (list == "")
        path = currentPath;
      else
        path = currentPath + list;

      // 檢查目錄是否存在
      if (Directory.Exists(path))
      {
        if (blnBinary)
        {
          if (showDetail)
            showMessage("150 Opening Binary mode data connection /bin/ls.");
          else
            showMessage("150 Opening Binary mode data connection for file list.");
        }
        else
        {
          if (showDetail)
            showMessage("150 Opening ASCII mode data connection /bin/ls.");
          else
            showMessage("150 Opening ASCII mode data connection for file list.");
        }

        // 取得瀏覽目錄下所有檔案之檔案名稱
        string[] fileEntries = Directory.GetFiles(path);
        FileInfo fileInfo;
        string name, size, date, space;

        // 顯示檔案內容
        foreach (string fileName in fileEntries)
        {
          if (showDetail)
          {
            // 顯示詳細檔案內容
            fileInfo = new FileInfo(fileName);

            date = fileInfo.LastWriteTime.ToString("MM-dd-yy  HH:mm");
            size = fileInfo.Length.ToString();
            name = fileName.Substring(fileName.LastIndexOf("\\") + 1);

            space = new string((char)32, 20 - size.Length);

            buffer = buffer + date + space + size + " " + name + "\r\n";
          }
          else
          {
            // 僅顯示檔案名稱
            name = fileName.Substring(fileName.LastIndexOf("\\") + 1);
            buffer = buffer + name + "\r\n";
          }
        }

        // 取得瀏覽目錄下所有子目錄之目錄名稱
        string[] dirEntries = Directory.GetDirectories(path);
        DirectoryInfo dirInfo;

        // 顯示目錄內容
        foreach (string dirName in dirEntries)
        {
          if (showDetail)
          {
            // 顯示詳細目錄內容
            dirInfo = new DirectoryInfo(dirName);

            date = dirInfo.LastWriteTime.ToString("MM-dd-yy  HH:mm");
            name = dirName.Substring(dirName.LastIndexOf("\\") + 1);
            buffer = buffer + date + "       <DIR>         " + name + "\r\n";
          }
          else
          {
            // 僅顯示目錄名稱
            name = dirName.Substring(dirName.LastIndexOf("\\") + 1);
            buffer = buffer + name + "\r\n";
          }
        }

        // Use data port to send path information 
        showData(buffer);

        byte[] sendByte = Encoding.Default.GetBytes(buffer);

        showMessage("226 Transfer complete.");

        // Demo only
        showMessage("ftp: " + sendByte.Length + " bytes received.");
      }
      else
        showMessage(path + " is not a valid file or directory.");
    }
  }
}

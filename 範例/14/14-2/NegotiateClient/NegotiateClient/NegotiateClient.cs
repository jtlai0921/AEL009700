using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Principal;

namespace NegotiateClient
{
  class NegotiateClient
  {
    static void Main(string[] args)
    {
      if (args.Length < 3)
      {
        Console.WriteLine("Usage: NegotiateClient [NegotiateServer DNS/IP] [Port] [Message]");
        return;
      }

      string host = args[0];
      string port = args[1];
      string message = args[2];

      NegotiateStream negotiateStream = null;

      try
      {
        IPAddress serverIP = Dns.Resolve(host).AddressList[0];

        // 使用伺服端之IPEndPoint
        IPEndPoint serverhost = new IPEndPoint(serverIP, Int32.Parse(port));

        TcpClient tcpClient = new TcpClient();

        // 建立用戶端與伺服端的連線
        tcpClient.Connect(serverhost);

        // 建立支援用戶端與伺服端間交涉安全性通訊協定之資料串流
        negotiateStream = new NegotiateStream(tcpClient.GetStream());

        // 用戶端登入網路或伺服端的帳號與密碼
        NetworkCredential credential = CredentialCache.DefaultNetworkCredentials;

        // 服務主要名稱（Service Primary Name），代表驗證伺服端的識別名稱
        string targetName = "domain\\user";

        // 已驗證資料串流所要求的安全性服務
        // 加密並簽署資料，以協助確保資料傳輸的機密性與完整性
        ProtectionLevel requiredProtectionLevel = ProtectionLevel.EncryptAndSign;

        // 設定伺服端處理序如何模擬用戶端之認證
        // 可在本機模擬用戶端的安全性內容，但無法在遠端模擬用戶端的安全性內容
        TokenImpersonationLevel allowedImpersonationLevel = TokenImpersonationLevel.Impersonation;

        // 由用戶端呼叫驗證連接中之用戶端，並選擇性驗證伺服端
        negotiateStream.AuthenticateAsClient(credential, targetName, requiredProtectionLevel, allowedImpersonationLevel);
        // 或是
        // negotiateStream.AuthenticateAsClient();

        // 判斷驗證（Authentication）是否成功
        if (negotiateStream.IsAuthenticated)
        {
          Console.WriteLine("IsAuthenticated: {0}", negotiateStream.IsAuthenticated);
          Console.WriteLine("IsEncrypted: {0}", negotiateStream.IsEncrypted);
          Console.WriteLine("IsMutuallyAuthenticated: {0}", negotiateStream.IsMutuallyAuthenticated);
          Console.WriteLine("IsServer: {0}", negotiateStream.IsServer);
          Console.WriteLine("IsSigned: {0}", negotiateStream.IsSigned);
        }

        // 判斷串流是否支援寫入功能
        if (negotiateStream.CanWrite)
        {
          // 設定傳送資料緩衝區
          byte[] msg = Encoding.ASCII.GetBytes(message);

          // 將資料寫入資料串流中
          negotiateStream.Write(msg, 0, msg.Length);

          Console.WriteLine("傳送的資料內容: " + "\r\n" + "{0}", message + "\r\n");
        }
        else
        {
          Console.WriteLine("串流不支援寫入功能.");
        }

        // 判斷串流是否支援讀取功能
        if (negotiateStream.CanRead)
        {
          // 設定接收資料緩衝區
          byte[] bytes = new Byte[1024];

          // 自資料串流中讀取資料
          int bytesReceived = negotiateStream.Read(bytes, 0, bytes.Length);

          string data = Encoding.ASCII.GetString(bytes, 0, bytesReceived);
 
          Console.WriteLine("接收的資料內容: " + "\r\n" + "{0}", data + "\r\n");
        }
        else
        {
          Console.WriteLine("串流不支援讀取功能.");
        }
      }
      catch (AuthenticationException ex)
      {
        Console.WriteLine(ex.Message);
      }
      catch (SocketException ex)
      {
        Console.WriteLine(ex.Message);
      }
      catch (IOException ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
        if (negotiateStream != null)
          // 關閉串流
          negotiateStream.Close();
      }

      Console.WriteLine("Press Enter to exit.");
      Console.Read();
    }
  }
}

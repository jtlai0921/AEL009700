using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FTPServer
{
	/// <summary>
	/// Server 的摘要描述。
	/// </summary>
	class Server
	{
		/// <summary>
		/// 應用程式的主進入點。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
      try 
      {
        //IPAddress serverIP = IPAddress.Parse("127.0.0.1") ;
        String hostname = Dns.GetHostName() ;
        IPAddress serverIP = Dns.Resolve(hostname).AddressList[0] ;

        // FTP Server Port = 21
        String Port = "21" ;

        IPEndPoint serverhost = new IPEndPoint(serverIP, Int32.Parse(Port)) ;

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, 
          SocketType.Stream, ProtocolType.Tcp) ;

        serverSocket.Bind(serverhost) ;

        // Backlog = 100
        serverSocket.Listen(100) ;

        Console.WriteLine("FTP server started at: " + serverhost.Address.ToString() + ":" + Port) ;

        FTPSession ftpSession = new FTPSession(serverSocket) ;

        ThreadStart serverThreadStart = new ThreadStart(ftpSession.FTPSessionThread);
        Thread serverthread = new Thread(serverThreadStart);

        serverthread.Start() ;
      }
      catch (Exception ex) 
      {
        Console.WriteLine(ex.StackTrace.ToString()) ;
      }
		}
	}
}

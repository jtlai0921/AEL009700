using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;

namespace HTTPRequest
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      System.Net.HttpWebRequest httpRequest;
      string result = "";

      if (txtURL.Text != "")
      {
        string uriString = "";

        if (!txtURL.Text.StartsWith("http://"))
        {
          uriString = "http://" + txtURL.Text;
          txtURL.Text = uriString;
        }
        else
          uriString = txtURL.Text;

        try
        {
          System.Uri httpURL = new Uri(uriString);

          // 以WebRequest抽象類別的Create方法建立HttpWebRequest
          httpRequest = (HttpWebRequest)WebRequest.Create(httpURL);

          // 用戶端實際回應要求的URI
          result = "Address: " + httpRequest.Address.ToString() + "\r\n";

          // 是否允許重新導向回應
          result = result + "AllowAutoRedirect: " + httpRequest.AllowAutoRedirect.ToString() + "\r\n";

          // 是否允許緩衝傳送資料
          result = result + "AllowWriteStreamBuffering: " + httpRequest.AllowWriteStreamBuffering.ToString() + "\r\n";

          // 用戶端安全性憑證
          result = result + "ClientCertificates: " + httpRequest.ClientCertificates.ToString() + "\r\n";

          // 與伺服端保持持續性的連結至下達close參數為止
          if (httpRequest.Connection != null)
            result = result + "Connection: " + httpRequest.Connection.ToString() + "\r\n";

          // 連結群組名稱
          if (httpRequest.ConnectionGroupName != null)
            result = result + "ConnectionGroupName: " + httpRequest.ConnectionGroupName.ToString() + "\r\n";

          // HTTP標題的Content-Length資訊，代表所傳送資料內容的大小
          if (httpRequest.ContentLength != -1)
            result = result + "ContentLength: " + httpRequest.ContentLength.ToString() + "\r\n";

          // HTTP標題的Content-Type資訊，代表所傳送資料內容的MIME格式
          if (httpRequest.ContentType != null)
            result = result + "ContentType: " + httpRequest.ContentType.ToString() + "\r\n";

          // 是否已接收HTTP伺服端的回應
          result = result + "HaveResponse: " + httpRequest.HaveResponse.ToString() + "\r\n";

          // HTTP標題的If-Modified-Since資訊
          result = result + "IfModifiedSince: " + httpRequest.IfModifiedSince.ToString() + "\r\n";

          // 在HTTP請求完成之後，是否關閉與HTTP伺服端之連結
          result = result + "KeepAlive: " + httpRequest.KeepAlive + "\r\n";

          // 最大重新導向數目
          result = result + "MaximumAutomaticRedirections: " + httpRequest.MaximumAutomaticRedirections + "\r\n";

          // 媒體類型
          if (httpRequest.MediaType != null)
            result = result + "MediaType: " + httpRequest.MediaType.ToString() + "\r\n";

          // 用戶端所使用的HTTP通訊協定方法
          result = result + "Method: " + httpRequest.Method.ToString() + "\r\n";

          // 是否要求預先驗證
          result = result + "PreAuthenticate: " + httpRequest.PreAuthenticate + "\r\n";

          // HTTP通訊協定的版本
          result = result + "ProtocolVersion: " + httpRequest.ProtocolVersion.ToString() + "\r\n";

          // HTTP標題的Referer資訊，代表用戶端上一次所瀏覽的資源
          if (httpRequest.Referer != null)
            result = result + "Referer: " + httpRequest.Referer.ToString() + "\r\n";

          // 用戶端所傳送的URI
          result = result + "RequestUri: " + httpRequest.RequestUri.ToString() + "\r\n";

          // HTTP標題的Transfer-encoding資訊，代表傳輸編碼方式
          if (httpRequest.TransferEncoding != null)
            result = result + "TransferEncoding: " + httpRequest.TransferEncoding.ToString() + "\r\n";

          // HTTP標題的User-agent資訊
          if (httpRequest.UserAgent != null)
            result = result + "UserAgent: " + httpRequest.UserAgent.ToString() + "\r\n";

          txtRequest.Text = result;
        }
        catch (Exception ex)
        {
          txtRequest.Text = ex.StackTrace.ToString();
        }
      }
    }
  }
}
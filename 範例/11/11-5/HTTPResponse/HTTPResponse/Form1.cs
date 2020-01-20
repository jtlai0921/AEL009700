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

namespace HTTPResponse
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
      System.Net.HttpWebResponse httpResponse;

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

          // 使用HttpWebRequest類別的GetResponse方法建立HttpWebResponse
          httpResponse = (HttpWebResponse)httpRequest.GetResponse();

          // HttpWebResponse 類別之屬性
          // 回應的字元編碼格式
          result = "CharacterSet: " + httpResponse.CharacterSet.ToString() + "\r\n";

          // 回應的壓縮及編碼格式
          result = result + "ContentEncoding: " + httpResponse.ContentEncoding.ToString() + "\r\n";

          // 回應資料內容的大小
          result = result + "ContentLength: " + httpResponse.ContentLength.ToString() + "\r\n";

          // 回應資料內容的MIME格式
          result = result + "ContentType: " + httpResponse.ContentType.ToString() + "\r\n";

          // 最近修改回應內容的日期時間
          result = result + "LastModified: " + httpResponse.LastModified.ToString() + "\r\n";

          // 回應所使用的通訊協定方法
          result = result + "Method: " + httpResponse.Method.ToString() + "\r\n";

          // 回應所使用HTTP通訊協定的版本
          result = result + "ProtocolVersion: " + httpResponse.ProtocolVersion.ToString() + "\r\n";

          // 伺服端所回應的URI
          result = result + "ResponseUri: " + httpResponse.ResponseUri.ToString() + "\r\n";

          // 傳送回應的伺服器名稱
          result = result + "Server: " + httpResponse.Server.ToString() + "\r\n";

          // 回應訊息狀態的編碼
          result = result + "StatusCode: " + httpResponse.StatusCode.ToString() + "\r\n";

          // 回應訊息狀態的描述
          result = result + "StatusDescription: " + httpResponse.StatusDescription.ToString() + "\r\n";

          txtResponse.Text = result;
        }
        catch (Exception ex)
        {
          txtResponse.Text = ex.StackTrace.ToString();
        }
      }
    }
  }
}
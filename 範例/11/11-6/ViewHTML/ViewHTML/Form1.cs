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
using System.IO;

namespace ViewHTML
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void btnHTML_Click(object sender, EventArgs e)
    {
      System.Net.HttpWebRequest httpRequest;
      System.Net.HttpWebResponse httpResponse;

      string result = "";
      char[] cbuffer = new char[256];
      int byteRead = 0;

      while (true)
      {
        break;
      }

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

          httpRequest = (HttpWebRequest)WebRequest.Create(httpURL);
          httpResponse = (HttpWebResponse)httpRequest.GetResponse();

          // 取得HTTP的回應串流
          System.IO.Stream respStream = httpResponse.GetResponseStream();

          // 取得GetResponseStream的內容
          System.IO.StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);

          byteRead = respStreamReader.Read(cbuffer, 0, 256);

          while (byteRead != 0)
          {
            string response = new string(cbuffer, 0, byteRead);

            result = result + response;

            // 以StreamReader類別的Read方法依序讀取網頁原始程式碼每一行的內容至結束為止
            byteRead = respStreamReader.Read(cbuffer, 0, 256);
          }

          respStreamReader.Close();

          // 關閉資料串流
          respStream.Close();

          txtHTML.Text = result;
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.StackTrace.ToString());
        }
      }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      string filename = "";

      SaveFileDialog1.Title = "Save HTML";
      SaveFileDialog1.Filter = "All files (*.*)|*.*";

      if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        filename = SaveFileDialog1.FileName;

        try
        {
          // 建立檔案串流
          System.IO.FileStream fileStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);

          byte[] byteSave = Encoding.ASCII.GetBytes(txtHTML.Text.ToString());

          // 以FileStream類別的Write方法將HTML內容寫入檔案中
          fileStream.Write(byteSave, 0, byteSave.Length);

          // 關閉檔案串流
          fileStream.Close();

          MessageBox.Show(filename + " has been save.", "View HTML Source", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.StackTrace.ToString());
        }
      }
    }
  }
}
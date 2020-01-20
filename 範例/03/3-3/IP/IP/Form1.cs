using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// 匯入System.Net命名空間
using System.Net;

namespace IP
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void Button1_Click(object sender, EventArgs e)
    {
      IPAddress ipAddress;
      IPEndPoint ipEndPoint;
    
      txtResult.Text = "";

      try
      {
        // 將IP位址字串轉換為IPAddress類別
        ipAddress = IPAddress.Parse(txtIP.Text);
        // 建立IPEndPoint物件
        ipEndPoint = new System.Net.IPEndPoint(ipAddress, Int32.Parse(txtPort.Text));

        // 若IP位址為IPv4位址形態，則AddressFamily屬性回傳InterNetwork；若為IPv6位址形態，則回傳InterNetworkV6
        txtResult.Text = "Address Family: " + ipEndPoint.AddressFamily.ToString() + "\r\n";

        // 以IPEndPoint的Address與Port屬性取得IP位址及通訊埠
        txtResult.Text = txtResult.Text + "IP:Port: " + ipEndPoint.Address.ToString() + ":" + ipEndPoint.Port.ToString() + "\r\n";
        
        // 將IPEndPoint序列化為SocketAddress
        txtResult.Text = txtResult.Text + "SocketAddress 內容: " + ipEndPoint.Serialize().ToString() + "\r\n";
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
    }
  }
}

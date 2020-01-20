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

namespace Host
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void Button1_Click(object sender, EventArgs e)
    {
      IPHostEntry hostEntry = null;
      IPAddress[] addrList = null;
      string strTemp = "";

      try
      {
        // 將以點分隔的DNS主機名稱或IP位址解析為IPHostEntry物件
        hostEntry = Dns.Resolve(txtHost.Text);

        // 主機IP位址清單
        addrList = hostEntry.AddressList;

        for (int i = 0; i <= addrList.Length - 1; i++)
          strTemp = strTemp + addrList[i].ToString() + "\r\n";

        MessageBox.Show("Address List: " + "\r\n" + strTemp,
          "DNS Resolve", MessageBoxButtons.OK,
          MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }
  }
}

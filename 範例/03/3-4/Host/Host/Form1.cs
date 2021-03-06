﻿using System;
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
      string[] aliasList = null;
      IPAddress[] addrList = null;

      string strTemp = "";

      try
      {
        // 取得主機的DNS相關資訊及IP位址
        hostEntry = Dns.GetHostEntry(txtHost.Text);

        // 取得主機別名（Alias）清單
        aliasList = hostEntry.Aliases;

        // 由於主機有可能有一個以上的 Alias
        // 因此程式中以迴圈方式判斷 Aliases 
        for (int i = 0; i <= aliasList.Length - 1; i++)
          strTemp = strTemp + aliasList[i].ToString() + " ";

        txtAlias.Text = strTemp;

        strTemp = "";
        
        // 取得主機IP位址清單
        addrList = hostEntry.AddressList;

        // 由於主機有可能有一個以上的 IP Address
        // 因此程式中以迴圈方式判斷 AddressList 
        for (int i = 0; i <= addrList.Length - 1; i++)
          strTemp = strTemp + addrList[i].ToString() + "\r\n";

        txtIP.Text = strTemp;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace.ToString());
      }
    }
  }
}

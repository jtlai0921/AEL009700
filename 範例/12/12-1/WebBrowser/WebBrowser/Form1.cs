﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebBrowser
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      // 瀏覽 C: 磁碟機
      webBrowser.Navigate("C:\\");
    }

    private void Form1_Resize(object sender, EventArgs e)
    {
      Control control = (Control)sender;

      txtURL.Location = new System.Drawing.Point(44, 4);
      txtURL.Size = new System.Drawing.Size(control.Size.Width - 64, 22);
    }

    private void ToolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
    {
      switch (ToolBar1.Buttons.IndexOf(e.Button))
      {
        case 0:
          // 判斷是否有上一頁的記錄
          if (webBrowser.CanGoBack)
            // 瀏覽上一頁
            webBrowser.GoBack();
          break;

        case 1:
          // 判斷是否有下一頁的記錄
          if (webBrowser.CanGoForward)
            // 瀏覽下一頁
            webBrowser.GoForward();
          break;

        case 2:
          // 停止
          webBrowser.Stop();
          break;

        case 3:
          // 重新整理
          webBrowser.Refresh();
          break;

        case 4:
          // 瀏覽首頁
          webBrowser.GoHome();
          break;

        case 5:
          // 搜尋
          webBrowser.GoSearch();
          break;
      }

      StatusBar1.Panels[0].Text = webBrowser.Url.ToString();
    }

    private void txtURL_KeyPress(object sender, KeyPressEventArgs e)
    {
      // 若在txtURL按下Enter則瀏覽txtURL網頁
      if (e.KeyChar == (char)Keys.Return)
        webBrowser.Navigate(txtURL.Text);
    }

    private void mnuCaption_Click(object sender, EventArgs e)
    {
      // 是否顯示文字標籤
      mnuCaption.Checked = !mnuCaption.Checked;

      if (mnuCaption.Checked)
      {
        ToolBar1.Buttons[0].Text = "Back";
        ToolBar1.Buttons[1].Text = "Forward";
        ToolBar1.Buttons[2].Text = "Stop";
        ToolBar1.Buttons[3].Text = "Refresh";
        ToolBar1.Buttons[4].Text = "Home";
        ToolBar1.Buttons[5].Text = "Search";

        ToolBar1.ButtonSize = new System.Drawing.Size(50, 50);
      }
      else
      {
        for (int i = 0; i <= 5; i++)
        {
          ToolBar1.Buttons[i].Text = "";
        }
        ToolBar1.ButtonSize = new System.Drawing.Size(30, 30);
      }
    }

    private void mnuExit_Click(object sender, EventArgs e)
    {
      DialogResult result = MessageBox.Show("Are you sure to quit?", "Web Browser", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

      if (result == DialogResult.Yes)
      {
        // 釋放WebBrowser所佔用之資源
        webBrowser.Dispose();
        this.Close();
      }
    }

    // 當瀏覽網頁或文件完畢時所觸發之事件
    private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
    {
      // 若瀏覽下載完畢
      txtURL.Text = webBrowser.Url.ToString();
      StatusBar1.Panels[0].Text = webBrowser.Url.ToString();
    }
  }
}

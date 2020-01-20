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

namespace Cookie
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void Button1_Click(object sender, EventArgs e)
    {
      System.Net.Cookie cookie = new System.Net.Cookie("HASH", "", "/", "microsoft.com");

      txtCookie.Text = "Comment: " + cookie.Comment.ToString() + "\r\n";
      txtCookie.Text = txtCookie.Text + "Domain: " + cookie.Domain.ToString() + "\r\n";
      txtCookie.Text = txtCookie.Text + "Expired: " + cookie.Expired.ToString() + "\r\n";
      txtCookie.Text = txtCookie.Text + "Expires: " + cookie.Expires.ToString() + "\r\n";
      txtCookie.Text = txtCookie.Text + "Name: " + cookie.Name.ToString() + "\r\n";
      txtCookie.Text = txtCookie.Text + "Path: " + cookie.Path.ToString() + "\r\n";
      txtCookie.Text = txtCookie.Text + "Port: " + cookie.Port.ToString() + "\r\n";
      txtCookie.Text = txtCookie.Text + "Secure: " + cookie.Secure.ToString() + "\r\n";
      txtCookie.Text = txtCookie.Text + "Value: " + cookie.Value.ToString() + "\r\n";
      txtCookie.Text = txtCookie.Text + "Version: " + (cookie.Version == 1 ? "2109" : "2965") + "\r\n";
    }
  }
}

﻿namespace Proxy
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.txtPort = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtProxy = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.Button1 = new System.Windows.Forms.Button();
      this.txtRequest = new System.Windows.Forms.TextBox();
      this.Label2 = new System.Windows.Forms.Label();
      this.txtURL = new System.Windows.Forms.TextBox();
      this.Label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // txtPort
      // 
      this.txtPort.Location = new System.Drawing.Point(55, 68);
      this.txtPort.Name = "txtPort";
      this.txtPort.Size = new System.Drawing.Size(240, 22);
      this.txtPort.TabIndex = 47;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(15, 72);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(44, 16);
      this.label4.TabIndex = 53;
      this.label4.Text = "Port:";
      // 
      // txtProxy
      // 
      this.txtProxy.Location = new System.Drawing.Point(55, 40);
      this.txtProxy.Name = "txtProxy";
      this.txtProxy.Size = new System.Drawing.Size(240, 22);
      this.txtProxy.TabIndex = 46;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(15, 44);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(44, 16);
      this.label3.TabIndex = 52;
      this.label3.Text = "Proxy:";
      // 
      // Button1
      // 
      this.Button1.Location = new System.Drawing.Point(109, 300);
      this.Button1.Name = "Button1";
      this.Button1.Size = new System.Drawing.Size(92, 28);
      this.Button1.TabIndex = 49;
      this.Button1.Text = "OK";
      this.Button1.Click += new System.EventHandler(this.Button1_Click);
      // 
      // txtRequest
      // 
      this.txtRequest.Location = new System.Drawing.Point(15, 120);
      this.txtRequest.Multiline = true;
      this.txtRequest.Name = "txtRequest";
      this.txtRequest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtRequest.Size = new System.Drawing.Size(280, 168);
      this.txtRequest.TabIndex = 48;
      // 
      // Label2
      // 
      this.Label2.Location = new System.Drawing.Point(15, 104);
      this.Label2.Name = "Label2";
      this.Label2.Size = new System.Drawing.Size(104, 16);
      this.Label2.TabIndex = 51;
      this.Label2.Text = "Web Request:";
      // 
      // txtURL
      // 
      this.txtURL.Location = new System.Drawing.Point(55, 12);
      this.txtURL.Name = "txtURL";
      this.txtURL.Size = new System.Drawing.Size(240, 22);
      this.txtURL.TabIndex = 45;
      // 
      // Label1
      // 
      this.Label1.Location = new System.Drawing.Point(15, 16);
      this.Label1.Name = "Label1";
      this.Label1.Size = new System.Drawing.Size(44, 16);
      this.Label1.TabIndex = 50;
      this.Label1.Text = "URL:";
      // 
      // Form1
      // 
      this.AcceptButton = this.Button1;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(310, 341);
      this.Controls.Add(this.txtPort);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.txtProxy);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.Button1);
      this.Controls.Add(this.txtRequest);
      this.Controls.Add(this.Label2);
      this.Controls.Add(this.txtURL);
      this.Controls.Add(this.Label1);
      this.MaximizeBox = false;
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Proxy";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtPort;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtProxy;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button Button1;
    private System.Windows.Forms.TextBox txtRequest;
    private System.Windows.Forms.Label Label2;
    private System.Windows.Forms.TextBox txtURL;
    private System.Windows.Forms.Label Label1;
  }
}


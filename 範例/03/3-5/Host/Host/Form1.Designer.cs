﻿namespace Host
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
      this.txtHost = new System.Windows.Forms.TextBox();
      this.Label1 = new System.Windows.Forms.Label();
      this.Button1 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // txtHost
      // 
      this.txtHost.Location = new System.Drawing.Point(82, 12);
      this.txtHost.Name = "txtHost";
      this.txtHost.Size = new System.Drawing.Size(190, 22);
      this.txtHost.TabIndex = 10;
      // 
      // Label1
      // 
      this.Label1.Location = new System.Drawing.Point(14, 16);
      this.Label1.Name = "Label1";
      this.Label1.Size = new System.Drawing.Size(64, 20);
      this.Label1.TabIndex = 12;
      this.Label1.Text = "Host Name:";
      // 
      // Button1
      // 
      this.Button1.Location = new System.Drawing.Point(103, 53);
      this.Button1.Name = "Button1";
      this.Button1.Size = new System.Drawing.Size(80, 28);
      this.Button1.TabIndex = 11;
      this.Button1.Text = "OK";
      this.Button1.Click += new System.EventHandler(this.Button1_Click);
      // 
      // Form1
      // 
      this.AcceptButton = this.Button1;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(287, 99);
      this.Controls.Add(this.txtHost);
      this.Controls.Add(this.Label1);
      this.Controls.Add(this.Button1);
      this.MaximizeBox = false;
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "DNS";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    internal System.Windows.Forms.TextBox txtHost;
    internal System.Windows.Forms.Label Label1;
    internal System.Windows.Forms.Button Button1;

  }
}


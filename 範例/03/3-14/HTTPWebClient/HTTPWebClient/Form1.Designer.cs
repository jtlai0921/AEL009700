﻿namespace HTTPWebClient
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
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.StatusBar = new System.Windows.Forms.ToolStripStatusLabel();
      this.btnDownload = new System.Windows.Forms.Button();
      this.txtURL = new System.Windows.Forms.TextBox();
      this.Label1 = new System.Windows.Forms.Label();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBar});
      this.statusStrip1.Location = new System.Drawing.Point(0, 100);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(345, 22);
      this.statusStrip1.TabIndex = 92;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // StatusBar
      // 
      this.StatusBar.Name = "StatusBar";
      this.StatusBar.Size = new System.Drawing.Size(0, 17);
      // 
      // btnDownload
      // 
      this.btnDownload.BackColor = System.Drawing.SystemColors.Control;
      this.btnDownload.Cursor = System.Windows.Forms.Cursors.Default;
      this.btnDownload.ForeColor = System.Drawing.SystemColors.ControlText;
      this.btnDownload.Location = new System.Drawing.Point(130, 53);
      this.btnDownload.Name = "btnDownload";
      this.btnDownload.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.btnDownload.Size = new System.Drawing.Size(84, 28);
      this.btnDownload.TabIndex = 90;
      this.btnDownload.Text = "&Download";
      this.btnDownload.UseVisualStyleBackColor = false;
      this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
      // 
      // txtURL
      // 
      this.txtURL.AcceptsReturn = true;
      this.txtURL.BackColor = System.Drawing.SystemColors.Window;
      this.txtURL.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtURL.ForeColor = System.Drawing.SystemColors.WindowText;
      this.txtURL.Location = new System.Drawing.Point(83, 12);
      this.txtURL.MaxLength = 0;
      this.txtURL.Name = "txtURL";
      this.txtURL.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.txtURL.Size = new System.Drawing.Size(248, 22);
      this.txtURL.TabIndex = 89;
      this.txtURL.Text = "http://i.microsoft.com/h/all/i/zh-TW.gif";
      // 
      // Label1
      // 
      this.Label1.BackColor = System.Drawing.SystemColors.Control;
      this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
      this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.Label1.Location = new System.Drawing.Point(13, 15);
      this.Label1.Name = "Label1";
      this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.Label1.Size = new System.Drawing.Size(64, 16);
      this.Label1.TabIndex = 91;
      this.Label1.Text = "HTTP URL:";
      // 
      // Form1
      // 
      this.AcceptButton = this.btnDownload;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(345, 122);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.btnDownload);
      this.Controls.Add(this.txtURL);
      this.Controls.Add(this.Label1);
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "WebClient - HTTP Protocol";
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel StatusBar;
    public System.Windows.Forms.Button btnDownload;
    public System.Windows.Forms.TextBox txtURL;
    public System.Windows.Forms.Label Label1;
  }
}


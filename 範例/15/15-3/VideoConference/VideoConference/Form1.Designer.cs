namespace VideoConference
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
      this.components = new System.ComponentModel.Container();
      this.txtHost = new System.Windows.Forms.TextBox();
      this.btnStart = new System.Windows.Forms.Button();
      this.btnStop = new System.Windows.Forms.Button();
      this.btnSend = new System.Windows.Forms.Button();
      this.btnListen = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.picRemote = new System.Windows.Forms.PictureBox();
      this.label1 = new System.Windows.Forms.Label();
      this.picLocal = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.picRemote)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picLocal)).BeginInit();
      this.SuspendLayout();
      // 
      // txtHost
      // 
      this.txtHost.Location = new System.Drawing.Point(394, 12);
      this.txtHost.Name = "txtHost";
      this.txtHost.Size = new System.Drawing.Size(176, 22);
      this.txtHost.TabIndex = 44;
      // 
      // btnStart
      // 
      this.btnStart.Location = new System.Drawing.Point(16, 277);
      this.btnStart.Name = "btnStart";
      this.btnStart.Size = new System.Drawing.Size(74, 28);
      this.btnStart.TabIndex = 43;
      this.btnStart.Text = "錄影";
      this.btnStart.UseVisualStyleBackColor = true;
      this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
      // 
      // btnStop
      // 
      this.btnStop.Location = new System.Drawing.Point(104, 277);
      this.btnStop.Name = "btnStop";
      this.btnStop.Size = new System.Drawing.Size(74, 28);
      this.btnStop.TabIndex = 42;
      this.btnStop.Text = "停止";
      this.btnStop.UseVisualStyleBackColor = true;
      this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
      // 
      // btnSend
      // 
      this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.btnSend.Location = new System.Drawing.Point(412, 277);
      this.btnSend.Name = "btnSend";
      this.btnSend.Size = new System.Drawing.Size(74, 28);
      this.btnSend.TabIndex = 41;
      this.btnSend.Text = "傳送";
      this.btnSend.UseVisualStyleBackColor = true;
      this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
      // 
      // btnListen
      // 
      this.btnListen.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.btnListen.Location = new System.Drawing.Point(192, 277);
      this.btnListen.Name = "btnListen";
      this.btnListen.Size = new System.Drawing.Size(74, 28);
      this.btnListen.TabIndex = 40;
      this.btnListen.Text = "Listen";
      this.btnListen.UseVisualStyleBackColor = true;
      this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(320, 16);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(68, 12);
      this.label2.TabIndex = 39;
      this.label2.Text = "Remote Host:";
      // 
      // picRemote
      // 
      this.picRemote.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.picRemote.Location = new System.Drawing.Point(320, 41);
      this.picRemote.Name = "picRemote";
      this.picRemote.Size = new System.Drawing.Size(250, 220);
      this.picRemote.TabIndex = 38;
      this.picRemote.TabStop = false;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(14, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(58, 12);
      this.label1.TabIndex = 37;
      this.label1.Text = "Local Host:";
      // 
      // picLocal
      // 
      this.picLocal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.picLocal.Location = new System.Drawing.Point(16, 41);
      this.picLocal.Name = "picLocal";
      this.picLocal.Size = new System.Drawing.Size(250, 220);
      this.picLocal.TabIndex = 36;
      this.picLocal.TabStop = false;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(584, 324);
      this.Controls.Add(this.txtHost);
      this.Controls.Add(this.btnStart);
      this.Controls.Add(this.btnStop);
      this.Controls.Add(this.btnSend);
      this.Controls.Add(this.btnListen);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.picRemote);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.picLocal);
      this.MaximizeBox = false;
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Video Conference";
      this.Load += new System.EventHandler(this.Form1_Load);
      ((System.ComponentModel.ISupportInitialize)(this.picRemote)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picLocal)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtHost;
    internal System.Windows.Forms.Button btnStart;
    internal System.Windows.Forms.Button btnStop;
    private System.Windows.Forms.Button btnSend;
    private System.Windows.Forms.Button btnListen;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Timer timer1;
    internal System.Windows.Forms.PictureBox picRemote;
    private System.Windows.Forms.Label label1;
    internal System.Windows.Forms.PictureBox picLocal;
  }
}


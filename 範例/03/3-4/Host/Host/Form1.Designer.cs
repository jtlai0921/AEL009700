namespace Host
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
      this.txtIP = new System.Windows.Forms.TextBox();
      this.txtAlias = new System.Windows.Forms.TextBox();
      this.Label3 = new System.Windows.Forms.Label();
      this.Label2 = new System.Windows.Forms.Label();
      this.txtHost = new System.Windows.Forms.TextBox();
      this.Label1 = new System.Windows.Forms.Label();
      this.Button1 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // txtIP
      // 
      this.txtIP.Location = new System.Drawing.Point(15, 104);
      this.txtIP.Multiline = true;
      this.txtIP.Name = "txtIP";
      this.txtIP.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtIP.Size = new System.Drawing.Size(276, 120);
      this.txtIP.TabIndex = 29;
      // 
      // txtAlias
      // 
      this.txtAlias.Location = new System.Drawing.Point(81, 46);
      this.txtAlias.Name = "txtAlias";
      this.txtAlias.Size = new System.Drawing.Size(212, 22);
      this.txtAlias.TabIndex = 24;
      // 
      // Label3
      // 
      this.Label3.Location = new System.Drawing.Point(13, 50);
      this.Label3.Name = "Label3";
      this.Label3.Size = new System.Drawing.Size(64, 20);
      this.Label3.TabIndex = 28;
      this.Label3.Text = "Alias:";
      // 
      // Label2
      // 
      this.Label2.Location = new System.Drawing.Point(13, 82);
      this.Label2.Name = "Label2";
      this.Label2.Size = new System.Drawing.Size(64, 20);
      this.Label2.TabIndex = 27;
      this.Label2.Text = "IP Address:";
      // 
      // txtHost
      // 
      this.txtHost.Location = new System.Drawing.Point(81, 14);
      this.txtHost.Name = "txtHost";
      this.txtHost.Size = new System.Drawing.Size(212, 22);
      this.txtHost.TabIndex = 23;
      // 
      // Label1
      // 
      this.Label1.Location = new System.Drawing.Point(13, 18);
      this.Label1.Name = "Label1";
      this.Label1.Size = new System.Drawing.Size(64, 20);
      this.Label1.TabIndex = 26;
      this.Label1.Text = "Host Name:";
      // 
      // Button1
      // 
      this.Button1.Location = new System.Drawing.Point(113, 239);
      this.Button1.Name = "Button1";
      this.Button1.Size = new System.Drawing.Size(80, 28);
      this.Button1.TabIndex = 25;
      this.Button1.Text = "OK";
      this.Button1.Click += new System.EventHandler(this.Button1_Click);
      // 
      // Form1
      // 
      this.AcceptButton = this.Button1;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(306, 280);
      this.Controls.Add(this.txtIP);
      this.Controls.Add(this.txtAlias);
      this.Controls.Add(this.Label3);
      this.Controls.Add(this.Label2);
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

    internal System.Windows.Forms.TextBox txtIP;
    internal System.Windows.Forms.TextBox txtAlias;
    internal System.Windows.Forms.Label Label3;
    internal System.Windows.Forms.Label Label2;
    internal System.Windows.Forms.TextBox txtHost;
    internal System.Windows.Forms.Label Label1;
    internal System.Windows.Forms.Button Button1;
  }
}


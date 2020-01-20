namespace HTTPResponse
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
      this.btnOK = new System.Windows.Forms.Button();
      this.txtResponse = new System.Windows.Forms.TextBox();
      this.Label2 = new System.Windows.Forms.Label();
      this.txtURL = new System.Windows.Forms.TextBox();
      this.Label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(113, 242);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(79, 28);
      this.btnOK.TabIndex = 19;
      this.btnOK.Text = "OK";
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // txtResponse
      // 
      this.txtResponse.Location = new System.Drawing.Point(12, 61);
      this.txtResponse.Multiline = true;
      this.txtResponse.Name = "txtResponse";
      this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtResponse.Size = new System.Drawing.Size(280, 168);
      this.txtResponse.TabIndex = 18;
      // 
      // Label2
      // 
      this.Label2.Location = new System.Drawing.Point(12, 45);
      this.Label2.Name = "Label2";
      this.Label2.Size = new System.Drawing.Size(104, 16);
      this.Label2.TabIndex = 17;
      this.Label2.Text = "HTTP Response:";
      // 
      // txtURL
      // 
      this.txtURL.Location = new System.Drawing.Point(52, 13);
      this.txtURL.Name = "txtURL";
      this.txtURL.Size = new System.Drawing.Size(240, 22);
      this.txtURL.TabIndex = 16;
      this.txtURL.Text = "http://www.microsoft.com/taiwan";
      // 
      // Label1
      // 
      this.Label1.Location = new System.Drawing.Point(12, 17);
      this.Label1.Name = "Label1";
      this.Label1.Size = new System.Drawing.Size(44, 16);
      this.Label1.TabIndex = 15;
      this.Label1.Text = "URL:";
      // 
      // Form1
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(304, 284);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.txtResponse);
      this.Controls.Add(this.Label2);
      this.Controls.Add(this.txtURL);
      this.Controls.Add(this.Label1);
      this.MaximizeBox = false;
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "HTTP Response";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.TextBox txtResponse;
    private System.Windows.Forms.Label Label2;
    private System.Windows.Forms.TextBox txtURL;
    private System.Windows.Forms.Label Label1;
  }
}


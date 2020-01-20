namespace IP
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
          this.txtResult = new System.Windows.Forms.TextBox();
          this.txtPort = new System.Windows.Forms.TextBox();
          this.txtIP = new System.Windows.Forms.TextBox();
          this.Label1 = new System.Windows.Forms.Label();
          this.Button1 = new System.Windows.Forms.Button();
          this.Label2 = new System.Windows.Forms.Label();
          this.SuspendLayout();
          // 
          // txtResult
          // 
          this.txtResult.Location = new System.Drawing.Point(11, 74);
          this.txtResult.Multiline = true;
          this.txtResult.Name = "txtResult";
          this.txtResult.Size = new System.Drawing.Size(256, 122);
          this.txtResult.TabIndex = 20;
          // 
          // txtPort
          // 
          this.txtPort.Location = new System.Drawing.Point(49, 42);
          this.txtPort.Name = "txtPort";
          this.txtPort.Size = new System.Drawing.Size(132, 22);
          this.txtPort.TabIndex = 18;
          // 
          // txtIP
          // 
          this.txtIP.Location = new System.Drawing.Point(49, 14);
          this.txtIP.Name = "txtIP";
          this.txtIP.Size = new System.Drawing.Size(132, 22);
          this.txtIP.TabIndex = 17;
          // 
          // Label1
          // 
          this.Label1.Location = new System.Drawing.Point(13, 18);
          this.Label1.Name = "Label1";
          this.Label1.Size = new System.Drawing.Size(72, 20);
          this.Label1.TabIndex = 21;
          this.Label1.Text = "IP:";
          // 
          // Button1
          // 
          this.Button1.Location = new System.Drawing.Point(189, 18);
          this.Button1.Name = "Button1";
          this.Button1.Size = new System.Drawing.Size(80, 28);
          this.Button1.TabIndex = 19;
          this.Button1.Text = "OK";
          this.Button1.Click += new System.EventHandler(this.Button1_Click);
          // 
          // Label2
          // 
          this.Label2.Location = new System.Drawing.Point(13, 46);
          this.Label2.Name = "Label2";
          this.Label2.Size = new System.Drawing.Size(56, 20);
          this.Label2.TabIndex = 22;
          this.Label2.Text = "Port:";
          // 
          // Form1
          // 
          this.AcceptButton = this.Button1;
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(280, 210);
          this.Controls.Add(this.txtResult);
          this.Controls.Add(this.txtPort);
          this.Controls.Add(this.txtIP);
          this.Controls.Add(this.Label1);
          this.Controls.Add(this.Button1);
          this.Controls.Add(this.Label2);
          this.MaximizeBox = false;
          this.Name = "Form1";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "IPEndPoint";
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtResult;
        internal System.Windows.Forms.TextBox txtPort;
        internal System.Windows.Forms.TextBox txtIP;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Label Label2;

    }
}


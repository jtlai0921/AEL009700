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
          this.Button1 = new System.Windows.Forms.Button();
          this.txtIP = new System.Windows.Forms.TextBox();
          this.Label1 = new System.Windows.Forms.Label();
          this.SuspendLayout();
          // 
          // Button1
          // 
          this.Button1.Location = new System.Drawing.Point(91, 59);
          this.Button1.Name = "Button1";
          this.Button1.Size = new System.Drawing.Size(82, 27);
          this.Button1.TabIndex = 14;
          this.Button1.Text = "Check";
          this.Button1.UseVisualStyleBackColor = true;
          this.Button1.Click += new System.EventHandler(this.Button1_Click);
          // 
          // txtIP
          // 
          this.txtIP.Location = new System.Drawing.Point(81, 18);
          this.txtIP.Name = "txtIP";
          this.txtIP.Size = new System.Drawing.Size(167, 22);
          this.txtIP.TabIndex = 23;
          // 
          // Label1
          // 
          this.Label1.Location = new System.Drawing.Point(17, 22);
          this.Label1.Name = "Label1";
          this.Label1.Size = new System.Drawing.Size(67, 20);
          this.Label1.TabIndex = 24;
          this.Label1.Text = "IP Address:";
          // 
          // Form1
          // 
          this.AcceptButton = this.Button1;
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(264, 105);
          this.Controls.Add(this.txtIP);
          this.Controls.Add(this.Label1);
          this.Controls.Add(this.Button1);
          this.MaximizeBox = false;
          this.Name = "Form1";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "IP Loopback";
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Button1;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label Label1;
    }
}


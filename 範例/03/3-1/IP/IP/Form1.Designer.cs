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
            this.txtIP2 = new System.Windows.Forms.TextBox();
            this.txtIP1 = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(90, 93);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(82, 27);
            this.Button1.TabIndex = 14;
            this.Button1.Text = "Check";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // txtIP2
            // 
            this.txtIP2.Location = new System.Drawing.Point(56, 52);
            this.txtIP2.Name = "txtIP2";
            this.txtIP2.Size = new System.Drawing.Size(186, 22);
            this.txtIP2.TabIndex = 13;
            // 
            // txtIP1
            // 
            this.txtIP1.Location = new System.Drawing.Point(56, 21);
            this.txtIP1.Name = "txtIP1";
            this.txtIP1.Size = new System.Drawing.Size(186, 22);
            this.txtIP1.TabIndex = 12;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(20, 55);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(27, 12);
            this.Label2.TabIndex = 11;
            this.Label2.Text = "IP 2:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(20, 25);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(27, 12);
            this.Label1.TabIndex = 10;
            this.Label1.Text = "IP 1:";
            // 
            // Form1
            // 
            this.AcceptButton = this.Button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 140);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.txtIP2);
            this.Controls.Add(this.txtIP1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.TextBox txtIP2;
        internal System.Windows.Forms.TextBox txtIP1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
    }
}


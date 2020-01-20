namespace WebBrowser
{
  partial class Form2
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
      this.txtSource = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // txtSource
      // 
      this.txtSource.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtSource.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
      this.txtSource.Location = new System.Drawing.Point(0, 0);
      this.txtSource.Multiline = true;
      this.txtSource.Name = "txtSource";
      this.txtSource.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtSource.Size = new System.Drawing.Size(384, 364);
      this.txtSource.TabIndex = 3;
      // 
      // Form2
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(384, 364);
      this.Controls.Add(this.txtSource);
      this.Name = "Form2";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Source Code";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.TextBox txtSource;
  }
}
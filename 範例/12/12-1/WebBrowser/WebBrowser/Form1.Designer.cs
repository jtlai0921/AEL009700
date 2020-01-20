namespace WebBrowser
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.ImageList1 = new System.Windows.Forms.ImageList();
      this.MainMenu1 = new System.Windows.Forms.MainMenu();
      this.mnuFile = new System.Windows.Forms.MenuItem();
      this.mnuCaption = new System.Windows.Forms.MenuItem();
      this.MenuItem1 = new System.Windows.Forms.MenuItem();
      this.mnuExit = new System.Windows.Forms.MenuItem();
      this.StatusBar1 = new System.Windows.Forms.StatusBar();
      this.StatusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
      this.ToolBar1 = new System.Windows.Forms.ToolBar();
      this.ToolBarBack = new System.Windows.Forms.ToolBarButton();
      this.ToolBarForward = new System.Windows.Forms.ToolBarButton();
      this.ToolBarStop = new System.Windows.Forms.ToolBarButton();
      this.ToolBarRefresh = new System.Windows.Forms.ToolBarButton();
      this.ToolBarHome = new System.Windows.Forms.ToolBarButton();
      this.ToolBarSearch = new System.Windows.Forms.ToolBarButton();
      this.Panel1 = new System.Windows.Forms.Panel();
      this.txtURL = new System.Windows.Forms.TextBox();
      this.Label1 = new System.Windows.Forms.Label();
      this.webBrowser = new System.Windows.Forms.WebBrowser();
      ((System.ComponentModel.ISupportInitialize)(this.StatusBarPanel1)).BeginInit();
      this.Panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // ImageList1
      // 
      this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
      this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.ImageList1.Images.SetKeyName(0, "back.gif");
      this.ImageList1.Images.SetKeyName(1, "fwd.gif");
      this.ImageList1.Images.SetKeyName(2, "stop.gif");
      this.ImageList1.Images.SetKeyName(3, "refresh.gif");
      this.ImageList1.Images.SetKeyName(4, "home.gif");
      this.ImageList1.Images.SetKeyName(5, "search.gif");
      // 
      // MainMenu1
      // 
      this.MainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile});
      // 
      // mnuFile
      // 
      this.mnuFile.Index = 0;
      this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuCaption,
            this.MenuItem1,
            this.mnuExit});
      this.mnuFile.Text = "&File";
      // 
      // mnuCaption
      // 
      this.mnuCaption.Checked = true;
      this.mnuCaption.Index = 0;
      this.mnuCaption.Text = "&Show Caption";
      this.mnuCaption.Click += new System.EventHandler(this.mnuCaption_Click);
      // 
      // MenuItem1
      // 
      this.MenuItem1.Index = 1;
      this.MenuItem1.Text = "-";
      // 
      // mnuExit
      // 
      this.mnuExit.Index = 2;
      this.mnuExit.Text = "E&xit";
      this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
      // 
      // StatusBar1
      // 
      this.StatusBar1.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.StatusBar1.Location = new System.Drawing.Point(0, 321);
      this.StatusBar1.Name = "StatusBar1";
      this.StatusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.StatusBarPanel1});
      this.StatusBar1.ShowPanels = true;
      this.StatusBar1.Size = new System.Drawing.Size(384, 22);
      this.StatusBar1.TabIndex = 10;
      this.StatusBar1.Text = "Status:";
      // 
      // StatusBarPanel1
      // 
      this.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
      this.StatusBarPanel1.Name = "StatusBarPanel1";
      this.StatusBarPanel1.Text = "Status: ";
      this.StatusBarPanel1.Width = 367;
      // 
      // ToolBar1
      // 
      this.ToolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
      this.ToolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.ToolBarBack,
            this.ToolBarForward,
            this.ToolBarStop,
            this.ToolBarRefresh,
            this.ToolBarHome,
            this.ToolBarSearch});
      this.ToolBar1.ButtonSize = new System.Drawing.Size(50, 50);
      this.ToolBar1.DropDownArrows = true;
      this.ToolBar1.ImageList = this.ImageList1;
      this.ToolBar1.Location = new System.Drawing.Point(0, 0);
      this.ToolBar1.Name = "ToolBar1";
      this.ToolBar1.ShowToolTips = true;
      this.ToolBar1.Size = new System.Drawing.Size(384, 41);
      this.ToolBar1.TabIndex = 11;
      this.ToolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.ToolBar1_ButtonClick);
      // 
      // ToolBarBack
      // 
      this.ToolBarBack.ImageIndex = 0;
      this.ToolBarBack.Name = "ToolBarBack";
      this.ToolBarBack.Text = "Back";
      this.ToolBarBack.ToolTipText = "Back";
      // 
      // ToolBarForward
      // 
      this.ToolBarForward.ImageIndex = 1;
      this.ToolBarForward.Name = "ToolBarForward";
      this.ToolBarForward.Text = "Forward";
      this.ToolBarForward.ToolTipText = "Forward";
      // 
      // ToolBarStop
      // 
      this.ToolBarStop.ImageIndex = 2;
      this.ToolBarStop.Name = "ToolBarStop";
      this.ToolBarStop.Text = "Stop";
      this.ToolBarStop.ToolTipText = "Stop";
      // 
      // ToolBarRefresh
      // 
      this.ToolBarRefresh.ImageIndex = 3;
      this.ToolBarRefresh.Name = "ToolBarRefresh";
      this.ToolBarRefresh.Text = "Refresh";
      this.ToolBarRefresh.ToolTipText = "Refresh";
      // 
      // ToolBarHome
      // 
      this.ToolBarHome.ImageIndex = 4;
      this.ToolBarHome.Name = "ToolBarHome";
      this.ToolBarHome.Text = "Home";
      this.ToolBarHome.ToolTipText = "Home";
      // 
      // ToolBarSearch
      // 
      this.ToolBarSearch.ImageIndex = 5;
      this.ToolBarSearch.Name = "ToolBarSearch";
      this.ToolBarSearch.Text = "Search";
      this.ToolBarSearch.ToolTipText = "Search";
      // 
      // Panel1
      // 
      this.Panel1.Controls.Add(this.txtURL);
      this.Panel1.Controls.Add(this.Label1);
      this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.Panel1.Location = new System.Drawing.Point(0, 41);
      this.Panel1.Name = "Panel1";
      this.Panel1.Size = new System.Drawing.Size(384, 30);
      this.Panel1.TabIndex = 12;
      // 
      // txtURL
      // 
      this.txtURL.Location = new System.Drawing.Point(44, 4);
      this.txtURL.Name = "txtURL";
      this.txtURL.Size = new System.Drawing.Size(336, 22);
      this.txtURL.TabIndex = 1;
      this.txtURL.WordWrap = false;
      this.txtURL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtURL_KeyPress);
      // 
      // Label1
      // 
      this.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.Label1.Location = new System.Drawing.Point(10, 10);
      this.Label1.Name = "Label1";
      this.Label1.Size = new System.Drawing.Size(40, 15);
      this.Label1.TabIndex = 0;
      this.Label1.Text = "URL:";
      this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // webBrowser
      // 
      this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
      this.webBrowser.Location = new System.Drawing.Point(0, 71);
      this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser.Name = "webBrowser";
      this.webBrowser.Size = new System.Drawing.Size(384, 250);
      this.webBrowser.TabIndex = 13;
      this.webBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser_Navigated);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(384, 343);
      this.Controls.Add(this.webBrowser);
      this.Controls.Add(this.Panel1);
      this.Controls.Add(this.ToolBar1);
      this.Controls.Add(this.StatusBar1);
      this.Menu = this.MainMenu1;
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Web Browser";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.Resize += new System.EventHandler(this.Form1_Resize);
      ((System.ComponentModel.ISupportInitialize)(this.StatusBarPanel1)).EndInit();
      this.Panel1.ResumeLayout(false);
      this.Panel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    internal System.Windows.Forms.ImageList ImageList1;
    internal System.Windows.Forms.MainMenu MainMenu1;
    internal System.Windows.Forms.MenuItem mnuFile;
    internal System.Windows.Forms.MenuItem mnuCaption;
    internal System.Windows.Forms.MenuItem MenuItem1;
    internal System.Windows.Forms.MenuItem mnuExit;
    internal System.Windows.Forms.StatusBar StatusBar1;
    internal System.Windows.Forms.StatusBarPanel StatusBarPanel1;
    internal System.Windows.Forms.ToolBar ToolBar1;
    internal System.Windows.Forms.ToolBarButton ToolBarBack;
    internal System.Windows.Forms.ToolBarButton ToolBarForward;
    internal System.Windows.Forms.ToolBarButton ToolBarStop;
    internal System.Windows.Forms.ToolBarButton ToolBarRefresh;
    internal System.Windows.Forms.ToolBarButton ToolBarHome;
    internal System.Windows.Forms.ToolBarButton ToolBarSearch;
    internal System.Windows.Forms.Panel Panel1;
    internal System.Windows.Forms.TextBox txtURL;
    internal System.Windows.Forms.Label Label1;
    internal System.Windows.Forms.WebBrowser webBrowser;
  }
}


namespace ChatGPTIntegration
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            connectToolStripMenuItem = new ToolStripMenuItem();
            databaseEngineToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            dbList = new MenuStrip();
            databasesMenu = new ToolStripMenuItem();
            tablesMenu = new ToolStripMenuItem();
            viewsMenu = new ToolStripMenuItem();
            proceduresMenu = new ToolStripMenuItem();
            panel2 = new Panel();
            btnOptimize = new Button();
            label5 = new Label();
            label4 = new Label();
            txtResultAnalytics = new RichTextBox();
            txtNonOptimize = new RichTextBox();
            suggessionList = new ComboBox();
            label3 = new Label();
            txtSelectedSuggession = new RichTextBox();
            label2 = new Label();
            label1 = new Label();
            btnApply = new Button();
            btn_Generate = new Button();
            txtGPToutPut = new RichTextBox();
            txtSQLOutput = new RichTextBox();
            loaderPanel = new Panel();
            Loder = new Label();
            loadingProgress = new ProgressBar();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            dbList.SuspendLayout();
            panel2.SuspendLayout();
            loaderPanel.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { connectToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1387, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // connectToolStripMenuItem
            // 
            connectToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { databaseEngineToolStripMenuItem });
            connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            connectToolStripMenuItem.Size = new Size(64, 20);
            connectToolStripMenuItem.Text = "Connect";
            // 
            // databaseEngineToolStripMenuItem
            // 
            databaseEngineToolStripMenuItem.Name = "databaseEngineToolStripMenuItem";
            databaseEngineToolStripMenuItem.Size = new Size(161, 22);
            databaseEngineToolStripMenuItem.Text = "Database Engine";
            databaseEngineToolStripMenuItem.Click += databaseEngineToolStripMenuItem_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(dbList);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 24);
            panel1.Name = "panel1";
            panel1.Size = new Size(113, 696);
            panel1.TabIndex = 1;
            // 
            // dbList
            // 
            dbList.AutoSize = false;
            dbList.Dock = DockStyle.Left;
            dbList.GripStyle = ToolStripGripStyle.Visible;
            dbList.Items.AddRange(new ToolStripItem[] { databasesMenu, tablesMenu, viewsMenu, proceduresMenu });
            dbList.Location = new Point(0, 0);
            dbList.Name = "dbList";
            dbList.Size = new Size(109, 692);
            dbList.TabIndex = 0;
            dbList.Text = "menuStrip2";
            // 
            // databasesMenu
            // 
            databasesMenu.Enabled = false;
            databasesMenu.Name = "databasesMenu";
            databasesMenu.Size = new Size(105, 19);
            databasesMenu.Text = "Databases";
            // 
            // tablesMenu
            // 
            tablesMenu.Enabled = false;
            tablesMenu.Name = "tablesMenu";
            tablesMenu.Size = new Size(105, 19);
            tablesMenu.Text = "Tables";
            // 
            // viewsMenu
            // 
            viewsMenu.Enabled = false;
            viewsMenu.Name = "viewsMenu";
            viewsMenu.Size = new Size(105, 19);
            viewsMenu.Text = "Views";
            // 
            // proceduresMenu
            // 
            proceduresMenu.Enabled = false;
            proceduresMenu.Name = "proceduresMenu";
            proceduresMenu.Size = new Size(105, 19);
            proceduresMenu.Text = "Procedures";
            // 
            // panel2
            // 
            panel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(loaderPanel);
            panel2.Controls.Add(btnOptimize);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(txtResultAnalytics);
            panel2.Controls.Add(txtNonOptimize);
            panel2.Controls.Add(suggessionList);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(txtSelectedSuggession);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(btnApply);
            panel2.Controls.Add(btn_Generate);
            panel2.Controls.Add(txtGPToutPut);
            panel2.Controls.Add(txtSQLOutput);
            panel2.Dock = DockStyle.Fill;
            panel2.ForeColor = Color.White;
            panel2.Location = new Point(113, 24);
            panel2.Name = "panel2";
            panel2.Size = new Size(1274, 696);
            panel2.TabIndex = 2;
            panel2.Paint += panel2_Paint;
            // 
            // btnOptimize
            // 
            btnOptimize.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOptimize.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnOptimize.BackColor = Color.LightSlateGray;
            btnOptimize.Enabled = false;
            btnOptimize.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnOptimize.ForeColor = Color.Transparent;
            btnOptimize.Location = new Point(914, 632);
            btnOptimize.Name = "btnOptimize";
            btnOptimize.Size = new Size(111, 40);
            btnOptimize.TabIndex = 14;
            btnOptimize.Text = "Optomize";
            btnOptimize.UseVisualStyleBackColor = false;
            btnOptimize.Click += btnOptimize_ClickAsync;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Transparent;
            label5.Location = new Point(1244, 366);
            label5.Name = "label5";
            label5.Size = new Size(160, 21);
            label5.TabIndex = 13;
            label5.Text = "Optimized Analytics";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Transparent;
            label4.Location = new Point(727, 363);
            label4.Name = "label4";
            label4.Size = new Size(193, 21);
            label4.TabIndex = 12;
            label4.Text = "Non Optrimize Analytics";
            // 
            // txtResultAnalytics
            // 
            txtResultAnalytics.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtResultAnalytics.Location = new Point(1244, 390);
            txtResultAnalytics.Name = "txtResultAnalytics";
            txtResultAnalytics.ReadOnly = true;
            txtResultAnalytics.Size = new Size(16, 236);
            txtResultAnalytics.TabIndex = 11;
            txtResultAnalytics.Text = "";
            // 
            // txtNonOptimize
            // 
            txtNonOptimize.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtNonOptimize.Location = new Point(727, 390);
            txtNonOptimize.Name = "txtNonOptimize";
            txtNonOptimize.ReadOnly = true;
            txtNonOptimize.Size = new Size(511, 292);
            txtNonOptimize.TabIndex = 10;
            txtNonOptimize.Text = "";
            // 
            // suggessionList
            // 
            suggessionList.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            suggessionList.FormattingEnabled = true;
            suggessionList.Location = new Point(125, 359);
            suggessionList.Name = "suggessionList";
            suggessionList.Size = new Size(586, 25);
            suggessionList.TabIndex = 9;
            suggessionList.SelectedIndexChanged += suggessionList_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Transparent;
            label3.Location = new Point(6, 363);
            label3.Name = "label3";
            label3.Size = new Size(100, 21);
            label3.TabIndex = 8;
            label3.Text = "Suggessions";
            // 
            // txtSelectedSuggession
            // 
            txtSelectedSuggession.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtSelectedSuggession.Location = new Point(6, 390);
            txtSelectedSuggession.Name = "txtSelectedSuggession";
            txtSelectedSuggession.ReadOnly = true;
            txtSelectedSuggession.Size = new Size(715, 292);
            txtSelectedSuggession.TabIndex = 7;
            txtSelectedSuggession.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Transparent;
            label2.Location = new Point(869, 22);
            label2.Name = "label2";
            label2.Size = new Size(56, 21);
            label2.TabIndex = 5;
            label2.Text = "Result";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Transparent;
            label1.Location = new Point(6, 22);
            label1.Name = "label1";
            label1.Size = new Size(88, 21);
            label1.TabIndex = 4;
            label1.Text = "SQL Query";
            // 
            // btnApply
            // 
            btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnApply.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnApply.BackColor = Color.LightSlateGray;
            btnApply.Enabled = false;
            btnApply.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnApply.ForeColor = Color.Transparent;
            btnApply.Location = new Point(1148, 632);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(111, 40);
            btnApply.TabIndex = 3;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Click += btnApply_Click;
            // 
            // btn_Generate
            // 
            btn_Generate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_Generate.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn_Generate.BackColor = Color.LightSlateGray;
            btn_Generate.Enabled = false;
            btn_Generate.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Generate.ForeColor = Color.Transparent;
            btn_Generate.Location = new Point(1031, 632);
            btn_Generate.Name = "btn_Generate";
            btn_Generate.Size = new Size(111, 40);
            btn_Generate.TabIndex = 2;
            btn_Generate.Text = "Analyze";
            btn_Generate.UseVisualStyleBackColor = false;
            btn_Generate.Click += btn_Generate_Click;
            // 
            // txtGPToutPut
            // 
            txtGPToutPut.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtGPToutPut.Location = new Point(869, 53);
            txtGPToutPut.Name = "txtGPToutPut";
            txtGPToutPut.ReadOnly = true;
            txtGPToutPut.Size = new Size(391, 296);
            txtGPToutPut.TabIndex = 1;
            txtGPToutPut.Text = "";
            // 
            // txtSQLOutput
            // 
            txtSQLOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            txtSQLOutput.Location = new Point(6, 53);
            txtSQLOutput.Name = "txtSQLOutput";
            txtSQLOutput.ReadOnly = true;
            txtSQLOutput.Size = new Size(857, 296);
            txtSQLOutput.TabIndex = 0;
            txtSQLOutput.Text = "";
            // 
            // loaderPanel
            // 
            loaderPanel.BackColor = SystemColors.AppWorkspace;
            loaderPanel.Controls.Add(loadingProgress);
            loaderPanel.Controls.Add(Loder);
            loaderPanel.Dock = DockStyle.Fill;
            loaderPanel.Location = new Point(0, 0);
            loaderPanel.Name = "loaderPanel";
            loaderPanel.Size = new Size(1270, 692);
            loaderPanel.TabIndex = 6;
            loaderPanel.Visible = false;
            // 
            // Loder
            // 
            Loder.Anchor = AnchorStyles.None;
            Loder.AutoSize = true;
            Loder.Font = new Font("Showcard Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Loder.Location = new Point(728, 302);
            Loder.Name = "Loder";
            Loder.Size = new Size(170, 30);
            Loder.TabIndex = 1;
            Loder.Text = "Loading........";
            // 
            // loadingProgress
            // 
            loadingProgress.Anchor = AnchorStyles.None;
            loadingProgress.Location = new Point(523, 345);
            loadingProgress.Name = "loadingProgress";
            loadingProgress.Size = new Size(560, 29);
            loadingProgress.Style = ProgressBarStyle.Marquee;
            loadingProgress.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1387, 720);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Db Optimizer";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            dbList.ResumeLayout(false);
            dbList.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            loaderPanel.ResumeLayout(false);
            loaderPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem connectToolStripMenuItem;
        private ToolStripMenuItem databaseEngineToolStripMenuItem;
        private Panel panel1;
        public MenuStrip dbList;
        public ToolStripMenuItem databasesMenu;
        private ToolStripMenuItem tablesMenu;
        private ToolStripMenuItem viewsMenu;
        private ToolStripMenuItem proceduresMenu;
        private Panel panel2;
        private RichTextBox txtSQLOutput;
        private RichTextBox txtGPToutPut;
        private Button btnApply;
        private Button btn_Generate;
        private Label label1;
        private Label label2;
        private Panel loaderPanel;
        private Label Loder;
        private ProgressBar loadingProgress;
        private Label label3;
        private RichTextBox txtSelectedSuggession;
        private ComboBox suggessionList;
        private RichTextBox txtNonOptimize;
        private Label label5;
        private Label label4;
        private RichTextBox txtResultAnalytics;
        private Button btnOptimize;
    }
}

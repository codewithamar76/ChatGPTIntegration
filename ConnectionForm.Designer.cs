namespace ChatGPTIntegration
{
    partial class ConnectionForm
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
            lbl_DBServerName = new Label();
            txt_dbServerName = new TextBox();
            lbl_Authentication = new Label();
            com_AuthList = new ComboBox();
            txt_userName = new TextBox();
            lbl_UserName = new Label();
            txt_pass = new TextBox();
            lbl_pass = new Label();
            btn_connect = new Button();
            btn_Close = new Button();
            lblinstanceName = new Label();
            comDBName = new ComboBox();
            SuspendLayout();
            // 
            // lbl_DBServerName
            // 
            lbl_DBServerName.AutoSize = true;
            lbl_DBServerName.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold | FontStyle.Italic);
            lbl_DBServerName.ForeColor = SystemColors.ButtonHighlight;
            lbl_DBServerName.Location = new Point(12, 83);
            lbl_DBServerName.Name = "lbl_DBServerName";
            lbl_DBServerName.Size = new Size(87, 17);
            lbl_DBServerName.TabIndex = 0;
            lbl_DBServerName.Text = "Server Name";
            // 
            // txt_dbServerName
            // 
            txt_dbServerName.Location = new Point(121, 83);
            txt_dbServerName.Name = "txt_dbServerName";
            txt_dbServerName.Size = new Size(263, 23);
            txt_dbServerName.TabIndex = 1;
            // 
            // lbl_Authentication
            // 
            lbl_Authentication.AutoSize = true;
            lbl_Authentication.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold | FontStyle.Italic);
            lbl_Authentication.ForeColor = SystemColors.ButtonHighlight;
            lbl_Authentication.Location = new Point(12, 128);
            lbl_Authentication.Name = "lbl_Authentication";
            lbl_Authentication.Size = new Size(100, 17);
            lbl_Authentication.TabIndex = 4;
            lbl_Authentication.Text = "Authentication";
            // 
            // com_AuthList
            // 
            com_AuthList.FormattingEnabled = true;
            com_AuthList.Location = new Point(121, 128);
            com_AuthList.Name = "com_AuthList";
            com_AuthList.Size = new Size(263, 23);
            com_AuthList.TabIndex = 5;
            com_AuthList.Text = "Select authentication type";
            com_AuthList.SelectedIndexChanged += com_AuthList_SelectedIndexChanged;
            // 
            // txt_userName
            // 
            txt_userName.Location = new Point(121, 216);
            txt_userName.Name = "txt_userName";
            txt_userName.Size = new Size(263, 23);
            txt_userName.TabIndex = 7;
            // 
            // lbl_UserName
            // 
            lbl_UserName.AutoSize = true;
            lbl_UserName.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold | FontStyle.Italic);
            lbl_UserName.ForeColor = SystemColors.ButtonHighlight;
            lbl_UserName.Location = new Point(12, 216);
            lbl_UserName.Name = "lbl_UserName";
            lbl_UserName.Size = new Size(76, 17);
            lbl_UserName.TabIndex = 6;
            lbl_UserName.Text = "User Name";
            // 
            // txt_pass
            // 
            txt_pass.Location = new Point(121, 265);
            txt_pass.Name = "txt_pass";
            txt_pass.PasswordChar = '*';
            txt_pass.Size = new Size(263, 23);
            txt_pass.TabIndex = 9;
            // 
            // lbl_pass
            // 
            lbl_pass.AutoSize = true;
            lbl_pass.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold | FontStyle.Italic);
            lbl_pass.ForeColor = SystemColors.ButtonHighlight;
            lbl_pass.Location = new Point(12, 265);
            lbl_pass.Name = "lbl_pass";
            lbl_pass.Size = new Size(66, 17);
            lbl_pass.TabIndex = 8;
            lbl_pass.Text = "Password";
            // 
            // btn_connect
            // 
            btn_connect.BackColor = Color.LightSlateGray;
            btn_connect.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btn_connect.ForeColor = SystemColors.ButtonHighlight;
            btn_connect.Location = new Point(181, 417);
            btn_connect.Name = "btn_connect";
            btn_connect.Size = new Size(86, 36);
            btn_connect.TabIndex = 10;
            btn_connect.Text = "Connect";
            btn_connect.UseVisualStyleBackColor = false;
            btn_connect.Click += btn_connect_Click;
            // 
            // btn_Close
            // 
            btn_Close.BackColor = Color.LightSlateGray;
            btn_Close.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btn_Close.ForeColor = SystemColors.ButtonHighlight;
            btn_Close.Location = new Point(298, 417);
            btn_Close.Name = "btn_Close";
            btn_Close.Size = new Size(86, 36);
            btn_Close.TabIndex = 11;
            btn_Close.Text = "Close";
            btn_Close.UseVisualStyleBackColor = false;
            btn_Close.Click += btn_Close_Click;
            // 
            // lblinstanceName
            // 
            lblinstanceName.AutoSize = true;
            lblinstanceName.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold | FontStyle.Italic);
            lblinstanceName.ForeColor = SystemColors.ButtonHighlight;
            lblinstanceName.Location = new Point(12, 176);
            lblinstanceName.Name = "lblinstanceName";
            lblinstanceName.Size = new Size(101, 17);
            lblinstanceName.TabIndex = 2;
            lblinstanceName.Text = "Instance Name";
            // 
            // comDBName
            // 
            comDBName.FormattingEnabled = true;
            comDBName.Location = new Point(121, 173);
            comDBName.Name = "comDBName";
            comDBName.Size = new Size(263, 23);
            comDBName.TabIndex = 3;
            comDBName.Text = "Select instance name";
            // 
            // ConnectionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(400, 465);
            ControlBox = false;
            Controls.Add(comDBName);
            Controls.Add(lblinstanceName);
            Controls.Add(btn_Close);
            Controls.Add(btn_connect);
            Controls.Add(txt_pass);
            Controls.Add(lbl_pass);
            Controls.Add(txt_userName);
            Controls.Add(lbl_UserName);
            Controls.Add(com_AuthList);
            Controls.Add(lbl_Authentication);
            Controls.Add(txt_dbServerName);
            Controls.Add(lbl_DBServerName);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConnectionForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Connection";
            Load += ConnectionForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_DBServerName;
        private TextBox txt_dbServerName;
        private Label lbl_Authentication;
        private ComboBox com_AuthList;
        private TextBox txt_userName;
        private Label lbl_UserName;
        private TextBox txt_pass;
        private Label lbl_pass;
        private Button btn_connect;
        private Button btn_Close;
        private Label lblinstanceName;
        private ComboBox comDBName;
    }
}
namespace polojenietoetejko
{
    partial class ConnectionDialogBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ContinueButton = new Button();
            CancelButton = new Button();
            usernameTextbox = new TextBox();
            connectionTextbox = new TextBox();
            connectionLabel = new Label();
            usernameLabel = new Label();
            cbdTabControl = new TabControl();
            connectionTP = new TabPage();
            serverListTabPage = new TabPage();
            serverListbox = new ListBox();
            errorProvider1 = new ErrorProvider(components);
            label1 = new Label();
            cbdTabControl.SuspendLayout();
            connectionTP.SuspendLayout();
            serverListTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // ContinueButton
            // 
            ContinueButton.DialogResult = DialogResult.OK;
            ContinueButton.Enabled = false;
            ContinueButton.Location = new Point(3, 291);
            ContinueButton.Name = "ContinueButton";
            ContinueButton.Size = new Size(73, 26);
            ContinueButton.TabIndex = 0;
            ContinueButton.Text = "Continue";
            ContinueButton.UseVisualStyleBackColor = true;
            ContinueButton.Click += ContinueButtonClick;
            // 
            // CancelButton
            // 
            CancelButton.DialogResult = DialogResult.Cancel;
            CancelButton.Location = new Point(222, 291);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 26);
            CancelButton.TabIndex = 1;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // usernameTextbox
            // 
            usernameTextbox.Location = new Point(6, 31);
            usernameTextbox.Name = "usernameTextbox";
            usernameTextbox.PlaceholderText = "Insert your username...";
            usernameTextbox.Size = new Size(254, 23);
            usernameTextbox.TabIndex = 2;
            usernameTextbox.TextChanged += usernameTextbox_TextChanged;
            // 
            // connectionTextbox
            // 
            connectionTextbox.Location = new Point(6, 170);
            connectionTextbox.Name = "connectionTextbox";
            connectionTextbox.PlaceholderText = "Address:Port | Example: 127.0.0.1:25565";
            connectionTextbox.Size = new Size(254, 23);
            connectionTextbox.TabIndex = 3;
            connectionTextbox.TextChanged += connectionTextbox_TextChanged;
            // 
            // connectionLabel
            // 
            connectionLabel.AutoSize = true;
            connectionLabel.Location = new Point(6, 152);
            connectionLabel.Name = "connectionLabel";
            connectionLabel.Size = new Size(82, 15);
            connectionLabel.TabIndex = 4;
            connectionLabel.Text = "Server IP/Port:";
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(6, 13);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(63, 15);
            usernameLabel.TabIndex = 5;
            usernameLabel.Text = "Username:";
            // 
            // cbdTabControl
            // 
            cbdTabControl.Controls.Add(connectionTP);
            cbdTabControl.Controls.Add(serverListTabPage);
            cbdTabControl.Location = new Point(3, 3);
            cbdTabControl.Name = "cbdTabControl";
            cbdTabControl.SelectedIndex = 0;
            cbdTabControl.Size = new Size(294, 282);
            cbdTabControl.TabIndex = 6;
            cbdTabControl.SelectedIndexChanged += cbdTabControl_SelectedIndexChanged;
            // 
            // connectionTP
            // 
            connectionTP.Controls.Add(label1);
            connectionTP.Controls.Add(connectionTextbox);
            connectionTP.Controls.Add(usernameLabel);
            connectionTP.Controls.Add(connectionLabel);
            connectionTP.Controls.Add(usernameTextbox);
            connectionTP.Location = new Point(4, 24);
            connectionTP.Name = "connectionTP";
            connectionTP.Padding = new Padding(3);
            connectionTP.Size = new Size(286, 254);
            connectionTP.TabIndex = 0;
            connectionTP.Text = "Manual Connection";
            connectionTP.UseVisualStyleBackColor = true;
            // 
            // serverListTabPage
            // 
            serverListTabPage.Controls.Add(serverListbox);
            serverListTabPage.Location = new Point(4, 24);
            serverListTabPage.Name = "serverListTabPage";
            serverListTabPage.Padding = new Padding(3);
            serverListTabPage.Size = new Size(286, 254);
            serverListTabPage.TabIndex = 1;
            serverListTabPage.Text = "Server List";
            serverListTabPage.UseVisualStyleBackColor = true;
            // 
            // serverListbox
            // 
            serverListbox.FormattingEnabled = true;
            serverListbox.Location = new Point(6, 6);
            serverListbox.Name = "serverListbox";
            serverListbox.Size = new Size(274, 244);
            serverListbox.TabIndex = 0;
            serverListbox.SelectedIndexChanged += serverListbox_SelectedIndexChanged;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 196);
            label1.Name = "label1";
            label1.Size = new Size(269, 45);
            label1.TabIndex = 6;
            label1.Text = "You can either provide an IP address and port\r\n that you know is hosting a server, \r\nor you can select a LAN server from the server list.";
            // 
            // ConnectionDialogBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(cbdTabControl);
            Controls.Add(CancelButton);
            Controls.Add(ContinueButton);
            Name = "ConnectionDialogBox";
            Size = new Size(300, 320);
            cbdTabControl.ResumeLayout(false);
            connectionTP.ResumeLayout(false);
            connectionTP.PerformLayout();
            serverListTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button ContinueButton;
        private Button CancelButton;
        private TextBox usernameTextbox;
        private TextBox connectionTextbox;
        private Label connectionLabel;
        private Label usernameLabel;
        private TabControl cbdTabControl;
        private TabPage connectionTP;
        private TabPage serverListTabPage;
        private ListBox serverListbox;
        private ErrorProvider errorProvider1;
        private Label label1;
    }
}

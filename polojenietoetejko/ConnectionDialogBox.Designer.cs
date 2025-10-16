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
            ContinueButton = new Button();
            CancelButton = new Button();
            usernameTextbox = new TextBox();
            connectionTextbox = new TextBox();
            connectionLabel = new Label();
            usernameLabel = new Label();
            SuspendLayout();
            // 
            // ContinueButton
            // 
            ContinueButton.DialogResult = DialogResult.OK;
            ContinueButton.Location = new Point(13, 182);
            ContinueButton.Name = "ContinueButton";
            ContinueButton.Size = new Size(75, 23);
            ContinueButton.TabIndex = 0;
            ContinueButton.Text = "Continue";
            ContinueButton.UseVisualStyleBackColor = true;
            ContinueButton.Click += ContinueButtonClick;
            // 
            // CancelButton
            // 
            CancelButton.DialogResult = DialogResult.Cancel;
            CancelButton.Location = new Point(157, 182);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 23);
            CancelButton.TabIndex = 1;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            // 
            // usernameTextbox
            // 
            usernameTextbox.Location = new Point(13, 57);
            usernameTextbox.Name = "usernameTextbox";
            usernameTextbox.Size = new Size(219, 23);
            usernameTextbox.TabIndex = 2;
            // 
            // connectionTextbox
            // 
            connectionTextbox.Location = new Point(13, 118);
            connectionTextbox.Name = "connectionTextbox";
            connectionTextbox.Size = new Size(219, 23);
            connectionTextbox.TabIndex = 3;
            // 
            // connectionLabel
            // 
            connectionLabel.AutoSize = true;
            connectionLabel.Location = new Point(13, 100);
            connectionLabel.Name = "connectionLabel";
            connectionLabel.Size = new Size(82, 15);
            connectionLabel.TabIndex = 4;
            connectionLabel.Text = "Server IP/Port:";
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(13, 39);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(63, 15);
            usernameLabel.TabIndex = 5;
            usernameLabel.Text = "Username:";
            // 
            // ConnectionDialogBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(usernameLabel);
            Controls.Add(connectionLabel);
            Controls.Add(connectionTextbox);
            Controls.Add(usernameTextbox);
            Controls.Add(CancelButton);
            Controls.Add(ContinueButton);
            Name = "ConnectionDialogBox";
            Size = new Size(240, 210);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ContinueButton;
        private Button CancelButton;
        private TextBox usernameTextbox;
        private TextBox connectionTextbox;
        private Label connectionLabel;
        private Label usernameLabel;
    }
}

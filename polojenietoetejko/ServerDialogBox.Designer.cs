namespace polojenietoetejko
{
    partial class ServerDialogBox
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
            serverTextbox = new TextBox();
            portTestbox = new TextBox();
            serverLabel = new Label();
            portLabel = new Label();
            SuspendLayout();
            // 
            // ContinueButton
            // 
            ContinueButton.DialogResult = DialogResult.OK;
            ContinueButton.Location = new Point(3, 184);
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
            CancelButton.Location = new Point(162, 184);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 23);
            CancelButton.TabIndex = 1;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            // 
            // serverTextbox
            // 
            serverTextbox.Location = new Point(64, 66);
            serverTextbox.Name = "serverTextbox";
            serverTextbox.Size = new Size(100, 23);
            serverTextbox.TabIndex = 2;
            // 
            // portTestbox
            // 
            portTestbox.Location = new Point(64, 135);
            portTestbox.Name = "portTestbox";
            portTestbox.Size = new Size(100, 23);
            portTestbox.TabIndex = 3;
            // 
            // serverLabel
            // 
            serverLabel.AutoSize = true;
            serverLabel.Location = new Point(64, 39);
            serverLabel.Name = "serverLabel";
            serverLabel.Size = new Size(55, 15);
            serverLabel.TabIndex = 4;
            serverLabel.Text = "Server IP:";
            // 
            // portLabel
            // 
            portLabel.AutoSize = true;
            portLabel.Location = new Point(64, 108);
            portLabel.Name = "portLabel";
            portLabel.Size = new Size(32, 15);
            portLabel.TabIndex = 5;
            portLabel.Text = "Port:";
            // 
            // ServerDialogBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(portLabel);
            Controls.Add(serverLabel);
            Controls.Add(portTestbox);
            Controls.Add(serverTextbox);
            Controls.Add(CancelButton);
            Controls.Add(ContinueButton);
            Name = "ServerDialogBox";
            Size = new Size(240, 210);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ContinueButton;
        private Button CancelButton;
        private TextBox serverTextbox;
        private TextBox portTestbox;
        private Label serverLabel;
        private Label portLabel;
    }
}

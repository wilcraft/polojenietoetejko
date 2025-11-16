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
            components = new System.ComponentModel.Container();
            ContinueButton = new Button();
            CancelButton = new Button();
            serverTextbox = new TextBox();
            portTestbox = new TextBox();
            serverLabel = new Label();
            portLabel = new Label();
            errorProvider1 = new ErrorProvider(components);
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // ContinueButton
            // 
            ContinueButton.DialogResult = DialogResult.OK;
            ContinueButton.Location = new Point(3, 294);
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
            CancelButton.Location = new Point(222, 294);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 23);
            CancelButton.TabIndex = 1;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            // 
            // serverTextbox
            // 
            serverTextbox.Location = new Point(81, 112);
            serverTextbox.Name = "serverTextbox";
            serverTextbox.PlaceholderText = "0.0.0.0";
            serverTextbox.Size = new Size(139, 23);
            serverTextbox.TabIndex = 2;
            serverTextbox.TextChanged += serverTextbox_TextChanged;
            // 
            // portTestbox
            // 
            portTestbox.Location = new Point(81, 174);
            portTestbox.MaxLength = 5;
            portTestbox.Name = "portTestbox";
            portTestbox.PlaceholderText = "25565 | Accepts: 1000-65565";
            portTestbox.Size = new Size(139, 23);
            portTestbox.TabIndex = 3;
            portTestbox.TextChanged += portTestbox_TextChanged;
            // 
            // serverLabel
            // 
            serverLabel.AutoSize = true;
            serverLabel.Location = new Point(81, 94);
            serverLabel.Name = "serverLabel";
            serverLabel.Size = new Size(55, 15);
            serverLabel.TabIndex = 4;
            serverLabel.Text = "Server IP:";
            // 
            // portLabel
            // 
            portLabel.AutoSize = true;
            portLabel.Location = new Point(81, 156);
            portLabel.Name = "portLabel";
            portLabel.Size = new Size(32, 15);
            portLabel.TabIndex = 5;
            portLabel.Text = "Port:";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 200);
            label1.Name = "label1";
            label1.Size = new Size(278, 30);
            label1.TabIndex = 6;
            label1.Text = "You can leave the fields empty as is, they default to \r\n\"0.0.0.0\" and port \"25565\".\r\n";
            // 
            // ServerDialogBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(portLabel);
            Controls.Add(serverLabel);
            Controls.Add(portTestbox);
            Controls.Add(serverTextbox);
            Controls.Add(CancelButton);
            Controls.Add(ContinueButton);
            Name = "ServerDialogBox";
            Size = new Size(300, 320);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
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
        private ErrorProvider errorProvider1;
        private Label label1;
    }
}

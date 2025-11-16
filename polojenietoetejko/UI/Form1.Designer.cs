namespace polojenietoetejko
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
            ConsoleButton = new Button();
            TabPagesController = new TabControl();
            homePage = new TabPage();
            button1 = new Button();
            tabPage2 = new TabPage();
            label5 = new Label();
            button2 = new Button();
            label4 = new Label();
            TabPagesController.SuspendLayout();
            homePage.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // ConsoleButton
            // 
            ConsoleButton.Location = new Point(656, 350);
            ConsoleButton.Name = "ConsoleButton";
            ConsoleButton.Size = new Size(75, 23);
            ConsoleButton.TabIndex = 4;
            ConsoleButton.Text = "Open";
            ConsoleButton.UseVisualStyleBackColor = true;
            ConsoleButton.Click += ConsoleButtonClick;
            // 
            // TabPagesController
            // 
            TabPagesController.Controls.Add(homePage);
            TabPagesController.Controls.Add(tabPage2);
            TabPagesController.Location = new Point(12, 21);
            TabPagesController.Name = "TabPagesController";
            TabPagesController.SelectedIndex = 0;
            TabPagesController.Size = new Size(776, 417);
            TabPagesController.TabIndex = 5;
            // 
            // homePage
            // 
            homePage.Controls.Add(button1);
            homePage.Location = new Point(4, 24);
            homePage.Name = "homePage";
            homePage.Padding = new Padding(3);
            homePage.Size = new Size(768, 389);
            homePage.TabIndex = 0;
            homePage.Text = "Home";
            homePage.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(300, 153);
            button1.Name = "button1";
            button1.Size = new Size(132, 61);
            button1.TabIndex = 9;
            button1.Text = "Connect";
            button1.UseVisualStyleBackColor = true;
            button1.Click += ButtonClick;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(button2);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(ConsoleButton);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(768, 389);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "DEBUG";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(656, 279);
            label5.Name = "label5";
            label5.Size = new Size(66, 15);
            label5.TabIndex = 7;
            label5.Text = "Start Server";
            // 
            // button2
            // 
            button2.Location = new Point(656, 297);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 6;
            button2.Text = "Start";
            button2.UseVisualStyleBackColor = true;
            button2.Click += ServerButtonClick;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(635, 332);
            label4.Name = "label4";
            label4.Size = new Size(116, 15);
            label4.TabIndex = 5;
            label4.Text = "Open/Close Console";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(TabPagesController);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chat App";
            TabPagesController.ResumeLayout(false);
            homePage.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button ConsoleButton;
        private TabControl TabPagesController;
        private TabPage homePage;
        private TabPage tabPage2;
        private Label label4;
        private Label label5;
        private Button button2;
        private Button button1;
        private TextBox messageBox;
        private RichTextBox chatHistoryRCTB;
        private TableLayoutPanel tableLayoutPanel1;
        private ListBox listBox1;
    }
}

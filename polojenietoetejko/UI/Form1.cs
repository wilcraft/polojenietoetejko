using Microsoft.VisualBasic.ApplicationServices;
using polojenietoetejko.Core;
using polojenietoetejko.Helper;
using polojenietoetejko.Miscellaneous;
using System.DirectoryServices.ActiveDirectory;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace polojenietoetejko
{
    public partial class Form1 : Form
    {
        ConnectionDialogBox cbd = new();
        ServerDialogBox sdb = new();
        Form testform = new();
        internal string username = string.Empty;
        internal string connectionAddress = string.Empty;
        private bool isConnected = false;
        private bool clientCreated = false;
        public Form1()
        {
            InitializeComponent();
            testform.FormBorderStyle = FormBorderStyle.FixedDialog;
            testform.MaximizeBox = false;
            testform.MinimizeBox = false;
            testform.StartPosition = FormStartPosition.CenterParent;
            testform.ClientSize = new Size(300, 320);
        }
        public async void ButtonClick(object sender, EventArgs e)
        {
            testform.Controls.Add(cbd);
            if (!isConnected && !clientCreated)
            {
                testform.ShowDialog();
                if (testform.DialogResult == DialogResult.OK)
                {
                    await Client.ConnectToServerAsync(cbd.ConnectionAddress, cbd.Username.Trim());
                    ClientHandler(cbd.ConnectionAddress);
                    TabPage tab = CreateTabChat(cbd.ConnectionAddress);
                    TabPagesController.Controls.Add(tab);
                    username = cbd.Username;
                    connectionAddress = cbd.ConnectionAddress;
                    isConnected = true;
                    clientCreated = true;
                    button1.Text = "Disconnect";
                    testform.Controls.Clear();
                    TabPagesController.SelectedTab = tab;
                }
                else
                {
                    testform.Controls.Remove(cbd);
                }
            }
            else if (isConnected && clientCreated)
            {
                await Client.Instance.SendMessageAsync("DISCONNECT");
                isConnected = false;
                button1.Text = "Reconnect";
                foreach (TabPage tab in TabPagesController.TabPages)
                {
                    if (tab.Text == connectionAddress)
                    {
                        var input = (ChatTabContextTags)tab.Tag;
                        input.Input.Enabled = false;
                    }
                }
            }
            else if (!isConnected && clientCreated)
            {
                await Client.ConnectToServerAsync(connectionAddress, username);
                ClientHandler(connectionAddress);
                button1.Text = "Disconnect";
                isConnected = true;
                foreach (TabPage tab in TabPagesController.TabPages)
                {
                    if (tab.Text == connectionAddress)
                    {
                        var input = (ChatTabContextTags)tab.Tag;
                        input.Input.Enabled = true;
                    }
                }
            }
        }
        public void ConsoleButtonClick(object sender, EventArgs e)
        {
            ConsoleButton.Text = Utilities.ConsoleLogger();
            this.Activate();
        }
        public void ServerButtonClick(object sender, EventArgs e)
        {
            testform.Controls.Add(sdb);
            testform.ShowDialog();
            if (testform.DialogResult == DialogResult.OK)
            {
                Server server = new Server($"{sdb.ServerAddress}:{sdb.Port}", this);
                server.CreateServer();
                testform.Controls.Clear();
            }
            else
            {
                testform.Controls.Remove(sdb);
            }
        }
        public async void EnterKeydown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (isConnected)
                {
                    var input = sender as TextBox;
                    await Client.Instance.SendMessageAsync(input.Text);
                    input.Clear();
                }
                else
                {
                    return;
                }
            }
        }
        private void ClientHandler(string chatTitle)
        {
            Client.Instance.MessageReceived += text =>
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() => UpdateChatBox(text + "\n", chatTitle)));
                }
                else
                {
                    UpdateChatBox(text + "\n", chatTitle);
                }
            };
            Client.Instance.UserListReceived += users =>
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() => UpdateUserList(users, chatTitle)));
                }
                else
                {
                    UpdateUserList(users, chatTitle);
                }
            };
            Client.Instance.ClientDisconnected += () =>
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() => UpdateUserList(chatTitle, "DELETE")));
                }
                else
                {
                    UpdateUserList(chatTitle, "DELETE");
                }
            };
        }
        public void UpdateChatBox(string message, string chatTitle)
        {
            foreach(TabPage tab in TabPagesController.TabPages)
            {
                if(tab.Text == chatTitle)
                {
                    var chatBox = (ChatTabContextTags)tab.Tag;
                    ChatFormatter.AppendFormattedText(chatBox.ChatBox, message);
                    //chatBox.ChatBox.AppendText(message);
                }
            }
        }
        public void UpdateUserList(String[] users, string chatTitle)
        {
            foreach (TabPage tab in TabPagesController.TabPages)
            {
                if (tab.Text == chatTitle)
                {
                    var userList = (ChatTabContextTags)tab.Tag;
                    userList.UserList.Items.Clear();
                    foreach (string user in users)
                    {
                        userList.UserList.Items.Add(user);
                    }
                }
            }
        }
        public void UpdateUserList(string chatTitle, string command)
        {
            foreach (TabPage tab in TabPagesController.TabPages)
            {
                if (tab.Text == chatTitle && command.Equals("DELETE"))
                {
                    var userList = (ChatTabContextTags)tab.Tag;
                    userList.UserList.Items.Clear();
                }
            }
        }
        
        public TabPage CreateTabChat(string ipaddress)
        {
            TabPage tab = new TabPage($"{ipaddress}");

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tab.Controls.Add(layout);

            RichTextBox ChatHistoryTB = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true
            };
            ListBox usersList = new ListBox
            {
                Dock = DockStyle.Fill
            };
            TextBox InputTB = new TextBox
            {
                Dock = DockStyle.Fill
            };
            layout.Controls.Add(ChatHistoryTB, 0, 0);
            layout.Controls.Add(usersList, 1, 0);
            layout.Controls.Add(InputTB, 0, 1);
            layout.SetColumnSpan(InputTB, 2);

            InputTB.KeyDown += EnterKeydown;

            tab.Tag = new ChatTabContextTags()
            {
                ChatBox = ChatHistoryTB,
                Input = InputTB,
                UserList = usersList
            };
            return tab;
        }
    }
}

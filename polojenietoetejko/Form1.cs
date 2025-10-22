using System.Runtime.InteropServices;

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
            testform.ClientSize = new Size(240, 210);
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
                    username = cbd.Username;
                    connectionAddress = cbd.ConnectionAddress;
                    isConnected = true;
                    clientCreated = true;
                    button1.Text = "Disconnect";
                    testform.Controls.Clear();
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
            }
            else if (!isConnected && clientCreated)
            {
                await Client.ConnectToServerAsync(connectionAddress, username);
                button1.Text = "Disconnect";
                isConnected = true;
            }
                
        }
        public void ConsoleButtonClick(object sender, EventArgs e)
        {
            ConsoleButton.Text = Utilities.ConsoleLogger();
        }
        public void ServerButtonClick(object sender, EventArgs e)
        {
            testform.Controls.Add(sdb);
            testform.ShowDialog();
            if (testform.DialogResult == DialogResult.OK)
            {
                Server server = new Server(sdb.ServerAddress, this);
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
                    await Client.Instance.SendMessageAsync(messageBox.Text);
                }
                else
                {
                    return;
                }
                e.Handled = true;
            }
        }
        public void UpdateChatBox(string message)
        {
            if (chatHistoryRCTB.InvokeRequired)
            {
                chatHistoryRCTB.BeginInvoke(new Action(() => chatHistoryRCTB.Text += message + "\n"));
            }
            else
            {
                chatHistoryRCTB.Text += message;
            }
        }
    }
}

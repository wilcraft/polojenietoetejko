using System.Runtime.InteropServices;

namespace polojenietoetejko
{
    public partial class Form1 : Form
    {
        ConnectionDialogBox cbd = new();
        ServerDialogBox sdb = new();
        Form testform = new();
        public Form1()
        {
            InitializeComponent();
            testform.FormBorderStyle = FormBorderStyle.FixedDialog;
            testform.MaximizeBox = false;
            testform.MinimizeBox = false;
            testform.ClientSize = new Size(240,210);
        }
        public async void ButtonClick(object sender, EventArgs e)
        {
            testform.Controls.Add(cbd);
            testform.ShowDialog();
            if (testform.DialogResult == DialogResult.OK)
            {
                await ClientLogic.ConnectToServeraAsync(cbd.ConnectionAddress, cbd.Port, cbd.Username);
                testform.Controls.Clear();
            }
        }
        public void ConsoleButtonClick(object sender, EventArgs e)
        {
            ConsoleButton.Text = Utilities.ConsoleLogger();
        }
        public async void ServerButtonClick(object sender, EventArgs e)
        {
            testform.Controls.Add(sdb);
            testform.ShowDialog();
            if(testform.DialogResult == DialogResult.OK)
            {
                await ServerLogic.CreateServer(sdb.ServerAddress, sdb.Port);
                testform.Controls.Clear();
            }
        }
        public async void EnterKeydown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                await ClientLogic.SendMessageAsync(messageBox.Text);
                e.Handled = true;
            }
        }
    }
}

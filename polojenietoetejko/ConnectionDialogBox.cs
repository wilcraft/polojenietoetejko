using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace polojenietoetejko
{
    public partial class ConnectionDialogBox : UserControl
    {
        string username;
        string connectionAddress;
        string port;
        public ConnectionDialogBox()
        {
            InitializeComponent();
            this.VisibleChanged += ConnectionDialogBox_VisibleChanged;

        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Username
        { get => username; set => username = value; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ConnectionAddress
        { get => connectionAddress; set => connectionAddress = value; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Port
        { get => port; set => port = value; }
        public void ContinueButtonClick(object sender, EventArgs e)
        {
            username = usernameTextbox.Text;
            string temp = connectionTextbox.Text;

            connectionAddress = connectionTextbox.Text;
            port = temp.Split(":").Last();

            usernameTextbox.Text = string.Empty;
            connectionTextbox.Text = string.Empty;
        }
        private async void ConnectionDialogBox_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void serverListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (serverListbox.SelectedItem != null)
            {
                connectionTextbox.Text = serverListbox.SelectedItem.ToString();
                connectionTextbox.ReadOnly = true;
            }
        }

        private async void cbdTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbdTabControl.SelectedTab == serverListTabPage)
            {
                Console.WriteLine("Hello!");
                List<string> serverList = await ServerDiscovery.ClientDisocverServersAsync();
                serverListbox.Items.Clear();
                foreach (string server in serverList)
                {
                    Console.WriteLine(server);
                    serverListbox.Items.Add(server);
                }
            }
        }
    }
}

using polojenietoetejko.Core;
using polojenietoetejko.Helper;
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
            usernameTextbox.Tag = false;
            connectionTextbox.Tag = false;
            errorProvider1.SetIconAlignment(usernameTextbox, ErrorIconAlignment.MiddleRight);
            errorProvider1.SetIconPadding(usernameTextbox, -25);
            errorProvider1.SetIconAlignment(connectionTextbox, ErrorIconAlignment.MiddleRight);
            errorProvider1.SetIconPadding(connectionTextbox, -25);
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

            usernameTextbox.Clear();
            connectionTextbox.Clear();
        }


        private void serverListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (serverListbox.SelectedItem != null)
            {
                connectionTextbox.Text = serverListbox.SelectedItem.ToString();
                connectionTextbox.ReadOnly = true;
                connectionTextbox.Tag = true;
            }
        }

        private async void cbdTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbdTabControl.SelectedTab == serverListTabPage)
            {
                List<string> serverList = await ServerDiscovery.ClientDisocverServersAsync();
                serverListbox.Items.Clear();
                foreach (string server in serverList)
                {
                    Console.WriteLine(server);
                    serverListbox.Items.Add(server);
                }
            }
        }

        private void usernameTextbox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Trim().Length < 4 || tb.Text.Trim().Length > 20)
            {
                tb.Tag = false;
                errorProvider1.SetError(tb, "Username cannot be shorter than 4 letters or longer than 20!");
            }
            else
            {
                tb.Tag = true;
                errorProvider1.SetError(tb, "");
            }
            ValidateButtonChange();
        }

        private void connectionTextbox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (Validation.isAddressValid(tb.Text.Trim()))
            {
                tb.Tag = true;
                errorProvider1.SetError(tb, "");
            }
            else
            {
                tb.Tag = false;
                errorProvider1.SetError(tb, "Ip address must follow the convention! 0-255.0-255.0-255.0-255!");
            }
            ValidateButtonChange();
        }
        private void ValidateButtonChange()
        {
            ContinueButton.Enabled = (
                (bool)connectionTextbox.Tag
                && (bool)usernameTextbox.Tag
            );
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            usernameTextbox.Clear();
            connectionTextbox.Clear();
            errorProvider1.Clear();
        }
    }
}

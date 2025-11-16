using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using polojenietoetejko.Core;


namespace polojenietoetejko
{
    public partial class ServerDialogBox : UserControl
    {
        string serverAddress;
        string port;
        public ServerDialogBox()
        {
            InitializeComponent();
            serverTextbox.Tag = true;
            portTestbox.Tag = true;
            errorProvider1.SetIconAlignment(serverTextbox, ErrorIconAlignment.MiddleRight);
            errorProvider1.SetIconPadding(serverTextbox, -25);
            errorProvider1.SetIconAlignment(portTestbox, ErrorIconAlignment.MiddleRight);
            errorProvider1.SetIconPadding(portTestbox, -25);
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Port { get => port; set => port = value; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ServerAddress
        { get => serverAddress; set => serverAddress = value; }
        public void ContinueButtonClick(object sender, EventArgs e)
        {
            port = portTestbox.Text;
            serverAddress = serverTextbox.Text;
            portTestbox.Text = string.Empty;
            serverTextbox.Text = string.Empty;
        }

        private void serverTextbox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (Validation.isAddressValid(tb.Text.Trim()))
            {
                tb.Tag = true;
                errorProvider1.SetError(tb, "");
            }else if (string.IsNullOrEmpty(tb.Text.Trim()))
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
                (bool)serverTextbox.Tag
                && (bool)portTestbox.Tag
            );
        }

        private void portTestbox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (Validation.IsPortValid(tb.Text.Trim()))
            {
                tb.Tag = true;
                errorProvider1.SetError(tb, "");
            }else if (string.IsNullOrEmpty(tb.Text.Trim()))
            {
                tb.Tag = true;
                errorProvider1.SetError(tb, "");
            }
            else
            {
                tb.Tag = false;
                errorProvider1.SetError(tb, "Port must be a number in the range between 1000-65565!");
            }
            ValidateButtonChange();
        }
    }
}

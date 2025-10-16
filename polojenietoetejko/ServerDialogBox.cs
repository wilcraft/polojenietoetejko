using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace polojenietoetejko
{
    public partial class ServerDialogBox : UserControl
    {
        string serverAddress;
        string port;
        public ServerDialogBox()
        {
            InitializeComponent();
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
    }
}

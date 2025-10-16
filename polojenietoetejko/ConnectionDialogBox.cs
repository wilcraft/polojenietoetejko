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

            connectionAddress = temp.Split(':').First();
            port = temp.Split(":").Last();

            usernameTextbox.Text = string.Empty;
            connectionTextbox.Text = string.Empty;
        }
    }
}

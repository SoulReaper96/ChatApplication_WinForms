using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_Application_WinForms
{
    public partial class AddUserForm : Form
    {
        public string? _userName { get; private set; }

        public AddUserForm()
        {
            InitializeComponent();
        }

        private void AddUser_btn_Click(object sender, EventArgs e)
        {
            _userName = Username_tb.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

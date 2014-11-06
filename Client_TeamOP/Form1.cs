using Client_TeamOP.Klassen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_TeamOP
{
    public partial class Form1 : Form
    {
        private Backend backend;
        public Form1()
        {
            InitializeComponent();
            backend = new Backend();

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected_1(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void sendenB_Click(object sender, EventArgs e)
        {

        }
    }
}

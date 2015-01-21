using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client_TeamOP.Klassen;

namespace Client_TeamOP
{
    public partial class Dragonfight : Form
    {
        IBackend backend;
        public Dragonfight(IBackend backend)
        {
            this.backend = backend;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backend.sendDecision(button1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            backend.sendDecision(button2.Text);
        }
    }
}

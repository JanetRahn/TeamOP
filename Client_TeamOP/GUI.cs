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
    public partial class GUI : Form
    {
        private IBackend backend;    
        public GUI(IBackend backend) : base()
        {
            if(backend != null) {
            this.backend = backend;
            InitializeComponent(); 
            }
            
                
        }

      

        public bool refreshGui() {
            return false;
        }
    }
}

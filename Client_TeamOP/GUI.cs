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


        protected void drawMapTile(Graphics g, IMap tile, int absX, int absY, int width, int height) {
            Color colour = Color.BurlyWood;
            if(tile.isForest()) {
                if(tile.isHuntable()) {
                    colour = Color.YellowGreen;
                } else {
                    colour = Color.Green;
                }
            } else if(tile.isWater()) {
                colour = Color.Blue;
            } else if(!tile.isWalkable()) {
                colour = Color.DimGray;
            }
            g.FillRectangle(new SolidBrush(colour), absX, absY, width, height);
            g.DrawRectangle(new Pen(new SolidBrush(Color.Black)), new Rectangle(absX, absY, width, height));
        }

      

        public bool refreshGui() {
            return false;
        }
    }
}

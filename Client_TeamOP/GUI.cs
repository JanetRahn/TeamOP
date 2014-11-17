using Client_TeamOP.Klassen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        protected void drawMapTile(Graphics g, IMap map, int absX, int absY, int width, int height)
        {
            Color colour = Color.BurlyWood;
            if (map.isForest())
            {
                if (map.isHuntable())
                {
                    colour = Color.YellowGreen;
                }
                else
                {
                    colour = Color.Green;
                }
            }
            else if (map.isWater())
            {
                colour = Color.Blue;
            }
            else if (!map.isWalkable())
            {
                colour = Color.DimGray;
            }
            g.FillRectangle(new SolidBrush(colour), absX, absY, width, height);
            g.DrawRectangle(new Pen(new SolidBrush(Color.Black)), new Rectangle(absX, absY, width, height));
        }


        protected Size getTileSize()
        {
            IPositionable[][] cells = this.backend.getMap();
            if (cells == null)
            {
                throw new ArgumentNullException("backend returned null as map");
            }
            if (cells.Length == 0)
            {
                throw new IndexOutOfRangeException("outer dimension of the retrieved map has length 0");
            }
            if (cells[0].Length == 0)
            {
                throw new IndexOutOfRangeException("inner dimension of the retrieved map has length 0");
            }
            int cellWidth = this.MapWindow.Size.Width / cells.Length;
            int cellHeight = this.MapWindow.Size.Height / cells[0].Length;
            Console.Write(cellWidth + " +  " + cellHeight);
            return new Size(cellWidth, cellHeight);
        }

        public bool refreshGui() {
           
           //getMap()
            //getPlayer()
            //getDragon()
            //getLog()
                return false;
        }  

        private void MapWindow_Paint_1(object sender, PaintEventArgs e)
        {
            Size tileSize = this.getTileSize();
            IMap[][] cells = this.backend.getMap();
            // validity checked beforehand in getTileSize
            Debug.Assert(cells != null);
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(this.MapWindow.CreateGraphics(), this.MapWindow.DisplayRectangle);
            Graphics g = this.CreateGraphics();
            for (int x = 0; x < cells.Length; x++)
            {
                for (int y = 0; y < cells[x].Length; y++)
                {
                    this.drawMapTile(buffer.Graphics, cells[x][y], x * tileSize.Width, y * tileSize.Height, tileSize.Width, tileSize.Height);
                }
            }
            buffer.Render();
        }

        private void LEFT_Click(object sender, EventArgs e)
        {
            this.backend.sendCommand("ask:mv:lft");
        }

        private void RIGHT_Click(object sender, EventArgs e)
        {
            this.backend.sendCommand("ask:mv:rgt");
        }

        private void UP_Click(object sender, EventArgs e)
        {
            this.backend.sendCommand("ask:mv:up");
        }

        private void DOWN_Click(object sender, EventArgs e)
        {
          this.backend.sendCommand("ask:mv:dwn");
        }

    
    }
}

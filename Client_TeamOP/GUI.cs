using Client_TeamOP.Klassen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_TeamOP
{
    public partial class GUI : Form
    {
        private IBackend backend;
        public delegate void refresh();
        public delegate void openPop();
        public refresh myDelegate;
        public openPop myDelegatePopUp;

        
        public GUI() : base()
        {
            
            this.backend = new Backend(this);
            InitializeComponent();
            
            //AllocConsole();
           
            this.MapWindow.Paint += MapWindow_Paint_1;
            this.MapWindow.Paint += board_PaintEntities;
            this.ChatInput.KeyPress += chatInput_KeyPress;
            this.MapWindow.KeyPress += map_KeyPress;           

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            myDelegate = new refresh(refreshGui);
            myDelegatePopUp = new openPop(openPopUp);
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
            IMap[,] cells = this.backend.getMap();
            if (cells == null)
            {
                throw new ArgumentNullException("backend returned null as map");
            }
            if (cells.GetLength(0) == 0)
            {
                throw new IndexOutOfRangeException("outer dimension of the retrieved map has length 0");
            }
            if (cells.GetLength(1) == 0)
            {
                throw new IndexOutOfRangeException("inner dimension of the retrieved map has length 0");
            }
            int cellWidth = this.MapWindow.Size.Width / cells.GetLength(0);
            int cellHeight = this.MapWindow.Size.Height / cells.GetLength(1);
            return new Size(cellWidth, cellHeight);
        }

        public void refreshGui() {
            this.MapWindow_Paint_1(null, null);
            List<String> log = this.backend.getLog();
            ChatWindow.Clear();
            foreach (String tmp in log)
            {
                ChatWindow.AppendText(tmp + "\r\n");
            }
            ChatWindow.ScrollToCaret();
            Refresh();       
            
            //return true;
        }  

        private void MapWindow_Paint_1(object sender, PaintEventArgs e)
        {
            Size tileSize = this.getTileSize();
            IMap[,] cells = this.backend.getMap();
            // validity checked beforehand in getTileSize
            Debug.Assert(cells != null);
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(this.MapWindow.CreateGraphics(), this.MapWindow.DisplayRectangle);
            Graphics g = this.CreateGraphics();
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    this.drawMapTile(buffer.Graphics, cells[x, y], x * tileSize.Width, y * tileSize.Height, tileSize.Width, tileSize.Height);
                }
            }
            buffer.Render();
        }

        private void chatInput_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (this.ChatInput.TextLength > 0)
                    {
                        string input = this.ChatInput.Text.Trim();
                        this.backend.sendCommand(input);
                        ChatWindow.ScrollToCaret();
                        this.ChatInput.Clear();
                        this.MapWindow.Focus();
                    }
                }
        }

        private void map_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            // fall-through-cases for capital letters
            switch (e.KeyChar)
            {
                case (char)Keys.Enter:
                    this.ChatInput.Focus();
                    break;
                case 'a':
                case 'A':
                    this.backend.moveLeft();
                    break;
                case 'd':
                case 'D':
                    this.backend.moveRight();
                    break;
                case 'w':
                case 'W':
                    this.backend.moveUp();
                    break;
                case 's':
                case 'S':
                    this.backend.moveDown();
                    break;                
            }
        }

        private void board_PaintEntities(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            List<IPositionable> dragons = this.backend.getPositionableDragon();
            foreach (IPositionable dragon in dragons)
            {
                this.drawDragon(e.Graphics, dragon);
            }
            List<IPositionable> players = this.backend.getPositionableHumans();
            foreach (IPositionable player in players)
            {
                this.drawPlayer(e.Graphics, player);
            }
        }

        protected void drawPlayer(Graphics g, IPositionable player)
        {
            Size tileSize = this.getTileSize();
            List<IPositionable> p = backend.getPositionableHumans();
            foreach (IPositionable tmp in p)
            {
                g.FillRectangle(new SolidBrush(Color.Black),
             tmp.getX() * tileSize.Width + tileSize.Width / 2 - tileSize.Width / 4,
             tmp.getY() * tileSize.Height + tileSize.Height / 2 - tileSize.Height / 4,
             tileSize.Width / 2,
             tileSize.Height / 2);
            }
        }

        protected void drawDragon(Graphics g, IPositionable dragon)
        {
            Size tileSize = this.getTileSize();
            List<IPositionable> p = backend.getPositionableDragon();
            foreach (IPositionable tmp in p)
            {
                g.FillRectangle(new SolidBrush(Color.DarkGoldenrod),
               tmp.getX() * tileSize.Width + tileSize.Width / 2 - tileSize.Width / 4,
               tmp.getY() * tileSize.Height + tileSize.Height / 2 - tileSize.Height / 4,
               tileSize.Width / 2,
               tileSize.Height / 2);
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void MapWindow_Click(object sender, EventArgs e)
        {
            //MouseEventArgs mouse = (MouseEventArgs)e;
            //Point pos = this.PointToClient(new Point(mouse.X, mouse.Y));
            this.MapWindow.Focus();
            //MessageBox.Show("" + pos.ToString());
            Size pos = getFieldFromPixel(((Panel)sender).PointToClient(MousePosition).X, ((Panel)sender).PointToClient(MousePosition).Y);
            backend.autowalkGotoField(pos.Width, pos.Height);            
        }

        private Size getFieldFromPixel(float x, float y) 
        {
         
            float xFieldPos, yFieldPos;
            xFieldPos = (x / MapWindow.Size.Width) * 20;
            yFieldPos = (y / MapWindow.Size.Height) * 20;

            return new Size((int)xFieldPos,(int)yFieldPos);
        }

        private void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            backend.exit();            
        }

        public void openPopUp()
        {
            String type = backend.getMinigameType();

            if (type.Equals("SKIRMISH"))
            {
                Skirmish minigameSkrimish = new Skirmish(backend);
                minigameSkrimish.Show();
            }
            if (type.Equals("DRAGON"))
            {
                Dragonfight minigameDragonfight = new Dragonfight(backend);
                minigameDragonfight.Show();
            }
            if (type.Equals("STAGHUNT"))
            {
                Staghunt minigameStaghunt = new Staghunt(backend);
                minigameStaghunt.Show();
            }
            
            
        }
    }
}

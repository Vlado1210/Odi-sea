using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;

namespace TILED_BASED
{
    public partial class MAIN : Form
    {
        Map map;
        
        private Bitmap img, buf, cat, wood, wood02;
        
        private static Rectangle src, dst;
        static int X = 0;
        int Y = 0;
        int increment;
        Graphics gBuf, gImg;

        public MAIN()
        {
            InitializeComponent();      
        }

        private void MAIN_SizeChanged(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            map = new Map();

            cat = new Bitmap("cato.png");
            wood = new Bitmap("wood.jpg");
            wood02 = new Bitmap("wood02.jpg");
            increment = 12;

            buf = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);  
            gBuf = Graphics.FromImage(buf);

            //generate the image
            GenerateIMG();

            dst = new Rectangle(0, 0, buf.Width, buf.Height);
            src = new Rectangle(0, 0, buf.Width, buf.Height);

            gBuf.DrawImageUnscaledAndClipped(img, new Rectangle(0, 0, img.Width, img.Height));
        }

        private void GenerateIMG()
        {
            // map properties
            int w = 256;// width
            int h = 22;// height
            int u = 45;//unit
            char v;

            img = new Bitmap(w * u, h * u);
            gImg = Graphics.FromImage(img);

            this.Text = img.Width.ToString();

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    v = map.map[(y * w) + x];
                    if (v == '.')
                        gImg.FillRectangle(Brushes.Tomato, x * u, y * u, u, u);
                    if (v == '*')
                        gImg.DrawImage(wood02, x * u, y * u, u, u);
                    if (v == '#')
                        gImg.DrawImage(wood, x * u, y * u, u, u);
                }
            }
        }

        private void CANVAS_Action(int X)
        {   
            src.X = X;
            PCT_CANVAS.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (img == null)
                return;

            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (X <= 0)
                        X = 25;
                    X -= increment;
                    CANVAS_Action(X);
                    break;
                case Keys.Right:
                    X += increment;
                    CANVAS_Action(X);
                    break;
            } 
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (img != null)
            {
            }
        }

        private void PCT_CANVAS_Paint(object sender, PaintEventArgs e)
        {
            if (img != null)
            {
                e.Graphics.PixelOffsetMode  = PixelOffsetMode.HighSpeed;
                e.Graphics.SmoothingMode    = SmoothingMode.HighSpeed;

                e.Graphics.DrawImage(img, dst, src, GraphicsUnit.Pixel);
                e.Graphics.DrawImage(cat, PCT_CANVAS.Width/2, 700);
            }
        }

    }
}

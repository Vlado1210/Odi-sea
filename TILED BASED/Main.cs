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
using System.IO;
using TILED_BASED;


namespace TILED_BASED
{
    public partial class MAIN : Form
    {
        Map map;

        static CResolution ChangeRes;
        static Screen Srn;

        private Bitmap img, cat, wood, wood02,black, pinkbush,tree,fence,rocks,water;
        private Bitmap sprite, coin, spriteTrim,gifImage;
        private static Rectangle src, dst, sprD,sprS;
        static int X = 0;
        int Y = 0;
        int increment;
        Graphics gImg;
        int tempHeight, tempWidth;
        bool changed,full, right, hold, up;
        bool hit = false;
        int counter = 0;
        int countery = 0;
        Random rnd = new Random();
        private Point coloriz;
               

        int motion1 = 2;
        int motion2 = 4;
        int motion3 = 8;
        int motion4 = 16;

        int width =  1280;
        int height = 720;

        Bitmap layer0, layer1, layer2, layer3;

        int l1_x1, l1_x2, l2_x1, l2_x2, l3_x1, l3_x2;
        int l1_y1, l1_y2, l2_y1, l2_y2, l3_y1, l3_y2;
        static Graphics g;
        
        
       
         private void BackgroundMove()
        {
            if (l2_x1 < -width) { l2_x1 = width - motion2; }
            { l2_x1 -= motion2; l2_x2 -= motion2; }
            if (l2_x2 < -width) { l2_x2 = width - motion2; }

            if (l1_x1 < -width) { l1_x1 = width - motion1; }
            { l1_x1 -= motion1; l1_x2 -= motion1; }
            if (l1_x2 < -width) { l1_x2 = width - motion1; }

            if (l3_x1 < -width) { l3_x1 = width - motion3; }
            { l3_x1 -= motion3; l3_x2 -= motion3; }
            if (l3_x2 < -width) { l3_x2 = width - motion3; }

            Invalidate();

        }
        private void BackgroundMoveUp()
{
    if (l1_y1 > height) { l1_y1 = -height + motion1; l1_y2 = motion1; }
    { l1_y1 += motion1; l1_y2 += motion1; }
    Invalidate();
}

private void BackgroundMoveDown()
{
    if (l1_y1 < -height) { l1_y1 = height - motion1; l1_y2 = height + motion1; }
    { l1_y1 -= motion1; l1_y2 -= motion1; }
    Invalidate();
}

       
        private void BackgroundMoveLeft()
{
    if (l2_x1 > width) { l2_x1 = -width + motion2; }
    { l2_x1 += motion2; l2_x2 += motion2; }
    if (l2_x2 > width) { l2_x2 = -width + motion2; }

    if (l1_x1 > width) { l1_x1 = -width + motion1; }
    { l1_x1 += motion1; l1_x2 += motion1; }
    if (l1_x2 > width) { l1_x2 = -width + motion1; }

    if (l3_x1 > width) { l3_x1 = -width + motion3; }
    { l3_x1 += motion3; l3_x2 += motion3; }
    if (l3_x2 > width) { l3_x2 = -width + motion3; }

    Invalidate();

}


        public MAIN()
        {
            InitializeComponent(); 
            
            
            //this.KeyDown += new KeyEventHandler(MAIN_KeyDown);
            layer1 = Resource1.L1_1_;
            layer2 = Resource1.L2_2_;
            layer3 = Resource1.L3_1_;


            l1_x1 = l2_x1 = l3_x1 = 0;
            l1_x2 = l2_x2 = l3_x2 = width;
        }

        private void MAIN_SizeChanged(object sender, EventArgs e)
        {
            Screen Srn = Screen.PrimaryScreen;
       
            Init();
            this.Text = "User Resolution is " + tempHeight.ToString() + " X " + tempWidth.ToString();
        }

        private void Init()
        {
            //scroll
            map         = new Map();
            cat         = new Bitmap("tile43.png");
            wood        = new Bitmap("tile42.png");
            wood02      = new Bitmap("tile52.png");
            black       = new Bitmap("tile11.png");
            pinkbush    = new Bitmap("pink_bush2.png");
            tree    = new Bitmap("trees2_1.png");
            fence   = new Bitmap("fence.png");
            rocks   = new Bitmap("rocks1_5.png");
            coin =new Bitmap("33Ho.gif");
            water =new Bitmap("ola3.png");
            
            increment   = 12;

            spriteTrim  = new Bitmap(375, 65);
            sprite      = Resource1.PULPO;
            


            //screen
            changed     = false;
            full        = false;
            Srn         = Screen.PrimaryScreen;
         
            tempHeight  = Srn.Bounds.Width;
            tempWidth   = Srn.Bounds.Height;

            right = false;

            //generate the image
            GenerateIMG();

            dst = new Rectangle(0, 0, PCT_CANVAS.Width, PCT_CANVAS.Height);
            src = new Rectangle(0, 0, PCT_CANVAS.Width, PCT_CANVAS.Height);

            sprD = new Rectangle(400, Y+300, 120, 120);//subcanvas destiny
            sprS = new Rectangle(0, 0, 44, 70);
            
        }


        private void PCT_CANVAS_Paint(object sender, PaintEventArgs e)
{
    if (img != null)
    {
        e.Graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
        e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;

        // Dibujar el fondo primero
        e.Graphics.DrawImage(layer1, l1_x1, 0, layer1.Width, this.Height);
        e.Graphics.DrawImage(layer1, l1_x2, 0, layer1.Width, this.Height);

        e.Graphics.DrawImage(layer2, l2_x1, l1_y1, layer1.Width, this.Height+180);
        e.Graphics.DrawImage(layer2, l2_x2, l1_y2, layer1.Width, this.Height+180);

        e.Graphics.DrawImage(layer3, l3_x1, l1_y1, layer1.Width, this.Height - 50);
        e.Graphics.DrawImage(layer3, l3_x2, l1_y2, layer1.Width, this.Height - 50);
        
        // Dibujar las imágenes después del fondo
        e.Graphics.DrawImage(img, dst, src, GraphicsUnit.Pixel);
        //e.Graphics.DrawImage(cat, PCT_CANVAS.Width / 2, 500);
        e.Graphics.DrawImage(sprite, sprD, sprS, GraphicsUnit.Pixel);
        e.Graphics.DrawImage(sprite, sprD, sprS, GraphicsUnit.Pixel);
        Rectangle destRect = new Rectangle(20, 30, 50, 50);
        e.Graphics.DrawImage(Resource1.Coin, destRect);
        Font font = new Font("Arial", 12);
        e.Graphics.DrawString("Numero de gemas: 0", font,Brushes.Black,new PointF(60,45));
                

    
       
           
        
    }
}
      


       
    List<Point> wallTiles = new List<Point>();
        private void GenerateIMG()
        {
            // map properties
            int w = 256;// width
            int h = 22;// height
            int u = 45;//unit
            char v;
            
            img  = new Bitmap(w * u, h * u);
            gImg = Graphics.FromImage(img);

            this.Text = img.Width.ToString();


            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    int posX = x * u;
                    int posY = y * u;
                    v = map.map[(y * w) + x];
                    if (v == '.')
                        //gImg.DrawImage(Resource1.background, x * u, y * u, u, u);
                        //gImg.FillRectangle(Brushes.DeepSkyBlue, x * u, y * u, u, u);
                    if (v == '1')
                        gImg.DrawImage(wood02, x * u, y * u, u, u);
                    if (v == '#')
                        gImg.DrawImage(cat, x * u, y * u, u, u);
                        wallTiles.Add(new Point(x * u, y * u));
                    if (v == '$')
                        gImg.DrawImage(wood02, x * u, y * u, u, u);
                    if (v == '%')
                        gImg.DrawImage(pinkbush, x * u, y * u, u, u);
                    if (v == '&')
                        gImg.DrawImage(tree, x * u, y * u, 200, 200);
                    if (v == '/')
                        gImg.DrawImage(fence, x * u, y * u-20, 100, 100);
                    if (v == '(')
                        gImg.DrawImage(rocks, x * u, y * u-20, 100, 100);
                     if (v == ')')
                        gImg.DrawImage(coin, x * u, y * u, 50, 50);
                     if (v == '=')
                        gImg.DrawImage(water, x * u, y * u, 100, 100);
                       
                    if (v == '-')
                        gImg.DrawImage(wood, x * u, y * u, u, u);
                    if (v == ',')
                        gImg.DrawImage(black, x * u, y * u, u, u);
                }
            }

            
        }
        
        private void CANVAS_Action(int X)
        {
            counter++;

            src.X = X;                    
            sprS.X = (counter * 48) % sprite.Width;
        }
         private void CANVAS_Actiony(int Y)
        {
            countery++;

            src.Y = Y;                    
            sprS.X = (countery * 48) % sprite.Width;
        }
       

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (img == null)
                return;
 
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                    this.WindowState = FormWindowState.Normal;
                    this.TopMost = false;
                    break;
                case Keys.Left:
                    
                      X -= increment;
                     CANVAS_Action(X);
                     BackgroundMoveLeft();   

                   
                                      
                    break;
                case Keys.Right:   
                    
                     X += increment;
                     CANVAS_Action(X);
                     BackgroundMove();
                     right = true;
                                     
                    
                    break;
                case Keys.Up:
                    Y -= increment;
                    CANVAS_Actiony(Y);
                    BackgroundMoveUp();
                    up = true;
                    break;
                case Keys.Down:
                    Y += increment;
                    CANVAS_Actiony(Y);
                    BackgroundMoveDown();
                    up = true;
                    break;

                case Keys.F:
                    if (!full)
                    {
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                        this.WindowState = FormWindowState.Maximized;
                        this.TopMost = true;
                    }
                    else
                    {
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                        this.WindowState = FormWindowState.Normal;
                        this.TopMost = false;
                    }
                    full = !full;
                    break;
                case Keys.S:
                    Change(1600, 900);        
                    break;
                case Keys.A:
                    Change(1920, 1080);
                    break;
                case Keys.Space:
                    //Y = -50;
                    sprD.Y = 250;
                    break;
            } 
            
            PCT_CANVAS.Invalidate();//*/
           
        }

        private void Change(int a, int b)
        {            
            ChangeRes = new TILED_BASED.CResolution(a, b);            
        }

        private void MAIN_KeyUp(object sender, KeyEventArgs e)
        {            
            counter = 0;
            sprS.X = 0;

            if (e.KeyCode == Keys.Right)
                right = false;
            

            PCT_CANVAS.Invalidate();//*/
        }

        
       
        private void TIMER_Tick(object sender, EventArgs e)
        {
            
                        
            
            if (sprD.Y < 300)
                sprD.Y+=8;

            if (sprD.Y > 300)
                sprD.Y = 300;

            if (right)
            {
                X += increment;
                CANVAS_Action(X);
            }
           

            PCT_CANVAS.Invalidate();
        }



    }
}

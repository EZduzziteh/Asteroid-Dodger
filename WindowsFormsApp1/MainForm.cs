using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroid_Dodger
{
    
    public partial class MainForm : Form
    {
        List<Asteroid> asteroids = new List<Asteroid>();
        SpaceShip ship = new SpaceShip(new Point(100,100), new Size(100,28));

        bool gameOver = false;

        int Health = 5;
        int score = 0;
        int counter = 0;
        public MainForm()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mainTimer.Interval = 16;
            
            this.Size = new Size(1200, 800);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.BackColor = Color.Black;
            this.Paint += new PaintEventHandler(this.PaintObjects);
            mainTimer.Start();
            CreateAsteroids();
        }

        private void Form1_Paint(object sender, PaintEventArgs args)
        {

        }

        private void PaintObjects(Object sender, PaintEventArgs args)
        {
            Rectangle rect = new Rectangle(100, 100, 1000, 600);
            args.Graphics.DrawRectangle(Pens.White, rect);

            Region clippingRegion = new Region(rect);
            args.Graphics.Clip = clippingRegion;
            int asteroidIndex = asteroids.Count - 1;
            Console.WriteLine(asteroids.Count - 1);
            while (asteroidIndex >= 0 && gameOver == false)
            {

                if (asteroids[asteroidIndex].Collision(ship))
                {
                    asteroids.RemoveAt(asteroidIndex);
                    Health--;
                    if (Health <= 0)
                    {
                        Console.WriteLine("Game IS LOST!");

                        args.Graphics.DrawString("GAME OVER!", new Font("Arial", 30, FontStyle.Regular), Brushes.Red, 400, 200);
                        gameOver = true;
                    }
                    else
                    {
                        //generate more asteroids
                        

                        GenerateAsteroids(2, 4, 10, 40, 660, 100, 1050, 45);
                        

                       
                    }
                }
                else
                {
                    asteroids[asteroidIndex].Draw(args);
                }
                asteroidIndex--;

                


            }
            args.Graphics.ResetClip();

            args.Graphics.DrawString("Score: " + score.ToString(), new Font("Arial", 30, FontStyle.Regular), Brushes.White, 125, 20);




            if (Health <= 0)
            {
                args.Graphics.DrawString("Health: " + Health.ToString(), new Font("Arial", 30, FontStyle.Regular), Brushes.Gray, 850, 20);
            } 
            else if (Health == 1)
            {
                args.Graphics.DrawString("Health: " + Health.ToString(), new Font("Arial", 30, FontStyle.Regular), Brushes.Red, 850, 20);
            }
            else if (Health == 5)
            {
                args.Graphics.DrawString("Health: " + Health.ToString(), new Font("Arial", 30, FontStyle.Regular), Brushes.Green, 850, 20);
            }
            else
            {
                args.Graphics.DrawString("Health: " + Health.ToString(), new Font("Arial", 30, FontStyle.Regular), Brushes.Yellow, 850, 20);
            }
            
            
            
            ship.Draw(args);

            foreach (Asteroid ast in asteroids)
            {
                ast.Draw(args);
            }

        }

        private void timerAsteroid_Tick(object sender, EventArgs e)
        {
            if (!gameOver)
            {
                foreach (Asteroid ast in asteroids)
                {

                    ast.Move(100, 1075, 100, 850);

                }
                counter++;
                if (counter >= 60)
                {
                    score++;
                    counter = 0;
                }

                ship.Move(110, 1100, -94, 550);
                this.Refresh();
            }
            


        }

        public void CreateAsteroids()
        {

            Random rand = new Random();
            



            GenerateAsteroids(6, 12, 10, 40, 660, 100, 600, 120);

           
        }

        public void GenerateAsteroids(int minAsteroids, int maxAsteroids, int minSize, int maxSize, int minHeight, int maxHeight, int locationX, int maxSpaceBetween)
        {
            Random rand = new Random();
            int asteroidAmount = rand.Next(minAsteroids, maxAsteroids);
            int spaceBetween = 0;
            for(int i = 0; i <asteroidAmount; i++)
            {
                int scale = rand.Next(minSize, maxSize);
                Size size =new Size(scale , scale);
                Point location = new Point(locationX+spaceBetween, rand.Next(maxHeight, minHeight));
                spaceBetween += rand.Next(0, maxSpaceBetween);
                
                Asteroid asteroid = new Asteroid(size, location, -2, 0);
                asteroids.Add(asteroid);
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            ship.MoveX = 0;
            ship.MoveY = 0;
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                ship.MoveX = -5;
             
            }
            if (e.KeyCode == Keys.Right)
            {
                ship.MoveX = 5;

            }
            if (e.KeyCode == Keys.Up)
            {
                ship.MoveY = -5;

            }
            if (e.KeyCode == Keys.Down)
            {
                ship.MoveY = 5;

            }

        }
    }
    /*
     int radius = rand.next);
     while asteroidCount<totalAsteroids{
     
        Random rand = new Random();
        assets = new List<Asteroid>();
        int x = rand.Next(100,500);
        asteroid.MoveX = movement[rand.Next();];
        }
         
         
         */

    public class Asset
    {

        public Rectangle rect;
        public Point location;
        public Size size;
        public virtual void Move(int X1, int X2, int Y1, int Y2)
        {
        }
    }
    public class SpaceShip:Asset
    {
        

        public int MoveX { get; internal set; }
        public int MoveY { get; internal set; }

        public SpaceShip(Point location, Size size)
        {
            this.location = location;
            this.size = size;
            this.rect = new Rectangle(location.X, location.Y, size.Width, size.Height);
            
        }

        public void Draw(PaintEventArgs args)
        {

            Pen shipPen = new Pen(Color.FromArgb(26, 125, 48));
            SolidBrush shipBrush = new SolidBrush(Color.FromArgb(22, 85, 38));
            //#TODO implement this as ship position.



            
            //draw starship enterprise

            //shaft
            Point[] points = new Point[4];
            points[0] = new Point(35 + location.X, 218 + location.Y);
            points[1] = new Point(55 + location.X, 208 + location.Y);
            points[2] = new Point(65 + location.X, 208 + location.Y);
            points[3] = new Point(50 + location.X, 218 + location.Y);
            args.Graphics.FillPolygon(shipBrush, points);
            args.Graphics.DrawPolygon(shipPen, points);

            //bridge
            args.Graphics.FillEllipse(shipBrush, 50 + location.X, 200 + location.Y, 50, 10);
            args.Graphics.DrawEllipse(shipPen, 50 + location.X, 200 + location.Y, 50, 10);
            args.Graphics.DrawEllipse(shipPen, 65 + location.X, 195 + location.Y, 25, 8);
            args.Graphics.FillEllipse(shipBrush, 65 + location.X, 195.5f + location.Y, 25, 8);

            //engine shaft
            Point[] points2 = new Point[4];
            points2[0] = new Point(27 + location.X, 218 + location.Y);
            points2[1] = new Point(22 + location.X, 200 + location.Y);
            points2[2] = new Point(34 + location.X, 200 + location.Y);
            points2[3] = new Point(44 + location.X, 218 + location.Y);
            args.Graphics.FillPolygon(shipBrush, points2);
            args.Graphics.DrawPolygon(shipPen, points2);

            //Center
            args.Graphics.FillEllipse(shipBrush, 20 + location.X, 215 + location.Y, 40, 8);
            args.Graphics.DrawEllipse(shipPen, 20 + location.X, 215 + location.Y, 40, 8);

            
            //engines
            args.Graphics.FillEllipse(shipBrush, -20 + location.X, 195 + location.Y, 65, 12);
            args.Graphics.DrawEllipse(shipPen, -20 + location.X, 195 + location.Y, 65, 12);

            //
            
            //draw collision box
            //args.Graphics.DrawRectangle(shipPen, rect);
        }

        public override void Move(int minLocationX, int maxLocationX, int minLocationY, int maxLocationY)
        {
            int newX = location.X + MoveX;
            if (newX < minLocationX)
            {
                newX = minLocationX;
            }
            else if (newX > maxLocationX - 100)
            {
                newX = maxLocationX - 100;
            }


            int newY = location.Y + MoveY;
            if (newY < minLocationY)
            {
                newY = minLocationY;
            }else if (newY> maxLocationY - 75)
            {
                newY = maxLocationY - 75;
            }

            location = new Point(newX, newY);
            rect.X = location.X;
            rect.Y = location.Y+195;
        }

        
        
    }

 
    public class Asteroid:Asset
    {   
        public int moveSpeedHorizontal = 1;

        public int moveSpeedVertical = 1;

        public Asteroid(Size size, Point location, int moveSpeedHorizontal, int moveSpeedVertical)
        {
            this.size = size;
            this.location = location;
            this.rect = new Rectangle(location.X, location.Y, size.Width, size.Height);
            this.moveSpeedHorizontal = moveSpeedHorizontal;
            this.moveSpeedVertical = moveSpeedVertical;
        }

        public void Draw(PaintEventArgs args)
        {
            Graphics g = args.Graphics;
            g.FillEllipse(Brushes.DarkGray, location.X, location.Y, size.Width, size.Height);
            g.DrawEllipse(Pens.LightGray,  location.X, location.Y, size.Width, size.Height);
           //draw collision box
            // g.DrawRectangle(Pens.Red, rect);
        }
        public override void Move(int minLocationX, int maxLocationX, int minLocationY, int maxLocationY)
        {
            location.X += moveSpeedHorizontal;
            location.Y -= moveSpeedVertical;
            if (location.X < minLocationX)
            {
                location.X = maxLocationX;
            }
            if (location.Y < minLocationY)
            {
                location.Y = maxLocationY - 1;
            }
            if (location.Y > maxLocationY)
            {
                location.Y = minLocationY + 1;
            }
            rect.X = location.X;
            rect.Y = location.Y;

          
        }
            public bool Collision(Asset ship)
        {
            if (rect.IntersectsWith(ship.rect))
            {

                Console.WriteLine("Collision");
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}

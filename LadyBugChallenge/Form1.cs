using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LadyBugChallenge
{
    public partial class Form1 : Form
    {

        private int count = 0;
        private int boxC = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int dX = 1;
            int dY = 1;

            CircularPictureBox PB = new CircularPictureBox();
            PB.Size = new Size(35, 35);
            PB.SizeMode = PictureBoxSizeMode.StretchImage;
            PB.Location = RandomLoc();
            PB.BackgroundImage = Image.FromFile("c:\\Users\\");

            Action b = () => { this.Controls.Add(PB); };   //delegate for the adding task

            Task.Run(() =>   //adding task to make thread sleep to prevent overlapping stuck
            {
                foreach (CircularPictureBox box in this.Controls.OfType<CircularPictureBox>())
                {
                    while (PB != box && PB.Bounds.IntersectsWith(box.Bounds))
                    {
                        Thread.Sleep(15);
                    }
                }

                if (boxC < 10)
                {
                    this.BeginInvoke(b);
                    boxC++;
                }

                Thread.Sleep(10);
            });

            PB.MouseClick += new MouseEventHandler(PictureboxClick);   //on mouse clicking event

            Action a = () => { PB.Location = new Point(PB.Location.X + dX, PB.Location.Y + dY); };   //delegate for the moving task

            Task.Run(()=>  //moving task for the picturebox
            {
                int width = this.ClientSize.Width;
                int height = this.ClientSize.Height;

                while (true)
                {
                    foreach (CircularPictureBox box in this.Controls.OfType<CircularPictureBox>())
                    {
                        if (PB != box && PB.Bounds.IntersectsWith(box.Bounds))
                        {
                            dX = -dX;
                            dY = -dY;
                            this.BeginInvoke(a);
                            Thread.Sleep(25);
                        }
                    }

                    if (PB.Location.X >= width - PB.Width || PB.Location.X <= 0)
                    {
                        dX = -dX;
                    }

                    if (PB.Location.Y >= height - PB.Height || PB.Location.Y <= 0)
                    {
                        dY = -dY;
                    }

                    this.BeginInvoke(a);

                    Thread.Sleep(10);
                }
            });
        }

        private Point RandomLoc()   //method to randomize points for picturebox
        {
            Random r = new Random();
            Point point = new Point();
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;
            point.X = r.Next(width-65);
            point.Y = r.Next(height-65);
            return point;
        }

        private void PictureboxClick(object sender,MouseEventArgs e)   //method for the on click mouse event
        {
            foreach(CircularPictureBox box in this.Controls.OfType<CircularPictureBox>())
            {
                if (box.Equals(sender))
                {
                    box.Dispose();
                    count++;
                    if (count == 10)   //drawing text counter
                    {
                        DrawString();
                    }
                }
            }
        }

        private void DrawString()   //method to draw on screen
        {
            System.Drawing.Graphics formGraphics = this.CreateGraphics();
            string drawString = "You Win!";
            System.Drawing.Font drawFont = new System.Drawing.Font("David", 16);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            float x = 50.0F;
            float y = 50.0F;
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            formGraphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            drawFont.Dispose();
            drawBrush.Dispose();
            formGraphics.Dispose();
        }
    }
}

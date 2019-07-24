using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BouncingBallProject
{
    public partial class Form1 : Form
    {
        private int dX = 1;
        private int dY = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void LocationChange(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;

            circularPictureBox2.Location = new Point(circularPictureBox2.Location.X + dX, circularPictureBox2.Location.Y + dY);

            if (circularPictureBox2.Location.X > width - circularPictureBox2.Width || circularPictureBox2.Location.X < 0)
            {
                dX = -dX; 
            }

            if (circularPictureBox2.Location.Y > height - circularPictureBox2.Height || circularPictureBox2.Location.Y < 0)
            {
                dY = -dY;
            }
        }
    }
}

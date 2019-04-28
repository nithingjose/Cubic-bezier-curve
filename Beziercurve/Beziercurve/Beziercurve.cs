using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beziercurve
{
    public partial class Beziercurve : Form
    {
        private PointF[] Points = new PointF[4];
        // The index of the next point to define.
        private int NextPoint = 0;

        public Beziercurve()
        {
            InitializeComponent();
        }
       
        // Select a point.
        private void picCanvas_MouseClick(object sender, MouseEventArgs e)
        {
        
            // get the first point.
            if (NextPoint > 3)
            {
                NextPoint = 0;
            }
            // Save this point.
            Points[NextPoint].X = e.X;
            Points[NextPoint].Y = e.Y;

            //populate the control points for reference in the labels
            if (NextPoint == 0)
            {
                label1.Text = label2.Text = label3.Text = label4.Text = "";
            }
            var x = this.Controls.OfType<Label>().FirstOrDefault(c => c.Name == "label" + (NextPoint + 1));
            x?.Invoke((MethodInvoker)(() => x.Text = "P" + NextPoint + " = " + Points[NextPoint]));

            // Move to the next point.
            NextPoint++;

            // Redraw.
            picCanvas.Refresh();
        }

        // Draw the currently selected points. 
        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(picCanvas.BackColor);
            if (NextPoint >= 4)
            {
               // Draw a curve.
                BezeirFunctions.DrawBezier(e.Graphics, Pens.Black, 0.01f,
                    Points[0], Points[1], Points[2], Points[3]);
            }

            // Draw the control points.
            for (int i = 0; i < NextPoint; i++)
            {
                e.Graphics.FillRectangle(Brushes.White, Points[i].X - 3, Points[i].Y - 3, 6, 6);
                e.Graphics.DrawRectangle(Pens.Yellow, Points[i].X - 3, Points[i].Y - 3, 6, 6);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NextPoint = 0;
            label1.Text = label2.Text = label3.Text = label4.Text = textBox1.Text = "";
            picCanvas.Image = null;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch >= 0 && ch <= 1)
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float tpoint = float.Parse(textBox1.Text);
            float pointx = BezeirFunctions.GetPoint(tpoint, Points[0].X, Points[1].X, Points[2].X, Points[3].X);
            float pointy = BezeirFunctions.GetPoint(tpoint, Points[0].Y, Points[1].Y, Points[2].Y, Points[3].Y);
            Graphics g = picCanvas.CreateGraphics();
            PointF point = new PointF { X = pointx, Y = pointy };
            g.FillRectangle(Brushes.Pink, point.X - 3, point.Y - 3, 6, 6);
            g.DrawRectangle(Pens.DeepPink, point.X - 3, point.Y - 3, 6, 6);
        }

    }
}

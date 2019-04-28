using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beziercurve
{
    class BezeirFunctions
    {
        // Parametric functions for drawing a cubic Bezier curve.
        public static float GetPoint(float t, float p0, float p1, float p2, float p3)
        {
            return (float)(
                p0 * Math.Pow((1 - t), 3) +
                p1 * 3 * t * Math.Pow((1 - t), 2) +
                p2 * 3 * Math.Pow(t, 2) * (1 - t) +
                p3 * Math.Pow(t, 3)
            );
        }

        // Draw the Bezier curve.
        public static void DrawBezier(Graphics gr, Pen the_pen, float dt, PointF pt0, PointF pt1, PointF pt2, PointF pt3)
        {
            // Draw the curve.
            List<PointF> points = new List<PointF>();
            for (float t = 0.0f; t < 1.0; t += dt)
            {
                points.Add(new PointF(
                    GetPoint(t, pt0.X, pt1.X, pt2.X, pt3.X),
                    GetPoint(t, pt0.Y, pt1.Y, pt2.Y, pt3.Y)));
            }

            // Connect to the final point.
            points.Add(new PointF(
                GetPoint(1.0f, pt0.X, pt1.X, pt2.X, pt3.X),
                GetPoint(1.0f, pt0.Y, pt1.Y, pt2.Y, pt3.Y)));

            // Draw the curve.
            gr.DrawLines(the_pen, points.ToArray());

            // Draw lines connecting the control points.
            gr.DrawLine(Pens.Red, pt0, pt1);
            gr.DrawLine(Pens.Green, pt1, pt2);
            gr.DrawLine(Pens.Blue, pt2, pt3);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PhysicsWinForm
{
    public class Polygon : VMM
    {
        public PointF[] points;
        private VMM trans = new VMM();
        public PointF center;
        
        public Polygon()
        {
            
        }

        public Polygon(int numOfPoints)
        {
            if(numOfPoints < 3)
            {
                throw new Exception("Need at least 3 points for a polygon");
            }            

            if(numOfPoints == 4)
            {
                PointF p1 = new PointF(150f, 150f);
                PointF p2 = new PointF(p1.X + 50, p1.Y);
                PointF p3 = new PointF(p2.X, p2.Y + 50);
                PointF p4 = new PointF(p3.X - 50f, p3.Y);
                points = new PointF[]{ p1, p2, p3, p4};
                center = new PointF(p4.X + 25f, p3.Y - 25f);

            }
        }

        public void MovePolygon(float dx, float dy)
        {
            for (int i = 0; i < this.points.Length; i++)
            {
                float[,] temp = this.Translate(this.points[i].X, this.points[i].Y, dx, dy);
                this.points[i].X = temp[0, 0];
                this.points[i].Y = temp[1, 0];
            }
        }

        public void ProjectPolygon()
        {
            
        }
    }
}

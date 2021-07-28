using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationsLibrary
{
    public class BVHNode
    {
        public Polygon Bounds { get; private set; }
        private bool containsMouse;
        public BVHNode[] Children { get; private set; }

        public BVHNode(Polygon bounds, params BVHNode[] children)
        {
            Bounds = bounds;
            Bounds.ShowVertices = false;
            Children = children;
        }
        public BVHNode(Polygon bounds) : this(bounds, new BVHNode[] { }) { }

        public bool CheckCollision(Point mousePosition)
        {
            containsMouse = Bounds.Contains(mousePosition);
            Bounds.Pen = containsMouse ? Pens.Red : Pens.Black;

            if (containsMouse)
            {
                for (int i = 0; i < Children.Length; i++)
                {
                    if (Children[i].CheckCollision(mousePosition))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public void Draw(Graphics graphics)
        {
            Bounds.Draw(graphics);

            if (containsMouse)
            {
                for (int i = 0; i < Children.Length; i++)
                {
                    if (!Children[i].containsMouse)
                    {
                        Children[i].Draw(graphics);
                    }
                }
                for (int i = 0; i < Children.Length; i++)
                {
                    if (Children[i].containsMouse)
                    {
                        Children[i].Draw(graphics);
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImplementationsLibrary
{
    public class QuadTree
    {
        public int MaxLevels { get; private set; }

        public QuadNode Head { get; private set; }

        private Action<MovingCircle, List<MovingCircle>> UpdateFunction;

        public QuadTree(MovingCircle[] circles, Action<MovingCircle, List<MovingCircle>> updateFunction, int maxLevels, int minX, int maxX, int minY, int maxY)
        {
            MaxLevels = maxLevels;

            Head = new QuadNode(circles.ToList(), updateFunction, maxLevels, 0, minX, maxX, minY, maxY);

            UpdateFunction = updateFunction;
        }

        public void Update()
        {
            (List<MovingCircle> up, List<MovingCircle> left, List<MovingCircle> down, List<MovingCircle> right) = Head.Update();

            foreach (MovingCircle movingCircle in up)
            {
                movingCircle.Direction *= -1;
            }
            foreach (MovingCircle movingCircle in down)
            {
                movingCircle.Direction *= -1;
            }
            foreach (MovingCircle movingCircle in left)
            {
                movingCircle.Direction = (float)(Math.PI - movingCircle.Direction);
            }
            foreach (MovingCircle movingCircle in right)
            {
                movingCircle.Direction = (float)(Math.PI - movingCircle.Direction);
            }
            Head.AddCircles(up);
            Head.AddCircles(left);
            Head.AddCircles(down);
            Head.AddCircles(right);
        }

        public void Draw(Graphics graphics)
        {
            Head.Draw(graphics);
        }
    }
}

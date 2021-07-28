using System;
using System.Collections.Generic;
using System.Drawing;

namespace ImplementationsLibrary
{
    public class QuadNode
    {
        public List<MovingCircle> Circles { get; private set; }
        public int MaxLevels { get; private set; }
        public int CurrentLevel { get; private set; }
        public float MinX { get; private set; }
        public float MaxX { get; private set; }
        public float MinY { get; private set; }
        public float MaxY { get; private set; }

        public bool IsFinalLayer { get; private set; }

        public QuadNode TopLeft { get; private set; }
        public QuadNode TopRight { get; private set; }
        public QuadNode BottomLeft { get; private set; }
        public QuadNode BottomRight { get; private set; }

        public bool ContainsCircleAnyDepth
        {
            get
            {
                if (Circles.Count > 0)
                {
                    return true;
                }
                if (IsFinalLayer)
                {
                    return false;
                }
                return TopLeft.ContainsCircleAnyDepth || TopRight.ContainsCircleAnyDepth || BottomLeft.ContainsCircleAnyDepth || BottomRight.ContainsCircleAnyDepth;
            }
        }

        private Action<MovingCircle, List<MovingCircle>> UpdateFunction;

        public QuadNode(List<MovingCircle> circles, Action<MovingCircle, List<MovingCircle>> updateFunction, int maxLevels, int currentLevel, float minX, float maxX, float minY, float maxY)
        {
            Circles = circles;
            MaxLevels = maxLevels;
            CurrentLevel = currentLevel;
            IsFinalLayer = maxLevels - 1 == currentLevel;
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;

            UpdateFunction = updateFunction;

            if (!IsFinalLayer)
            {
                float middleX = MinX + (MaxX - MinX) / 2;
                float middleY = MinY + (MaxY - MinY) / 2;

                TopLeft = new QuadNode(new List<MovingCircle>(), updateFunction, MaxLevels, CurrentLevel + 1, MinX, middleX, MinY, middleY);
                TopRight = new QuadNode(new List<MovingCircle>(), updateFunction, MaxLevels, CurrentLevel + 1, middleX, MaxX, MinY, middleY);
                BottomLeft = new QuadNode(new List<MovingCircle>(), updateFunction, MaxLevels, CurrentLevel + 1, MinX, middleX, middleY, MaxY);
                BottomRight = new QuadNode(new List<MovingCircle>(), updateFunction, MaxLevels, CurrentLevel + 1, middleX, MaxX, middleY, maxY);
            }
            
            AddCircles(circles);
        }

        public void AddCircles(List<MovingCircle> newCircles)
        {
            float middleX = MinX + (MaxX - MinX) / 2;
            float middleY = MinY + (MaxY - MinY) / 2;
            if (!IsFinalLayer)
            {
                for (int i = 0; i < newCircles.Count; i++)
                {
                    if (newCircles[i].X <= middleX && newCircles[i].Y <= middleY)
                    {
                        TopLeft.AddCircle(newCircles[i]);
                    }
                    else if (newCircles[i].X > middleX && newCircles[i].Y <= middleY)
                    {
                        TopRight.AddCircle(newCircles[i]);
                    }
                    else if (newCircles[i].X <= middleX && newCircles[i].Y > middleY)
                    {
                        BottomLeft.AddCircle(newCircles[i]);
                    }
                    else
                    {
                        BottomRight.AddCircle(newCircles[i]);
                    }
                }
            }
            else
            {
                Circles.AddRange(newCircles);
            }
        }
        public void AddCircle(MovingCircle circle)
        {
            float middleX = MinX + (MaxX - MinX) / 2;
            float middleY = MinY + (MaxY - MinY) / 2;
            if (!IsFinalLayer)
            {
                if (circle.X <= middleX && circle.Y <= middleY)
                {
                    TopLeft.AddCircle(circle);
                }
                else if (circle.X > middleX && circle.Y <= middleY)
                {
                    TopRight.AddCircle(circle);
                }
                else if (circle.X <= middleX && circle.Y > middleY)
                {
                    BottomLeft.AddCircle(circle);
                }
                else
                {
                    BottomRight.AddCircle(circle);
                }
            }
            else
            {
                Circles.Add(circle);
            }
        }

        /// <summary>
        /// this function returns four lists:
        /// 1) circles that have moved up
        /// 2) circles that have moved left
        /// 3) circles that have moved down
        /// 4) circles that have moved right
        /// </summary>
        /// <returns></returns>
        public (List<MovingCircle>, List<MovingCircle>, List<MovingCircle>, List<MovingCircle>) Update()
        {
            if (!IsFinalLayer)// && CurrentLevel != 0)
            {
                (List<MovingCircle> up, List<MovingCircle> left, List<MovingCircle> down, List<MovingCircle> right) topLeft = TopLeft.Update();
                (List<MovingCircle> up, List<MovingCircle> left, List<MovingCircle> down, List<MovingCircle> right) topRight = TopRight.Update();
                (List<MovingCircle> up, List<MovingCircle> left, List<MovingCircle> down, List<MovingCircle> right) bottomLeft = BottomLeft.Update();
                (List<MovingCircle> up, List<MovingCircle> left, List<MovingCircle> down, List<MovingCircle> right) bottomRight = BottomRight.Update();

                TopLeft.AddCircles(topRight.left);
                TopLeft.AddCircles(bottomLeft.up);

                TopRight.AddCircles(topLeft.right);
                TopRight.AddCircles(bottomRight.up);

                BottomLeft.AddCircles(topLeft.down);
                BottomLeft.AddCircles(bottomRight.left);

                BottomRight.AddCircles(topRight.down);
                BottomRight.AddCircles(bottomLeft.right);

                List<MovingCircle> up = topLeft.up;
                up.AddRange(topRight.up);

                List<MovingCircle> left = topLeft.left;
                left.AddRange(bottomLeft.left);

                List<MovingCircle> down = bottomLeft.down;
                down.AddRange(bottomRight.down);

                List<MovingCircle> right = topRight.right;
                right.AddRange(bottomRight.right);

                return (up, left, down, right);
            }
            else
            {
                List<MovingCircle> up = new List<MovingCircle>();
                List<MovingCircle> left = new List<MovingCircle>();
                List<MovingCircle> down = new List<MovingCircle>();
                List<MovingCircle> right = new List<MovingCircle>();
                for (int i = 0; i < Circles.Count; i++)
                {
                    Circles[i].Update();
                    UpdateFunction(Circles[i], Circles);
                    if (Circles[i].Y < MinY)
                    {
                        up.Add(Circles[i]);
                        Circles.RemoveAt(i);
                        i--;
                        continue;
                    }
                    if (Circles[i].Y > MaxY)
                    {
                        down.Add(Circles[i]);
                        Circles.RemoveAt(i);
                        i--;
                        continue;
                    }
                    if (Circles[i].X < MinX)
                    {
                        left.Add(Circles[i]);
                        Circles.RemoveAt(i);
                        i--;
                        continue;
                    }
                    if (Circles[i].X > MaxX)
                    {
                        right.Add(Circles[i]);
                        Circles.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
                return (up, left, down, right);
            }
        }

        public void Draw(Graphics graphics)
        {
            if (ContainsCircleAnyDepth)
            {
                graphics.DrawRectangle(Pens.LightBlue, MinX, MinY, MaxX - MinX, MaxY - MinY);
            }
            if (!IsFinalLayer)
            {
                TopRight.Draw(graphics);
                TopLeft.Draw(graphics);
                BottomLeft.Draw(graphics);
                BottomRight.Draw(graphics);
            }
            else
            {
                foreach (MovingCircle circle in Circles)
                {
                    circle.Draw(graphics);
                }
            }
        }
    }
}

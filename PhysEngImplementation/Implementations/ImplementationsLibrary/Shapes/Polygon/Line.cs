using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationsLibrary
{
    public class Line
    {
        private Vertex start;
        public ref Vertex Start => ref start;
        private Vertex end;
        public ref Vertex End => ref end;

        public float Slope => (float)Math.Round((End.Y - Start.Y) / (End.X - Start.X), 5);
        public float PerpendicularSlope => -1 / Slope;


        public float YIntercept => (float)Math.Round(Start.Y - Slope * Start.X, 5);
        public float XIntercept => (float)Math.Round(-Slope / YIntercept, 5);

        public float Rotation => (float)Math.Atan2(End.Y - Start.Y, End.X - Start.X);
        public float ManagedRotation => (float)((Rotation + Math.PI) % Math.PI * 180 / Math.PI);

        public float MinX => Start.X < End.X ? Start.X : End.X;
        public float MaxX => Start.X >= End.X ? Start.X : End.X;
        public float MinY => Start.Y < End.Y ? Start.Y : End.Y;
        public float MaxY => Start.Y >= End.Y ? Start.Y : End.Y;

        public Vertex LeftMostVertex => Start.X < End.X ? Start : End;
        public Vertex RightMostVertex => Start.X > End.X ? Start : End;
        public Vertex TopMostVertex => Start.Y < End.Y ? Start : End;
        public Vertex BottomMostVertex => Start.Y > End.Y ? Start : End;



        public float DeltaX => End.X - Start.X;
        public float DeltaY => End.Y - Start.Y;

        public float Length => Vector2D.Distance(Start, End);

        public Vector2D Center => new Vector2D((MaxX + MinX) / 2, (MaxY + MinY) / 2);

        public LineType LineType => Start.X == End.X ? LineType.Vertical : Start.Y == End.Y ? LineType.Horizontal : LineType.Other;
        public LineDirection LineDirection => LineType != LineType.Other ? LineDirection.Other : Slope > 0 ? LineDirection.Positive : LineDirection.Negative;

        public Line PerpendicularLine => new Line(Center + new Vector2D(-DeltaY / 2, DeltaX / 2), Center + new Vector2D(DeltaY / 2, -DeltaX / 2));//new Line(Center, Center + new Vector2(-DeltaY, DeltaX));

        public Line(Vertex start, Vertex end)
        {
            Start = start;
            End = end;

            if (Start != LeftMostVertex)
            {
                SwapEndpoints();
            }
        }

        public Line(Vector2D start, Vector2D end)
        {
            Start = new Vertex(start.X, start.Y);
            End = new Vertex(end.X, end.Y);

            if (Start != LeftMostVertex)
            {
                SwapEndpoints();
            }
        }

        public override string ToString()
        {
            return $"({Start.X}, {Start.Y}) => ({End.X}, {End.Y})";
            //return $"Y = {Slope}X + {YIntercept}";
        }

        public Vector2D AsVector()
        {
            return new Vector2D(End.X - Start.X, End.Y - Start.Y);
        }

        public Vector2D GetPointByDistanceAlongLine(float distance)
        {
            return Start + distance * new Vector2D((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
        }

        public void Rotate(float radians)
        {
            Vector2D center = Center;
            Start.Translate(-center);
            Start.Rotate(radians);
            Start.Translate(center);

            End.Translate(-center);
            End.Rotate(radians);
            End.Translate(center);

            if (Start != LeftMostVertex)
            {
                SwapEndpoints();
            }
        }

        public void Translate(Vector2D change)
        {
            Start.Translate(change);
            End.Translate(change);

            if (Start != LeftMostVertex)
            {
                SwapEndpoints();
            }
        }


        public bool Intersects(Line line, bool includeEndpoints = false)
        {
            if (Slope == line.Slope)
            {
                if (YIntercept != line.YIntercept)
                {
                    return false;
                }

            }
            else
            {
                if (Start == line.Start || End == line.Start || Start == line.End || End == line.End)
                {
                    return includeEndpoints;
                }
            }

            Vector2D projectionLine = PerpendicularLine.AsVector();

            (float min, float max) first = ScalarProjectOntoVector(projectionLine);
            (float min, float max) second = line.ScalarProjectOntoVector(projectionLine);

            if (first.min > second.min)
            {
                (float min, float max) temp = second;
                second = first;
                first = temp;
            }

            if (first.max - second.min < 0)
            {
                return false;
            }

            projectionLine = line.PerpendicularLine.AsVector();

            first = ScalarProjectOntoVector(projectionLine);
            second = line.ScalarProjectOntoVector(projectionLine);

            if (first.min > second.min)
            {
                (float min, float max) temp = second;
                second = first;
                first = temp;
            }

            if (first.max - second.min < 0)
            {
                return false;
            }

            if (Slope == line.Slope && YIntercept == line.YIntercept)
            {

                //This means that they are the same line
                //We need to add this part of the check

                //throw new NotImplementedException();
            }
            float intersectionX = (YIntercept - line.YIntercept) / (line.Slope - Slope);
            float intersectionY = Slope * intersectionX + YIntercept;

            if (Start.X == intersectionX && Start.Y == intersectionY ||
                End.X == intersectionX && End.Y == intersectionY ||
                line.Start.X == intersectionX && line.Start.Y == intersectionY ||
                line.End.X == intersectionX && line.End.Y == intersectionY)
            {
                return includeEndpoints;
            }

            return true;
        }

        /// <summary>
        /// [Deprecated]
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool IntersectsOld(Line line)
        {
            return IntersectsOld(line, out _);
        }

        /// <summary>
        /// [Deprecated]
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool IntersectsOld(Line line, out Vector2D? point)
        {
            //1mx + 1b = 2mx + 2b
            //x = (2b - 1b)/(2m - 1m)
            //y = 1m(x) + 1b

            //--- && ---: false
            // |  &&  | : false
            //--- &&  | : we know x and y
            // |  && ---: we know x and y
            //--- && any: we know y1, find x
            //any && ---: we know y2, find x
            // |  && any: we know x1, find y
            //any &&  | : we know x2, find y
            //any && any: do math for both

            float intersectionX;
            float intersectionY;

            if (LineType == line.LineType && Slope == line.Slope)
            {
                point = null;

                var verticalThis = ScalarProjectOntoVector(new Vector2D(0, 1));
                var verticalOther = line.ScalarProjectOntoVector(new Vector2D(0, 1));

                var horizontalThis = ScalarProjectOntoVector(new Vector2D(1, 0));
                var horizontalOther = line.ScalarProjectOntoVector(new Vector2D(1, 0));

                if (verticalOther.min < verticalThis.min)
                {
                    var temp = verticalOther;
                    verticalOther = verticalThis;
                    verticalThis = temp;
                }

                if (horizontalOther.min < horizontalThis.min)
                {
                    var temp = horizontalOther;
                    horizontalOther = horizontalThis;
                    horizontalThis = temp;
                }

                if (horizontalThis.max < horizontalOther.min || verticalThis.max < verticalOther.min)
                {
                    return false;
                }

                return true;
            }
            else if (LineType == LineType.Horizontal && line.LineType == LineType.Vertical)
            {
                intersectionX = line.Start.X;
                intersectionY = Start.Y;
            }
            else if (LineType == LineType.Vertical && line.LineType == LineType.Horizontal)
            {
                intersectionX = Start.X;
                intersectionY = line.Start.Y;
            }
            else if (LineType == LineType.Horizontal && line.LineType == LineType.Other)
            {
                intersectionY = Start.Y;
                intersectionX = (intersectionY - line.YIntercept) / line.Slope;
            }
            else if (LineType == LineType.Other && line.LineType == LineType.Horizontal)
            {
                intersectionY = line.Start.Y;
                intersectionX = (intersectionY - YIntercept) / Slope;
            }
            else if (LineType == LineType.Vertical && line.LineType == LineType.Other)
            {
                intersectionX = Start.X;
                intersectionY = line.Slope * intersectionX + line.YIntercept;
            }
            else if (LineType == LineType.Other && line.LineType == LineType.Vertical)
            {
                intersectionX = line.Start.X;
                intersectionY = Slope * intersectionX + YIntercept;

            }
            else if (LineType == LineType.Other && line.LineType == LineType.Other)
            {
                intersectionX = (YIntercept - line.YIntercept) / (line.Slope - Slope);
                intersectionY = Slope * intersectionX + YIntercept;
            }
            else
            {
                throw new NotImplementedException();
            }

            point = new Vector2D(intersectionX, intersectionY);

            bool intersects = intersectionX < MaxX && intersectionX > MinX && intersectionY < MaxY && intersectionY > MinY
                && intersectionX < line.MaxX && intersectionX > line.MinX && intersectionY < line.MaxY && intersectionY > line.MinY;


            return intersects;
        }

        public void SwapEndpoints()
        {
            //Vertex temp = Start;
            //Start = End;
            //End = temp;
        }

        public float DistanceFromPoint(Vector2D point)
        {
            return (XValueAt(point.Y) - point.X) * (YValueAt(point.X) - point.Y) / Vector2D.Distance(new Vector2D(XValueAt(point.Y), point.Y), new Vector2D(point.X, YValueAt(point.X)));
        }

        public Line ProjectOntoLine(Line line)
        {
            Vector2D vectorToProjectOnto = line.AsVector();

            Vector2D firstPoint = Start;
            Vector2D secondPoint = End;

            Vector2D firstProjection = (float)(Vector2D.Dot(firstPoint, vectorToProjectOnto) / Math.Pow(vectorToProjectOnto.Length(), 2)) * vectorToProjectOnto;
            Vector2D secondProjection = (float)(Vector2D.Dot(secondPoint, vectorToProjectOnto) / Math.Pow(vectorToProjectOnto.Length(), 2)) * vectorToProjectOnto;

            Line projectionLine = new Line(firstProjection, secondProjection);

            return projectionLine;// (projectionLine, firstPoint, secondPoint, firstProjection, secondProjection);
        }

        public (float min, float max) ScalarProjectOntoLine(Line projectionLine) => ScalarProjectOntoVector(projectionLine.AsVector());
        public (float min, float max) ScalarProjectOntoVector(Vector2D projectionVector)
        {
            //There is no need to divide by the magnitude squared because that just scales it so 
            //that if you are visualizing it, it looks more accurate.
            //You can either do that every time or never do it and that basically just scales the whole check

            float first = Vector2D.Dot(projectionVector, Start);// / (float)Math.Pow(projectionVector.Length(), 2);

            float second = Vector2D.Dot(projectionVector, End);// / (float)Math.Pow(projectionVector.Length(), 2);

            return first < second ? (first, second) : (second, first);
        }

        public Vector2D ProjectPointOntoLine(Vector2D point)
        {
            Vector2D newVector = point - Start;
            float distance = Vector2D.Dot(newVector, AsVector()) / Length;
            return GetPointByDistanceAlongLine(distance);
        }

        public float YValueAt(float x)
        {
            if (LineType == LineType.Vertical)
            {
                return Start.Y;
            }

            return Slope * x + YIntercept;
        }
        public float XValueAt(float y)
        {
            if (LineType == LineType.Horizontal)
            {
                return Start.X;
            }

            return (y - YIntercept) / Slope;
        }

        public Rectangle AsFlatRectangle()
        {
            return new Rectangle((int)Start.X, (int)Start.Y, (int)Length, 1);
        }
        public static void DrawLine(Line line, Graphics graphics, Pen pen)
        {
            //spriteBatch.Draw(pixel, line.Start, null, color, line.Rotation, new Vector2(0, 0.5f), new Vector2(line.Length, 1), SpriteEffects.None, 0);
            graphics.DrawLine(pen, line.Start.X, line.Start.Y, line.End.X, line.End.Y);
        }


        public static Line operator +(Line line1, Line line2)
        {
            return new Line(line1.Start + line2.Start, line1.End + line2.End);
        }
        public static Line operator +(Line line1, Vector2D change)
        {
            return new Line(line1.Start + change, line1.End + change);
        }
        public static Line operator *(Line line, float f)
        {
            return new Line(line.Start * f, line.End * f);
        }

        public static float Dot(Line line1, Line line2)
        {
            return Vector2D.Dot(line1.AsVector(), line2.AsVector());
        }
    }
}

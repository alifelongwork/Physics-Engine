using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ImplementationsLibrary
{
    /// <summary>
    /// Polygon class for creating shapes based on vertices. Finds the center of the polygon based on the vertices.
    /// Has functions to perform transformations, initialize hit boxes, draw the shape on the screen, perform 
    /// concavity checks, and collision checks using the separating axis theorem. Creates optional lines for 
    /// showing normals, intersections, whether the polygon contains a point or the mouse pointer. Functions to 
    /// display projection of the object on to an edge or surface and the creation of triangles based on point 
    /// location to display the concept of triangular meshing.
    /// </summary>
    public class Polygon : ISATable, IScalarSATable
    {

        public Vertex[] Vertices { get; protected set; }
        public Line[] Edges { get; protected set; }
        private List<Line> projections = new List<Line>();
        private List<Line> normals = new List<Line>();
        public int EdgeToFocusOn = -1;
        public bool ShouldFocus = false;

        private Vector2D? centroid = null;
        public Vector2D Centroid
        {
            get
            {
                if (centroid == null)
                {
                    float x = 0;
                    float y = 0;
                    for (int i = 0; i < Vertices.Length; i++)
                    {
                        x += Vertices[i].X;
                        y += Vertices[i].Y;
                    }

                    x /= Vertices.Length;
                    y /= Vertices.Length;

                    centroid = new Vector2D(x, y);
                }

                return centroid.Value;
            }
            set
            {
                TranslateBy(value - Centroid);
            }
        }

        private float minX = float.PositiveInfinity;
        public float MinX
        {
            get
            {
                if (minX == float.PositiveInfinity)
                {
                    minX = float.MaxValue;
                    for (int i = 0; i < Vertices.Length; i++)
                    {
                        if (minX > Vertices[i].X)
                        {
                            minX = Vertices[i].X;
                        }
                    }
                }

                return minX;
            }
        }
        private float minY = float.PositiveInfinity;
        public float MinY
        {
            get
            {
                if (minY == float.PositiveInfinity)
                {
                    minY = float.MaxValue;
                    for (int i = 0; i < Vertices.Length; i++)
                    {
                        if (minY > Vertices[i].Y)
                        {
                            minY = Vertices[i].Y;
                        }
                    }
                }

                return minY;
            }
        }
        private float maxX = float.PositiveInfinity;
        public float MaxX
        {
            get
            {
                if (maxX == float.PositiveInfinity)
                {
                    maxX = float.MinValue;
                    for (int i = 0; i < Vertices.Length; i++)
                    {
                        if (maxX < Vertices[i].X)
                        {
                            maxX = Vertices[i].X;
                        }
                    }
                }

                return maxX;
            }
        }
        private float maxY = float.PositiveInfinity;
        public float MaxY
        {
            get
            {
                if (maxY == float.PositiveInfinity)
                {
                    maxY = float.MinValue;
                    for (int i = 0; i < Vertices.Length; i++)
                    {
                        if (maxY < Vertices[i].Y)
                        {
                            maxY = Vertices[i].Y;
                        }
                    }
                }

                return maxY;
            }
        }

        public float Height => MaxY - MinY;
        public float Width => MaxX - MinX;

        private Vertex smallestVertex = null;
        public Vertex SmallestVertex
        {
            get
            {
                if (smallestVertex == null)
                {
                    smallestVertex = ClosestVertexToPoint(new Vector2D(float.MinValue));
                }
                return smallestVertex;
            }
        }

        public float Rotation { get; private set; }

        private Rectangle? aabb = null;
        public Rectangle AABB
        {
            get
            {
                if (aabb == null)
                {
                    aabb = new Rectangle((int)MinX, (int)MinY, (int)(MaxX - MinX), (int)(MaxY - MinY));
                }

                return aabb.Value;
            }
        }

        private float boundingSphereRadius = float.PositiveInfinity;
        public float BoundingSphereRadius
        {
            get
            {
                if (boundingSphereRadius == float.PositiveInfinity)
                {
                    float max = 0;

                    foreach (Vertex vertex in Vertices)
                    {
                        if (max < Vector2D.Distance(Centroid, vertex))
                        {
                            max = Vector2D.Distance(Centroid, vertex);
                        }
                    }

                    boundingSphereRadius = max;
                }

                return boundingSphereRadius;
            }
        }

        public bool ShowAABB { get; set; } = false;
        public bool ShowBoundingSphere { get; set; } = false;
        public bool ShowSAT { get; set; } = false;
        public bool ShowNormals { get; set; } = false;
        public bool ShowVertices { get; set; } = true;


        private bool? isConvex = null;
        public bool IsConvex
        {
            get
            {
                if (isConvex == null)
                {
                    CheckConcavity();
                }
                return isConvex.Value;
            }
        }

        public bool IsValidPolygon
        {
            get
            {
                for (int i = 0; i < Edges.Length; i++)
                {
                    for (int j = 0; j < Edges.Length; j++)
                    {
                        if (i != j && Edges[i].Intersects(Edges[j]))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public Brush DefaultBrush { get; set; } = Brushes.Black;
        public Brush Brush { get; set; } = Brushes.Black;

        public Pen DefaultPen { get; set; } = Pens.Black;
        public Pen Pen { get; set; } = Pens.Black;

        public Polygon(params Vertex[] vertices)
        {
            Vertices = vertices;
            Edges = new Line[Vertices.Length];

            for (int i = 0; i < Edges.Length; i++)
            {
                int j = (i + 1) % Edges.Length;

                Edges[i] = new Line(Vertices[i], Vertices[j]);
            }

            CheckConcavity();
        }
        public static Polygon CreateRegularPolygon(Brush brush, Pen pen, int numberOfSides, float radius, Point position)
            => CreateRegularPolygon(brush, pen, numberOfSides, radius, position.X, position.Y);
        public static Polygon CreateRegularPolygon(Brush brush, Pen pen, int numberOfSides, float radius, Vector2D position)
            => CreateRegularPolygon(brush, pen, numberOfSides, radius, position.X, position.Y);
        public static Polygon CreateRegularPolygon(Brush brush, Pen pen, int numberOfSides, float radius, float x, float y)
        {
            Vertex[] vertices = new Vertex[numberOfSides];

            for (int i = 0; i < numberOfSides; i++)
            {
                vertices[i] = new Vertex(x + radius * (float)Math.Cos(i * (Math.PI * 2 / numberOfSides) - Math.PI / 2), y + radius * (float)Math.Sin(i * (Math.PI * 2 / numberOfSides) - Math.PI / 2));
            }
            return new Polygon(vertices) { DefaultBrush = brush, DefaultPen = pen, Brush = brush, Pen = pen };
        }

        private void ResetDueToTranslation()
        {
            centroid = null;
            minX = float.PositiveInfinity;
            minY = float.PositiveInfinity;
            maxX = float.PositiveInfinity;
            maxY = float.PositiveInfinity;
            aabb = null;
            smallestVertex = null;
        }
        private void ResetDueToRotation()
        {
            minX = float.PositiveInfinity;
            minY = float.PositiveInfinity;
            maxX = float.PositiveInfinity;
            maxY = float.PositiveInfinity;
            aabb = null;
            smallestVertex = null;
        }
        private void ResetDueToVertexUpdate()
        {
            centroid = null;
            minX = float.PositiveInfinity;
            minY = float.PositiveInfinity;
            maxX = float.PositiveInfinity;
            maxY = float.PositiveInfinity;
            aabb = null;
            smallestVertex = null;
            isConvex = null;
        }

        public void CheckConcavity()
        {
            isConvex = CheckConcavity(Edges.ToList());
        }

        private bool CheckConcavity(List<Line> edges)
        {
            bool isConvexForwards = true;
            bool isConvexBackwards = true;
            for (int i = 0; i < edges.Count; i++)
            {
                int j = (i + 1) % edges.Count;

                if (AngleBetweenEdges(Edges[j], Edges[i]) < Math.PI)
                {
                    isConvexForwards = false;
                }

                if (AngleBetweenEdges(Edges[j], Edges[i]) > Math.PI)
                {
                    isConvexBackwards = false;
                }
            }


            return isConvexForwards || isConvexBackwards;

        }


        public void TranslateBy(Vector2D deltaVector) => TranslateBy(deltaVector.X, deltaVector.Y);
        public void TranslateBy(float deltaX, float deltaY)
        {
            ResetDueToTranslation();
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i].Translate(deltaX, deltaY);
            }
        }
        public void TranslateVertexBy(Vector2D deltaVector, int index) => TranslateVertexBy(deltaVector.X, deltaVector.Y, index);
        public void TranslateVertexBy(float deltaX, float deltaY, int index)
        {
            ResetDueToVertexUpdate();
            Vertices[index].Translate(deltaX, deltaY);
        }
        public void RotateTo(float radians)
        {
            RotateBy(radians - Rotation);
        }
        public void RotateBy(float radians)
        {
            //shift points to origin relative to centroid
            //rotate
            //shift back
            ResetDueToRotation();

            for (int i = 0; i < Vertices.Length; i++)
            {
                //shift to origin by subtracting centroid
                //rotate point by theta
                //shift back by adding centroid

                Vertices[i].Translate(-Centroid);

                Vertices[i].Rotate(radians);

                Vertices[i].Translate(Centroid);
            }

            Rotation += radians;
        }

        public Vertex ClosestVertexToPoint(Vector2D point)
        {

            Vertex min = Vertices[0];
            float minDistance = Vector2D.Distance(point, min);

            for (int i = 1; i < Vertices.Length; i++)
            {
                float newDistance = Vector2D.Distance(point, Vertices[i]);
                if (minDistance > newDistance)
                {
                    min = Vertices[i];
                    minDistance = newDistance;
                }
            }

            return min;
        }

        public bool Contains(Vector2D point)
        {
            Line horizontal = new Line(new Vertex(MinX - 1, point.Y), new Vertex(point.X, point.Y));

            int count = 0;
            foreach (Line edge in Edges)
            {
                if (edge.Intersects(horizontal))
                {
                    count++;
                }
            }

            return count % 2 == 1;
        }
        public bool Contains(Point point) => Contains(new Vector2D(point.X, point.Y));
        public bool Contains(float x, float y) => Contains(new Vector2D(x, y));

        public bool Contains(Polygon other)
        {
            for (int i = 0; i < Edges.Length; i++)
            {
                for (int j = 0; j < other.Edges.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (Edges[i].Intersects(other.Edges[j]))
                    {
                        return false;
                    }
                }
            }


            for (int i = 0; i < other.Vertices.Length; i++)
            {
                if (!Contains(other.Vertices[i].X, other.Vertices[i].Y))
                {
                    return false;
                }
            }

            return true;
        }

        public bool Intersects(Polygon other, bool scalarMethod = false)
        {
            if (!IsConvex || !other.IsConvex)
            {
                throw new NotImplementedException();
            }

            if (!AABB.IntersectsWith(other.AABB))
            {
                return false;
            }

            if (!scalarMethod)
            {
                return SeparatingAxisTheorem(other);
            }
            else
            {
                return SeparatingAxisTheoremScalar(other);
            }
        }

        public bool SeparatingAxisTheoremWithSimulation(Polygon other)
        {
            projections.Clear();
            normals.Clear();

            bool doesNotIntersect = false;

            for (int i = 0; i < Edges.Length; i++)
            {
                Line perpendicularToProjectOnto = Edges[i].PerpendicularLine;

                Line thisProjection = ProjectOntoLine(perpendicularToProjectOnto, true);
                Line otherProjection = other.ProjectOntoLine(perpendicularToProjectOnto, true);
                if (EdgeToFocusOn == i || !ShouldFocus)
                {
                    normals.Add(perpendicularToProjectOnto);
                    projections.Add(thisProjection);
                    projections.Add(otherProjection);
                }

                if (!thisProjection.Intersects(otherProjection))
                {
                    doesNotIntersect = true;
                }
                else if (!otherProjection.Intersects(thisProjection))
                {

                }
            }

            for (int i = 0; i < other.Edges.Length; i++)
            {
                Line perpendicularToProjectOnto = other.Edges[i].PerpendicularLine;

                Line thisProjection = ProjectOntoLine(perpendicularToProjectOnto, true);
                Line otherProjection = other.ProjectOntoLine(perpendicularToProjectOnto, true);

                if (other.EdgeToFocusOn == i || !ShouldFocus)
                {
                    normals.Add(perpendicularToProjectOnto);
                    projections.Add(thisProjection);
                    projections.Add(otherProjection);
                }

                if (!thisProjection.Intersects(otherProjection))
                {
                    doesNotIntersect = true;
                }
                else if (!otherProjection.Intersects(thisProjection))
                {

                }
            }

            return !doesNotIntersect;
        }
        public bool SeparatingAxisTheorem(ISATable other) => SATHelper.SeparatingAxisTheorem(this, other);
        public (bool, ((float min, float max), (float min, float max))[]) SeparatingAxisTheoremScalarWithSimulation(Polygon other)
        {
            List<((float min, float max), (float min, float max))> projections = new List<((float min, float max), (float min, float max))>();
            bool result = true;

            for (int i = 0; i < Edges.Length; i++)
            {
                Line perpendicularToProjectOnto = Edges[i].PerpendicularLine;

                (float min, float max) first = ProjectOntoLineScalar(perpendicularToProjectOnto);
                (float min, float max) second = other.ProjectOntoLineScalar(perpendicularToProjectOnto);
                projections.Add((first, second));

                if (first.min > second.min)
                {
                    var temp = first;
                    first = second;
                    second = temp;
                }

                if (first.max < second.min)
                {
                    result = false;
                }
            }

            for (int i = 0; i < other.Edges.Length; i++)
            {
                Line perpendicularToProjectOnto = other.Edges[i].PerpendicularLine;

                (float min, float max) first = ProjectOntoLineScalar(perpendicularToProjectOnto);
                (float min, float max) second = other.ProjectOntoLineScalar(perpendicularToProjectOnto);
                projections.Add((first, second));

                if (first.min > second.min)
                {
                    var temp = first;
                    first = second;
                    second = temp;
                }

                if (first.max < second.min)
                {
                    result = false;
                }
            }

            return (result, projections.ToArray());
        }
        public bool SeparatingAxisTheoremScalar(IScalarSATable other) => SATHelper.SeparatingAxisTheoremScalar(this, other);
        public Polygon[] Triangulation()
        {
            if (!IsValidPolygon)
            {
                return new Polygon[] { };
            }

            if (IsConvex)
            {
                return ConvexTriangulation(Vertices.ToList()).ToArray();
            }
            else
            {
                return ConcaveTriangulation(Vertices.ToList(), Edges.ToList()).ToArray();
            }


        }

        private List<Polygon> ConvexTriangulation(List<Vertex> vertices)
        {
            List<Polygon> polygons = new List<Polygon>();
            for (int i = 0; i < Vertices.Length; i += 3)
            {
                int j = (i + 1) % Vertices.Length;
                int k = (i + 2) % Vertices.Length;

                polygons.Add(new Polygon(Vertices[i], Vertices[j], Vertices[k]));
            }
            return polygons;
        }

        private List<Polygon> ConcaveTriangulation(List<Vertex> vertices, List<Line> edges)
        {
            List<Polygon> polygons = new List<Polygon>();

            for (int counter = 0; vertices.Count > 3; counter++)
            {
                if (counter > vertices.Count)
                {
                    //polygons.AddRange(ConvexTriangulation(vertices));
                    //polygons.AddRange(new Polygon(vertices.ToArray()).Triangulation());
                    //return polygons;
                }

                int i = (counter - 1 + vertices.Count) % vertices.Count;
                int j = (counter + 0) % vertices.Count;
                int k = (counter + 1) % vertices.Count;

                Line first = new Line(vertices[i], vertices[j]);
                Line second = new Line(vertices[j], vertices[k]);

                if (AngleBetweenEdges(first, second) > Math.PI)
                {
                    continue;
                }
                Line newLine = new Line(vertices[i], vertices[k]);
                bool shouldContinue = false;
                foreach (Line line in edges)
                {
                    if (newLine.Intersects(line))
                    {
                        shouldContinue = true;
                        break;
                    }
                }
                if (shouldContinue)
                {
                    continue;
                }

                polygons.Add(new Polygon(vertices[i], vertices[j], vertices[k]));
                edges.Add(newLine);
                vertices.RemoveAt(j);
                //counter--;                                                                                               
            }

            return polygons;
        }

        public static float AngleBetweenEdges(Line line1, Line line2)
        {
            return (float)((line2.Rotation - line1.Rotation + Math.PI * 2) % (Math.PI * 2));
            //return line2.Rotation - line1.Rotation;
            //return (float)Math.Acos(Line.Dot(line1, line2) / (line1.Length * line2.Length));
            //return (float)Math.Atan((line2.Slope - line1.Slope) / (1 + line1.Slope * line2.Slope));
        }

        public void Draw(Graphics graphics)
        {
            for (int i = 0; i < Vertices.Length; i++)
            {
                //spriteBatch.Draw(circle, Vertices[i], null, Color, 0, new Vector2(circle.Width / 2f), 1, SpriteEffects.None, 0);

                if (ShowVertices)
                {
                    graphics.FillEllipse(Brush, Vertices[i].AsRectangle());
                }
                //int j = (i + 1) % Vertices.Length;
                //spriteBatch.Draw(pixel, Vertices[i], null, Color, AngleBetweenVertices(Vertices[i], Vertices[j]), new Vector2(0, 0.5f), new Vector2(DistanceBetweenVertices(Vertices[i], Vertices[j]), 1), SpriteEffects.None, 0);

                Line.DrawLine(Edges[i], graphics, Pen);
            }
            if (ShowAABB)
            {
                //Vector2D topLeft = new Vector2D(MinX, MinY);
                //Vector2D topRight = new Vector2D(MaxX, MinY);
                //Vector2D bottomLeft = new Vector2D(MinX, MaxY);
                //Vector2D bottomRight = new Vector2D(MaxX, MaxY);

                //spriteBatch.Draw(pixel, topLeft, null, Color, AngleBetweenVertices(topLeft, topRight), new Vector2(0, 0.5f), new Vector2(DistanceBetweenVertices(topLeft, topRight), 1), SpriteEffects.None, 0);
                //spriteBatch.Draw(pixel, topRight, null, Color, AngleBetweenVertices(topRight, bottomRight), new Vector2(0, 0.5f), new Vector2(DistanceBetweenVertices(topRight, bottomRight), 1), SpriteEffects.None, 0);
                //spriteBatch.Draw(pixel, bottomRight, null, Color, AngleBetweenVertices(bottomRight, bottomLeft), new Vector2(0, 0.5f), new Vector2(DistanceBetweenVertices(bottomRight, bottomLeft), 1), SpriteEffects.None, 0);
                //spriteBatch.Draw(pixel, bottomLeft, null, Color, AngleBetweenVertices(bottomLeft, topLeft), new Vector2(0, 0.5f), new Vector2(DistanceBetweenVertices(bottomLeft, topLeft), 1), SpriteEffects.None, 0);

                graphics.DrawRectangle(Pen, AABB.X, AABB.Y, AABB.Width, AABB.Height);
            }

            if (ShouldFocus)
            {
                for (int i = 0; i < normals.Count; i++)
                {
                    Line.DrawLine(normals[i], graphics, Pens.Gray);
                }
                for (int i = 0; i < projections.Count; i += 2)
                {
                    Line.DrawLine(projections[i], graphics, Pens.Yellow);
                    if (ShowVertices)
                    {
                        graphics.FillEllipse(Brushes.Yellow, projections[i].Start.AsRectangle());
                        graphics.FillEllipse(Brushes.Yellow, projections[i].End.AsRectangle());
                    }
                    //spriteBatch.Draw(circle, projections[i].Start, null, Brush.Yellow, 0, new Vector2(circle.Width / 2f), 0.5f, SpriteEffects.None, 0);
                    //spriteBatch.Draw(circle, projections[i].End, null, Brush.Yellow, 0, new Vector2(circle.Width / 2f), 0.5f, SpriteEffects.None, 0);

                    Line.DrawLine(projections[i + 1], graphics, Pens.Pink);
                    if (ShowVertices)
                    {
                        graphics.FillEllipse(Brushes.Pink, projections[i + 1].Start.AsRectangle());
                        graphics.FillEllipse(Brushes.Pink, projections[i + 1].End.AsRectangle());
                    }
                    //spriteBatch.Draw(circle, projections[i + 1].Start, null, Brush.Pink, 0, new Vector2(circle.Width / 2f), 0.5f, SpriteEffects.None, 0);
                    //spriteBatch.Draw(circle, projections[i + 1].End, null, Brush.Pink, 0, new Vector2(circle.Width / 2f), 0.5f, SpriteEffects.None, 0);

                }
            }

            if (ShowNormals)
            {
                for (int i = 0; i < normals.Count; i++)
                {
                    Line.DrawLine(normals[i], graphics, Pens.Gray);
                }
            }
            if (ShowSAT)
            {
                //for (int i = 0; i < projections.Count; i += 2)
                //{
                //    Line.DrawLine(projections[i], spriteBatch, pixel, Brush.Yellow);

                //    spriteBatch.Draw(circle, projections[i].Start, null, Brush.Yellow, 0, new Vector2(circle.Width / 2f), 0.5f, SpriteEffects.None, 0);
                //    spriteBatch.Draw(circle, projections[i].End, null, Brush.Yellow, 0, new Vector2(circle.Width / 2f), 0.5f, SpriteEffects.None, 0);

                //    Line.DrawLine(projections[i + 1], spriteBatch, pixel, Brush.Pink);
                //    spriteBatch.Draw(circle, projections[i + 1].Start, null, Brush.Pink, 0, new Vector2(circle.Width / 2f), 0.5f, SpriteEffects.None, 0);
                //    spriteBatch.Draw(circle, projections[i + 1].End, null, Brush.Pink, 0, new Vector2(circle.Width / 2f), 0.5f, SpriteEffects.None, 0);
                //}

                for (int i = 0; i < projections.Count; i += 2)
                {
                    Line.DrawLine(projections[i], graphics, Pens.Yellow);
                    if (ShowVertices)
                    {
                        graphics.FillEllipse(Brushes.Yellow, projections[i].Start.AsRectangle());
                        graphics.FillEllipse(Brushes.Yellow, projections[i].End.AsRectangle());
                    }
                    //spriteBatch.Draw(circle, projections[i].Start, null, Brush.Yellow, 0, new Vector2(circle.Width / 2f), 0.5f, SpriteEffects.None, 0);
                    //spriteBatch.Draw(circle, projections[i].End, null, Brush.Yellow, 0, new Vector2(circle.Width / 2f), 0.5f, SpriteEffects.None, 0);

                    Line.DrawLine(projections[i + 1], graphics, Pens.Pink);
                    if (ShowVertices)
                    {
                        graphics.FillEllipse(Brushes.Pink, projections[i + 1].Start.AsRectangle());
                        graphics.FillEllipse(Brushes.Pink, projections[i + 1].End.AsRectangle());
                    }
                    //spriteBatch.Draw(circle, projections[i + 1].Start, null, Brush.Pink, 0, new Vector2(circle.Width / 2f), 0.5f, SpriteEffects.None, 0);
                    //spriteBatch.Draw(circle, projections[i + 1].End, null, Brush.Pink, 0, new Vector2(circle.Width / 2f), 0.5f, SpriteEffects.None, 0);

                }
            }

        }



        public Line ProjectOntoLine(Line line) => ProjectOntoLine(line, true);
        public Line ProjectOntoLine(Line line, bool realign)
        {
            Line[] projections = ProjectOntoLineSeparate(line, false);

            Vector2D small = new Vector2D(float.MaxValue);
            Vector2D big = new Vector2D(float.MinValue);

            if (line.LineType == LineType.Vertical)
            {
                for (int i = 0; i < projections.Length; i++)
                {
                    if (projections[i].MinY < small.Y)
                    {
                        small = projections[i].TopMostVertex;
                    }
                    if (projections[i].MaxY > big.Y)
                    {
                        big = projections[i].BottomMostVertex;
                    }
                }
            }
            else
            {
                for (int i = 0; i < projections.Length; i++)
                {
                    if (projections[i].MinX < small.X)
                    {
                        small = projections[i].LeftMostVertex;
                    }
                    if (projections[i].MaxX > big.X)
                    {
                        big = projections[i].RightMostVertex;
                    }
                }
            }

            Line projection = new Line(small, big);

            if (realign)
            {
                Vector2D tempLine = line.LineType != LineType.Vertical ? line.LeftMostVertex : line.TopMostVertex;
                Vector2D tempProjection = projection.LineType != LineType.Vertical ? projection.LeftMostVertex : projection.TopMostVertex;
                Vector2D tempProjection2 = projection.LineType != LineType.Vertical ? projection.RightMostVertex : projection.BottomMostVertex;
                Vector2D pointToCenterOn = tempLine - (line.Center - Centroid);
                projection.Translate(line.ProjectPointOntoLine(ClosestVertexToPoint(pointToCenterOn) - small));
            }

            return projection;
        }
        public Line[] ProjectOntoLineSeparate(Line line, bool realign = true)
        {
            Line[] projections = new Line[Edges.Length];

            if (realign)
            {
                Vector2D averageCenter = Vector2D.Zero;
                for (int i = 0; i < Edges.Length; i++)
                {
                    projections[i] = Edges[i].ProjectOntoLine(line);
                    averageCenter += projections[i].Center;
                }
                averageCenter /= Edges.Length;

                for (int i = 0; i < projections.Length; i++)
                {
                    projections[i].Translate(line.Center - averageCenter);
                }
            }
            else
            {
                for (int i = 0; i < Edges.Length; i++)
                {
                    projections[i] = Edges[i].ProjectOntoLine(line);
                }
            }

            return projections;
        }

        public (float min, float max) ProjectOntoLineScalar(Line line)
        {
            (float min, float max)[] projections = ScalarProjectOntoLineSeparate(line);

            float min = float.MaxValue;
            float max = float.MinValue;

            for (int i = 0; i < projections.Length; i++)
            {
                if (projections[i].min < min)
                {
                    min = projections[i].min;
                }
                if (projections[i].max > max)
                {
                    max = projections[i].max;
                }
            }
            return (min, max);
        }
        public (float min, float max)[] ScalarProjectOntoLineSeparate(Line line)
        {
            (float min, float max)[] projections = new (float min, float max)[Edges.Length];

            for (int i = 0; i < Edges.Length; i++)
            {
                projections[i] = Edges[i].ScalarProjectOntoLine(line);
            }

            return projections;
        }
        public IEnumerable<Line> GetProjectionLines()
        {
            List<Line> lines = new List<Line>();
            foreach (Line line in Edges)
            {
                lines.Add(line.PerpendicularLine);
            }
            return lines;
        }
    }
}

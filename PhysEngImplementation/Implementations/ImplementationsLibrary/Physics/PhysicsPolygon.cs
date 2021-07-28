using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationsLibrary
{
    public class PhysicsPolygon : Polygon
    {
        private Vector2D velocity;
        public ref Vector2D Velocity => ref velocity;

        public bool IsStatic { get; private set; }
        public bool IsOnPlatform { get; set; } = false;

        public PhysicsPolygon(params Vertex[] vertices)
            : this(false, vertices)
        {

        }
        public PhysicsPolygon(bool isStatic, params Vertex[] vertices)
            : base(vertices)
        {
            IsStatic = isStatic;
        }
        public PhysicsPolygon(Polygon polygon)
            :this (polygon, false)
        {

        }
        public PhysicsPolygon(Polygon polygon, bool isStatic)
        {
            Vertices = polygon.Vertices;
            Edges = polygon.Edges;
            IsStatic = isStatic;
        }
    }
}

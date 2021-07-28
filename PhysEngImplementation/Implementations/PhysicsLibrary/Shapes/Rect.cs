using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsLibrary
{
    public class Rect : Polygon
    {
        public Rect(UnitF x, UnitF y, UnitF width, UnitF height)
            : base (new Vertex(x, y), new Vertex(x + width, y), new Vertex(x + width, y + height), new Vertex(x, y + height))
        {

        }
        public Rect(Vector2D position, Vector2D size)
            : this (position.X, position.Y, size.X, size.Y)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhysicsLibrary
{
    public class PhysicsPoint
    {
        private Vector2D velocity;
        public ref Vector2D Velocity => ref velocity;

        private Vector2D position;
        public ref Vector2D Position => ref position;

        public PhysicsPoint(Vector2D position, Vector2D startingVelocity)
        {
            Position = position;
            Velocity = startingVelocity;
        }
    }

    public class PhysicsObject
    {
        public Vector2D Position
        {
            get
            {
                return Bounds.Centroid;
            }
            internal set
            {
                Bounds.TranslateBy(value - Bounds.Centroid);
            }
        }
        private Vector2D velocity;
        public ref Vector2D Velocity => ref velocity;

        private Vector2D accaleration;
        public ref Vector2D Acceleration => ref accaleration;

        public float Mass { get; private set; }

        public Polygon Bounds { get; private set; }

        public bool IsStatic { get; private set; }

        public PhysicsObject(Vector2D position, float mass, Polygon bounds, bool isStatic)
        {
            Bounds = bounds;
            Position = position;
            Mass = mass;
            IsStatic = isStatic;
            Velocity = Vector2D.ZeroSI;
            Acceleration = Vector2D.ZeroSI;
        }
    }
}

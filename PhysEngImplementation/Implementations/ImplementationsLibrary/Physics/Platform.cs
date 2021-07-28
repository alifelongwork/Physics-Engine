using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationsLibrary
{
    [Flags]
    public enum DirectionOfCollision
    {
        None = 0,
        Above = 1,
        Below = 2,
        Left = 4,
        Right = 8
    }
    public class Platform
    {
        public RectangleF Hitbox { get; private set; }

        public Platform(float x, float y, float width, float height)
        {
            Hitbox = new RectangleF(x, y, width, height);
        }

        public DirectionOfCollision HasCollided(PhysicsPolygon polygon)
        {
            if (!Hitbox.IntersectsWith(polygon.AABB))
            {
                return DirectionOfCollision.None;
            }

            DirectionOfCollision returnDirection = DirectionOfCollision.None; 
            polygon.Centroid -= new Vector2D(0, Math.Abs(polygon.Velocity.Y));
            if (!Hitbox.IntersectsWith(polygon.AABB) && polygon.Velocity.Y > 0)
            {
                returnDirection |= DirectionOfCollision.Above;
            }
            polygon.Centroid += new Vector2D(0,2* Math.Abs(polygon.Velocity.Y));
            if (!Hitbox.IntersectsWith(polygon.AABB) && polygon.Velocity.Y < 0)
            {
                returnDirection |= DirectionOfCollision.Below;
            }
            polygon.Centroid -= new Vector2D(Math.Abs(polygon.Velocity.X), Math.Abs(polygon.Velocity.Y));
            if (!Hitbox.IntersectsWith(polygon.AABB) && polygon.Velocity.X > 0)
            {
                returnDirection |= DirectionOfCollision.Left;
            }
            polygon.Centroid += new Vector2D(2 * Math.Abs(polygon.Velocity.X), 0);
            if (!Hitbox.IntersectsWith(polygon.AABB) && polygon.Velocity.X < 0)
            {
                returnDirection |= DirectionOfCollision.Right;
            }
            polygon.Centroid -= new Vector2D(Math.Abs(polygon.Velocity.X), 0);

            return returnDirection;
        }
    }
}

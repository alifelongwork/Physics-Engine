using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationsLibrary
{
    /// <summary>
    /// This does not work
    /// Do NOT use this class
    /// </summary>
    public class PhysicsHelper
    {
        public static PhysicsHelper Instance { get; private set; } = new PhysicsHelper();

        public Vector2D Gravity { get; private set; }
        public Vector2D VelocitySlop { get; private set; }
        public float DampeningOnJump { get; private set; }
        public float DampeningOnGlide { get; private set; }

        public List<PhysicsPolygon> Objects { get; }

        private PhysicsHelper()
        {
            SetGravity(new Vector2D(0, 0.5f));
            SetDampeningOnJump(0.5f);
            SetVelocitySlop(Gravity * 3);
            SetDampeningOnGlide(0.5f);
            Objects = new List<PhysicsPolygon>();
        }

        public void SetGravity(Vector2D gravity)
        {
            Gravity = gravity;
        }
        public void SetVelocitySlop(Vector2D slop)
        {
            VelocitySlop = slop;
        }
        public void SetDampeningOnJump(float dampening)
        {
            DampeningOnJump = dampening;
        }
        public void SetDampeningOnGlide(float dampening)
        {
            DampeningOnGlide = dampening;
        }

        public void AddObject(PhysicsPolygon polygon)
        {
            Objects.Add(polygon);
        }

        public void Update()
        {
            foreach (PhysicsPolygon obj in Objects)
            {
                if (!obj.IsStatic)
                {
                    /*if (obj.IsOnGround)
                    {
                        obj.Velocity += Gravity * Vector2D.UnitX;
                    }
                    else
                    {
                        obj.Velocity += Gravity;
                    }*/
                }
                foreach (PhysicsPolygon other in Objects)
                {
                    if (obj == other)
                    {
                        continue;
                    }

                    if (obj.AABB.IntersectsWith(other.AABB))
                    {
                        obj.TranslateBy(-obj.Velocity * Vector2D.UnitY);
                        bool onYAxisBottom = !obj.AABB.IntersectsWith(other.AABB);
                        obj.TranslateBy(2 * obj.Velocity * Vector2D.UnitY);
                        bool onYAxisTop = !obj.AABB.IntersectsWith(other.AABB);
                        obj.TranslateBy(-obj.Velocity * Vector2D.UnitY);

                        if (onYAxisTop ^ onYAxisBottom)
                        {
                            obj.Velocity.Y *= -DampeningOnJump;
                            if (Math.Abs(obj.Velocity.X) <= VelocitySlop.X)
                            {
                                obj.Velocity.X = 0;
                            }
                            if (Math.Abs(obj.Velocity.Y) <= VelocitySlop.Y)
                            {
                                obj.Velocity.Y = 0;
                                //obj.IsOnGround = true;
                            }
                        }
                        else
                        {
                            obj.Velocity.X *= -1;
                        }
                    }
                }
                if (!obj.IsStatic)
                {
                    obj.Centroid += obj.Velocity;
                }
            }
        }
    }
}

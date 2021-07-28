using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhysicsLibrary
{
    [Flags]
    public enum DirectionOfCollision
    {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8
    }

    public sealed class PhysicsHelper
    {
        public Vector2D Gravity { get; private set; }
        public float ConversionFactor { get; private set; }
        public float VelocityDecayOnCollision { get; private set; }

        private static PhysicsHelper instance;
        public static PhysicsHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PhysicsHelper();

                    instance.SetData(new Vector2D(0, 9.81f, false), 0.05f, 0.9f);
                    instance.PhysicsObjects = new List<PhysicsObject>();
                }
                return instance;
            }
        }

        public List<PhysicsObject> PhysicsObjects { get; private set; }
        public List<PhysicsObject> StaticObject => PhysicsObjects.FindAll((po) => po.IsStatic == true);
        public List<PhysicsObject> DynamicObjects => PhysicsObjects.FindAll((po) => po.IsStatic == false);

        private PhysicsHelper()
        {
        }
        static PhysicsHelper()
        {

        }

        public void SetData(Vector2D gravity, float conversionFactor, float velocityDecayOnCollision)
        {
            ConversionFactor = conversionFactor;
            gravity.X.ConversionFactor = conversionFactor;
            gravity.Y.ConversionFactor = conversionFactor;
            Gravity = gravity;
            VelocityDecayOnCollision = velocityDecayOnCollision;
        }

        public void AddObject(PhysicsObject newObject)
        {
            PhysicsObjects.Add(newObject);
        }

        public void UpdateAll()
        {
            List<PhysicsObject> dynamics = DynamicObjects;
            foreach (PhysicsObject obj in dynamics)
            {
                obj.Acceleration = Gravity;
                obj.Velocity += obj.Acceleration;
                obj.Position += obj.Velocity;

                foreach (PhysicsObject other in PhysicsObjects)
                {
                    if (other.Equals(obj))
                    {
                        continue;
                    }
                    if (other.Bounds.Intersects(obj.Bounds))
                    {
                        obj.Position -= obj.Velocity;
                        //direction of velocities
                        float direction = other.Bounds.Rotation - obj.Velocity.Angle;
                        //magnitude
                        UnitF magnitude = obj.Velocity.Length() + other.Velocity.Length();
                        //new velocity vector
                        Vector2D newVelocity = new Vector2D(magnitude * (float)Math.Cos(direction),magnitude * (float)Math.Sin(direction));
                        //apply decay
                        newVelocity *= VelocityDecayOnCollision;


                        obj.Velocity = newVelocity;
                        //obj.Position += obj.Velocity;

                        /*DirectionOfCollision objDirection = FindDirectionOfCollision(obj, other);
                        DirectionOfCollision otherDirection = FindDirectionOfCollision(other, obj);

                        if ((objDirection & DirectionOfCollision.Up) == DirectionOfCollision.Up ^
                            (objDirection & DirectionOfCollision.Down) == DirectionOfCollision.Down)

                        {
                            // *= new Vector2D(1, -1, obj.Velocity.X.UsingPixel);
                            obj.Velocity.Y *= -VelocityDecayOnCollision;//obj.Velocity.FlipY();
                        }
                        if ((objDirection & DirectionOfCollision.Left) == DirectionOfCollision.Left ^
                            (objDirection & DirectionOfCollision.Right) == DirectionOfCollision.Right)
                        {
                            // *= new Vector2D(-1, 1, obj.Velocity.X.UsingPixel);
                            obj.Velocity.X *= -VelocityDecayOnCollision;
                        }

                        if ((otherDirection & DirectionOfCollision.Up) == DirectionOfCollision.Up ^
                            (otherDirection & DirectionOfCollision.Down) == DirectionOfCollision.Down)

                        {
                            other.Velocity.Y *= -VelocityDecayOnCollision;// *= new Vector2D(1, -1, obj.Velocity.X.UsingPixel);
                        }
                        if ((otherDirection & DirectionOfCollision.Left) == DirectionOfCollision.Left ^
                            (otherDirection & DirectionOfCollision.Right) == DirectionOfCollision.Right)
                        {
                            other.Velocity *= -VelocityDecayOnCollision;//  *= new Vector2D(-1, 1, obj.Velocity.X.UsingPixel);
                        }

                        obj.Position += obj.Velocity;
                        other.Position += other.Velocity;*/
                    }
                }
            }
        }

        private DirectionOfCollision FindDirectionOfCollision(PhysicsObject dynamicObj, PhysicsObject staticObj)
        {
            DirectionOfCollision returnValue = DirectionOfCollision.None;

            Vector2D velocityUp = dynamicObj.Velocity * -Vector2D.UnitY;
            Vector2D velocityDown = dynamicObj.Velocity * Vector2D.UnitY;
            Vector2D velocityLeft = dynamicObj.Velocity * -Vector2D.UnitX;
            Vector2D velocityRight = dynamicObj.Velocity * Vector2D.UnitX;

            dynamicObj.Position += velocityUp;
            if (dynamicObj.Bounds.Intersects(staticObj.Bounds))
            {
                returnValue |= DirectionOfCollision.Up;
            }
            dynamicObj.Position -= velocityUp;


            dynamicObj.Position += velocityDown;
            if (dynamicObj.Bounds.Intersects(staticObj.Bounds))
            {
                returnValue |= DirectionOfCollision.Down;
            }
            dynamicObj.Position -= velocityDown;


            dynamicObj.Position += velocityLeft;
            if (dynamicObj.Bounds.Intersects(staticObj.Bounds))
            {
                returnValue |= DirectionOfCollision.Left;
            }
            dynamicObj.Position -= velocityLeft;


            dynamicObj.Position += velocityRight;
            if (dynamicObj.Bounds.Intersects(staticObj.Bounds))
            {
                returnValue |= DirectionOfCollision.Right;
            }
            dynamicObj.Position -= velocityRight;


            return returnValue;
        }
    }
}

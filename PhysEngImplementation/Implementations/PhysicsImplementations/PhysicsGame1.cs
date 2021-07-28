using Microsoft.Xna.Framework;
using MonoGame.Forms.Controls;
using PhysicsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhysicsImplementations
{
    public class PhysicsGame1 : MonoGameControl
    {
        //public PhysicsObject square;
        public PhysicsObject floor;

        public PhysicsPoint[] points;

        protected override void Initialize()
        {
            base.Initialize();

            //square = new PhysicsObject(new Vector2D(Editor.graphics.Viewport.Width / 2, 100, true), 100, new Rect(Vector2D.Zero, new Vector2D(100, 100, true)), false);
            floor = new PhysicsObject(new Vector2D(Editor.graphics.Viewport.Width / 2, Editor.graphics.Viewport.Height - 25, true), 100, new Rect(Vector2D.Zero, new Vector2D(GraphicsDevice.Viewport.Width - 100, 50, true)), true);
            floor.Bounds.RotateBy((float)Math.PI / 64);
            //PhysicsHelper.Instance.AddObject(square);
            //PhysicsHelper.Instance.AddObject(floor);

            Vector2D middle = new Vector2D(Editor.graphics.Viewport.Width / 2, 250, true);
            float radius = 50;
            float angle = (float)Math.PI / 4;
            points = new PhysicsPoint[4];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new PhysicsPoint(middle + new Vector2D((float)Math.Cos(angle), (float)Math.Sin(angle), true) * radius, Vector2D.Zero);
                angle += (float)Math.PI * 2 / points.Length;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //PhysicsHelper.Instance.UpdateAll();

            foreach (PhysicsPoint point in points)
            {
                point.Velocity.Y += 0.05f;
                point.Position += point.Velocity;
                if (floor.Bounds.Contains(point.Position))
                {
                    point.Velocity.Y *= -0.9f;
                }
            }

            FixShape(points, 50);
        }

        public void FixLine(PhysicsPoint first, PhysicsPoint second, float preferedLength)
        {
            float actualDistance = Vector2D.Distance(first.Position, second.Position);
            if (actualDistance != preferedLength)
            {
                Vector2D middle = (first.Position + second.Position) / 2;

                float angle = (float)Math.Atan2(second.Position.Y - first.Position.Y, second.Position.X - first.Position.X);

                float newX = middle.X - (float)Math.Cos(angle) * preferedLength/2;
                float newY = middle.Y - (float)Math.Sin(angle) * preferedLength/2;
                first.Position.X = new UnitF(newX);
                first.Position.Y = new UnitF(newY);

                newX = middle.X + (float)Math.Cos(angle) * preferedLength / 2;
                newY = middle.Y + (float)Math.Sin(angle) * preferedLength / 2;
                second.Position.X = new UnitF(newX);
                second.Position.Y = new UnitF(newY);
            }
        }
        public void FixShape(PhysicsPoint[] points, float preferredRadius)
        {
            Vector2D middle = Vector2D.Zero;
            Vector2D averageVelocity = Vector2D.Zero;
            foreach( PhysicsPoint point in points)
            {
                middle += point.Position;
                averageVelocity += point.Velocity;
            }
            middle /= points.Length;
            averageVelocity /= points.Length;

            foreach(PhysicsPoint point in points)
            {
                float actualRadius = Vector2D.Distance(middle, point.Position);

                if (actualRadius != preferredRadius)
                {
                    float angle = (float)Math.Atan2(middle.Y - point.Position.Y, middle.X - point.Position.X);
                    point.Position = middle - new Vector2D((float)Math.Cos(angle), (float)Math.Sin(angle), true) * preferredRadius;
                }
            }

            float otherAngle = (float)Math.PI / points.Length;
            float sideLength = preferredRadius * (float)(Math.Sin(otherAngle * 2) / Math.Sin(otherAngle));

            for (int i = 0; i < points.Length; i++)
            {
                int j = (i + 1) % points.Length;

                FixLine(points[i], points[j], sideLength);
            }
        }

        protected override void Draw()
        {
            base.Draw();

            Editor.spriteBatch.Begin();

            Editor.spriteBatch.DrawPolygon(floor.Bounds, Color.Black, 1);

            for (int i = 0; i < points.Length; i++)
            {
                Editor.spriteBatch.DrawLine(points[i].Position, points[(i + 1) % points.Length].Position, Color.Red, 1);
            }
            

            //foreach (PhysicsObject obj in PhysicsHelper.Instance.PhysicsObjects)
            //{
            //    Editor.spriteBatch.DrawPolygon(obj.Bounds, Color.Black, 2);
            //}

            Editor.spriteBatch.End();
        }
    }
}

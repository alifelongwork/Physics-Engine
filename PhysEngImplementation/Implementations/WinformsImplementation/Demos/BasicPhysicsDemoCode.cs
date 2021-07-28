using ImplementationsLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformsImplementation
{
    public partial class Form1
    {
        Platform ground;
        Platform platform;

        List<Platform> platforms;
        PhysicsPolygon physicsSquare;
        PointF physicsMousePosition;

        float siToPixelConversionFactor = 0.5f / 9.81f;
        float gravity = 9.81f / 10;//gravity but divided by 10 to look a little bit more natural

        public float GetPixelUnits(float siUnits) => siUnits * siToPixelConversionFactor;
        public PointF GetPixelUnits(PointF siUnits) => new PointF(siUnits.X * siToPixelConversionFactor, siUnits.Y * siToPixelConversionFactor);
        //public RectangleF GetPixelUnits(RectangleF siUnits) => new RectangleF(siUnits.X * siToPixelConversionFactor, siUnits.Y * siToPixelConversionFactor, siUnits.Width * siToPixelConversionFactor, siUnits.Height * siToPixelConversionFactor);
        public Rectangle GetPixelUnits(RectangleF siUnits) => new Rectangle((int)(siUnits.X * siToPixelConversionFactor), (int)(siUnits.Y * siToPixelConversionFactor), (int)(siUnits.Width * siToPixelConversionFactor), (int)(siUnits.Height * siToPixelConversionFactor));
        public PointF GetPixelUnits(float siUnitsX, float siUnitsY) => new PointF(siUnitsX * siToPixelConversionFactor, siUnitsY * siToPixelConversionFactor);
        public float GetSIUnits(float pixelUnits) => pixelUnits / siToPixelConversionFactor;
        public PointF GetSIUnits(PointF pixelUnits) => new PointF(pixelUnits.X / siToPixelConversionFactor, pixelUnits.Y / siToPixelConversionFactor);
        public PointF GetSIUnits(float pixelUnitsX, float pixelUnitsY) => new PointF(pixelUnitsX / siToPixelConversionFactor, pixelUnitsY / siToPixelConversionFactor);


        private void BasicPhysicsDemoTab_Enter(object sender, EventArgs e)
        {
            siToPixelConversionFactor = BasicPhysicsPictureBox.Width / 1530f;//mm

            platforms = new List<Platform>();
            ground = new Platform(GetSIUnits(10), GetSIUnits(BasicPhysicsPictureBox.Height - 10) - 100, GetSIUnits(BasicPhysicsPictureBox.Width - 20), 100);

            platform = new Platform(100, 300, 200, 100);

            platforms.Add(ground);
            platforms.Add(platform);

            physicsSquare = new PhysicsPolygon(Polygon.CreateRegularPolygon(Brushes.Black, Pens.Black, 4, GetSIUnits(25f), GetSIUnits(BasicPhysicsPictureBox.Width / 2), 200/*mm*/));
            physicsSquare.RotateBy((float)Math.PI / 4);

            BasicPhysicTimer.Enabled = true;
        }
        public void DrawBasicPhysics()
        {
            Bitmap bitmap = new Bitmap(BasicPhysicsPictureBox.Width, BasicPhysicsPictureBox.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            //ground.Draw(graphics);
            //physicsSquare.Draw(graphics);
            foreach (Line line in physicsSquare.Edges)
            {
                graphics.DrawLine(Pens.Black, GetPixelUnits(line.Start.X, line.Start.Y), GetPixelUnits(line.End.X, line.End.Y));
            }
            graphics.DrawRectangle(Pens.Black, GetPixelUnits(platform.Hitbox));
            graphics.DrawRectangle(Pens.Black, GetPixelUnits(ground.Hitbox));

            graphics.DrawLine(Pens.Red, GetPixelUnits(physicsSquare.Centroid.X), GetPixelUnits(physicsSquare.Centroid.Y), physicsMousePosition.X, physicsMousePosition.Y);

            BasicPhysicsPictureBox.Image = bitmap;
        }

        private void BasicPhysicsDemoTab_Leave(object sender, EventArgs e)
        {
            BasicPhysicTimer.Enabled = false;
        }

        private void BasicPhysicsPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            physicsSquare.IsOnPlatform = false;
            physicsSquare.Velocity += new Vector2D(GetSIUnits(physicsMousePosition.X) - physicsSquare.Centroid.X, GetSIUnits(physicsMousePosition.Y) - physicsSquare.Centroid.Y) / 20f;//increase velocity in the direction towards the mouse
        }
        private void BasicPhysicTimer_Tick(object sender, EventArgs e)
        {
            /*if (physicsSquare.AABB.IntersectsWith(ground.Hitbox))
            {
                physicsSquare.Velocity.Y = 0;
                physicsSquare.Centroid = new Vector2D(physicsSquare.Centroid.X, ground.MinY - physicsSquare.Height / 2);//adjusting for passing through ground on collision
                physicsSquare.IsOnPlatform = true;
            }*/


            #region Collision Checking
            foreach ( Platform platform in platforms)
            {
                DirectionOfCollision platformResult = platform.HasCollided(physicsSquare);
                if (platformResult == DirectionOfCollision.Above)
                {
                    physicsSquare.Velocity.Y = 0;
                    physicsSquare.Centroid = new Vector2D(physicsSquare.Centroid.X, platform.Hitbox.Top - physicsSquare.Height / 2);//adjusting for passing through ground on collision
                    physicsSquare.IsOnPlatform = true;
                }
                else if (platformResult == DirectionOfCollision.Below)// ---- remove this to be able to pass through bottom
                {
                    physicsSquare.Velocity.Y = 0;
                    physicsSquare.Centroid = new Vector2D(physicsSquare.Centroid.X, platform.Hitbox.Bottom + physicsSquare.Height / 2);//adjusting for passing through ground on collision
                }

                if (platformResult == DirectionOfCollision.Left)
                {
                    physicsSquare.Velocity.X = 0;
                    physicsSquare.Centroid = new Vector2D(platform.Hitbox.Left - physicsSquare.Width / 2, physicsSquare.Centroid.Y);//adjusting for passing through ground on collision
                }
                else if (platformResult == DirectionOfCollision.Right)
                {
                    physicsSquare.Velocity.X = 0;
                    physicsSquare.Centroid = new Vector2D(platform.Hitbox.Right + physicsSquare.Width / 2, physicsSquare.Centroid.Y);//adjusting for passing through ground on collision
                }
            }
            #endregion

            #region Should Fall Checking
            physicsSquare.Centroid += new Vector2D(0, 2 * gravity);
            physicsSquare.IsOnPlatform = false;
            foreach (Platform platform in platforms)
            {
                if (platform.Hitbox.IntersectsWith(physicsSquare.AABB))
                {
                    physicsSquare.IsOnPlatform = true;
                }
            }
            #endregion

            physicsSquare.Centroid -= new Vector2D(0, 2 * gravity);

            if (physicsSquare.IsOnPlatform)
            {
                physicsSquare.Velocity.X *= 0.9f;//friction on floor
            }
            else
            {
                physicsSquare.Velocity.Y += gravity;
            }

            physicsSquare.Centroid += physicsSquare.Velocity;

            DrawBasicPhysics();
        }

        private void BasicPhysicsPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            physicsMousePosition = e.Location;
        }
    }
}

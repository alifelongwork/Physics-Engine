using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using ImplementationsLibrary;

namespace WinformsImplementation
{
    public partial class Form1
    {
        //BouncingCircle[] quadTreeCircles;
        QuadTree quadTree;

        void DrawQuadTree()
        {
            Bitmap bitmap = new Bitmap(QuadTreePictureBox.Width, QuadTreePictureBox.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            /*
                        if (quadTreeCircles == null)
                        {
                            Random random = new Random();
                            quadTreeCircles = new BouncingCircle[25];
                            for (int i = 0; i < quadTreeCircles.Length; i++)
                            {
                                quadTreeCircles[i] = new BouncingCircle(new Vector2D(random.Next(50, bitmap.Width - 50), random.Next(50, bitmap.Height - 50)), 10, 2.5f, (float)(random.NextDouble() * Math.PI * 2));
                            }
                        }

                        for (int i = 0; i < quadTreeCircles.Length; i++)
                        {
                            quadTreeCircles[i].Update(bitmap.Width, bitmap.Height);
                        }

                        for (int i = 0; i < quadTreeCircles.Length; i++)
                        {
                            quadTreeCircles[i].Draw(graphics);
                        }
                        */

            if (quadTree == null)
            {
                Random random = new Random();
                MovingCircle[] movingCircles = new MovingCircle[25];
                for (int i = 0; i < movingCircles.Length; i++)
                {
                    movingCircles[i] = new MovingCircle(new Vector2D(random.Next(50, bitmap.Width - 50), random.Next(50, bitmap.Height - 50)), 4, 2f, (float)(random.NextDouble() * Math.PI * 2));
                }
                quadTree = new QuadTree(movingCircles, QuadTreeCollisionChecks, 4, 0, bitmap.Width, 0, bitmap.Height);
            }
            QuadTreeTotalCollisionChecksPerUpdate = 0;
            quadTree.Update();
            QuadTreeNumberOfCollisionChecksLabel.Text = $"Number Of Collision Checks \nPer Update: {QuadTreeTotalCollisionChecksPerUpdate}";

            quadTree.Draw(graphics);

            QuadTreePictureBox.Image = bitmap;
        }

        int QuadTreeTotalCollisionChecksPerUpdate = 0;
        void QuadTreeCollisionChecks(MovingCircle movingCircle, List<MovingCircle> circles)
        {
            this.Update();

            bool collides = false;
            foreach (MovingCircle circle in circles)
            {
                QuadTreeTotalCollisionChecksPerUpdate++;
                if (circle.Equals(movingCircle))
                {
                    continue;
                }
                if (Vector2D.Distance(movingCircle.Position, circle.Position) < circle.Radius + movingCircle.Radius)
                {
                    circle.Color = Brushes.Blue;
                    collides = true;
                }
            }
            movingCircle.Color = collides ? Brushes.Blue : Brushes.Black;
        }

        private void QuadTreeDemoTab_Enter(object sender, EventArgs e)
        {
            QuadTreeTimer.Enabled = true;
        }

        private void QuadTreeDemoTab_Leave(object sender, EventArgs e)
        {
            QuadTreeTimer.Enabled = false;
        }
        private void QuadTreeTimer_Tick(object sender, EventArgs e)
        {
            DrawQuadTree();
        }
        private void ApplyQuadTreeChangesButton_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            MovingCircle[] movingCircles = new MovingCircle[(int)QuadTreeNumberOfParticlesUpDown.Value];
            for (int i = 0; i < movingCircles.Length; i++)
            {
                movingCircles[i] = new MovingCircle(new Vector2D(random.Next(50, QuadTreePictureBox.Width - 50), random.Next(50, QuadTreePictureBox.Height - 50)), 4, 2f, (float)(random.NextDouble() * Math.PI * 2));
            }
            quadTree = new QuadTree(movingCircles, QuadTreeCollisionChecks, (int)QuadTreeMaxLevelsUpDown.Value, 0, QuadTreePictureBox.Width, 0, QuadTreePictureBox.Height);
        }
    }
}

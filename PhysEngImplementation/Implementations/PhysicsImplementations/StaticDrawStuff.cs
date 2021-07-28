using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhysicsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsImplementations
{
    public static class StaticDrawStuff
    {
        private static Texture2D pixel = null;

        public static void DrawPolygon(this SpriteBatch spriteBatch, Polygon polygon, Color color, float thickness)
        {
            if (pixel == null)
            {
                pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                pixel.SetData(new[] { Color.White });
            }

            foreach (Line line in polygon.Edges)
            {
                spriteBatch.DrawLine(line, color, thickness);
            }
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Line line, Color color, float thickness)
        {
            spriteBatch.Draw(pixel, new Vector2(line.Start.X.PixelValue, line.Start.Y.PixelValue), null, color, line.Rotation, Vector2.Zero, new Vector2(line.Length.PixelValue, thickness), SpriteEffects.None, 0);
        }
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2D first, Vector2D second, Color color, float thickness)
        {
            spriteBatch.Draw(pixel, new Vector2(first.X.PixelValue, first.Y.PixelValue), null, color, (float)Math.Atan2(second.Y - first.Y, second.X - first.X), Vector2.Zero, new Vector2(Vector2D.Distance(first, second).PixelValue, thickness), SpriteEffects.None, 0);
        }
    }
}

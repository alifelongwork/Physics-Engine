using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Forms.Controls;
using Microsoft.Xna.Framework.Graphics;
namespace PhysicsForm
{
    public class MonoPhysDraw : MonoGameControl
    {
        protected override void Initialize()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw()
        {
            Game newGame = new Game();

            Texture2D WhiteRect = new Texture2D(newGame.GraphicsDevice, 40, 40);
            Color[] data = new Color[40 * 40];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.White;
            }
            WhiteRect.SetData(data);
            base.Draw();
            Editor.spriteBatch.Begin();
            Editor.spriteBatch.Draw(WhiteRect, new Vector2(400, 200), new Rectangle(0, 0, WhiteRect.Width, WhiteRect.Height), Color.Red);
            Editor.spriteBatch.End();
        }
    }
}

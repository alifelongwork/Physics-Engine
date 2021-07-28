using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xna = Microsoft.Xna.Framework;
using xnaI = Microsoft.Xna.Framework.Input;
using xnaG = Microsoft.Xna.Framework.Graphics;

namespace PhysicsForm
{    
    public partial class Form1 : Form
    {
        xnaI.KeyboardState ks;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Test";
            velTrackerValue.Text = velTrackBar.Value.ToString();
            angVelLabel.Text = angVelBar.Value.ToString();
            angDegLabel.Text = "0";
            //xna.Game newGame = new xna.Game();
           
            //xnaG.Texture2D WhiteRect = new xnaG.Texture2D(newGame.GraphicsDevice, 40, 40);
            //xna.Color[] data = new xna.Color[40 * 40];
            //for (int i = 0; i < data.Length; i++)
            //{
            //    data[i] = xna.Color.White;
            //}
            //WhiteRect.SetData(data);
            
        }

        private void textBox1_Key(object sender, KeyPressEventArgs e)
        {
            ks = xnaI.Keyboard.GetState();
            if (ks.IsKeyDown(xnaI.Keys.Enter))
            {
                this.Text = "Hello";
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            ks = xnaI.Keyboard.GetState();
            if (ks.IsKeyDown(xnaI.Keys.Enter))
            {
                this.Text = "Hello";
            }
        }

        private void velTrackBar_Scroll(object sender, EventArgs e)
        {
            velTrackerValue.Text = velTrackBar.Value.ToString();
        }

        private void angVelBar_Scroll(object sender, EventArgs e)
        {
            angVelLabel.Text = angVelBar.Value.ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            angDegLabel.Text = ((angRadUpDown.Value * 180) / (decimal)Math.PI).ToString("#.##");
        }
    }
}

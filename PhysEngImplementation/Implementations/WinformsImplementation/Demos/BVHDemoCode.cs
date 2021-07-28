using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImplementationsLibrary;
using WinformsImplementation.Properties;

namespace WinformsImplementation
{
    public partial class Form1
    {
        Point mousePositionBVH;
        Vertex topLeftBVH = new Vertex(293, 9);
        BVHNode headBVH;

        void DrawPerson()
        {
            Bitmap bitmap = new Bitmap(BVHPictureBox.Width, BVHPictureBox.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            graphics.DrawImage(Resources.basicPerson, new Rectangle(bitmap.Width/2 - 100, bitmap.Height / 2 - 200, 200, 400));

            //topLeftBVH = new Vertex(bitmap.Width / 2 - 100, bitmap.Height / 2 - 200);


            ContainsMouseLabel.Text = $"Contains Mouse: {headBVH.CheckCollision(mousePositionBVH)}";
            headBVH.Draw(graphics);
            BVHPictureBox.Image = bitmap;
        }

        private void BVHDemoTab_Enter(object sender, EventArgs e)
        {
            //left arm
            BVHNode child121 = new BVHNode(new Polygon(new Vertex(315, 185), new Vertex(315, 196), new Vertex(335, 204), new Vertex(375, 173), new Vertex(375, 146)));
            //rest of torso
            BVHNode child122 = new BVHNode(new Polygon(new Vertex(375, 146), new Vertex(375, 173), new Vertex(367, 250), new Vertex(452, 248), new Vertex(452, 144), new Vertex(457, 96), new Vertex(449, 77), new Vertex(429, 69), new Vertex(370, 69)));
            //right arm
            BVHNode child123 = new BVHNode(new Polygon(new Vertex(457, 96), new Vertex(477, 125), new Vertex(477, 228), new Vertex(451, 228), new Vertex(452, 144)));

            //left upper leg
            BVHNode child211 = new BVHNode(new Polygon(new Vertex(297, 380), new Vertex(293, 391), new Vertex(321, 405), new Vertex(360, 408), new Vertex(369, 379), new Vertex(336, 371)));
            //left foot
            BVHNode child212 = new BVHNode(new Polygon(new Vertex(369, 379), new Vertex(336, 371), new Vertex(359, 250), new Vertex(405, 250), new Vertex(405, 269)));

            //right upper leg
            BVHNode child221 = new BVHNode(new Polygon(new Vertex(406, 250), new Vertex(406, 269), new Vertex(451, 379), new Vertex(485, 367), new Vertex(444, 250)));
            //right foot
            BVHNode child222 = new BVHNode(new Polygon(new Vertex(451, 379), new Vertex(485, 367), new Vertex(493, 385), new Vertex(493, 408), new Vertex(420, 408), new Vertex(419, 397)));

            //head
            BVHNode child11 = new BVHNode(new Polygon(new Vertex(368, 10), new Vertex(368, 69), new Vertex(420, 69), new Vertex(420, 10)));
            //torso + arms
            BVHNode child12 = new BVHNode(new Polygon(new Vertex(313, 69), new Vertex(313, 250), new Vertex(478, 250), new Vertex(478, 69)), child121, child122, child123);

            //left leg
            BVHNode child21 = new BVHNode(new Polygon(new Vertex(350, 250), new Vertex(293, 380), new Vertex(293, 409), new Vertex(370, 409), new Vertex(406, 280), new Vertex(406, 250)), child211, child212);
            //right leg
            BVHNode child22 = new BVHNode(new Polygon(new Vertex(406, 250), new Vertex(406, 280), new Vertex(420, 409), new Vertex(493, 409), new Vertex(493, 370), new Vertex(445, 250)), child221, child222);

            //top half
            BVHNode child1 = new BVHNode(new Polygon(topLeftBVH, topLeftBVH + new Vertex(200, 0), topLeftBVH + new Vertex(200, 240), topLeftBVH + new Vertex(0, 240)), child11, child12);
            //bottom half
            BVHNode child2 = new BVHNode(new Polygon(topLeftBVH + new Vertex(0, 240), topLeftBVH + new Vertex(200, 240), topLeftBVH + new Vertex(200, 400), topLeftBVH + new Vertex(0, 400)), child21, child22);

            headBVH = new BVHNode(new Polygon(topLeftBVH, topLeftBVH + new Vertex(200, 0), topLeftBVH + new Vertex(200, 400), topLeftBVH + new Vertex(0, 400)), child1, child2);

            DrawPerson();
        }

        private void BVHPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            mousePositionBVH = new Point(e.X, e.Y);
            MousePositionLabel.Text = $"({e.X}, {e.Y})";
            DrawPerson();
        }
    }
}

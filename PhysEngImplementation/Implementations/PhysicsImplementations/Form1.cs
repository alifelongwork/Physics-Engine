using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhysicsImplementations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void PhysicsGame1Window_MouseWheel(object sender, MouseEventArgs e)
        {
            PhysicsGame1Window.floor.Bounds.RotateBy(e.Delta / 2000f);
        }
    }
}

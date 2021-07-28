namespace PhysicsImplementations
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PhysicsGame1Window = new PhysicsImplementations.PhysicsGame1();
            this.SuspendLayout();
            // 
            // PhysicsGame1Window
            // 
            this.PhysicsGame1Window.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PhysicsGame1Window.Location = new System.Drawing.Point(0, 0);
            this.PhysicsGame1Window.MouseHoverUpdatesOnly = false;
            this.PhysicsGame1Window.Name = "PhysicsGame1Window";
            this.PhysicsGame1Window.Size = new System.Drawing.Size(800, 450);
            this.PhysicsGame1Window.TabIndex = 0;
            this.PhysicsGame1Window.Text = "game 1";
            this.PhysicsGame1Window.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.PhysicsGame1Window_MouseWheel);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PhysicsGame1Window);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private PhysicsGame1 PhysicsGame1Window;
    }
}


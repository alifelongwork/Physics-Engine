namespace PhysicsWinForm
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
            this.components = new System.ComponentModel.Container();
            this.xPosition = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.yPosition = new System.Windows.Forms.NumericUpDown();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.rotateShapeAngle = new System.Windows.Forms.NumericUpDown();
            this.angleRotNum = new System.Windows.Forms.Label();
            this.rotateRadVal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.xPosition)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotateShapeAngle)).BeginInit();
            this.SuspendLayout();
            // 
            // xPosition
            // 
            this.xPosition.Location = new System.Drawing.Point(33, 58);
            this.xPosition.Margin = new System.Windows.Forms.Padding(4);
            this.xPosition.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.xPosition.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            -2147483648});
            this.xPosition.Name = "xPosition";
            this.xPosition.Size = new System.Drawing.Size(107, 22);
            this.xPosition.TabIndex = 0;
            this.xPosition.ValueChanged += new System.EventHandler(this.xPosition_ValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1067, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(263, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(804, 556);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 92);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Y:";
            // 
            // yPosition
            // 
            this.yPosition.Location = new System.Drawing.Point(33, 90);
            this.yPosition.Margin = new System.Windows.Forms.Padding(4);
            this.yPosition.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.yPosition.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.yPosition.Name = "yPosition";
            this.yPosition.Size = new System.Drawing.Size(107, 22);
            this.yPosition.TabIndex = 4;
            this.yPosition.ValueChanged += new System.EventHandler(this.yPosition_ValueChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // rotateShapeAngle
            // 
            this.rotateShapeAngle.DecimalPlaces = 2;
            this.rotateShapeAngle.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.rotateShapeAngle.Location = new System.Drawing.Point(33, 158);
            this.rotateShapeAngle.Margin = new System.Windows.Forms.Padding(4);
            this.rotateShapeAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.rotateShapeAngle.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.rotateShapeAngle.Name = "rotateShapeAngle";
            this.rotateShapeAngle.Size = new System.Drawing.Size(107, 22);
            this.rotateShapeAngle.TabIndex = 6;
            this.rotateShapeAngle.ValueChanged += new System.EventHandler(this.rotateShapeAngle_ValueChanged);
            // 
            // angleRotNum
            // 
            this.angleRotNum.AutoSize = true;
            this.angleRotNum.Location = new System.Drawing.Point(149, 165);
            this.angleRotNum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.angleRotNum.Name = "angleRotNum";
            this.angleRotNum.Size = new System.Drawing.Size(112, 17);
            this.angleRotNum.TabIndex = 7;
            this.angleRotNum.Text = "Angle (Degrees)";
            // 
            // rotateRadVal
            // 
            this.rotateRadVal.AutoSize = true;
            this.rotateRadVal.Location = new System.Drawing.Point(92, 191);
            this.rotateRadVal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rotateRadVal.Name = "rotateRadVal";
            this.rotateRadVal.Size = new System.Drawing.Size(74, 17);
            this.rotateRadVal.TabIndex = 8;
            this.rotateRadVal.Text = "#AngRads";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.rotateRadVal);
            this.Controls.Add(this.angleRotNum);
            this.Controls.Add(this.rotateShapeAngle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.yPosition);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.xPosition);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xPosition)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotateShapeAngle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown xPosition;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown yPosition;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label angleRotNum;
        private System.Windows.Forms.Label rotateRadVal;
        public System.Windows.Forms.NumericUpDown rotateShapeAngle;
    }
}


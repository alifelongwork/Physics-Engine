namespace PhysicsForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setObjectPreferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.positionLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.xPosLabel = new System.Windows.Forms.Label();
            this.yPosLabel = new System.Windows.Forms.Label();
            this.velTrackBar = new System.Windows.Forms.TrackBar();
            this.velTrackerValue = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.angVelBar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.angVelLabel = new System.Windows.Forms.Label();
            this.angRadUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.angDegLabel = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.monoPhysDraw1 = new PhysicsForm.MonoPhysDraw();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.velTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.angVelBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.angRadUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(914, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setObjectPreferencesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // setObjectPreferencesToolStripMenuItem
            // 
            this.setObjectPreferencesToolStripMenuItem.Name = "setObjectPreferencesToolStripMenuItem";
            this.setObjectPreferencesToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.setObjectPreferencesToolStripMenuItem.Text = "Set Object Preferences";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // positionLabel
            // 
            this.positionLabel.AutoSize = true;
            this.positionLabel.Location = new System.Drawing.Point(34, 98);
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(44, 13);
            this.positionLabel.TabIndex = 2;
            this.positionLabel.Text = "Position";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Velocity:";
            // 
            // xPosLabel
            // 
            this.xPosLabel.AutoSize = true;
            this.xPosLabel.Location = new System.Drawing.Point(1, 52);
            this.xPosLabel.Name = "xPosLabel";
            this.xPosLabel.Size = new System.Drawing.Size(17, 13);
            this.xPosLabel.TabIndex = 6;
            this.xPosLabel.Text = "X:";
            // 
            // yPosLabel
            // 
            this.yPosLabel.AutoSize = true;
            this.yPosLabel.Location = new System.Drawing.Point(1, 78);
            this.yPosLabel.Name = "yPosLabel";
            this.yPosLabel.Size = new System.Drawing.Size(17, 13);
            this.yPosLabel.TabIndex = 7;
            this.yPosLabel.Text = "Y:";
            // 
            // velTrackBar
            // 
            this.velTrackBar.BackColor = System.Drawing.SystemColors.Control;
            this.velTrackBar.Location = new System.Drawing.Point(12, 124);
            this.velTrackBar.Minimum = -10;
            this.velTrackBar.Name = "velTrackBar";
            this.velTrackBar.Size = new System.Drawing.Size(157, 45);
            this.velTrackBar.TabIndex = 9;
            this.velTrackBar.Scroll += new System.EventHandler(this.velTrackBar_Scroll);
            // 
            // velTrackerValue
            // 
            this.velTrackerValue.AutoSize = true;
            this.velTrackerValue.Location = new System.Drawing.Point(117, 172);
            this.velTrackerValue.Name = "velTrackerValue";
            this.velTrackerValue.Size = new System.Drawing.Size(28, 13);
            this.velTrackerValue.TabIndex = 10;
            this.velTrackerValue.Text = "vel#";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDown1.Location = new System.Drawing.Point(24, 217);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Coefficient of Restitution";
            // 
            // angVelBar
            // 
            this.angVelBar.Location = new System.Drawing.Point(0, 377);
            this.angVelBar.Minimum = -10;
            this.angVelBar.Name = "angVelBar";
            this.angVelBar.Size = new System.Drawing.Size(157, 45);
            this.angVelBar.TabIndex = 14;
            this.angVelBar.Scroll += new System.EventHandler(this.angVelBar_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 425);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Angular Velocity:";
            // 
            // angVelLabel
            // 
            this.angVelLabel.AutoSize = true;
            this.angVelLabel.Location = new System.Drawing.Point(101, 425);
            this.angVelLabel.Name = "angVelLabel";
            this.angVelLabel.Size = new System.Drawing.Size(47, 13);
            this.angVelLabel.TabIndex = 16;
            this.angVelLabel.Text = "angVel#";
            // 
            // angRadUpDown
            // 
            this.angRadUpDown.DecimalPlaces = 2;
            this.angRadUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.angRadUpDown.Location = new System.Drawing.Point(24, 318);
            this.angRadUpDown.Maximum = new decimal(new int[] {
            628,
            0,
            0,
            131072});
            this.angRadUpDown.Name = "angRadUpDown";
            this.angRadUpDown.Size = new System.Drawing.Size(63, 20);
            this.angRadUpDown.TabIndex = 17;
            this.angRadUpDown.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(93, 320);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Angle (radians)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(93, 352);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Angle (degrees)";
            // 
            // angDegLabel
            // 
            this.angDegLabel.AutoSize = true;
            this.angDegLabel.Location = new System.Drawing.Point(25, 352);
            this.angDegLabel.Name = "angDegLabel";
            this.angDegLabel.Size = new System.Drawing.Size(53, 13);
            this.angDegLabel.TabIndex = 20;
            this.angDegLabel.Text = "AngDeg#";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(24, 50);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(71, 20);
            this.numericUpDown2.TabIndex = 21;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(24, 75);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(71, 20);
            this.numericUpDown3.TabIndex = 22;
            // 
            // monoPhysDraw1
            // 
            this.monoPhysDraw1.Location = new System.Drawing.Point(193, 0);
            this.monoPhysDraw1.MouseHoverUpdatesOnly = false;
            this.monoPhysDraw1.Name = "monoPhysDraw1";
            this.monoPhysDraw1.Size = new System.Drawing.Size(721, 565);
            this.monoPhysDraw1.TabIndex = 23;
            this.monoPhysDraw1.Text = "monoPhysDraw1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 561);
            this.Controls.Add(this.monoPhysDraw1);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.angDegLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.angRadUpDown);
            this.Controls.Add(this.angVelLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.angVelBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.velTrackerValue);
            this.Controls.Add(this.velTrackBar);
            this.Controls.Add(this.yPosLabel);
            this.Controls.Add(this.xPosLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.positionLabel);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.velTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.angVelBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.angRadUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setObjectPreferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label positionLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label xPosLabel;
        private System.Windows.Forms.Label yPosLabel;
        private System.Windows.Forms.TrackBar velTrackBar;
        private System.Windows.Forms.Label velTrackerValue;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar angVelBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label angVelLabel;
        private System.Windows.Forms.NumericUpDown angRadUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label angDegLabel;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private MonoPhysDraw monoPhysDraw1;
    }
}


namespace Xmas_22_Activity_Check_In
{
    partial class frmStartUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStartUp));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmdSettings = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdActor = new System.Windows.Forms.Button();
            this.cmdCheckIn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(280, 190);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // cmdSettings
            // 
            this.cmdSettings.BackColor = System.Drawing.Color.Goldenrod;
            this.cmdSettings.FlatAppearance.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.cmdSettings.FlatAppearance.BorderSize = 5;
            this.cmdSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.cmdSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.cmdSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSettings.Font = new System.Drawing.Font("Century", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSettings.Location = new System.Drawing.Point(35, 364);
            this.cmdSettings.Name = "cmdSettings";
            this.cmdSettings.Size = new System.Drawing.Size(95, 55);
            this.cmdSettings.TabIndex = 1;
            this.cmdSettings.Text = "Settings";
            this.cmdSettings.UseVisualStyleBackColor = false;
            this.cmdSettings.Click += new System.EventHandler(this.cmdSettings_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.Goldenrod;
            this.cmdClose.FlatAppearance.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.cmdClose.FlatAppearance.BorderSize = 5;
            this.cmdClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.cmdClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClose.Font = new System.Drawing.Font("Century", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(154, 363);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(95, 55);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdActor
            // 
            this.cmdActor.BackColor = System.Drawing.Color.Goldenrod;
            this.cmdActor.FlatAppearance.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.cmdActor.FlatAppearance.BorderSize = 5;
            this.cmdActor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.cmdActor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.cmdActor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdActor.Font = new System.Drawing.Font("Century", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdActor.Location = new System.Drawing.Point(35, 286);
            this.cmdActor.Name = "cmdActor";
            this.cmdActor.Size = new System.Drawing.Size(214, 55);
            this.cmdActor.TabIndex = 3;
            this.cmdActor.Text = "Activity Actor Info";
            this.cmdActor.UseVisualStyleBackColor = false;
            this.cmdActor.Click += new System.EventHandler(this.cmdActor_Click);
            // 
            // cmdCheckIn
            // 
            this.cmdCheckIn.BackColor = System.Drawing.Color.Goldenrod;
            this.cmdCheckIn.FlatAppearance.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.cmdCheckIn.FlatAppearance.BorderSize = 5;
            this.cmdCheckIn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.cmdCheckIn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.cmdCheckIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCheckIn.Font = new System.Drawing.Font("Century", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCheckIn.Location = new System.Drawing.Point(35, 209);
            this.cmdCheckIn.Name = "cmdCheckIn";
            this.cmdCheckIn.Size = new System.Drawing.Size(214, 55);
            this.cmdCheckIn.TabIndex = 4;
            this.cmdCheckIn.Text = "Activty Check In";
            this.cmdCheckIn.UseVisualStyleBackColor = false;
            this.cmdCheckIn.Click += new System.EventHandler(this.cmdCheckIn_Click);
            // 
            // frmStartUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(280, 430);
            this.Controls.Add(this.cmdCheckIn);
            this.Controls.Add(this.cmdActor);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdSettings);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmStartUp";
            this.Text = "Activity Check In";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStartUp_FormClosing);
            this.Load += new System.EventHandler(this.frmStartUp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button cmdSettings;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdActor;
        private System.Windows.Forms.Button cmdCheckIn;
    }
}


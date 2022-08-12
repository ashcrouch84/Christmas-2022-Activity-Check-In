namespace Xmas_22_Activity_Check_In
{
    partial class frmCheckActivities
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckActivities));
            this.pnlCheck = new System.Windows.Forms.Panel();
            this.gbFindByDateTime = new System.Windows.Forms.GroupBox();
            this.cmdCheckByDateAndTime = new System.Windows.Forms.Button();
            this.dgvCheckFamilies = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboCheckDate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboCheckTime = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCheckRef = new System.Windows.Forms.Label();
            this.lblCheckName = new System.Windows.Forms.Label();
            this.gbFindByRef = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdCheckByRef = new System.Windows.Forms.Button();
            this.txtCheckRef = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.dgvCheck = new System.Windows.Forms.DataGridView();
            this.Activity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdClearCheck = new System.Windows.Forms.Button();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.cmdFindByDateAndTime = new System.Windows.Forms.Button();
            this.cmdFindByRef = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tmDownload = new System.Windows.Forms.Timer(this.components);
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblBackupInfo = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.tmFindFamilies = new System.Windows.Forms.Timer(this.components);
            this.tmDisplayInfo = new System.Windows.Forms.Timer(this.components);
            this.tmDisplayTime = new System.Windows.Forms.Timer(this.components);
            this.pnlCheck.SuspendLayout();
            this.gbFindByDateTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckFamilies)).BeginInit();
            this.gbFindByRef.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheck)).BeginInit();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCheck
            // 
            this.pnlCheck.BackColor = System.Drawing.Color.Black;
            this.pnlCheck.Controls.Add(this.gbFindByDateTime);
            this.pnlCheck.Controls.Add(this.lblCheckRef);
            this.pnlCheck.Controls.Add(this.lblCheckName);
            this.pnlCheck.Controls.Add(this.gbFindByRef);
            this.pnlCheck.Controls.Add(this.label21);
            this.pnlCheck.Controls.Add(this.label25);
            this.pnlCheck.Controls.Add(this.dgvCheck);
            this.pnlCheck.Controls.Add(this.label1);
            this.pnlCheck.Controls.Add(this.cmdClearCheck);
            this.pnlCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCheck.Location = new System.Drawing.Point(0, 0);
            this.pnlCheck.Name = "pnlCheck";
            this.pnlCheck.Size = new System.Drawing.Size(1184, 759);
            this.pnlCheck.TabIndex = 16;
            // 
            // gbFindByDateTime
            // 
            this.gbFindByDateTime.Controls.Add(this.cmdCheckByDateAndTime);
            this.gbFindByDateTime.Controls.Add(this.dgvCheckFamilies);
            this.gbFindByDateTime.Controls.Add(this.cboCheckDate);
            this.gbFindByDateTime.Controls.Add(this.label3);
            this.gbFindByDateTime.Controls.Add(this.label4);
            this.gbFindByDateTime.Controls.Add(this.cboCheckTime);
            this.gbFindByDateTime.Controls.Add(this.label6);
            this.gbFindByDateTime.Location = new System.Drawing.Point(243, 223);
            this.gbFindByDateTime.Name = "gbFindByDateTime";
            this.gbFindByDateTime.Size = new System.Drawing.Size(568, 233);
            this.gbFindByDateTime.TabIndex = 50;
            this.gbFindByDateTime.TabStop = false;
            // 
            // cmdCheckByDateAndTime
            // 
            this.cmdCheckByDateAndTime.BackColor = System.Drawing.Color.Goldenrod;
            this.cmdCheckByDateAndTime.FlatAppearance.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.cmdCheckByDateAndTime.FlatAppearance.BorderSize = 5;
            this.cmdCheckByDateAndTime.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.cmdCheckByDateAndTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCheckByDateAndTime.Font = new System.Drawing.Font("Century Gothic", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCheckByDateAndTime.Location = new System.Drawing.Point(78, 182);
            this.cmdCheckByDateAndTime.Name = "cmdCheckByDateAndTime";
            this.cmdCheckByDateAndTime.Size = new System.Drawing.Size(103, 38);
            this.cmdCheckByDateAndTime.TabIndex = 50;
            this.cmdCheckByDateAndTime.Text = "Find";
            this.cmdCheckByDateAndTime.UseVisualStyleBackColor = false;
            this.cmdCheckByDateAndTime.Click += new System.EventHandler(this.cmdCheckByDateAndTime_Click);
            // 
            // dgvCheckFamilies
            // 
            this.dgvCheckFamilies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCheckFamilies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCheckFamilies.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgvCheckFamilies.Location = new System.Drawing.Point(221, 34);
            this.dgvCheckFamilies.Name = "dgvCheckFamilies";
            this.dgvCheckFamilies.RowHeadersWidth = 45;
            this.dgvCheckFamilies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCheckFamilies.Size = new System.Drawing.Size(332, 180);
            this.dgvCheckFamilies.TabIndex = 49;
            this.dgvCheckFamilies.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCheckFamilies_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Reference";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 84;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Name";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 62;
            // 
            // cboCheckDate
            // 
            this.cboCheckDate.Font = new System.Drawing.Font("Century Gothic", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCheckDate.FormattingEnabled = true;
            this.cboCheckDate.Location = new System.Drawing.Point(58, 32);
            this.cboCheckDate.Name = "cboCheckDate";
            this.cboCheckDate.Size = new System.Drawing.Size(121, 29);
            this.cboCheckDate.TabIndex = 45;
            this.cboCheckDate.SelectedIndexChanged += new System.EventHandler(this.cboCheckDate_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12.22642F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label3.Location = new System.Drawing.Point(5, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 23);
            this.label3.TabIndex = 48;
            this.label3.Text = "Time";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12.22642F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label4.Location = new System.Drawing.Point(5, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 23);
            this.label4.TabIndex = 46;
            this.label4.Text = "Date";
            // 
            // cboCheckTime
            // 
            this.cboCheckTime.Font = new System.Drawing.Font("Century Gothic", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCheckTime.FormattingEnabled = true;
            this.cboCheckTime.Location = new System.Drawing.Point(58, 67);
            this.cboCheckTime.Name = "cboCheckTime";
            this.cboCheckTime.Size = new System.Drawing.Size(121, 29);
            this.cboCheckTime.TabIndex = 47;
            this.cboCheckTime.SelectedIndexChanged += new System.EventHandler(this.cboCheckTime_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 14.26415F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label6.Location = new System.Drawing.Point(3, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(238, 25);
            this.label6.TabIndex = 44;
            this.label6.Text = "Find By Date and Time";
            // 
            // lblCheckRef
            // 
            this.lblCheckRef.AutoSize = true;
            this.lblCheckRef.Font = new System.Drawing.Font("Century Gothic", 14.26415F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckRef.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.lblCheckRef.Location = new System.Drawing.Point(424, 503);
            this.lblCheckRef.Name = "lblCheckRef";
            this.lblCheckRef.Size = new System.Drawing.Size(45, 25);
            this.lblCheckRef.TabIndex = 48;
            this.lblCheckRef.Text = "VIP";
            // 
            // lblCheckName
            // 
            this.lblCheckName.AutoSize = true;
            this.lblCheckName.Font = new System.Drawing.Font("Century Gothic", 14.26415F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckName.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.lblCheckName.Location = new System.Drawing.Point(424, 459);
            this.lblCheckName.Name = "lblCheckName";
            this.lblCheckName.Size = new System.Drawing.Size(45, 25);
            this.lblCheckName.TabIndex = 47;
            this.lblCheckName.Text = "VIP";
            // 
            // gbFindByRef
            // 
            this.gbFindByRef.Controls.Add(this.label2);
            this.gbFindByRef.Controls.Add(this.cmdCheckByRef);
            this.gbFindByRef.Controls.Add(this.txtCheckRef);
            this.gbFindByRef.Location = new System.Drawing.Point(225, 100);
            this.gbFindByRef.Name = "gbFindByRef";
            this.gbFindByRef.Size = new System.Drawing.Size(550, 100);
            this.gbFindByRef.TabIndex = 49;
            this.gbFindByRef.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 14.26415F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Find By Reference";
            // 
            // cmdCheckByRef
            // 
            this.cmdCheckByRef.BackColor = System.Drawing.Color.Goldenrod;
            this.cmdCheckByRef.FlatAppearance.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.cmdCheckByRef.FlatAppearance.BorderSize = 5;
            this.cmdCheckByRef.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.cmdCheckByRef.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCheckByRef.Font = new System.Drawing.Font("Century Gothic", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCheckByRef.Location = new System.Drawing.Point(238, 42);
            this.cmdCheckByRef.Name = "cmdCheckByRef";
            this.cmdCheckByRef.Size = new System.Drawing.Size(110, 38);
            this.cmdCheckByRef.TabIndex = 7;
            this.cmdCheckByRef.Text = "Find";
            this.cmdCheckByRef.UseVisualStyleBackColor = false;
            this.cmdCheckByRef.Click += new System.EventHandler(this.cmdCheckByRef_Click);
            // 
            // txtCheckRef
            // 
            this.txtCheckRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.26415F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckRef.Location = new System.Drawing.Point(10, 49);
            this.txtCheckRef.Name = "txtCheckRef";
            this.txtCheckRef.Size = new System.Drawing.Size(218, 31);
            this.txtCheckRef.TabIndex = 8;
            this.txtCheckRef.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCheckRef_KeyDown);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Century Gothic", 14.26415F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label21.Location = new System.Drawing.Point(251, 503);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(115, 25);
            this.label21.TabIndex = 46;
            this.label21.Text = "Reference";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Century Gothic", 14.26415F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label25.Location = new System.Drawing.Point(247, 459);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(147, 25);
            this.label25.TabIndex = 45;
            this.label25.Text = "Family Name";
            // 
            // dgvCheck
            // 
            this.dgvCheck.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCheck.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Activity,
            this.Column1,
            this.Column2});
            this.dgvCheck.Location = new System.Drawing.Point(243, 555);
            this.dgvCheck.Name = "dgvCheck";
            this.dgvCheck.RowHeadersWidth = 45;
            this.dgvCheck.Size = new System.Drawing.Size(411, 68);
            this.dgvCheck.TabIndex = 10;
            // 
            // Activity
            // 
            this.Activity.HeaderText = "Activity";
            this.Activity.MinimumWidth = 6;
            this.Activity.Name = "Activity";
            this.Activity.Width = 110;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Attended";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 110;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Time";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 110;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 14.26415F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label1.Location = new System.Drawing.Point(247, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Check Activities Attended";
            // 
            // cmdClearCheck
            // 
            this.cmdClearCheck.BackColor = System.Drawing.Color.Goldenrod;
            this.cmdClearCheck.FlatAppearance.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.cmdClearCheck.FlatAppearance.BorderSize = 5;
            this.cmdClearCheck.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.cmdClearCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClearCheck.Font = new System.Drawing.Font("Century Gothic", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClearCheck.Location = new System.Drawing.Point(421, 672);
            this.cmdClearCheck.Name = "cmdClearCheck";
            this.cmdClearCheck.Size = new System.Drawing.Size(172, 38);
            this.cmdClearCheck.TabIndex = 44;
            this.cmdClearCheck.Text = "Clear Family";
            this.cmdClearCheck.UseVisualStyleBackColor = false;
            this.cmdClearCheck.Click += new System.EventHandler(this.cmdClearCheck_Click);
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.Black;
            this.pnlLeft.Controls.Add(this.cmdFindByDateAndTime);
            this.pnlLeft.Controls.Add(this.cmdFindByRef);
            this.pnlLeft.Controls.Add(this.cmdClose);
            this.pnlLeft.Controls.Add(this.pictureBox1);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(202, 759);
            this.pnlLeft.TabIndex = 17;
            // 
            // cmdFindByDateAndTime
            // 
            this.cmdFindByDateAndTime.BackColor = System.Drawing.Color.Goldenrod;
            this.cmdFindByDateAndTime.FlatAppearance.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.cmdFindByDateAndTime.FlatAppearance.BorderSize = 5;
            this.cmdFindByDateAndTime.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.cmdFindByDateAndTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdFindByDateAndTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdFindByDateAndTime.ForeColor = System.Drawing.Color.Black;
            this.cmdFindByDateAndTime.Location = new System.Drawing.Point(12, 225);
            this.cmdFindByDateAndTime.Name = "cmdFindByDateAndTime";
            this.cmdFindByDateAndTime.Size = new System.Drawing.Size(174, 51);
            this.cmdFindByDateAndTime.TabIndex = 8;
            this.cmdFindByDateAndTime.Text = "Find by Date and Time";
            this.cmdFindByDateAndTime.UseVisualStyleBackColor = false;
            this.cmdFindByDateAndTime.Click += new System.EventHandler(this.cmdFindByDateAndTime_Click);
            // 
            // cmdFindByRef
            // 
            this.cmdFindByRef.BackColor = System.Drawing.Color.Goldenrod;
            this.cmdFindByRef.FlatAppearance.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.cmdFindByRef.FlatAppearance.BorderSize = 5;
            this.cmdFindByRef.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.cmdFindByRef.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdFindByRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdFindByRef.ForeColor = System.Drawing.Color.Black;
            this.cmdFindByRef.Location = new System.Drawing.Point(12, 162);
            this.cmdFindByRef.Name = "cmdFindByRef";
            this.cmdFindByRef.Size = new System.Drawing.Size(174, 55);
            this.cmdFindByRef.TabIndex = 7;
            this.cmdFindByRef.Text = "Find by Reference";
            this.cmdFindByRef.UseVisualStyleBackColor = false;
            this.cmdFindByRef.Click += new System.EventHandler(this.cmdFindByRef_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.Goldenrod;
            this.cmdClose.FlatAppearance.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.cmdClose.FlatAppearance.BorderSize = 5;
            this.cmdClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClose.Font = new System.Drawing.Font("Century", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(33, 672);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(151, 62);
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "Close Check Activitie";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(206, 146);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // tmDownload
            // 
            this.tmDownload.Tick += new System.EventHandler(this.tmDownload_Tick);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Black;
            this.pnlTop.Controls.Add(this.lblBackupInfo);
            this.pnlTop.Controls.Add(this.lblTime);
            this.pnlTop.Controls.Add(this.lblName);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(202, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(982, 100);
            this.pnlTop.TabIndex = 18;
            // 
            // lblBackupInfo
            // 
            this.lblBackupInfo.AutoSize = true;
            this.lblBackupInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBackupInfo.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.lblBackupInfo.Location = new System.Drawing.Point(61, 23);
            this.lblBackupInfo.Name = "lblBackupInfo";
            this.lblBackupInfo.Size = new System.Drawing.Size(70, 24);
            this.lblBackupInfo.TabIndex = 30;
            this.lblBackupInfo.Text = "label21";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.lblTime.Location = new System.Drawing.Point(758, 23);
            this.lblTime.Name = "lblTime";
            this.lblTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTime.Size = new System.Drawing.Size(81, 29);
            this.lblTime.TabIndex = 31;
            this.lblTime.Text = "label5";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Century Gothic", 14.26415F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.lblName.Location = new System.Drawing.Point(342, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(45, 25);
            this.lblName.TabIndex = 32;
            this.lblName.Text = "VIP";
            // 
            // tmFindFamilies
            // 
            this.tmFindFamilies.Tick += new System.EventHandler(this.tmFindFamilies_Tick);
            // 
            // tmDisplayInfo
            // 
            this.tmDisplayInfo.Interval = 2000;
            this.tmDisplayInfo.Tick += new System.EventHandler(this.tmDisplayInfo_Tick);
            // 
            // tmDisplayTime
            // 
            this.tmDisplayTime.Enabled = true;
            this.tmDisplayTime.Interval = 1000;
            this.tmDisplayTime.Tick += new System.EventHandler(this.tmDisplayTime_Tick);
            // 
            // frmCheckActivities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 759);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlCheck);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCheckActivities";
            this.Text = "frmCheckActivities";
            this.Load += new System.EventHandler(this.frmCheckActivities_Load);
            this.Resize += new System.EventHandler(this.frmCheckActivities_Resize);
            this.pnlCheck.ResumeLayout(false);
            this.pnlCheck.PerformLayout();
            this.gbFindByDateTime.ResumeLayout(false);
            this.gbFindByDateTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckFamilies)).EndInit();
            this.gbFindByRef.ResumeLayout(false);
            this.gbFindByRef.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheck)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCheck;
        private System.Windows.Forms.Label lblCheckRef;
        private System.Windows.Forms.Label lblCheckName;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button cmdClearCheck;
        private System.Windows.Forms.DataGridView dgvCheck;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdCheckByRef;
        private System.Windows.Forms.TextBox txtCheckRef;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.GroupBox gbFindByDateTime;
        private System.Windows.Forms.Button cmdCheckByDateAndTime;
        private System.Windows.Forms.DataGridView dgvCheckFamilies;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.ComboBox cboCheckDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboCheckTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gbFindByRef;
        private System.Windows.Forms.Button cmdFindByDateAndTime;
        private System.Windows.Forms.Button cmdFindByRef;
        private System.Windows.Forms.Timer tmDownload;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Timer tmFindFamilies;
        private System.Windows.Forms.Label lblBackupInfo;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Timer tmDisplayInfo;
        private System.Windows.Forms.Timer tmDisplayTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Activity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xmas_22_Activity_Check_In
{
    public partial class frmStartUp : Form
    {
        bool bexit = false;
        public frmStartUp()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Are you sure you want to close this program?", "Close?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dg == DialogResult.Yes)
            {
                Application.Exit();
                bexit = true;
            }
        }

        private void frmStartUp_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void cmdSettings_Click(object sender, EventArgs e)
        {
            frmSettings frmSettings = new frmSettings();
            frmSettings.Show();
        }

        private void cmdCheckIn_Click(object sender, EventArgs e)
        {
            frmCheck frmCheck = new frmCheck();
            frmCheck.Show();
        }

        private void cmdActor_Click(object sender, EventArgs e)
        {
            frmInfo frmInfo = new frmInfo();
            frmInfo.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void frmStartUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bexit != true)
            {
                DialogResult dg = MessageBox.Show("Are you sure you want to close this program?", "Close?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dg == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}

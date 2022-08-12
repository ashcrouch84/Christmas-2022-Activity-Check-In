using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Xmas_22_Activity_Check_In
{
    public partial class frmSettings : Form
    {
        int i;
        bool bloaded = false;
        string strDateFrom, strDateTo;
        string[] strFamilyChecked;
        string[] strAdultChecked;
        string[] strChildChecked;
        int intCShowCount, intAShowCount, intFShowCount;

        public frmSettings()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            loadSettings();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void loadSettings()
        {
            loadFTPDetails();

            txtLocal.Text = Properties.Settings.Default.aciLocalSave.ToString();
            txtLocalBackup.Text = Properties.Settings.Default.aciLocalBackup.ToString();
            txtProgramName.Text = Properties.Settings.Default.aciName.ToString();

            string[] dateFrom = Properties.Settings.Default.aciFrom.ToString().Split('/');
            string[] dateTo = Properties.Settings.Default.aciTo.ToString().Split('/');
            dtpFrom.Value = new DateTime(Int32.Parse(dateFrom[2]), Int32.Parse(dateFrom[1]), Int32.Parse(dateFrom[0]));
            dtpTo.Value = new DateTime(Int32.Parse(dateTo[2]), Int32.Parse(dateTo[1]), Int32.Parse(dateTo[0]));

            i = 1;
            while (i < 120)
            {
                cboUpload.Items.Add(i.ToString());
                i++;
            }
            cboUpload.SelectedIndex = Properties.Settings.Default.aciUploadInterval - 1;

            i = 1;
            while (i < 30)
            {
                cboWait.Items.Add(i.ToString());
                i = i + 1;
            }
            cboWait.SelectedIndex = Properties.Settings.Default.aciWait - 1;

            loadOtherActivities();
            loadQuestions();
            loadInfoSettings();
            txtReadInterval.Text = Properties.Settings.Default.aciReadWait.ToString();
            txtReadLocation.Text = Properties.Settings.Default.aciTableRead.ToString();
            loadTableNames();
            txtTableSave.Text = Properties.Settings.Default.aciTableWrite.ToString();
            bloaded = true;
        }

        private void loadTableNames()
        {
            lstTableNames.Items.Clear();
            lstTableNo.Items.Clear();

            if (Properties.Settings.Default.aciTables == true)
            {
                rbUseTables.Checked = true;
            }
            else
            {
                rbDontUseTables.Checked = true;
            }

            i = 0;
            string[] strTemp = Properties.Settings.Default.aciTableNames.ToString().Split(',');
            for (i = 0; i < strTemp.Count(); i++)
            {
                lstTableNo.Items.Add(i + 1);
                lstTableNames.Items.Add(strTemp[i]);
            }

            //string gs = Properties.Settings.Default.aciTableWrite + "\\table.txt";
            //File.WriteAllText(gs, string.Empty);
        }

        private void loadInfoSettings()
        {
            if (Properties.Settings.Default.aciUseInfo == true)
            {
                rbUseInfo.Checked = true;
            }
            else
            {
                rbDontUseInfo.Checked = true;

            }
            txtCrypto.Text = Properties.Settings.Default.aciDCPW.ToString();
        }

        private void loadQuestions()
        {
            var list = new List<string>();
            string strread = Properties.Settings.Default.aciQuestionFile + "\\Questions.txt";
            var fileStream = new FileStream(strread, FileMode.Open, System.IO.FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
            fileStream.Close();

            //seperate the lines into adults, children and family
            string[] lstChild = list[0].ToString().Split(',');
            string[] lstAdult = list[1].ToString().Split(',');
            string[] lstFamily = list[2].ToString().Split(',');

            //child checkbox setup
           
            //name and display check boxes
            intCShowCount = 0;
            try { chkC1.Text = lstChild[1].ToString(); chkC1.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC1.Visible = false; }
            try { chkC2.Text = lstChild[2].ToString(); chkC2.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC2.Visible = false; }
            try { chkC3.Text = lstChild[3].ToString(); chkC3.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC3.Visible = false; }
            try { chkC4.Text = lstChild[4].ToString(); chkC4.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC4.Visible = false; }
            try { chkC5.Text = lstChild[5].ToString(); chkC5.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC5.Visible = false; }
            try { chkC6.Text = lstChild[6].ToString(); chkC6.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC6.Visible = false; }
            try { chkC7.Text = lstChild[7].ToString(); chkC7.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC7.Visible = false; }
            try { chkC8.Text = lstChild[8].ToString(); chkC8.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC8.Visible = false; }
            try { chkC9.Text = lstChild[9].ToString(); chkC9.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC9.Visible = false; }
            try { chkC10.Text = lstChild[10].ToString(); chkC10.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC10.Visible = false; }
            try { chkC11.Text = lstChild[11].ToString(); chkC11.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC11.Visible = false; }
            try { chkC12.Text = lstChild[12].ToString(); chkC12.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC12.Visible = false; }
            try { chkC13.Text = lstChild[13].ToString(); chkC13.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC13.Visible = false; }
            try { chkC14.Text = lstChild[14].ToString(); chkC14.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC14.Visible = false; }
            try { chkC15.Text = lstChild[15].ToString(); chkC15.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC15.Visible = false; }
            try { chkC16.Text = lstChild[16].ToString(); chkC16.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC16.Visible = false; }
            try { chkC17.Text = lstChild[17].ToString(); chkC17.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC17.Visible = false; }
            try { chkC18.Text = lstChild[18].ToString(); chkC18.Visible = true; intCShowCount = intCShowCount + 1; } catch { chkC18.Visible = false; }
            //check check boxes
            strChildChecked = Properties.Settings.Default.aciChildChecked.ToString().Split(',');
            if (strChildChecked[0] == "True" || strChildChecked[0]=="true") { chkC1.Checked = true; } else { chkC1.Checked = false; }
            if (strChildChecked[1] == "True" || strChildChecked[1] == "true") { chkC2.Checked = true; } else { chkC2.Checked = false; }
            if (strChildChecked[2] == "True" || strChildChecked[2] == "true") { chkC3.Checked = true; } else { chkC3.Checked = false; }
            if (strChildChecked[3] == "True" || strChildChecked[3] == "true") { chkC4.Checked = true; } else { chkC4.Checked = false; }
            if (strChildChecked[4] == "True" || strChildChecked[4] == "true") { chkC5.Checked = true; } else { chkC5.Checked = false; }
            if (strChildChecked[5] == "True" || strChildChecked[5] == "true") { chkC6.Checked = true; } else { chkC6.Checked = false; }
            if (strChildChecked[6] == "True" || strChildChecked[6] == "true") { chkC7.Checked = true; } else { chkC7.Checked = false; }
            if (strChildChecked[7] == "True" || strChildChecked[7] == "true") { chkC8.Checked = true; } else { chkC8.Checked = false; }
            if (strChildChecked[8] == "True" || strChildChecked[8] == "true") { chkC9.Checked = true; } else { chkC9.Checked = false; }
            if (strChildChecked[9] == "True" || strChildChecked[9] == "true") { chkC10.Checked = true; } else { chkC10.Checked = false; }
            if (strChildChecked[10] == "True" || strChildChecked[10] == "true") { chkC11.Checked = true; } else { chkC11.Checked = false; }
            if (strChildChecked[11] == "True" || strChildChecked[11] == "true") { chkC12.Checked = true; } else { chkC12.Checked = false; }
            if (strChildChecked[12] == "True" || strChildChecked[12] == "true") { chkC13.Checked = true; } else { chkC13.Checked = false; }
            if (strChildChecked[13] == "True" || strChildChecked[13] == "true") { chkC14.Checked = true; } else { chkC14.Checked = false; }
            if (strChildChecked[14] == "True" || strChildChecked[14] == "true") { chkC15.Checked = true; } else { chkC15.Checked = false; }
            if (strChildChecked[15] == "True" || strChildChecked[15] == "true") { chkC16.Checked = true; } else { chkC16.Checked = false; }
            if (strChildChecked[16] == "True" || strChildChecked[16] == "true") { chkC17.Checked = true; } else { chkC17.Checked = false; }
            if (strChildChecked[17] == "True" || strChildChecked[17] == "true") { chkC18.Checked = true; } else { chkC18.Checked = false; }

            //adult checkbox setup
            
            //name and display check boxes
            intAShowCount = 0;
            try { chkA1.Text = lstAdult[1].ToString(); chkA1.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA1.Visible = false; }
            try { chkA2.Text = lstAdult[2].ToString(); chkA2.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA2.Visible = false; }
            try { chkA3.Text = lstAdult[3].ToString(); chkA3.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA3.Visible = false; }
            try { chkA4.Text = lstAdult[4].ToString(); chkA4.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA4.Visible = false; }
            try { chkA5.Text = lstAdult[5].ToString(); chkA5.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA5.Visible = false; }
            try { chkA6.Text = lstAdult[6].ToString(); chkA6.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA6.Visible = false; }
            try { chkA7.Text = lstAdult[7].ToString(); chkA7.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA7.Visible = false; }
            try { chkA8.Text = lstAdult[8].ToString(); chkA8.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA8.Visible = false; }
            try { chkA9.Text = lstAdult[9].ToString(); chkA9.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA9.Visible = false; }
            try { chkA10.Text = lstAdult[10].ToString(); chkA10.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA10.Visible = false; }
            try { chkA11.Text = lstAdult[11].ToString(); chkA11.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA11.Visible = false; }
            try { chkA12.Text = lstAdult[12].ToString(); chkA12.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA12.Visible = false; }
            try { chkA13.Text = lstAdult[13].ToString(); chkA13.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA13.Visible = false; }
            try { chkA14.Text = lstAdult[14].ToString(); chkA14.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA14.Visible = false; }
            try { chkA15.Text = lstAdult[15].ToString(); chkA15.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA15.Visible = false; }
            try { chkA16.Text = lstAdult[16].ToString(); chkA16.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA16.Visible = false; }
            try { chkA17.Text = lstAdult[17].ToString(); chkA17.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA17.Visible = false; }
            try { chkA18.Text = lstAdult[18].ToString(); chkA18.Visible = true; intAShowCount = intAShowCount + 1; } catch { chkA18.Visible = false; }
            //check check boxes
            strAdultChecked = Properties.Settings.Default.aciAdultChecked.ToString().Split(',');
            if (strAdultChecked[0] == "True" || strAdultChecked[0] == "true") { chkA1.Checked = true; } else { chkA1.Checked = false; }
            if (strAdultChecked[1] == "True" || strAdultChecked[1] == "true") { chkA2.Checked = true; } else { chkA2.Checked = false; }
            if (strAdultChecked[2] == "True" || strAdultChecked[2] == "true") { chkA3.Checked = true; } else { chkA3.Checked = false; }
            if (strAdultChecked[3] == "True" || strAdultChecked[3] == "true") { chkA4.Checked = true; } else { chkA4.Checked = false; }
            if (strAdultChecked[4] == "True" || strAdultChecked[4] == "true") { chkA5.Checked = true; } else { chkA5.Checked = false; }
            if (strAdultChecked[5] == "True" || strAdultChecked[5] == "true") { chkA6.Checked = true; } else { chkA6.Checked = false; }
            if (strAdultChecked[6] == "True" || strAdultChecked[6] == "true") { chkA7.Checked = true; } else { chkA7.Checked = false; }
            if (strAdultChecked[7] == "True" || strAdultChecked[7] == "true") { chkA8.Checked = true; } else { chkA8.Checked = false; }
            if (strAdultChecked[8] == "True" || strAdultChecked[8] == "true") { chkA9.Checked = true; } else { chkA9.Checked = false; }
            if (strAdultChecked[9] == "True" || strAdultChecked[9] == "true") { chkA10.Checked = true; } else { chkA10.Checked = false; }
            if (strAdultChecked[10] == "True" || strAdultChecked[10] == "true") { chkA11.Checked = true; } else { chkA11.Checked = false; }
            if (strAdultChecked[11] == "True" || strAdultChecked[11] == "true") { chkA12.Checked = true; } else { chkA12.Checked = false; }
            if (strAdultChecked[12] == "True" || strAdultChecked[12] == "true") { chkA13.Checked = true; } else { chkA13.Checked = false; }
            if (strAdultChecked[13] == "True" || strAdultChecked[13] == "true") { chkA14.Checked = true; } else { chkA14.Checked = false; }
            if (strAdultChecked[14] == "True" || strAdultChecked[14] == "true") { chkA15.Checked = true; } else { chkA15.Checked = false; }
            if (strAdultChecked[15] == "True" || strAdultChecked[15] == "true") { chkA16.Checked = true; } else { chkA16.Checked = false; }
            if (strAdultChecked[16] == "True" || strAdultChecked[16] == "true") { chkA17.Checked = true; } else { chkA17.Checked = false; }
            if (strAdultChecked[17] == "True" || strAdultChecked[17] == "true") { chkA18.Checked = true; } else { chkA18.Checked = false; }

            //family checkbox setup
           
            //name and display check boxes
            intFShowCount = 0;
            try { chkF1.Text = lstFamily[1].ToString(); chkF1.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF1.Visible = false; }
            try { chkF2.Text = lstFamily[2].ToString(); chkF2.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF2.Visible = false; }
            try { chkF3.Text = lstFamily[3].ToString(); chkF3.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF3.Visible = false; }
            try { chkF4.Text = lstFamily[4].ToString(); chkF4.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF4.Visible = false; }
            try { chkF5.Text = lstFamily[5].ToString(); chkF5.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF5.Visible = false; }
            try { chkF6.Text = lstFamily[6].ToString(); chkF6.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF6.Visible = false; }
            try { chkF7.Text = lstFamily[7].ToString(); chkF7.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF7.Visible = false; }
            try { chkF8.Text = lstFamily[8].ToString(); chkF8.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF8.Visible = false; }
            try { chkF9.Text = lstFamily[9].ToString(); chkF9.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF9.Visible = false; }
            try { chkF10.Text = lstFamily[10].ToString(); chkF10.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF10.Visible = false; }
            try { chkF11.Text = lstFamily[11].ToString(); chkF11.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF11.Visible = false; }
            try { chkF12.Text = lstFamily[12].ToString(); chkF12.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF12.Visible = false; }
            try { chkF13.Text = lstFamily[13].ToString(); chkF13.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF13.Visible = false; }
            try { chkF14.Text = lstFamily[14].ToString(); chkF14.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF14.Visible = false; }
            try { chkF15.Text = lstFamily[15].ToString(); chkF15.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF15.Visible = false; }
            try { chkF16.Text = lstFamily[16].ToString(); chkF16.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF16.Visible = false; }
            try { chkF17.Text = lstFamily[17].ToString(); chkF17.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF17.Visible = false; }
            try { chkF18.Text = lstFamily[18].ToString(); chkF18.Visible = true; intFShowCount = intFShowCount + 1; } catch { chkF18.Visible = false; }
            //check check boxes
            strFamilyChecked = Properties.Settings.Default.aciFamilyChecked.ToString().Split(',');
            if (strFamilyChecked[0] == "True" || strFamilyChecked[0] == "true" ) { chkF1.Checked = true; } else { chkF1.Checked = false; }
            if (strFamilyChecked[1] == "True" || strFamilyChecked[1] == "true") { chkF2.Checked = true; } else { chkF2.Checked = false; }
            if (strFamilyChecked[2] == "True" || strFamilyChecked[2] == "true") { chkF3.Checked = true; } else { chkF3.Checked = false; }
            if (strFamilyChecked[3] == "True" || strFamilyChecked[3] == "true") { chkF4.Checked = true; } else { chkF4.Checked = false; }
            if (strFamilyChecked[4] == "True" || strFamilyChecked[4] == "true") { chkF5.Checked = true; } else { chkF5.Checked = false; }
            if (strFamilyChecked[5] == "True" || strFamilyChecked[5] == "true") { chkF6.Checked = true; } else { chkF6.Checked = false; }
            if (strFamilyChecked[6] == "True" || strFamilyChecked[6] == "true") { chkF7.Checked = true; } else { chkF7.Checked = false; }
            if (strFamilyChecked[7] == "True" || strFamilyChecked[7] == "true") { chkF8.Checked = true; } else { chkF8.Checked = false; }
            if (strFamilyChecked[8] == "True" || strFamilyChecked[8] == "true") { chkF9.Checked = true; } else { chkF9.Checked = false; }
            if (strFamilyChecked[9] == "True" || strFamilyChecked[9] == "true") { chkF10.Checked = true; } else { chkF10.Checked = false; }
            if (strFamilyChecked[10] == "True" || strFamilyChecked[10] == "true") { chkF11.Checked = true; } else { chkF11.Checked = false; }
            if (strFamilyChecked[11] == "True" || strFamilyChecked[11] == "true") { chkF12.Checked = true; } else { chkF12.Checked = false; }
            if (strFamilyChecked[12] == "True" || strFamilyChecked[12] == "true") { chkF13.Checked = true; } else { chkF13.Checked = false; }
            if (strFamilyChecked[13] == "True" || strFamilyChecked[13] == "true") { chkF14.Checked = true; } else { chkF14.Checked = false; }
            if (strFamilyChecked[14] == "True" || strFamilyChecked[14] == "true") { chkF15.Checked = true; } else { chkF15.Checked = false; }
            if (strFamilyChecked[15] == "True" || strFamilyChecked[15] == "true") { chkF16.Checked = true; } else { chkF16.Checked = false; }
            if (strFamilyChecked[16] == "True" || strFamilyChecked[16] == "true") { chkF17.Checked = true; } else { chkF17.Checked = false; }
            if (strFamilyChecked[17] == "True" || strFamilyChecked[17] == "true") { chkF18.Checked = true; } else { chkF18.Checked = false; }
        }

        private void loadFTPDetails()
        {
            txtHost.Text = Properties.Settings.Default.aciFTPHost.ToString();
            txtPassword.Text = Properties.Settings.Default.aciFTPPassword.ToString();
            txtPort.Text = Properties.Settings.Default.aciFTPPort.ToString();
            txtUsername.Text = Properties.Settings.Default.aciFTPUsername.ToString();
            txtRFAdult.Text = Properties.Settings.Default.aciRFAdult.ToString();
            txtRFChild.Text = Properties.Settings.Default.aciRFChild.ToString();
            txtRFFamily.Text = Properties.Settings.Default.aciRFFamily.ToString();
            txtRemoteFolder.Text = Properties.Settings.Default.aciRF.ToString();
            txtActivityLocation.Text = Properties.Settings.Default.aciUploadActivity.ToString();
        }

        private void cmdCancelFTP_Click(object sender, EventArgs e)
        {
            loadFTPDetails();
        }

        private void cmdSaveFTP_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.aciFTPHost = txtHost.Text;
            Properties.Settings.Default.aciFTPPassword = txtPassword.Text;
            Properties.Settings.Default.aciFTPUsername = txtUsername.Text;
            Properties.Settings.Default.aciFTPPort = Int32.Parse(txtPort.Text);
            Properties.Settings.Default.aciRF = txtRemoteFolder.Text;
            Properties.Settings.Default.aciRFChild = txtRFChild.Text;
            Properties.Settings.Default.aciRFAdult = txtRFAdult.Text;
            Properties.Settings.Default.aciRFFamily = txtRFFamily.Text;
            Properties.Settings.Default.aciRF = txtRemoteFolder.Text;
            Properties.Settings.Default.aciUploadActivity = txtActivityLocation.Text;
            Properties.Settings.Default.Save();
        }

        private void cmdBrowseSave_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLocal.Text = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.aciLocalSave = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void cmdLocalBackup_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLocalBackup.Text = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.aciLocalBackup = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void cmdProgramName_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.aciName = txtProgramName.Text;
            Properties.Settings.Default.Save();
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            if (bloaded == true)
            {
                strDateFrom = dtpFrom.Value.ToString("dd/MM/yyyy");
                Properties.Settings.Default.aciFrom = strDateFrom;
                Properties.Settings.Default.Save();
            }
        }

        private void cmdSavePassword_Click(object sender, EventArgs e)
        {
            if (txtOldPassword.Text == Properties.Settings.Default.aciPin.ToString())
            {
                if (txtNewPassword1.Text == txtNewPassword2.Text)
                {
                    Properties.Settings.Default.aciPin = txtNewPassword1.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Password has been successfully changed", "Password changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewPassword1.Text = "";
                    txtNewPassword2.Text = "";
                    txtOldPassword.Text = "";
                }
                else
                {
                    MessageBox.Show("New passwords are not identical, please reenter passwords and try again", "Password error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtNewPassword1.Focus();
                }
            }
            else
            {
                MessageBox.Show("Old password is not correct, please enter it again. Password hasn't been changed", "Password error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                txtOldPassword.Text = "";
                txtOldPassword.Focus();
            }
        }

        private void cmdCancelPassword_Click(object sender, EventArgs e)
        {
            txtOldPassword.Text = "";
            txtNewPassword1.Text = "";
            txtNewPassword2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.aciUploadInterval = Int32.Parse(cboUpload.SelectedIndex.ToString());
                Properties.Settings.Default.Save();
            }
            catch
            {
                MessageBox.Show("Wait interval not saved, please try again");
            }
        }

        private void cmdSaveInterval_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.aciWait = Int32.Parse(cboWait.SelectedIndex.ToString());
                Properties.Settings.Default.Save();
            }
            catch
            {
                MessageBox.Show("Wait interval not saved, please try again");
            }
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            if (bloaded == true)
            {
                strDateTo = dtpTo.Value.ToString("dd/MM/yyyy");
                Properties.Settings.Default.aciTo = strDateTo;
                Properties.Settings.Default.Save();
            }
        }

        private void cmdAddNewActivity_Click(object sender, EventArgs e)
        {
            addActivity();
        }

        private void rbUseInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUseInfo.Checked == true)
            {
                Properties.Settings.Default.aciUseInfo = true;
                Properties.Settings.Default.Save();
            }
        }

        private void rbDontUseInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDontUseInfo.Checked == true)
            {
                Properties.Settings.Default.aciUseInfo = false;
                Properties.Settings.Default.Save();
            }
        }

        private void cmdCrypto_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.aciDCPW = txtCrypto.ToString();
            Properties.Settings.Default.Save();
        }

        private void cmdSaveWait_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.aciReadWait = Int32.Parse(txtReadInterval.Text);
                Properties.Settings.Default.Save();
            }
            catch
            {
                MessageBox.Show("Please ensure the interval is a valid number, this has not been saved", "Number error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void cmdReadBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtReadLocation.Text = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.aciTableRead = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void chkC1_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }

        private void chkC2_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }

        private void chkC3_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }

        private void chkC4_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }

        private void chkC5_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }

        private void chkC6_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }

        private void chkC7_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }

        private void chkC8_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }

        private void chkC9_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }

        private void chkC10_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }

        private void chkC11_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }

        private void chkC12_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }

        private void chkC13_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }

        private void chkC14_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }

        private void chkC15_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }

        private void chkC16_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }

        private void chkC17_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }

        private void chkC18_CheckedChanged(object sender, EventArgs e)
        {
            updateChildChecked();
        }
        private void updateChildChecked()
        {
            if (bloaded == true)
            {
                string childToSave = chkC1.Checked.ToString() + "," + chkC2.Checked.ToString() + "," + chkC3.Checked.ToString() + "," + chkC4.Checked.ToString() + "," + chkC5.Checked.ToString() + "," + chkC6.Checked.ToString();
                childToSave = childToSave + "," + chkC7.Checked.ToString() + "," + chkC8.Checked.ToString() + "," + chkC9.Checked.ToString() + "," + chkC10.Checked.ToString() + "," + chkC11.Checked.ToString() + "," + chkC12.Checked.ToString();
                childToSave = childToSave + "," + chkC13.Checked.ToString() + "," + chkC14.Checked.ToString() + "," + chkC15.Checked.ToString() + "," + chkC16.Checked.ToString() + "," + chkC17.Checked.ToString() + "," + chkC18.Checked.ToString();
                Properties.Settings.Default.aciChildChecked = childToSave;
                Properties.Settings.Default.Save();
            }
        }

        private void chkA1_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void chkA2_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void chkA3_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void chkA4_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void chkA5_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void chkA6_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void chkA7_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void chkA8_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void chkA9_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void chkA10_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void chkA11_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void chkA12_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void chkA13_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void chkA14_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void chkA15_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void chkA16_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void chkA17_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void chkA18_CheckedChanged(object sender, EventArgs e)
        {
            updateAdultChecked();
        }

        private void updateAdultChecked()
        {
            if (bloaded == true)
            {
                string adultToSave = chkA1.Checked.ToString() + "," + chkA2.Checked.ToString() + "," + chkA3.Checked.ToString() + "," + chkA4.Checked.ToString() + "," + chkA5.Checked.ToString() + "," + chkA6.Checked.ToString();
                adultToSave = adultToSave + "," + chkA7.Checked.ToString() + "," + chkA8.Checked.ToString() + "," + chkA9.Checked.ToString() + "," + chkA10.Checked.ToString() + "," + chkA11.Checked.ToString() + "," + chkA12.Checked.ToString();
                adultToSave = adultToSave + "," + chkA13.Checked.ToString() + "," + chkA14.Checked.ToString() + "," + chkA15.Checked.ToString() + "," + chkA16.Checked.ToString() + "," + chkA17.Checked.ToString() + "," + chkA18.Checked.ToString();
                Properties.Settings.Default.aciAdultChecked = adultToSave;
                Properties.Settings.Default.Save();
            }
        }

        private void chkF1_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void chkF2_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void chkF3_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void chkF4_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void chkF5_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void chkF6_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void chkF7_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void chkF8_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void chkF9_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void chkF10_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void chkF11_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void chkF12_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void chkF13_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void chkF14_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void chkF15_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void chkF16_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void chkF17_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void chkF18_CheckedChanged(object sender, EventArgs e)
        {
            updateFamilyChecked();
        }

        private void updateFamilyChecked()
        {
            if (bloaded == true)
            {
                string famToSave = chkF1.Checked.ToString() + "," + chkF2.Checked.ToString() + "," + chkF3.Checked.ToString() + "," + chkF4.Checked.ToString() + "," + chkF5.Checked.ToString() + "," + chkF6.Checked.ToString();
                famToSave = famToSave + "," + chkF7.Checked.ToString() + "," + chkF8.Checked.ToString() + "," + chkF9.Checked.ToString() + "," + chkF10.Checked.ToString() + "," + chkF11.Checked.ToString() + "," + chkF12.Checked.ToString();
                famToSave = famToSave + "," + chkF13.Checked.ToString() + "," + chkF14.Checked.ToString() + "," + chkF15.Checked.ToString() + "," + chkF16.Checked.ToString() + "," + chkF17.Checked.ToString() + "," + chkF18.Checked.ToString();
                Properties.Settings.Default.aciFamilyChecked = famToSave;
                Properties.Settings.Default.Save();
            }
        }

        private void cmdTableBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtTableSave.Text = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.aciTableWrite = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void cmdDeleteTable_Click(object sender, EventArgs e)
        {
            if (lstTableNo.SelectedIndex < 0 || lstTableNames.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a table number before attempting to delete it", "Missing table number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                lstTableNames.Items.RemoveAt(lstTableNames.SelectedIndex);
                string strTemp = lstTableNames.Items[0].ToString();
                for (i = 1; i < lstTableNames.Items.Count; i++)
                {
                    strTemp = strTemp + "," + lstTableNames.Items[i].ToString();
                }
                Properties.Settings.Default.aciTableNames = strTemp;
                Properties.Settings.Default.Save();
                loadTableNames();
            }
        }

        private void cmdAddTable_Click(object sender, EventArgs e)
        {
            if (txtTableName.Text != "")
            {
                lstTableNames.Items.Add(txtTableName.Text);
                sortTables();
                string strTemp = lstTableNames.Items[0].ToString();
                for (i = 1; i < lstTableNames.Items.Count; i++)
                {
                    strTemp = strTemp + "," + lstTableNames.Items[i].ToString();
                }
                Properties.Settings.Default.aciTableNames = strTemp;
                Properties.Settings.Default.Save();
                loadTableNames();
            }
        }
        private void sortTables()
        {
            List<string> lstTableNameSort = new List<string>();
            for (i = 0; i < lstTableNames.Items.Count; i++)
            {
                lstTableNameSort.Add(lstTableNames.Items[i].ToString());
            }
        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void addActivity()
        {
            if (txtAddActivity.Text != "")
            {
                lstOtherActivites.Items.Add(txtAddActivity.Text);
                string strTemp = lstOtherActivites.Items[0].ToString();
                for (i = 1; i < lstOtherActivites.Items.Count; i++)
                {
                    strTemp = strTemp + "," + lstOtherActivites.Items[i].ToString();
                }
                Properties.Settings.Default.aciActivities = strTemp;
                Properties.Settings.Default.Save();
                loadOtherActivities();
            }
        }

        private void cmdDelOtherActivity_Click(object sender, EventArgs e)
        {
            if (lstOtherActivites.SelectedIndex > -1)
            {
                string strTemp = "";
                string strMessage = "Are you sure you want to delete " + lstOtherActivites.Items[lstOtherActivites.SelectedIndex].ToString() + "?";
                DialogResult dg = MessageBox.Show(strMessage, "Delete Activity", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dg == DialogResult.Yes)
                {
                    lstOtherActivites.Items.RemoveAt(lstOtherActivites.SelectedIndex);
                    if (lstOtherActivites.Items.Count > 0)
                    {
                        strTemp = lstOtherActivites.Items[0].ToString();
                        for (i = 1; i < lstOtherActivites.Items.Count; i++)
                        {
                            strTemp = strTemp + "," + lstOtherActivites.Items[i].ToString();
                        }
                    }
                    Properties.Settings.Default.aciActivities = strTemp;
                    Properties.Settings.Default.Save();
                    loadOtherActivities();
                }
            }
        }

        private void loadOtherActivities()
        {
            string[] strSplit = Properties.Settings.Default.aciActivities.ToString().Split(',');
            lstOtherActivites.Items.Clear();
            for (i = 0; i < strSplit.Length; i++)
            {
                lstOtherActivites.Items.Add(strSplit[i]);
            }
        }

    }
}

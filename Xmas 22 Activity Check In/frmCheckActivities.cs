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
using System.Net;
using Renci.SshNet;

namespace Xmas_22_Activity_Check_In
{
    public partial class frmCheckActivities : Form
    {
        int i,j;
        string strRef,b,c;
        string Host = Properties.Settings.Default.aciFTPHost.ToString();
        int Port = Properties.Settings.Default.aciFTPPort;
        string Username = Properties.Settings.Default.aciFTPUsername.ToString();
        string Password = Properties.Settings.Default.aciFTPPassword.ToString();
        bool bloaded;
        List<string> ftp_list = new List<string>();
        List<string> lstActivities = new List<string>();
        public frmCheckActivities()
        {
            InitializeComponent();
        }

        private void frmCheckActivities_Load(object sender, EventArgs e)
        {
            bloaded = false;
            loadSettings();
        }

        private void loadSettings()
        {
            this.Text = "Check activities for " + Properties.Settings.Default.aciName.ToString();
            this.Width = 1200;
            this.Height = 800;
            loadDates();
            loadTimes();
            if (Properties.Settings.Default.aciRemember==0)
            {
                gbFindByRef.Visible = true;
                gbFindByDateTime.Visible = false;
            }
            else
            {
                gbFindByRef.Visible = false;
                gbFindByDateTime.Visible = true;
            }
            lblName.Text = "Check Activites Attended by Customer";
            lblName.Left = 10;
            lblBackupInfo.Text = "";
            tmDownload.Interval = Properties.Settings.Default.aciWait * 1000;
            tmDownload.Stop();
            tmFindFamilies.Interval = 500;
            tmFindFamilies.Stop();
            bloaded = true;
            gbFindByDateTime.Left = 220;
            gbFindByDateTime.Top = 100;
            gbFindByRef.Left = 220;
            gbFindByRef.Top = 100;
            lblCheckRef.Text = "";
            lblCheckName.Text = "";
            cboCheckTime.Enabled = false;
            loadOtherActivities();
            displayTime();
            resizeAll();
        }

        private void loadOtherActivities()
        {
            string[] strSplit = Properties.Settings.Default.aciActivities.ToString().Split(',');
            lstActivities.Clear();
            dgvCheck.RowCount = strSplit.Count();
            for (i = 0; i < strSplit.Length; i++)
            {
                lstActivities.Add(strSplit[i]);
                dgvCheck.Rows[i].Cells[0].Value = strSplit[i];
            }
            dgvCheck.Height = 30 + (strSplit.Count() * 25);
            cmdClearCheck.Top = dgvCheck.Top + dgvCheck.Height + 10;
        }

        private void displayTime()
        {
            lblTime.Text = DateTime.Now.ToString("F");
        }

        private void loadDates()
        {
            cboCheckDate.Items.Clear();
            DateTime df = DateTime.Parse(Properties.Settings.Default.aciFrom);
            DateTime dt = DateTime.Parse(Properties.Settings.Default.aciTo);
            if (df < dt)
            {
                try
                {
                    DateTime dd = DateTime.Now;
                    string[] DT = Properties.Settings.Default.aciFrom.ToString().Split('/');
                    dd = new DateTime(Int32.Parse(DT[2]), Int32.Parse(DT[1]), Int32.Parse(DT[0]));
                    string strDate = df.ToString("dd/MM/yyyy");
                    while (strDate != dt.ToString("dd/MM/yyyy"))
                    {
                        strDate = dd.ToString("dd/MM/yyyy");
                        cboCheckDate.Items.Add(strDate);
                        dd = dd.AddDays(1);
                    }
                }
                catch
                { }
            }
        }
        private void loadTimes()
        {
            string strText;
            //load times
            i = 9;
            while (i < 19)
            {
                j = 0;
                while (j < 60)
                {
                    if (j == 0)
                    {
                        strText = i.ToString() + ":00";
                    }
                    else
                    {
                        strText = i.ToString() + ":" + j.ToString();
                    }
                    cboCheckTime.Items.Add(strText);
                    j = j + 10;
                }
                i = i + 1;
            }
        }

        private void cmdFindByRef_Click(object sender, EventArgs e)
        {
            gbFindByRef.Visible = true;
            gbFindByDateTime.Visible = false;
            Properties.Settings.Default.aciRemember = 0;
            Properties.Settings.Default.Save();
        }

        private void cmdFindByDateAndTime_Click(object sender, EventArgs e)
        {
            gbFindByRef.Visible = false;
            gbFindByDateTime.Visible = true;
            Properties.Settings.Default.aciRemember = 1;
            Properties.Settings.Default.Save();
        }

        private void cmdCheckByRef_Click(object sender, EventArgs e)
        {
            strRef = txtCheckRef.Text;
            findFamily();
        }

        private void findFamily()
        {
            clearInfo();
            if (txtCheckRef.Text == "" )
            {
                MessageBox.Show("Please enter a reference number");
            }
            else
            {
                if (CheckForInternetConnection() == true)
                {
                    //check fusemetrix
                    String urlstr = "https://4k-photos.co.uk/checkRef.php?ref=" + strRef.ToString() + "&name=" + Properties.Settings.Default.aciName.ToString(); ;
                    WebClient client = new WebClient();
                    System.IO.Stream response = client.OpenRead(urlstr);
                    System.IO.StreamReader reads = new System.IO.StreamReader(response);
                    tmDownload.Start();
                    client.Dispose();
                    response.Close();
                    response.Dispose();
                    reads.Close();
                    reads.Dispose();
                }
                else
                {
                    MessageBox.Show("Can't connect to internet, checking backups for today", "No Internet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void clearInfo()
        {
            lblCheckName.Text = "";
            lblCheckRef.Text = "";
            cmdClearCheck.Visible = false;
            dgvCheck.Rows.Clear();
            dgvCheck.Refresh();
            loadOtherActivities();

        }

        public static bool CheckForInternetConnection()
        {
            // try
            // {
            //     using (var client = new WebClient())
            //     using (client.OpenRead("http://google.com/generate_204"))
            return true;
            // }
            // catch
            // {
            //      return false;
            //  }
        }

        private void tmDownload_Tick(object sender, EventArgs e)
        {
            tmDownload.Stop();
            readCreatedFile();
        }

        private void readCreatedFile()
        {
            //assign temporary variables
            var list = new List<string>();
            string c = "", b = "", strread = "";
            bool dSuccess = true, rSuccess = true;

            //download created file from server
            try
            {
                strread = Properties.Settings.Default.aciName.ToString() + strRef.ToString() + ".txt";
                b = Properties.Settings.Default.aciLocalSave.ToString() + "/" + strread;
                c = Properties.Settings.Default.aciRF.ToString() + "/" + strread;
                using (var sftp = new SftpClient(Host, Port, Username, Password))
                {
                    sftp.Connect(); //connect to server

                    using (var file = File.OpenWrite(b))
                    {
                        sftp.DownloadFile(c, file);//download file
                    }
                    sftp.Disconnect();
                }
                dSuccess = true;
            }
            catch
            {
                dSuccess = false;
            }

            //read file
            if (dSuccess == true)
            {
                try
                {
                    var fileStream = new FileStream(b, FileMode.Open, System.IO.FileAccess.Read);
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            list.Add(line);
                        }
                    }
                    fileStream.Close();
                    rSuccess = true;
                }
                catch
                {
                    MessageBox.Show("Problem reading downloaded text file", "File Error");
                    rSuccess = false;
                }
            }

            //
            if (rSuccess == true && dSuccess == true)
            {
                //check if the family has already attended this session
                //read local file

                var fileStream = new FileStream(b, FileMode.Open, System.IO.FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        list.Add(line);
                    }
                }
                fileStream.Close();

                string[] strSplit = list[0].ToString().Split(',');
                if (strSplit[0] == "False")
                {
                    lblCheckName.Text = "Can't Find Booking Reference";
                    lblCheckName.BackColor = Color.Red;
                    lblCheckRef.Text = "Can't Find Booking Reference";
                    lblCheckRef.BackColor = Color.Red;
                }
                else
                {
                    int intAdult = 0, intChild = 0;
                    lblCheckRef.Text = strRef.ToString();
                    lblCheckRef.Visible = true;
                    lblCheckName.Text = strSplit[1].ToString();
                    i = 0;
                    while (i < strSplit.Count())
                    {
                        if (strSplit[i].ToString() == "Adult Name")
                        {
                            intAdult = intAdult + 1;
                        }
                        if (strSplit[i].ToString() == "Child's Name")
                        {
                            intChild = intChild + 1;
                        }
                        i = i + 1;
                    }

                }
                txtCheckRef.Text = "";
                cmdClearCheck.Visible = true;
                checkActivities();
            }
        }

        private void checkActivities()
        {
            //assign temporary variables
            var list = new List<string>();
            string c = "", b = "", strread = "";
            
            for (i=0; i<lstActivities.Count;i++)
            {
                bool dSuccess = false, rSuccess = false;
                try
                {
                    string strDate = DateTime.Now.ToString("ddMMyyyy");
                    strread = strDate.ToString() + "_" + lstActivities[i].ToString() + ".csv";
                    b = Properties.Settings.Default.aciLocalSave.ToString() + "\\" + strread;
                    c = Properties.Settings.Default.aciUploadActivity.ToString() + "/" + strread;
                    using (var sftp = new SftpClient(Host, Port, Username, Password))
                    {
                        sftp.Connect(); //connect to server

                        using (var file = File.OpenWrite(b))
                        {
                            sftp.DownloadFile(c, file);//download file
                        }
                        sftp.Disconnect();
                        dSuccess = true;
                    }
                }
                catch
                {
                }

                if (dSuccess == true)
                {
                    try
                    {
                        var fileStream = new FileStream(b, FileMode.Open, System.IO.FileAccess.Read);
                        using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                        {
                            string line;
                            while ((line = streamReader.ReadLine()) != null)
                            {
                                list.Add(line);
                            }
                        }
                        fileStream.Close();
                        rSuccess = true;
                    }
                    catch
                    {
                        MessageBox.Show("Problem reading downloaded text file", "File Error");
                        rSuccess = false;
                    }
                }

                if (dSuccess == true && rSuccess == true)
                {
                    for (int x = 0; x < list.Count; x++)
                    {
                        string[] strTemp = list[x].ToString().Split(',');
                        if (strTemp[1].ToString() == lblCheckRef.Text.ToString())
                        {
                            dgvCheck.Rows[i].Cells[1].Value = "Attended";
                            dgvCheck.Rows[i].Cells[2].Value = strTemp[0].ToString();
                            dgvCheck.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                }
            }
        }

        private void cmdCheckByDateAndTime_Click(object sender, EventArgs e)
        {
            strRef = lblCheckRef.Text;
            findFamily();
        }

        private void cboCheckTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bloaded == true)
            {
                if (cboCheckDate.Text == "" || cboCheckTime.Text == "")
                {
                    MessageBox.Show("Please select a date and time", "Missing Variables");
                }
                else
                {
                    if (CheckForInternetConnection() == true)
                    {
                        //reset grid
                        dgvCheckFamilies.Rows.Clear();
                        dgvCheckFamilies.Refresh();
                        lblCheckName.Text = "";
                        cmdCheckByDateAndTime.Enabled = false;
                        lblCheckRef.Text = "";
                        //reach out to api
                        c = cboCheckDate.Text.Replace(@"/", "") + cboCheckTime.Text.Replace(@":", "") + Properties.Settings.Default.aciName.ToString() + ".txt";
                        String urlstr = "https://4k-photos.co.uk/sessionTimeAllInfo.php?date=" + cboCheckDate.Text.ToString() + "&time=" + cboCheckTime.Text + "&name=" + Properties.Settings.Default.aciName.ToString();
                        WebClient client = new WebClient();
                        System.IO.Stream response = client.OpenRead(urlstr);
                        System.IO.StreamReader reads = new System.IO.StreamReader(response);
                        tmFindFamilies.Enabled = true;
                        lblBackupInfo.Text = "Finding Families";
                        tmDisplayInfo.Enabled = true;
                    }
                }
            }
        }

        private void tmFindFamilies_Tick(object sender, EventArgs e)
        {
            readFile();
            tmFindFamilies.Enabled = false;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCheckFamilies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    lblCheckName.Text = dgvCheckFamilies.Rows[e.RowIndex].Cells[1].Value.ToString();
                    lblCheckRef.Text = dgvCheckFamilies.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtCheckRef.Text = dgvCheckFamilies.Rows[e.RowIndex].Cells[0].Value.ToString();
                    cmdCheckByDateAndTime.Visible = true;
                    cmdCheckByDateAndTime.Enabled = true;
                }
            }
            catch { }
        }

        private void tmDisplayTime_Tick(object sender, EventArgs e)
        {
            displayTime();
        }

        private void txtCheckRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                strRef = txtCheckRef.Text;
                findFamily();
            }
        }

        private void cmdClearCheck_Click(object sender, EventArgs e)
        {
            clearInfo();
        }

        private void cboCheckDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCheckTime.SelectedIndex = -1;
            cboCheckTime.Text = "";
            cboCheckTime.Enabled = true;
        }

        private void frmCheckActivities_Resize(object sender, EventArgs e)
        {
            resizeAll();
        }

        private void readFile()
        {
            bool dSuccess = true, rSuccess = true, bColour = false;
            string strread;
            var list = new List<string>();

            try
            {
                ftp_list.Clear();
                using (var sftp = new SftpClient(Host, Port, Username, Password))
                {
                    sftp.Connect(); //connect to server


                    b = Properties.Settings.Default.aciLocalSave.ToString() + "/" + c;
                    c = Properties.Settings.Default.aciRF.ToString() + "/" + c;
                    using (var file = File.OpenWrite(b))
                    {
                        sftp.DownloadFile(c, file);//download file
                    }
                    dSuccess = true;
                    sftp.Disconnect();
                }
            }
            catch
            {
                MessageBox.Show("Problem reading downloaded text file", "File Error");
                dSuccess = false;
            }

            if (dSuccess == true)
            {
                try
                {

                    strread = Properties.Settings.Default.aciLocalSave.ToString() + "/" + cboCheckDate.Text.Replace(@"/", "") + cboCheckTime.Text.Replace(@":", "") + Properties.Settings.Default.aciName.ToString() + ".txt";
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
                    rSuccess = true;
                }
                catch
                {
                    MessageBox.Show("Problem reading downloaded text file", "File Error");
                    rSuccess = false;
                }

                if (rSuccess == true && dSuccess == true)
                {
                    if (list.Count > 0)
                    {
                        dgvCheckFamilies.RowCount = list.Count;
                        i = 0;
                        while (i < list.Count)
                        {
                            string[] strSplit = list[i].ToString().Split(',');
                            dgvCheckFamilies.Rows[i].Cells[0].Value = strSplit[0];
                            dgvCheckFamilies.Rows[i].Cells[1].Value = strSplit[1];
                            if (bColour == false)
                            {
                                dgvCheckFamilies.Rows[i].DefaultCellStyle.BackColor = Color.White;
                                bColour = true;
                            }
                            else
                            {
                                dgvCheckFamilies.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                                bColour = false;
                            }
                            i++;
                        }
                    }
                }
            }
        }

        private void tmDisplayInfo_Tick(object sender, EventArgs e)
        {
            tmDisplayInfo.Enabled = false;
            lblBackupInfo.Text = "";
        }

        private void resizeAll()
        {
            lblBackupInfo.Left = (pnlLeft.Width / 2) - (lblBackupInfo.Left / 2);
            lblTime.Left = pnlTop.Width - lblTime.Width - 10;
            cmdClose.Top = pnlLeft.Height - 15 - cmdClose.Height;
        }
    }
}

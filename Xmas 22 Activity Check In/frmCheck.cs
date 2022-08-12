using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Renci.SshNet;

namespace Xmas_22_Activity_Check_In
{
    public partial class frmCheck : Form
    {
        bool bloaded;
        bool bexit;
        string strMessage,strText,strRef,b,c;
        int i,j,intTotalPeople,intFamilyCount,intT;
        List<string> ftp_list = new List<string>();
        //Download the created file
        string Host = Properties.Settings.Default.aciFTPHost.ToString();
        int Port = Properties.Settings.Default.aciFTPPort;
        string Username = Properties.Settings.Default.aciFTPUsername.ToString();
        string Password = Properties.Settings.Default.aciFTPPassword.ToString();
        List<string> lstTableRef = new List<string>();
        List<int> intTimeAtTables = new List<int>();
        List<int> intTables = new List<int>();
        List<bool> bTableFull = new List<bool>();

        public frmCheck()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            strMessage = "Are you sure you want to close " + Properties.Settings.Default.aciName.ToString() + "?";
            DialogResult dg = MessageBox.Show(strMessage, "Close?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dg == DialogResult.Yes)
            {
                bexit = true;
                this.Close();
            }
        }

        private void frmCheck_Load(object sender, EventArgs e)
        {
            bloaded = false;
            this.Width = 1200;
            this.Height = 800;
            loadSettings();
        }

        private void loadSettings()
        {
            dgvTables.DefaultCellStyle.ForeColor = Color.Black;
            this.Text = "Activity Check In for " + Properties.Settings.Default.aciName.ToString();
            lblName.Text = Properties.Settings.Default.aciName.ToString() + " Check In";
            loadDates();
            loadTimes();
            displayTime();
            loadTables();
            tmDownload.Interval = Properties.Settings.Default.aciWait * 1000;
            tmDownload.Stop();
            tmFindFamilies.Interval = 500;
            tmFindFamilies.Stop();
            lblPeople.Text = "0";
            closePanels();
            clearFamilyDetails();
            if (Properties.Settings.Default.aciRemember == 0)
            {
                pnlFindByReference.Visible = true;
            }
            else
            {
                pnlFindbyDateandTime.Visible = true;
            }
            bloaded= true;
            lblBackupInfo.Text = "";
            resizeAll();
        }

        private void resizeAll()
        {
            pnlFindByReference.Left = pnlLeft.Width + 10;
            pnlFindByReference.Top = pnlTime.Top + pnlTime.Height + 10;
            pnlFindbyDateandTime.Left = pnlLeft.Width + 10;
            pnlFindbyDateandTime.Top = pnlTime.Top + pnlTime.Height + 10;
            pnlInfo.Left = pnlFindByReference.Left + pnlFindByReference.Width + 10;
            pnlInfo.Top = pnlTime.Top + pnlTime.Height + 10;
            pnlInfo.Width = this.Width - pnlLeft.Width - pnlFindByReference.Width - 40;
            pnlInfo.Height = this.Width - pnlTime.Height - 20;
            lblBackupInfo.Left = ((pnlLeft.Width + pnlTime.Width) / 2) - (lblName.Width / 2); 
            lblTime.Left = pnlTime.Width - lblTime.Width - 5;
            lblName.Left = 10;
            cmdClose.Top = pnlLeft.Height - 15 - cmdClose.Height;
            cmdCheckFamily.Top = cmdClose.Top - 15 - cmdCheckFamily.Height;
        }

        private void loadTables()
        {
            dgvTables.DataSource = null;
            dgvTables.Rows.Clear();
            cboTable.Items.Clear();
            intTables.Clear();
            intTimeAtTables.Clear();

            if (Properties.Settings.Default.aciTables == true)
            {
                lblTable.Visible = true;
                cboTable.Visible = true;
                cmdSendToTable.Visible = true;
            }
            else
            {
                lblTable.Visible = false;
                cboTable.Visible = false;
                cmdSendToTable.Visible = false;
            }

            i = 0;
            string[] strTemp = Properties.Settings.Default.aciTableNames.ToString().Split(',');
            dgvTables.RowCount = strTemp.Count();
            dgvTables.Height = strTemp.Count() * 30;
            bool bTables = true;
            for (i = 0; i < strTemp.Count(); i++)
            {
                intTimeAtTables.Add(0);
                dgvTables.Rows[i].Cells[0].Value = strTemp[i];
                dgvTables.Rows[i].Cells[1].Value = "Empty";
                dgvTables.Rows[i].Cells[2].Value = "Empty";
                cboTable.Items.Add(strTemp[i]);
                bTableFull.Add(false);
                intTables.Add(0);
                if (bTables==true)
                {
                    dgvTables.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    bTables = false;
                }
                else
                {
                    dgvTables.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                    bTables = true;
                }
            }

            lstTableRef.Clear();
            for (i = 0; i <strTemp.Count(); i++)
            {
                lstTableRef.Add("");
            }


            //string gs = Properties.Settings.Default.aciTableWrite + "\\table.txt";
            //File.WriteAllText(gs, string.Empty);
        }

        private void cmdFindByRef_Click(object sender, EventArgs e)
        {
            closePanels();
            pnlFindByReference.Visible = true;
            Properties.Settings.Default.aciRemember = 0;
            Properties.Settings.Default.Save();
        }

        private void closePanels()
        {
            pnlFindByReference.Visible = false;
            pnlFindbyDateandTime.Visible = false;
            if (pnlInfo.Visible==false)
            {
                pnlInfo.Visible = false;
            }
        }

        private void cmdFindByDateAndTime_Click(object sender, EventArgs e)
        {
            closePanels();
            cmdFindDT.Visible = false;
            lblRefFound.Text = "";
            lblFamilyFound.Text = "";
            dgvFamily.DataSource = null;
            dgvFamily.Rows.Clear();
            cboTime.Enabled = false;
            pnlFindbyDateandTime.Visible = true;
            Properties.Settings.Default.aciRemember = 1;
            Properties.Settings.Default.Save();
        }

        private void tmDisplayTime_Tick(object sender, EventArgs e)
        {
            displayTime();
        }

        private void displayTime()
        {
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void cmdFind_Click(object sender, EventArgs e)
        {
            strRef = txtFind.Text;
            findFamily();
        }

        private void findFamily()
        {
            clearInfo();
            if (txtFind.Text == "")
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
            lblRedeemed.Text = "";
            lblFamilyName.Text = "";
            lblAdults.Text = "";
            lblChildren.Text = "";
            lblFamilyName.Text = "";
            lblRef.Text = "";
            cmdCheckIn.Enabled = false;
        }

        private void cmdFindDT_Click(object sender, EventArgs e)
        {
            strRef = lblRefFound.Text;
            findFamily();
        }

        private void cboDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTime.SelectedIndex = -1;
            cboTime.Text = "";
            cboTime.Enabled = true;
        }

        private void cboTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bloaded == true)
            {
                if (cboDate.Text == "" || cboTime.Text == "")
                {
                    MessageBox.Show("Please select a date and time", "Missing Variables");
                }
                else
                {
                    if (CheckForInternetConnection() == true)
                    {
                        //reset grid
                        dgvFamily.Rows.Clear();
                        dgvFamily.Refresh();
                        lblFamilyFound.Text = "";
                        cmdFindDT.Enabled = false;
                        lblRefFound.Text = "";
                        //reach out to api
                        c = cboDate.Text.Replace(@"/", "") + cboTime.Text.Replace(@":", "") + Properties.Settings.Default.aciName.ToString() + ".txt";
                        String urlstr = "https://4k-photos.co.uk/sessionTimeAllInfo.php?date=" + cboDate.Text.ToString() + "&time=" + cboTime.Text + "&name=" + Properties.Settings.Default.aciName.ToString();
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

        private void tmDisplayInfo_Tick(object sender, EventArgs e)
        {
            tmDisplayInfo.Enabled = false;
            lblBackupInfo.Text = "";
        }

        private void dgvFamily_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    lblFamilyFound.Text = dgvFamily.Rows[e.RowIndex].Cells[1].Value.ToString();
                    lblRefFound.Text = dgvFamily.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtFind.Text = dgvFamily.Rows[e.RowIndex].Cells[0].Value.ToString();
                    cmdFindDT.Visible = true;
                    cmdFindDT.Enabled = true;
                }
            }
            catch { }
        }

        private void dgvFamily_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    lblFamilyFound.Text = dgvFamily.Rows[e.RowIndex].Cells[1].Value.ToString();
                    lblRefFound.Text = dgvFamily.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtFind.Text = dgvFamily.Rows[e.RowIndex].Cells[0].Value.ToString();
                    cmdFindDT.Visible = true;
                    cmdFindDT.Enabled = true;
                }
            }
            catch { }
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

            pnlInfo.Visible = true;

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
                    lblRedeemed.Text = "Can't Find Booking Reference";
                    lblRedeemed.BackColor = Color.Red;
                    lblFamilyName.Text = "";
                    lblAdults.Text = "";
                    lblChildren.Text = "";
                    lblUnders.Text = "";
                    lblRef.Text = "";
                }
                else
                {
                    int intAdult = 0, intChild = 0;
                    lblRef.Text = strRef.ToString();
                    lblRef.Visible = true;
                    lblFamilyName.Text = strSplit[1].ToString();
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
                    intTotalPeople = i;
                    lblAdults.Text = intAdult.ToString();
                    lblChildren.Text = intChild.ToString();
                    cmdCheckIn.Enabled = true;

                    isRedeemed();
                }
                txtFind.Text = "";
                lblFamilyFound.Text = "";
                lblRefFound.Text = "";
                cmdClearFamily.Visible = true;

                if (Properties.Settings.Default.aciTables == true)
                {
                    cmdCheckIn.Visible = false;
                    cmdCheckInAgain.Visible = false;
                    cboTable.Visible = true;
                }
            }
        }

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                strRef = txtFind.Text;
                findFamily();
            }
        }

        private void cmdCheckIn_Click(object sender, EventArgs e)
        {
            checkIn();
        }

        private void cmdFamilyLeft_Click(object sender, EventArgs e)
        {
            if (dgvTables.SelectedRows != null)
            {
                DialogResult dg = MessageBox.Show("Are you sure you want to remove the selected family?", "Remove family", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dg == DialogResult.Yes)
                {
                    dgvTables.Rows[dgvTables.CurrentCell.RowIndex].Cells[1].Value = "Empty";
                    dgvTables.Rows[dgvTables.CurrentCell.RowIndex].Cells[2].Value = "00:00";
                    bTableFull[dgvTables.CurrentCell.RowIndex] = false;
                    dgvTables.ClearSelection();
                    cmdFamilyLeft.Visible = false;
                }
            }
        }

        private void clearFamilyDetails()
        {
            lblAdults.Text = "0";
            lblChildren.Text = "0";
            lblWheelChair.Text = "0";
            lblUnders.Text = "0";
            cmdCheckIn.Visible = false;
            cmdCheckInAgain.Visible = false;
            cmdFamilyLeft.Visible = false;
            cmdClearFamily.Visible = false;
            lblFamilyName.Text = "";
            lblRedeemed.Text = "";
            txtFind.Text = "";
            txtFind.Focus();
            lblRef.Text = "";
            lblFamilyFound.Text = "";
            cmdSendToTable.Visible = false;
        }

        private void pnlInfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdClearFamily_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Are you sure you want to clear this family?", "Clear Family?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                clearFamilyDetails();
            }
        }

        private void cmdSendToTable_Click(object sender, EventArgs e)
        {
            checkIn();

            bool bReplace = false;

            if (cboTable.SelectedIndex < -1)
            {
                MessageBox.Show("Please select a table first", "Please select a table", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (intTables[cboTable.SelectedIndex] == 1)
                {
                    string strMessage = cboTable.Text + " is currently occupied, do you want to replace the people on this table?";
                    DialogResult dg = MessageBox.Show(strMessage, "Table Occupied", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dg == DialogResult.Yes)
                    {
                        bReplace = true;
                    }
                }
                else
                {
                    bReplace = true;
                }
                if (bReplace == true)
                {
                    //update table number
                    intTables[cboTable.SelectedIndex] = 1;
                    dgvTables.Rows[cboTable.SelectedIndex].Cells[1].Value = lblFamilyName.Text;
                    bTableFull[cboTable.SelectedIndex] = true;
                    intTimeAtTables[cboTable.SelectedIndex] = 0;
                    //send/store data for place to go
                    string strTables = dgvTables.Rows[0].Cells[1].Value.ToString();
                    string strTableName = dgvTables.Rows[0].Cells[0].Value.ToString();
                    lstTableRef[cboTable.SelectedIndex] = lblRef.Text;
                    string strRef1 = lstTableRef[0].ToString();
                    for (i = 1; i < dgvTables.RowCount; i++)
                    {
                        strTables = strTables + "," + dgvTables.Rows[i].Cells[1].Value.ToString();
                        strTableName = strTableName + "," + dgvTables.Rows[i].Cells[0].Value.ToString();
                        strRef1 = strRef1 + "," + lstTableRef[i].ToString();
                    }

                    Random rnd = new Random();
                    string r1 = rnd.Next(999999).ToString();

                    try
                    {
                        // Write file using StreamWriter to local location for upload
                        string gs = Properties.Settings.Default.aciTableWrite + "\\table.txt";
                        using (StreamWriter writer = new StreamWriter(gs))
                        {
                            writer.WriteLine(r1);
                            writer.WriteLine(strTableName);
                            writer.WriteLine(strTables);
                            writer.WriteLine(strRef1);
                        }
                        var lines = File.ReadAllLines(gs).Where(arg => !string.IsNullOrWhiteSpace(arg));
                        File.WriteAllLines(gs, lines);
                        cboTable.SelectedIndex = -1;
                        cboTable.Text = "";
                    }
                    catch
                    {
                        MessageBox.Show("Can't save information ready to upload, check it is a valid network connection", "Save error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
        }

        private void cboTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdSendToTable.Visible = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmdCheckFamily_Click(object sender, EventArgs e)
        {
            frmCheckActivities frmcheckactivities = new frmCheckActivities();
            frmcheckactivities.Show();
        }

        private void frmCheck_Resize(object sender, EventArgs e)
        {
            resizeAll();
        }

        private void lblTable_Click(object sender, EventArgs e)
        {

        }

        private void dgvTables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTables.Rows[e.RowIndex].Cells[1].Value != "Empty")
            {
                cmdFamilyLeft.Visible = true;
                cmdFamilyLeft.Enabled = true;
            }
            else
            {
                cmdFamilyLeft.Visible = false;
            }
        }

        private void dgvTables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTables.Rows[e.RowIndex].Cells[1].Value != "Empty")
            {
                cmdFamilyLeft.Visible = true;
                cmdFamilyLeft.Enabled = true;
            }
            else
            {
                cmdFamilyLeft.Visible = false;
            }
        }

        private void frmCheck_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bexit != true)
            {
                strMessage = "Are you sure you want to close " + Properties.Settings.Default.aciName.ToString() + "?";
                DialogResult dg = MessageBox.Show(strMessage, "Close?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dg == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void tmUpload_Tick(object sender, EventArgs e)
        {
            uploadData();
        }

        private void uploadData()
        {
            string strMessage = "";

            if (CheckForInternetConnection() == true)
            {
                try
               {
                    string Host = Properties.Settings.Default.aciFTPHost.ToString();
                    int Port = Properties.Settings.Default.aciFTPPort;
                    string Username = Properties.Settings.Default.aciFTPUsername.ToString();
                    string Password = Properties.Settings.Default.aciFTPPassword.ToString();
                    string targetDirectory = Properties.Settings.Default.aciUploadActivity.ToString();



                    string strDate = DateTime.Now.ToString("ddMMyyyy");
                    string strFileLocation = Properties.Settings.Default.aciLocalSave.ToString() + "\\" + strDate.ToString() + "_" + Properties.Settings.Default.aciName.ToString() + ".csv";

                    string strUploadFile = strDate.ToString() + "_" + Properties.Settings.Default.aciName.ToString() + ".csv";

                    using (var client = new SftpClient(Host, Port, Username, Password))
                    {
                        client.Connect();
                        client.ChangeDirectory(targetDirectory);
                        if (client.IsConnected)
                        {
                            using (var fileStream = new FileStream(strFileLocation, FileMode.Open))
                            {
                                client.UploadFile(fileStream, strUploadFile);
                            }
                            strMessage = "Backup Successful";
                        }
                        else
                        {
                            strMessage = "Backup did not upload successfully, client did not connect";
                        }
                        client.Disconnect();
                    }
                }
               catch
                {
                    strMessage = "Backup did not upload successfully, connection error";
               }
            }
            else
            {
                strMessage = "Backup did not upload, no internet connection";
            }
            tmUpload.Stop();
            tmDisplayInfo.Start();
            lblBackupInfo.Visible = true;
            lblBackupInfo.Text = strMessage.ToString();
        }

        private void cmdCheckInAgain_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Are you sure you want to check this family in again as they have already visited?", "Check in Again!?", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (dg == DialogResult.Yes)
            {
                checkIn();
            }
        }

        private void tmTimeTIck_Tick(object sender, EventArgs e)
        {
            for (intT = 0; intT < dgvTables.RowCount; intT++)
            {
                if (bTableFull[intT] == true)
                {
                    intTimeAtTables[intT] = intTimeAtTables[intT] + 1;
                    TimeSpan t = TimeSpan.FromSeconds(intTimeAtTables[intT]);
                    dgvTables.Rows[intT].Cells[2].Value = t.ToString();
                }
            }
        }

        private void checkIn()
        {

            string strDate = DateTime.Now.ToString("ddMMyyyy");
            string strSave = Properties.Settings.Default.aciLocalSave.ToString() + "\\" + strDate.ToString() + "_" + Properties.Settings.Default.aciName.ToString() + ".csv";

            string strTime = DateTime.Now.ToString("HH:mm:ss");
            string strToSave = strTime.ToString() + "," + strRef.ToString() + "," + intTotalPeople.ToString() + "," + lblAdults.Text + "," + lblChildren.Text + "," + lblUnders.Text + "," + lblWheelChair.Text;

            try
            {
                File.AppendAllText(strSave, strToSave.ToString() + Environment.NewLine);
                lblRedeemed.Text = "Family has been redeemed";
                lblRedeemed.BackColor = Color.Red;
                cmdCheckIn.Enabled = false;
                intFamilyCount++;
                lblFamilyCount.Text = intFamilyCount.ToString();
                lblRefFound.Text = "";
                cmdSendToTable.Enabled = true;
               
            }
            catch
            {
                MessageBox.Show("Can't save information to backup network location, please check the address is valid and try again", "Save error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            lblPeople.Text = (Int32.Parse(lblPeople.Text.ToString()) + Int32.Parse(lblAdults.Text.ToString()) + Int32.Parse(lblChildren.Text.ToString()) + Int32.Parse(lblWheelChair.Text.ToString()) + Int32.Parse(lblUnders.Text.ToString())).ToString();
            uploadData();
        }

        private void isRedeemed()
        {
            var list = new List<string>();
            bool bRedeemed = false;

            try
            {
                string strDate = DateTime.Now.ToString("ddMMyyyy");
                string strread = Properties.Settings.Default.aciLocalSave.ToString() + "\\" + strDate.ToString() + "_" + Properties.Settings.Default.aciName.ToString() + ".csv";
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
                i = 0;
                while (i < list.Count)
                {
                    if (list[i].Contains(strRef.ToString()))
                    {
                        lblRedeemed.Text = "Family has already visited";
                        lblRedeemed.BackColor = Color.Red;
                        cmdCheckInAgain.Visible = true;
                        bRedeemed = true;
                        cmdCheckIn.Enabled = false;
                        cmdCheckIn.Visible = false;
                        cmdCheckInAgain.Visible = true;
                        cmdCheckInAgain.Enabled = true;
                    }
                    i++;
                }
            }
            catch { }
            if (bRedeemed == false)
            {
                lblRedeemed.Text = "Family has not visited";
                lblRedeemed.BackColor = Color.Green;
                cmdCheckInAgain.Visible = false;
                cmdCheckIn.Enabled = true;
                cmdCheckIn.Visible = true;
            }
        }

        private void tmFindFamilies_Tick(object sender, EventArgs e)
        {
            readFile();
            tmFindFamilies.Enabled = false;
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

                    strread = Properties.Settings.Default.aciLocalSave.ToString() + "/" + cboDate.Text.Replace(@"/", "") + cboTime.Text.Replace(@":", "") + Properties.Settings.Default.aciName.ToString() + ".txt";
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
                        dgvFamily.RowCount = list.Count;
                        i = 0;
                        while (i < list.Count)
                        {
                            string[] strSplit = list[i].ToString().Split(',');
                            dgvFamily.Rows[i].Cells[0].Value = strSplit[0];
                            dgvFamily.Rows[i].Cells[1].Value = strSplit[1];
                            if (bColour == false)
                            {
                                dgvFamily.Rows[i].DefaultCellStyle.BackColor = Color.White;
                                bColour = true;
                            }
                            else
                            {
                                dgvFamily.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                                bColour = false;
                            }
                            i++;
                        }
                    }
                }
            }
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


        private void loadDates()
        {
            DateTime df = DateTime.Parse(Properties.Settings.Default.aciFrom);
            DateTime dt = DateTime.Parse(Properties.Settings.Default.aciTo);
            if (df < dt)
            {
                try
                {
                    cboDate.Items.Clear();
                    cboDate.Text = "";
                    DateTime dd = DateTime.Now;
                    string[] DT = Properties.Settings.Default.aciFrom.ToString().Split('/');
                    dd = new DateTime(Int32.Parse(DT[2]), Int32.Parse(DT[1]), Int32.Parse(DT[0]));
                    string strDate = df.ToString("dd/MM/yyyy");
                    while (strDate != dt.ToString("dd/MM/yyyy"))
                    {
                        strDate = dd.ToString("dd/MM/yyyy");
                        cboDate.Items.Add(strDate);
                        dd = dd.AddDays(1);
                    }
                }
                catch
                { }
            }
        }
        private void loadTimes()
        {
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
                    cboTime.Items.Add(strText);
                    j = j + 10;
                }
                i = i + 1;
            }
        }
    }
}

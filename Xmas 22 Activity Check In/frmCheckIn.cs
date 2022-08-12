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
    public partial class frmCheckIn : Form
    {
        string strRef;
        int i, intTotalPeople, intFamilyCount, intT;
        List<bool> bTableFull = new List<bool>();
        List<int> intTimeAtTables = new List<int>();
        List<int> intTables = new List<int>();
        List<string> lstTableRef = new List<string>();

        public frmCheckIn()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Are you sure you want to close Activity Check In?","Close?",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void cmdFind_Click(object sender, EventArgs e)
        {
            strRef = txtFind.Text;
            findFamily();
        }

        private void findFamily()
        {
            clearInfo();
            cmdCheckIn.Enabled = false;
            if (txtFind.Text == "")
            {
                MessageBox.Show("Please enter a reference number");
            }
            else
            {
                strRef = txtFind.Text;
                if (CheckForInternetConnection() == true)
                {
                    //check fusemetrix
                    String urlstr = "https://4k-photos.co.uk/checkRef.php?ref=" + strRef.ToString() + "&name=" + Properties.Settings.Default.aciName.ToString(); ;
                    WebClient client = new WebClient();
                    System.IO.Stream response = client.OpenRead(urlstr);
                    System.IO.StreamReader reads = new System.IO.StreamReader(response);
                    timer3.Start();
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

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
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
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Stop();
            readCreatedFile();
        }

        private void readCreatedFile()
        {
            var list = new List<string>();
            string c = "", b = "", strread = "";
            bool dSuccess = true, rSuccess = true;

            //Download the created file
            string Host = Properties.Settings.Default.aciFTPHost.ToString();
            int Port = Properties.Settings.Default.aciFTPPort;
            string Username = Properties.Settings.Default.aciFTPUsername.ToString();
            string Password = Properties.Settings.Default.aciFTPPassword.ToString();

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
                    lblRef.Text = strSplit[0].ToString();
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
            }
        }

        private void frmCheckIn_Load(object sender, EventArgs e)
        {
            loadSettings();
        }

        private void loadSettings()
        {
            timer3.Interval = Properties.Settings.Default.aciWait * 1000;
            pnlFindByReference.Visible = true;
            pnlFindbyDateandTime.Visible = false;
            pnlActivity.Visible = false;

            if (Properties.Settings.Default.aciTables == true)
            {
                lblTable.Visible = true;
                cboTable.Visible = true;
                cmdSendToTable.Visible = true;
                dgvTables.Visible = true;
            }
            else
            {
                lblTable.Visible = false;
                cboTable.Visible = false;
                cmdSendToTable.Visible = false;
                dgvTables.Visible = false;
            }
            

            cboTable.Items.Clear();
            string[] strTemp = Properties.Settings.Default.aciTableNames.ToString().Split(',');
            dgvTables.RowCount = strTemp.Count();
            dgvTables.Height = strTemp.Count() * 30;
            for (i = 0; i < strTemp.Count(); i++)
            {
                cboTable.Items.Add(strTemp[i]);
                dgvTables.Rows[i].Cells[0].Value = strTemp[i];
                dgvTables.Rows[i].Cells[1].Value = "Empty";
                dgvTables.Rows[i].Cells[2].Value = "Empty";
                bTableFull.Add(false);
                intTables.Add(0);
                intTimeAtTables.Add(0);
            }
            displayTime();
            clearAll();
            lblBackupInfo.Text = "";
            lblName.Text = "Family Attending " + Properties.Settings.Default.aciName.ToString();

            resizeAll();
        }

        private void cmdFindByRef_Click(object sender, EventArgs e)
        {

            hidePanels();
            pnlFindByReference.Visible = true;
            pnlInfo.Visible = true;
            pnlTime.Visible = true;
            txtFind.Text = "";
            txtFind.Focus();
            cmdFindByRef.BackColor = Color.White;
        }

        private void hidePanels()
        {
            pnlInfo.Visible = false;
            pnlFindByReference.Visible = false;
            pnlActivity.Visible = false;
            pnlFindbyDateandTime.Visible = false;
            pnlInfo.Visible = false;
            pnlTime.Visible = false;
            cmdFindByRef.BackColor = Color.Goldenrod;
            cmdFindByDateAndTime.BackColor = Color.Goldenrod;
            cmdCheckFamily.BackColor = Color.Goldenrod;
        }

        private void cmdFindByDateAndTime_Click(object sender, EventArgs e)
        {
            hidePanels();
            pnlFindbyDateandTime.Visible = true;
            pnlInfo.Visible = true;
            pnlTime.Visible = true;
            cmdFindByDateAndTime.BackColor = Color.White;
        }

        private void cmdCheckFamily_Click(object sender, EventArgs e)
        {
            hidePanels();
            pnlActivity.Visible = true;
            pnlTime.Visible = true;
            lstActivity.Visible = false;
            lstTime.Visible = false;
            lstAttended.Visible = false;
            lblActivity.Visible = false;
            lblTimeAttended.Visible = false;
            lblAttended.Visible = false;
            txtActivityFind.Text = "";
            txtActivityFind.Focus();
            cmdCheckFamily.BackColor = Color.White;
        }

        private void cmdCheckIn_Click(object sender, EventArgs e)
        {
            checkIn();
        }

        private void cmdCheckInAgain_Click(object sender, EventArgs e)
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
                    string strRef = lstTableRef[0].ToString();
                    for (i = 1; i < dgvTables.RowCount; i++)
                    {
                        strTables = strTables + "," + dgvTables.Rows[i].Cells[1].Value.ToString();
                        strTableName = strTableName + "," + dgvTables.Rows[i].Cells[0].Value.ToString();
                        strRef = strRef + "," + lstTableRef[i].ToString();
                    }

                    Random rnd = new Random(12);
                    string r1 = rnd.Next().ToString();

                    try
                    {
                        // Write file using StreamWriter to local location for upload
                        string gs = Properties.Settings.Default.aciTableWrite + "\\table.txt";
                        using (StreamWriter writer = new StreamWriter(gs))
                        {
                            writer.WriteLine(r1);
                            writer.WriteLine(strTableName);
                            writer.WriteLine(strTables);
                            writer.WriteLine(strRef);
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
                    string strRef = lstTableRef[0].ToString();
                    for (i = 1; i < dgvTables.RowCount; i++)
                    {
                        strTables = strTables + "," + dgvTables.Rows[i].Cells[1].Value.ToString();
                        strTableName = strTableName + "," + dgvTables.Rows[i].Cells[0].Value.ToString();
                        strRef = strRef + "," + lstTableRef[i].ToString();
                    }

                    Random rnd = new Random(12);
                    string r1 = rnd.Next().ToString();

                    try
                    {
                        // Write file using StreamWriter to local location for upload
                        string gs = Properties.Settings.Default.aciTableWrite + "\\table.txt";
                        using (StreamWriter writer = new StreamWriter(gs))
                        {
                            writer.WriteLine(r1);
                            writer.WriteLine(strTableName);
                            writer.WriteLine(strTables);
                            writer.WriteLine(strRef);
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

        private void cmdClearFamily_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void clearAll()
        {
            lblFamilyFound.Text = "";
            lblRefFound.Text = "";
            lblRef.Text = "";
            txtFind.Text = "";
            lblAdults.Text = "";
            lblChildren.Text = "";
            lblUnders.Text = "";
            lblWheelChair.Text = "";
            lblFamilyName.Text = "";
            lblRedeemed.Text = "";
        }

        private void tmUpload_Tick(object sender, EventArgs e)
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
            tmDisplay.Start();
            lblBackupInfo.Visible = true;
            lblBackupInfo.Text = strMessage.ToString();
        }

        private void tmDisplay_Tick(object sender, EventArgs e)
        {
            lblBackupInfo.Visible = false;
            tmUpload.Start();
            tmDisplay.Stop();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            displayTime();
        }

        private void displayTime()
        {
            lblTime.Text = DateTime.Now.ToString("F");
        }

        private void frmCheckIn_Resize(object sender, EventArgs e)
        {
            resizeAll();
        }

        private void resizeAll()
        {
            lblBackupInfo.Left = 0;
            lblTime.Left = pnlTime.Width - lblTime.Width;
            lblName.Left = (pnlTime.Width / 2) - (lblTime.Width / 2);
        }

        private void cmdFindDT_Click(object sender, EventArgs e)
        {
            strRef = lblRefFound.Text;

        }

        private void dgvTables_Click(object sender, EventArgs e)
        {
            cmdFamilyLeft.Visible = true;
            cmdFamilyLeft.Enabled = true;
        }

        private void dgvTables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cmdFamilyLeft.Visible = true;
            cmdFamilyLeft.Enabled = true;
        }

        private void tmTimeTick_Tick(object sender, EventArgs e)
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
                lblFamilyCount.Text = intFamilyCount.ToString(); ;
                lblPeople.Text = intTotalPeople.ToString();
                cmdSendToTable.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Can't save information to backup network location, please check the address is valid and try again", "Save error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
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
            }
        }
    }
}

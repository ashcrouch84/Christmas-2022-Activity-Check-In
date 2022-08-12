using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Net;
using Renci.SshNet;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;



namespace Xmas_22_Activity_Check_In
{
    public partial class frmInfo : Form
    {
        int x,i;
        bool bexit;
        string strRef,b,c, plainText, cipherText;
        string[,] arFamilies = new string[10,2];
        string Host = Properties.Settings.Default.aciFTPHost.ToString();
        int Port = Properties.Settings.Default.aciFTPPort;
        string Username = Properties.Settings.Default.aciFTPUsername.ToString();
        string Password = Properties.Settings.Default.aciFTPPassword.ToString();
        List<string> child_list = new List<string>();
        List<string> adult_list = new List<string>();
        List<string> family_list = new List<string>();

        public frmInfo()
        {
            InitializeComponent();
        }

        private void frmInfo_Load(object sender, EventArgs e)
        {
            this.Height = 800;
            this.Width = 1200;
            loadSettings();
        }

        private void loadSettings()
        {
            loadTables();
            checkTables();
            loadQuestions();
            Properties.Settings.Default.aciUnique = "";
            Properties.Settings.Default.Save();
            tmFindFamily.Interval = Properties.Settings.Default.aciReadWait * 1000;
            tmFindFamily.Stop();
            lblRef.Text = "000000";
            this.Text = "Actor Info for " + Properties.Settings.Default.aciName.ToString();
            resizeAll();
            lblRef.Text = "";
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

            string[] lstChild = list[0].ToString().Split(',');
            string[] lstAdult = list[1].ToString().Split(',');
            string[] lstFamily = list[2].ToString().Split(',');

            for (i=0; i<lstChild.Count();i++)
            {
                dgvChild.Columns[i].HeaderText = lstChild[i].ToString();
            }
            string[] strChildHidden = Properties.Settings.Default.aciChildChecked.ToString().Split(',');
            for (i=0;i<strChildHidden.Count();i++)
            {
                if (strChildHidden[i] == "0")
                {
                    dgvChild.Columns[i].Visible = false;
                }
            }

            for (i = 0; i < lstAdult.Count(); i++)
            {
                dgvAdult.Columns[i].HeaderText = lstAdult[i].ToString();
            }
            string[] strAdultHidden = Properties.Settings.Default.aciAdultChecked.ToString().Split(',');
            for (i = 0; i < strAdultHidden.Count(); i++)
            {
                if (strAdultHidden[i] == "0")
                {
                    dgvAdult.Columns[i].Visible = false;
                }
            }

            for (i = 0; i < lstFamily.Count(); i++)
            {
                dgvFamily.Columns[i].HeaderText = lstFamily[i].ToString();
            }
            string[] strFamilyHidden = Properties.Settings.Default.aciFamilyChecked.ToString().Split(',');
            for (i = 0; i < strFamilyHidden.Count(); i++)
            {
                if (strFamilyHidden[i] == "0")
                {
                    dgvFamily.Columns[i].Visible = false;
                }
            }
        }

        private void loadTables()
        {
            //clear the data grid view on tables, then load table names from propeperties
            dgvTables.DataSource = null;
            dgvTables.Rows.Clear();

            i = 0;
            string[] strTemp = Properties.Settings.Default.aciTableNames.ToString().Split(',');
            dgvTables.RowCount = strTemp.Count();
            dgvTables.Height = strTemp.Count() * 30;
            for (i = 0; i < strTemp.Count(); i++)
            {
                dgvTables.Rows[i].Cells[0].Value = strTemp[i];
                dgvTables.Rows[i].Cells[1].Value = "Empty";
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Are you sure you want to close Actor Info?", "Close?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                this.Close();
                bexit = true;
            }
        }

        private void tmTimeTick_Tick(object sender, EventArgs e)
        {
            checkTables();
        }

        private void checkTables()
        {
            try
            {
                //read the table text file
                var list = new List<string>();
                string strread = Properties.Settings.Default.aciTableRead + "\\table.txt";
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

                //check the unique number and if it is new, update the data grid view about tables
                //if (list[0].ToString() != Properties.Settings.Default.aciUnique.ToString())
                //{
                //Properties.Settings.Default.aciUnique = list[0].ToString();
                //Properties.Settings.Default.Save();
                string[] strNames = list[2].ToString().Split(',');
                string[] strRef = list[3].ToString().Split(',');
                bool bColour = false;
                for (i = 0; i < strNames.Count(); i++)
                {
                    dgvTables.Rows[i].Cells[1].Value = strNames[i].ToString();
                    arFamilies[i, 0] = strNames[i].ToString();
                    arFamilies[i, 1] = strRef[i].ToString();
                    if (bColour == false)
                    {
                        dgvTables.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        bColour = true;
                    }
                    else
                    {
                        dgvTables.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                        bColour = false;
                    }

                }
                dgvTables.Refresh();
            }
            catch { }
            //}
        }

        private void cmdShowHidden_Click(object sender, EventArgs e)
        {
            if (cmdShowHidden.Text == "Show Hidden")
            {
                i = 0;
                while (i < dgvChild.ColumnCount)
                {
                    dgvChild.Columns[i].Visible = true;
                    i = i + 1;
                }
                i = 0;
                while (i < dgvAdult.ColumnCount)
                {
                    dgvAdult.Columns[i].Visible = true;
                    i = i + 1;
                }
                i = 0;
                while (i < dgvFamily.ColumnCount)
                {
                    dgvFamily.Columns[i].Visible = true;
                    i = i + 1;
                }
                cmdShowHidden.Text = "Hide Information";
            }
            else
            {
                cmdShowHidden.Text = "Show Hidden";
                loadQuestions();
            }
        }

        private void dgvTables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    lblFamilyName.Text = dgvTables.Rows[e.RowIndex].Cells[1].Value.ToString();
                    lblRef.Text = arFamilies[e.RowIndex, 1].ToString();
                    strRef = lblRef.Text;
                    if (lblRef.Text != "")
                    {
                        findFamily();
                    }
                    else
                    {
                        dgvAdult.Rows.Clear();
                        dgvAdult.Refresh();
                        dgvChild.Rows.Clear();
                        dgvChild.Refresh();
                        dgvFamily.Rows.Clear();
                        dgvFamily.Refresh();
                    }
                }
            }
            catch { }
        }

        private void dgvTables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void frmInfo_Resize(object sender, EventArgs e)
        {
            resizeAll();
        }

        private void resizeAll()
        {
            lblRef.Left = pnlActorInfo.Width - lblRef.Width - 15;
            dgvAdult.Width = pnlActorInfo.Width - 20;
            dgvChild.Width = pnlActorInfo.Width - 20;
            dgvFamily.Width = pnlActorInfo.Width - 20;
        }

        private void dgvTables_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    if (dgvTables.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.Green)
                    {
                        dgvTables.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                    }
                    else
                    {
                        dgvTables.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                    }
                }
            }
            catch
            { }
            
        }

        private void frmInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bexit != true)
            {
                DialogResult dg = MessageBox.Show("Are you sure you want to close Actor Info?", "Close?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dg == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void findFamily()
        {
            //variables
            var list = new List<string>();
            int intChildCount = 0;
            int intAdultCount = 0;
            int intFamilyCount = 0;
            listBox1.Items.Clear();

            //check if directories for fmamiles, children, adults exists otherwise create them
            string gc = Properties.Settings.Default.aciTableRead.ToString() + "\\Child Info";
            System.IO.Directory.CreateDirectory(gc);
            string ga = Properties.Settings.Default.aciTableRead.ToString() + "\\Adult Info";
            System.IO.Directory.CreateDirectory(ga);
            string gf = Properties.Settings.Default.aciTableRead.ToString() + "\\Family Info";
            System.IO.Directory.CreateDirectory(gf);

            //check local directoris to see if files are already downloaded
            //children
            bool bCExists = false;
            DirectoryInfo dc = new DirectoryInfo(gc);
            FileInfo[] cfiles = dc.GetFiles();
            var lstChildFiles = new List<string>();
            for (i=0; i<cfiles.Count(); i++)
            {
                if (cfiles[i].Name.ToString().Contains(strRef))
                {
                    lstChildFiles.Add(cfiles[i].FullName.ToString());
                    bCExists = true;
                    listBox1.Items.Add(cfiles[i].FullName.ToString());
                }
            }
            //adults
            bool bAExists = false;
            DirectoryInfo da = new DirectoryInfo(ga);
            FileInfo[] afiles = da.GetFiles();
            var lstAdultFiles = new List<string>();
            for (i = 0; i < afiles.Count(); i++)
            {
                if (afiles[i].Name.ToString().Contains(strRef))
                {
                    lstAdultFiles.Add(afiles[i].FullName.ToString());
                    bAExists = true;
                    listBox1.Items.Add(afiles[i].FullName.ToString());
                }
            }
            //family
            bool bFExists = false;
            DirectoryInfo df = new DirectoryInfo(gf);
            FileInfo[] ffiles = df.GetFiles();
            var lstFamilyFiles = new List<string>();
            for (i = 0; i < ffiles.Count(); i++)
            {
                if (ffiles[i].Name.ToString().Contains(strRef))
                {
                    lstFamilyFiles.Add(ffiles[i].FullName.ToString());
                    bFExists = true;
                    listBox1.Items.Add(ffiles[i].FullName.ToString());
                }
            }

          

            //if files exist for children, adults and family then
            if (bCExists==true && bAExists==true && bFExists == true)
            {
                dgvChild.RowCount = lstChildFiles.Count();
                dgvAdult.RowCount = lstAdultFiles.Count();
                dgvFamily.RowCount = lstFamilyFiles.Count();
                //read child files and load into datagridview
                var cList = new List<string>();
                for (i = 0; i < lstChildFiles.Count(); i++)
                {
                    string strread = lstChildFiles[i];
                    listBox1.Items.Add(strread.ToString());
                    intChildCount++;
                    var fileStream = new FileStream(strread, FileMode.Open, System.IO.FileAccess.Read);
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            cList.Add(line);
                        }
                    }
                    fileStream.Close();

                    cipherText = cList[0].ToString();
                    listBox1.Items.Add(cipherText);
                    decryptData();
                    string[] splitDD = plainText.ToString().Split(',');
                    listBox1.Items.Add(plainText);
                    int j = 0;
                    while (j < splitDD.Count())
                    {
                        dgvChild.Rows[intChildCount - 1].Cells[j].Value = splitDD[j];
                        j = j + 1;
                    }
                    dgvChild.Refresh();

                    dgvChild.Height = (intChildCount * 50) + 60;
                }
                //read adult files and load into datagridview
                var aList = new List<string>();
                for (i=0;i<lstAdultFiles.Count();i++)
                {
                    string strread = lstAdultFiles[i];
                    listBox1.Items.Add(strread.ToString());
                    intAdultCount++;
                    var fileStream = new FileStream(strread, FileMode.Open, System.IO.FileAccess.Read);
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            aList.Add(line);
                        }
                    }
                    fileStream.Close();

                    cipherText = aList[0].ToString();
                    decryptData();
                    string[] splitDD = plainText.ToString().Split(',');
                    int j = 0;
                    while (j < splitDD.Count())
                    {
                        dgvAdult.Rows[intAdultCount - 1].Cells[j].Value = splitDD[j];
                        j = j + 1;
                        if (j == 2) { j = 3; }
                        if (j == 4) { j = 5; }
                        if (j == 13) { j = 14; }
                    }
                    dgvAdult.Refresh();

                    lblAdult.Top = dgvChild.Top + dgvChild.Height + 20;
                    dgvAdult.Top = lblAdult.Top + lblAdult.Height + 10;
                    dgvAdult.Height = (intAdultCount * 50) + 60;
                }
                //read family files and load into datagridview
                var fList = new List<string>();
                for (i=0; i<lstFamilyFiles.Count();i++)
                {
                    string strread = lstFamilyFiles[i];
                    listBox1.Items.Add(strread.ToString());
                    intFamilyCount++;
                    var fileStream = new FileStream(strread, FileMode.Open, System.IO.FileAccess.Read);
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            fList.Add(line);
                        }
                    }
                    fileStream.Close();

                    cipherText = fList[0].ToString();
                    decryptData();
                    string[] splitDD = plainText.ToString().Split(',');
                    int j = 0;
                    while (j < splitDD.Count())
                    {
                        dgvFamily.Rows[intFamilyCount - 1].Cells[j].Value = splitDD[j];
                        j = j + 1;
                    }
                    dgvFamily.Refresh();

                    lblFamily.Top = dgvAdult.Top + dgvAdult.Height + 20;
                    dgvFamily.Top = lblFamily.Top + lblFamily.Height + 10;
                    dgvFamily.Height = (intFamilyCount * 50) + 60;
                }
            }
            else
            {
                //search ftp site, download files and display on datagridview
                using (var sftp = new SftpClient(Host, Port, Username, Password))
                {
                    try
                    {
                        sftp.Connect(); //connect to server
                        child_list = sftp.ListDirectory(Properties.Settings.Default.aciRFChild).Where(f => !f.IsDirectory).Select(f => f.Name).ToList();
                        adult_list = sftp.ListDirectory(Properties.Settings.Default.aciRFAdult).Where(f => !f.IsDirectory).Select(f => f.Name).ToList();
                        family_list = sftp.ListDirectory(Properties.Settings.Default.aciRFFamily).Where(f => !f.IsDirectory).Select(f => f.Name).ToList();

                        for (i = 0; i < child_list.Count; i++)
                        {
                            if (child_list[i].ToString().Contains(lblRef.Text.ToString()) && child_list[i].ToString().Contains(".txt"))
                            {
                                var cList = new List<string>();
                                intChildCount = intChildCount + 1;
                                dgvChild.RowCount = intChildCount;
                                c = Properties.Settings.Default.aciRFChild + "/" + child_list[i]; //update download file from sftp
                                b = gc + "\\" + child_list[i];//update download folder to pc 
                                                                                                    //  try
                                                                                                    //  {
                                using (var file = File.OpenWrite(b))
                                {
                                    sftp.DownloadFile(c, file);//download file
                                }

                                string strread = b;
                                var fileStream = new FileStream(strread, FileMode.Open, System.IO.FileAccess.Read);
                                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                                {
                                    string line;
                                    while ((line = streamReader.ReadLine()) != null)
                                    {
                                        cList.Add(line);
                                    }
                                }
                                fileStream.Close();

                                cipherText = cList[0].ToString();
                                decryptData();
                                string[] splitDD = plainText.ToString().Split(',');
                                int j = 0;
                                while (j < splitDD.Count())
                                {
                                    dgvChild.Rows[intChildCount - 1].Cells[j].Value = splitDD[j];
                                    j = j + 1;
                                }
                                dgvChild.Refresh();

                                dgvChild.Height = (intChildCount *50) + 60;
                            }
                        }

                        for (i = 0; i < adult_list.Count; i++)
                        {
                            if (adult_list[i].ToString().Contains(lblRef.Text.ToString()) && adult_list[i].ToString().Contains(".txt"))
                            {
                                var aList = new List<string>();
                                intAdultCount = intAdultCount + 1;
                                dgvAdult.RowCount = intAdultCount;
                                c = Properties.Settings.Default.aciRFAdult + "/" + adult_list[i]; //update download file from sftp
                                b = ga + "\\" + adult_list[i];//update download folder to pc 
                                                                                                    //  try
                                                                                                    //  {
                                using (var file = File.OpenWrite(b))
                                {
                                    sftp.DownloadFile(c, file);//download file
                                }

                                string strread = b;
                                var fileStream = new FileStream(strread, FileMode.Open, System.IO.FileAccess.Read);
                                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                                {
                                    string line;
                                    while ((line = streamReader.ReadLine()) != null)
                                    {
                                        aList.Add(line);
                                    }
                                }
                                fileStream.Close();

                                cipherText = aList[0].ToString();
                                decryptData();
                                string[] splitDD = plainText.ToString().Split(',');
                                int j = 0;
                                while (j < splitDD.Count())
                                {
                                    dgvAdult.Rows[intAdultCount - 1].Cells[j].Value = splitDD[j];
                                    j = j + 1;
                                    if (j == 2) { j = 3; }
                                    if (j == 4) { j = 5; }
                                    if (j == 13) { j = 14; }
                                }
                                dgvAdult.Refresh();

                                lblAdult.Top = dgvChild.Top + dgvChild.Height + 20;
                                dgvAdult.Top = lblAdult.Top + lblAdult.Height + 10;
                                dgvAdult.Height = (intAdultCount * 50) + 60;

                            }
                        }

                        for (i = 0; i < family_list.Count; i++)
                        {
                            if (family_list[i].ToString().Contains(lblRef.Text.ToString()) && family_list[i].ToString().Contains(".txt"))
                            {
                                var fList = new List<string>();
                                intFamilyCount = intFamilyCount + 1;
                                dgvChild.RowCount = intFamilyCount;
                                c = Properties.Settings.Default.aciRFFamily + "/" + family_list[i]; //update download file from sftp
                                b = gf + "\\" + family_list[i];//update download folder to pc 
                                                                                                    //  try
                                                                                                    //  {
                                using (var file = File.OpenWrite(b))
                                {
                                    sftp.DownloadFile(c, file);//download file
                                }

                                string strread = b;
                                var fileStream = new FileStream(strread, FileMode.Open, System.IO.FileAccess.Read);
                                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                                {
                                    string line;
                                    while ((line = streamReader.ReadLine()) != null)
                                    {
                                        fList.Add(line);
                                    }
                                }
                                fileStream.Close();

                                cipherText = fList[0].ToString();
                                decryptData();
                                string[] splitDD = plainText.ToString().Split(',');
                                int j = 0;
                                while (j < splitDD.Count())
                                {
                                    dgvFamily.Rows[intFamilyCount - 1].Cells[j].Value = splitDD[j];
                                    j = j + 1;
                                }
                                dgvFamily.Refresh();

                                lblFamily.Top = dgvAdult.Top + dgvAdult.Height + 20;
                                dgvFamily.Top = lblFamily.Top + lblFamily.Height + 10;
                                dgvFamily.Height = (intFamilyCount * 50) + 60;
                            }
                        }


                        sftp.Disconnect();
                    }
                    catch
                    {
                        MessageBox.Show("Problem connecting to host, opening file and decrypting, please check settings and connection before trying again", "Error ash1912", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void decryptData()
        {
            //decrypt the file

            string password = Properties.Settings.Default.aciDCPW;

            // Create sha256 hash
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));

            // Create secret IV
            byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

            // Instantiate a new Aes object to perform string symmetric encryption
            Aes encryptor = Aes.Create();

            encryptor.Mode = CipherMode.CBC;

            // Set key and IV
            byte[] aesKey = new byte[32];
            Array.Copy(key, 0, aesKey, 0, 32);
            encryptor.Key = aesKey;
            encryptor.IV = iv;

            // Instantiate a new MemoryStream object to contain the encrypted bytes
            MemoryStream memoryStream = new MemoryStream();

            // Instantiate a new encryptor from our Aes object
            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();

            // Instantiate a new CryptoStream object to process the data and write it to the 
            // memory stream
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);

            // Will contain decrypted plaintext
            plainText = String.Empty;

            try
            {
                // Convert the ciphertext string into a byte array
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                // Decrypt the input ciphertext string
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);

                // Complete the decryption process
                cryptoStream.FlushFinalBlock();

                // Convert the decrypted data from a MemoryStream to a byte array
                byte[] plainBytes = memoryStream.ToArray();

                // Convert the decrypted byte array to string
                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
            }
            finally
            {
                // Close both the MemoryStream and the CryptoStream
                memoryStream.Close();
                cryptoStream.Close();
            }
        }

        private void tmFindFamily_Tick(object sender, EventArgs e)
        {
            tmFindFamily.Stop();
            readDownloaded();
        }

        private void readDownloaded()
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
        }

        private void displayDownloaded()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileEncryptor
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void butEncrypt_Click(object sender, EventArgs e)
        {
            LblRun.Visible = true;
            LblRun.Refresh();

            FileEncryptorBC.EncryptAndCompress(txtToEncrypt.Text,
                                               txtToDencrypt.Text,
                                               txtPassword.Text);
            LblRun.Visible = false;
            LblRun.Refresh();

        }

        private void butDecrypter_Click(object sender, EventArgs e)
        {
            LblRun.Visible = true;
            LblRun.Refresh();

            FileEncryptorBC.DecryptAndDecompress(txtToDencrypt.Text,
                                                 txtToEncrypt.Text,
                                                 txtPassword.Text);

            LblRun.Visible = false;
            LblRun.Refresh();

        }

        private void butSelectFileEnc_Click(object sender, EventArgs e)
        {
            txtToEncrypt.Text = GetFile("Sélectionner un fichier à encrypter",
                                        "Tous les fichiers|*.*");
        }

        private void butSelectFileDec_Click(object sender, EventArgs e)
        {
            txtToEncrypt.Text = GetFile("Sélectionner un fichier à décrypter",
                                        "Tous les fichiers|*.encz");
        }

        private string GetFile(string vstrWindowsTitle,
                               string vstrFilter)
        {
            string strFile = null;

            using (System.Windows.Forms.OpenFileDialog objOpenFileDialogEnc = new System.Windows.Forms.OpenFileDialog())
            {
                objOpenFileDialogEnc.CheckFileExists = true;

                objOpenFileDialogEnc.Title = vstrWindowsTitle; 
                objOpenFileDialogEnc.Filter = vstrFilter;
                objOpenFileDialogEnc.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (objOpenFileDialogEnc.ShowDialog() == DialogResult.OK)
                {

                    strFile = objOpenFileDialogEnc.FileName;

                }
            
            }

            return strFile;
        }

        private void chkSeePassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeePassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }

        }
    }
}

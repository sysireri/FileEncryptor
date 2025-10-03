using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            FileEncryptorBC.EncryptAndCompress(txtToEncrypt.Text,
                                               txtToDencrypt.Text,
                                               txtPassword.Text);
        }

        private void butDecrypter_Click(object sender, EventArgs e)
        {
            FileEncryptorBC.DecryptAndDecompress(txtToDencrypt.Text,
                                                 txtToEncrypt.Text,
                                                 txtPassword.Text);

        }
    }
}

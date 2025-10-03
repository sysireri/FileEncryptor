namespace FileEncryptor
{
    partial class FrmMain
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtToEncrypt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtToDencrypt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.butEncrypt = new System.Windows.Forms.Button();
            this.butDecrypter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mot de passe          : ";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(203, 40);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(429, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.Text = "ChangeMoi_!MotDePasseFort!";
            // 
            // txtToEncrypt
            // 
            this.txtToEncrypt.Location = new System.Drawing.Point(203, 82);
            this.txtToEncrypt.Name = "txtToEncrypt";
            this.txtToEncrypt.Size = new System.Drawing.Size(429, 20);
            this.txtToEncrypt.TabIndex = 3;
            this.txtToEncrypt.Text = "C:\\temp\\3BA00C2B42EB66AA-00-00.mrimg";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fichier à encrypter : ";
            // 
            // txtToDencrypt
            // 
            this.txtToDencrypt.Location = new System.Drawing.Point(203, 125);
            this.txtToDencrypt.Name = "txtToDencrypt";
            this.txtToDencrypt.Size = new System.Drawing.Size(429, 20);
            this.txtToDencrypt.TabIndex = 5;
            this.txtToDencrypt.Text = "C:\\temp\\3BA00C2B42EB66AA-00-00.encz";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(37, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fichier à décrypter : ";
            // 
            // butEncrypt
            // 
            this.butEncrypt.Location = new System.Drawing.Point(659, 79);
            this.butEncrypt.Name = "butEncrypt";
            this.butEncrypt.Size = new System.Drawing.Size(75, 23);
            this.butEncrypt.TabIndex = 6;
            this.butEncrypt.Text = "Encrypter";
            this.butEncrypt.UseVisualStyleBackColor = true;
            this.butEncrypt.Click += new System.EventHandler(this.butEncrypt_Click);
            // 
            // butDecrypter
            // 
            this.butDecrypter.Location = new System.Drawing.Point(659, 123);
            this.butDecrypter.Name = "butDecrypter";
            this.butDecrypter.Size = new System.Drawing.Size(75, 23);
            this.butDecrypter.TabIndex = 7;
            this.butDecrypter.Text = "Décrypter";
            this.butDecrypter.UseVisualStyleBackColor = true;
            this.butDecrypter.Click += new System.EventHandler(this.butDecrypter_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 192);
            this.Controls.Add(this.butDecrypter);
            this.Controls.Add(this.butEncrypt);
            this.Controls.Add(this.txtToDencrypt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtToEncrypt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label1);
            this.Name = "FrmMain";
            this.Text = "FrmMain";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtToEncrypt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtToDencrypt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button butEncrypt;
        private System.Windows.Forms.Button butDecrypter;
    }
}
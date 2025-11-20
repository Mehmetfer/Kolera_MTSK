namespace Tarantula_MTSK.Sayfalar
{
    partial class Form_Giris
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Giris));
            this.ComboBox_KullaniciAdi = new System.Windows.Forms.ComboBox();
            this.Txt_Parola = new System.Windows.Forms.TextBox();
            this.Lbl_kaz = new System.Windows.Forms.Label();
            this.Btngiris = new System.Windows.Forms.Button();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.Lbl_Kull = new System.Windows.Forms.Label();
            this.Lbl_2 = new System.Windows.Forms.Label();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // ComboBox_KullaniciAdi
            // 
            this.ComboBox_KullaniciAdi.BackColor = System.Drawing.SystemColors.WindowText;
            this.ComboBox_KullaniciAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ComboBox_KullaniciAdi.ForeColor = System.Drawing.SystemColors.Window;
            this.ComboBox_KullaniciAdi.FormattingEnabled = true;
            this.ComboBox_KullaniciAdi.Location = new System.Drawing.Point(241, 194);
            this.ComboBox_KullaniciAdi.Name = "ComboBox_KullaniciAdi";
            this.ComboBox_KullaniciAdi.Size = new System.Drawing.Size(169, 26);
            this.ComboBox_KullaniciAdi.TabIndex = 17;
            // 
            // Txt_Parola
            // 
            this.Txt_Parola.BackColor = System.Drawing.SystemColors.WindowText;
            this.Txt_Parola.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Txt_Parola.ForeColor = System.Drawing.SystemColors.Window;
            this.Txt_Parola.Location = new System.Drawing.Point(241, 249);
            this.Txt_Parola.Name = "Txt_Parola";
            this.Txt_Parola.PasswordChar = '*';
            this.Txt_Parola.Size = new System.Drawing.Size(169, 26);
            this.Txt_Parola.TabIndex = 16;
            // 
            // Lbl_kaz
            // 
            this.Lbl_kaz.AutoSize = true;
            this.Lbl_kaz.ForeColor = System.Drawing.Color.Red;
            this.Lbl_kaz.Location = new System.Drawing.Point(179, 150);
            this.Lbl_kaz.Name = "Lbl_kaz";
            this.Lbl_kaz.Size = new System.Drawing.Size(153, 13);
            this.Lbl_kaz.TabIndex = 14;
            this.Lbl_kaz.Text = "Kullanıcı Adı veya Parola Hatalı";
            this.Lbl_kaz.Visible = false;
            // 
            // Btngiris
            // 
            this.Btngiris.BackColor = System.Drawing.SystemColors.WindowText;
            this.Btngiris.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btngiris.BackgroundImage")));
            this.Btngiris.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btngiris.Location = new System.Drawing.Point(182, 351);
            this.Btngiris.Name = "Btngiris";
            this.Btngiris.Size = new System.Drawing.Size(188, 46);
            this.Btngiris.TabIndex = 15;
            this.Btngiris.UseVisualStyleBackColor = false;
            this.Btngiris.Click += new System.EventHandler(this.Btngiris_Click_1);
            // 
            // PictureBox1
            // 
            this.PictureBox1.Location = new System.Drawing.Point(12, 119);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(504, 345);
            this.PictureBox1.TabIndex = 19;
            this.PictureBox1.TabStop = false;
            this.PictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // PictureBox2
            // 
            this.PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox2.Image")));
            this.PictureBox2.Location = new System.Drawing.Point(12, 12);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(504, 110);
            this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox2.TabIndex = 47;
            this.PictureBox2.TabStop = false;
            // 
            // Lbl_Kull
            // 
            this.Lbl_Kull.AutoSize = true;
            this.Lbl_Kull.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Lbl_Kull.Location = new System.Drawing.Point(114, 194);
            this.Lbl_Kull.Name = "Lbl_Kull";
            this.Lbl_Kull.Size = new System.Drawing.Size(93, 20);
            this.Lbl_Kull.TabIndex = 48;
            this.Lbl_Kull.Text = "Kullanıcı Adı";
            // 
            // Lbl_2
            // 
            this.Lbl_2.AutoSize = true;
            this.Lbl_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Lbl_2.Location = new System.Drawing.Point(114, 255);
            this.Lbl_2.Name = "Lbl_2";
            this.Lbl_2.Size = new System.Drawing.Size(42, 20);
            this.Lbl_2.TabIndex = 48;
            this.Lbl_2.Text = "Şifre";
            // 
            // PictureBox4
            // 
            this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
            this.PictureBox4.Location = new System.Drawing.Point(477, 12);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(39, 36);
            this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox4.TabIndex = 49;
            this.PictureBox4.TabStop = false;
            this.PictureBox4.Click += new System.EventHandler(this.PictureBox4_Click_1);
            // 
            // PictureBox3
            // 
            this.PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox3.Image")));
            this.PictureBox3.Location = new System.Drawing.Point(21, 374);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(100, 90);
            this.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox3.TabIndex = 50;
            this.PictureBox3.TabStop = false;
            this.PictureBox3.Click += new System.EventHandler(this.PictureBox3_Click);
            // 
            // Form_Giris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(520, 463);
            this.Controls.Add(this.PictureBox3);
            this.Controls.Add(this.PictureBox4);
            this.Controls.Add(this.Lbl_2);
            this.Controls.Add(this.Lbl_Kull);
            this.Controls.Add(this.ComboBox_KullaniciAdi);
            this.Controls.Add(this.Txt_Parola);
            this.Controls.Add(this.Btngiris);
            this.Controls.Add(this.Lbl_kaz);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.PictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Giris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_Giris";
            this.Load += new System.EventHandler(this.Form_Giris_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox ComboBox_KullaniciAdi;
        private System.Windows.Forms.TextBox Txt_Parola;
        private System.Windows.Forms.Button Btngiris;
        private System.Windows.Forms.Label Lbl_kaz;
        private System.Windows.Forms.PictureBox PictureBox1;
        private System.Windows.Forms.PictureBox PictureBox2;
        private System.Windows.Forms.Label Lbl_Kull;
        private System.Windows.Forms.Label Lbl_2;
        private System.Windows.Forms.PictureBox PictureBox4;
        private System.Windows.Forms.PictureBox PictureBox3;
    }
}
using System;
using System.Windows.Forms;

namespace Tarantula_MTSK.Sayfalar
{
    partial class Ana_Menu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ana_Menu));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Menumain = new System.Windows.Forms.MenuStrip();
            this.Mebbis = new System.Windows.Forms.ToolStripMenuItem();
            this.Mebbisaktar = new System.Windows.Forms.ToolStripMenuItem();
            this.Mebgir = new System.Windows.Forms.ToolStripMenuItem();
            this.Lbl_Lisans = new System.Windows.Forms.Label();
            this.Pickusak = new System.Windows.Forms.PictureBox();
            this.Picture_Ozel = new System.Windows.Forms.PictureBox();
            this.Anasayfa = new System.Windows.Forms.ToolStripMenuItem();
            this.Kursiyer = new System.Windows.Forms.ToolStripMenuItem();
            this.Kursiyer_Ekle = new System.Windows.Forms.ToolStripMenuItem();
            this.Arama = new System.Windows.Forms.ToolStripMenuItem();
            this.Kursiyersil = new System.Windows.Forms.ToolStripMenuItem();
            this.Mebbis_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Arama_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Araclar_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Donem_Grup = new System.Windows.Forms.ToolStripMenuItem();
            this.Peronsel_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Ayarlar_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Raporlar_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Yedek_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Yardim_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Anamenu_Resim = new System.Windows.Forms.PictureBox();
            this.Menumain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pickusak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Ozel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Anamenu_Resim)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            // 
            // Menumain
            // 
            this.Menumain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Menumain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menumain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Anasayfa,
            this.Kursiyer,
            this.Mebbis,
            this.Mebbis_Button,
            this.Arama_Button,
            this.Araclar_Button,
            this.Donem_Grup,
            this.Peronsel_Button,
            this.Ayarlar_Button,
            this.Raporlar_Button,
            this.Yedek_Button,
            this.Yardim_Button});
            this.Menumain.Location = new System.Drawing.Point(0, 0);
            this.Menumain.Name = "Menumain";
            this.Menumain.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.Menumain.Size = new System.Drawing.Size(1439, 94);
            this.Menumain.TabIndex = 18;
            this.Menumain.Text = "Menu";
            this.Menumain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Menumain_ItemClicked);
            // 
            // Mebbis
            // 
            this.Mebbis.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Mebbisaktar,
            this.Mebgir});
            this.Mebbis.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Mebbis.Name = "Mebbis";
            this.Mebbis.Size = new System.Drawing.Size(12, 90);
            this.Mebbis.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Mebbis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // Mebbisaktar
            // 
            this.Mebbisaktar.Name = "Mebbisaktar";
            this.Mebbisaktar.Size = new System.Drawing.Size(158, 22);
            this.Mebbisaktar.Text = "Mebbis Aktarım";
            // 
            // Mebgir
            // 
            this.Mebgir.Name = "Mebgir";
            this.Mebgir.Size = new System.Drawing.Size(158, 22);
            this.Mebgir.Text = "Meb Giriş";
            // 
            // Lbl_Lisans
            // 
            this.Lbl_Lisans.AutoSize = true;
            this.Lbl_Lisans.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Lbl_Lisans.ForeColor = System.Drawing.Color.Transparent;
            this.Lbl_Lisans.Location = new System.Drawing.Point(1256, 794);
            this.Lbl_Lisans.Name = "Lbl_Lisans";
            this.Lbl_Lisans.Size = new System.Drawing.Size(145, 13);
            this.Lbl_Lisans.TabIndex = 27;
            this.Lbl_Lisans.Text = "Tarantula @2025 Ver.1.9.8.0";
            // 
            // Pickusak
            // 
            this.Pickusak.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Pickusak.Image = ((System.Drawing.Image)(resources.GetObject("Pickusak.Image")));
            this.Pickusak.Location = new System.Drawing.Point(0, 97);
            this.Pickusak.Name = "Pickusak";
            this.Pickusak.Size = new System.Drawing.Size(1439, 110);
            this.Pickusak.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pickusak.TabIndex = 25;
            this.Pickusak.TabStop = false;
            // 
            // Picture_Ozel
            // 
            this.Picture_Ozel.Image = ((System.Drawing.Image)(resources.GetObject("Picture_Ozel.Image")));
            this.Picture_Ozel.Location = new System.Drawing.Point(1308, 0);
            this.Picture_Ozel.Margin = new System.Windows.Forms.Padding(2);
            this.Picture_Ozel.Name = "Picture_Ozel";
            this.Picture_Ozel.Size = new System.Drawing.Size(94, 92);
            this.Picture_Ozel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Picture_Ozel.TabIndex = 20;
            this.Picture_Ozel.TabStop = false;
            // 
            // Anasayfa
            // 
            this.Anasayfa.Image = ((System.Drawing.Image)(resources.GetObject("Anasayfa.Image")));
            this.Anasayfa.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Anasayfa.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Anasayfa.Name = "Anasayfa";
            this.Anasayfa.Size = new System.Drawing.Size(72, 90);
            this.Anasayfa.Text = "Anasayfa";
            this.Anasayfa.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Anasayfa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Anasayfa.Click += new System.EventHandler(this.Anasayfa_Click_1);
            // 
            // Kursiyer
            // 
            this.Kursiyer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Kursiyer_Ekle,
            this.Arama,
            this.Kursiyersil});
            this.Kursiyer.Image = ((System.Drawing.Image)(resources.GetObject("Kursiyer.Image")));
            this.Kursiyer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Kursiyer.Name = "Kursiyer";
            this.Kursiyer.Size = new System.Drawing.Size(108, 90);
            this.Kursiyer.Text = "Kursiyer İşlemleri";
            this.Kursiyer.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Kursiyer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // Kursiyer_Ekle
            // 
            this.Kursiyer_Ekle.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Kursiyer_Ekle.Name = "Kursiyer_Ekle";
            this.Kursiyer_Ekle.Size = new System.Drawing.Size(141, 22);
            this.Kursiyer_Ekle.Text = "Yeni Kursiyer";
            this.Kursiyer_Ekle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Kursiyer_Ekle.Click += new System.EventHandler(this.Kursiyer_Ekle_Click);
            // 
            // Arama
            // 
            this.Arama.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Arama.Name = "Arama";
            this.Arama.Size = new System.Drawing.Size(141, 22);
            this.Arama.Text = "Kursiyer Ara";
            this.Arama.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Kursiyersil
            // 
            this.Kursiyersil.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Kursiyersil.Name = "Kursiyersil";
            this.Kursiyersil.Size = new System.Drawing.Size(141, 22);
            this.Kursiyersil.Text = "Kursiyer Sil";
            this.Kursiyersil.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Mebbis_Button
            // 
            this.Mebbis_Button.Image = ((System.Drawing.Image)(resources.GetObject("Mebbis_Button.Image")));
            this.Mebbis_Button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Mebbis_Button.Name = "Mebbis_Button";
            this.Mebbis_Button.Size = new System.Drawing.Size(132, 90);
            this.Mebbis_Button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Mebbis_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.Mebbis_Button.Click += new System.EventHandler(this.Mebbis_Button_Click);
            // 
            // Arama_Button
            // 
            this.Arama_Button.Image = ((System.Drawing.Image)(resources.GetObject("Arama_Button.Image")));
            this.Arama_Button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Arama_Button.Name = "Arama_Button";
            this.Arama_Button.Size = new System.Drawing.Size(72, 90);
            this.Arama_Button.Text = "Arama";
            this.Arama_Button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Arama_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // Araclar_Button
            // 
            this.Araclar_Button.Image = ((System.Drawing.Image)(resources.GetObject("Araclar_Button.Image")));
            this.Araclar_Button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Araclar_Button.Name = "Araclar_Button";
            this.Araclar_Button.Size = new System.Drawing.Size(72, 90);
            this.Araclar_Button.Text = "Araçlar";
            this.Araclar_Button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Araclar_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Araclar_Button.Click += new System.EventHandler(this.Araclar_Button_Click);
            // 
            // Donem_Grup
            // 
            this.Donem_Grup.Image = ((System.Drawing.Image)(resources.GetObject("Donem_Grup.Image")));
            this.Donem_Grup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Donem_Grup.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Donem_Grup.Name = "Donem_Grup";
            this.Donem_Grup.Size = new System.Drawing.Size(186, 90);
            this.Donem_Grup.Text = "Donem /Grup Bilgisi";
            this.Donem_Grup.Click += new System.EventHandler(this.Donem_Grup_Click);
            // 
            // Peronsel_Button
            // 
            this.Peronsel_Button.Image = ((System.Drawing.Image)(resources.GetObject("Peronsel_Button.Image")));
            this.Peronsel_Button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Peronsel_Button.Name = "Peronsel_Button";
            this.Peronsel_Button.Size = new System.Drawing.Size(72, 90);
            this.Peronsel_Button.Text = "Personel";
            this.Peronsel_Button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Peronsel_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Peronsel_Button.Click += new System.EventHandler(this.Peronsel_Button_Click);
            // 
            // Ayarlar_Button
            // 
            this.Ayarlar_Button.Image = ((System.Drawing.Image)(resources.GetObject("Ayarlar_Button.Image")));
            this.Ayarlar_Button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Ayarlar_Button.Name = "Ayarlar_Button";
            this.Ayarlar_Button.Size = new System.Drawing.Size(72, 90);
            this.Ayarlar_Button.Text = "Ayarlar";
            this.Ayarlar_Button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Ayarlar_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Ayarlar_Button.Click += new System.EventHandler(this.Ayarlar_Button_Click);
            // 
            // Raporlar_Button
            // 
            this.Raporlar_Button.Image = ((System.Drawing.Image)(resources.GetObject("Raporlar_Button.Image")));
            this.Raporlar_Button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Raporlar_Button.Name = "Raporlar_Button";
            this.Raporlar_Button.Size = new System.Drawing.Size(72, 90);
            this.Raporlar_Button.Text = "Rapor Al";
            this.Raporlar_Button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Raporlar_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Raporlar_Button.Click += new System.EventHandler(this.Raporlar_Button_Click);
            // 
            // Yedek_Button
            // 
            this.Yedek_Button.Image = ((System.Drawing.Image)(resources.GetObject("Yedek_Button.Image")));
            this.Yedek_Button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Yedek_Button.Name = "Yedek_Button";
            this.Yedek_Button.Size = new System.Drawing.Size(72, 90);
            this.Yedek_Button.Text = "Backup";
            this.Yedek_Button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Yedek_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Yedek_Button.Click += new System.EventHandler(this.Yedek_Button_Click);
            // 
            // Yardim_Button
            // 
            this.Yardim_Button.Image = ((System.Drawing.Image)(resources.GetObject("Yardim_Button.Image")));
            this.Yardim_Button.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Yardim_Button.Name = "Yardim_Button";
            this.Yardim_Button.Size = new System.Drawing.Size(116, 90);
            this.Yardim_Button.Text = "Yardım";
            this.Yardim_Button.Click += new System.EventHandler(this.Yardim_Button_Click);
            // 
            // Anamenu_Resim
            // 
            this.Anamenu_Resim.Image = ((System.Drawing.Image)(resources.GetObject("Anamenu_Resim.Image")));
            this.Anamenu_Resim.Location = new System.Drawing.Point(0, 213);
            this.Anamenu_Resim.Name = "Anamenu_Resim";
            this.Anamenu_Resim.Size = new System.Drawing.Size(1439, 609);
            this.Anamenu_Resim.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Anamenu_Resim.TabIndex = 21;
            this.Anamenu_Resim.TabStop = false;
            // 
            // Ana_Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1439, 816);
            this.Controls.Add(this.Lbl_Lisans);
            this.Controls.Add(this.Pickusak);
            this.Controls.Add(this.Picture_Ozel);
            this.Controls.Add(this.Menumain);
            this.Controls.Add(this.Anamenu_Resim);
            this.IsMdiContainer = true;
            this.Name = "Ana_Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ana_Menu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Menumain.ResumeLayout(false);
            this.Menumain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pickusak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Ozel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Anamenu_Resim)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox Anamenu_Resim;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem Yedek_Button;
        private System.Windows.Forms.ToolStripMenuItem Raporlar_Button;
        private System.Windows.Forms.ToolStripMenuItem Ayarlar_Button;
        private System.Windows.Forms.ToolStripMenuItem Peronsel_Button;
        private System.Windows.Forms.PictureBox Picture_Ozel;
        private System.Windows.Forms.ToolStripMenuItem Araclar_Button;
        private System.Windows.Forms.ToolStripMenuItem Mebgir;
        private System.Windows.Forms.ToolStripMenuItem Mebbisaktar;
        private System.Windows.Forms.ToolStripMenuItem Mebbis;
        private System.Windows.Forms.ToolStripMenuItem Kursiyersil;
        private System.Windows.Forms.ToolStripMenuItem Arama;
        private System.Windows.Forms.ToolStripMenuItem Kursiyer_Ekle;
        private System.Windows.Forms.ToolStripMenuItem Kursiyer;
        private System.Windows.Forms.ToolStripMenuItem Anasayfa;
        private System.Windows.Forms.ToolStripMenuItem Arama_Button;
        private System.Windows.Forms.MenuStrip Menumain;
        private PictureBox Pickusak;
        private Label Lbl_Lisans;
        private ToolStripMenuItem Mebbis_Button;
        private ToolStripMenuItem Yardim_Button;
        private ToolStripMenuItem Donem_Grup;
    }
}

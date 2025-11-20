namespace Tarantula_MTSK.Sayfalar
{
    partial class Form_Donemler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Donemler));
            this.Btn_Sil = new System.Windows.Forms.Button();
            this.Btn_Donem_Ekle = new System.Windows.Forms.Button();
            this.Btn_Guncelle = new System.Windows.Forms.Button();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Dtg_goster = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dtg_goster)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_Sil
            // 
            this.Btn_Sil.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Sil.Image")));
            this.Btn_Sil.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Btn_Sil.Location = new System.Drawing.Point(1008, 20);
            this.Btn_Sil.Name = "Btn_Sil";
            this.Btn_Sil.Size = new System.Drawing.Size(71, 75);
            this.Btn_Sil.TabIndex = 48;
            this.Btn_Sil.Text = "SİL";
            this.Btn_Sil.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Sil.UseVisualStyleBackColor = true;
            this.Btn_Sil.Click += new System.EventHandler(this.Btn_Sil_Click_1);
            // 
            // Btn_Donem_Ekle
            // 
            this.Btn_Donem_Ekle.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Donem_Ekle.Image")));
            this.Btn_Donem_Ekle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Btn_Donem_Ekle.Location = new System.Drawing.Point(768, 20);
            this.Btn_Donem_Ekle.Name = "Btn_Donem_Ekle";
            this.Btn_Donem_Ekle.Size = new System.Drawing.Size(122, 75);
            this.Btn_Donem_Ekle.TabIndex = 49;
            this.Btn_Donem_Ekle.Text = "Yeni Grup Ekle";
            this.Btn_Donem_Ekle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Donem_Ekle.UseVisualStyleBackColor = true;
            this.Btn_Donem_Ekle.Click += new System.EventHandler(this.Btn_Donem_Ekle_Click);
            // 
            // Btn_Guncelle
            // 
            this.Btn_Guncelle.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Guncelle.Image")));
            this.Btn_Guncelle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Btn_Guncelle.Location = new System.Drawing.Point(905, 20);
            this.Btn_Guncelle.Name = "Btn_Guncelle";
            this.Btn_Guncelle.Size = new System.Drawing.Size(83, 75);
            this.Btn_Guncelle.TabIndex = 50;
            this.Btn_Guncelle.Text = "Güncelle";
            this.Btn_Guncelle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Guncelle.UseVisualStyleBackColor = true;
            this.Btn_Guncelle.Click += new System.EventHandler(this.Btn_Guncelle_Click_1);
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(0, -2);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(1160, 110);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 51;
            this.PictureBox1.TabStop = false;
            // 
            // Dtg_goster
            // 
            this.Dtg_goster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dtg_goster.Location = new System.Drawing.Point(12, 133);
            this.Dtg_goster.Name = "Dtg_goster";
            this.Dtg_goster.Size = new System.Drawing.Size(1148, 475);
            this.Dtg_goster.TabIndex = 53;
            this.Dtg_goster.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dtg_goster_CellClick);
            this.Dtg_goster.DoubleClick += new System.EventHandler(this.Dtg_goster_DoubleClick);
            // 
            // Form_Donemler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 620);
            this.Controls.Add(this.Dtg_goster);
            this.Controls.Add(this.Btn_Sil);
            this.Controls.Add(this.Btn_Donem_Ekle);
            this.Controls.Add(this.Btn_Guncelle);
            this.Controls.Add(this.PictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_Donemler";
            this.Text = "Form_Donemler";
            this.Load += new System.EventHandler(this.Form_Donemler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dtg_goster)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Btn_Sil;
        private System.Windows.Forms.Button Btn_Donem_Ekle;
        private System.Windows.Forms.Button Btn_Guncelle;
        private System.Windows.Forms.PictureBox PictureBox1;
        private System.Windows.Forms.DataGridView Dtg_goster;
    }
}
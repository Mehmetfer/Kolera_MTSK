namespace Tarantula_MTSK.Sayfalar
{
    partial class Form_Raporlar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Raporlar));
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.Btn_Sil = new System.Windows.Forms.Button();
            this.Btn_Evrak = new System.Windows.Forms.Button();
            this.Btn_Kaydet = new System.Windows.Forms.Button();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox4
            // 
            this.PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox4.Image")));
            this.PictureBox4.Location = new System.Drawing.Point(1079, 20);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(72, 75);
            this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox4.TabIndex = 47;
            this.PictureBox4.TabStop = false;
            // 
            // Btn_Sil
            // 
            this.Btn_Sil.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Sil.Image")));
            this.Btn_Sil.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Btn_Sil.Location = new System.Drawing.Point(983, 20);
            this.Btn_Sil.Name = "Btn_Sil";
            this.Btn_Sil.Size = new System.Drawing.Size(71, 75);
            this.Btn_Sil.TabIndex = 43;
            this.Btn_Sil.Text = "SİL";
            this.Btn_Sil.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Sil.UseVisualStyleBackColor = true;
            // 
            // Btn_Evrak
            // 
            this.Btn_Evrak.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Evrak.Image")));
            this.Btn_Evrak.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Btn_Evrak.Location = new System.Drawing.Point(800, 20);
            this.Btn_Evrak.Name = "Btn_Evrak";
            this.Btn_Evrak.Size = new System.Drawing.Size(76, 75);
            this.Btn_Evrak.TabIndex = 44;
            this.Btn_Evrak.Text = "EVRAK ";
            this.Btn_Evrak.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Evrak.UseVisualStyleBackColor = true;
            // 
            // Btn_Kaydet
            // 
            this.Btn_Kaydet.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Kaydet.Image")));
            this.Btn_Kaydet.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Btn_Kaydet.Location = new System.Drawing.Point(894, 20);
            this.Btn_Kaydet.Name = "Btn_Kaydet";
            this.Btn_Kaydet.Size = new System.Drawing.Size(76, 75);
            this.Btn_Kaydet.TabIndex = 45;
            this.Btn_Kaydet.Text = "KAYDET";
            this.Btn_Kaydet.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Kaydet.UseVisualStyleBackColor = true;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(2, -2);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(1160, 110);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 46;
            this.PictureBox1.TabStop = false;
            // 
            // Form_Raporlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 450);
            this.Controls.Add(this.PictureBox4);
            this.Controls.Add(this.Btn_Sil);
            this.Controls.Add(this.Btn_Evrak);
            this.Controls.Add(this.Btn_Kaydet);
            this.Controls.Add(this.PictureBox1);
            this.Name = "Form_Raporlar";
            this.Text = "Form_Raporlar";
            this.Load += new System.EventHandler(this.Form_Raporlar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBox4;
        private System.Windows.Forms.Button Btn_Sil;
        private System.Windows.Forms.Button Btn_Evrak;
        private System.Windows.Forms.Button Btn_Kaydet;
        private System.Windows.Forms.PictureBox PictureBox1;
    }
}
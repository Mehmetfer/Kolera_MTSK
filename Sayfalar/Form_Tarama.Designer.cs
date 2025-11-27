namespace Tarantula_MTSK.Sayfalar
{
    partial class Form_Tarama
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
            this.Label4 = new System.Windows.Forms.Label();
            this.ListBox1 = new System.Windows.Forms.ListBox();
            this.Grp_1 = new System.Windows.Forms.GroupBox();
            this.Tmt = new System.Windows.Forms.TextBox();
            this.Ttt = new System.Windows.Forms.TextBox();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.Btn_Dosya = new System.Windows.Forms.Button();
            this.Btn_Tara = new System.Windows.Forms.Button();
            this.Grp_2 = new System.Windows.Forms.GroupBox();
            this.Btn_Parlak = new System.Windows.Forms.Button();
            this.TrackBarBrightness = new System.Windows.Forms.TrackBar();
            this.Bnt_Sag = new System.Windows.Forms.Button();
            this.Btn_Sol = new System.Windows.Forms.Button();
            this.RESIM_TARAMA = new System.Windows.Forms.PictureBox();
            this.Grp_1.SuspendLayout();
            this.Grp_2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RESIM_TARAMA)).BeginInit();
            this.SuspendLayout();
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(24, 283);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(87, 13);
            this.Label4.TabIndex = 8;
            this.Label4.Text = "Select a scanner";
            // 
            // ListBox1
            // 
            this.ListBox1.FormattingEnabled = true;
            this.ListBox1.Location = new System.Drawing.Point(12, 308);
            this.ListBox1.Name = "ListBox1";
            this.ListBox1.Size = new System.Drawing.Size(194, 147);
            this.ListBox1.TabIndex = 0;
            // 
            // Grp_1
            // 
            this.Grp_1.BackColor = System.Drawing.Color.Silver;
            this.Grp_1.Controls.Add(this.Tmt);
            this.Grp_1.Controls.Add(this.Ttt);
            this.Grp_1.Controls.Add(this.TextBox2);
            this.Grp_1.Controls.Add(this.TextBox1);
            this.Grp_1.Controls.Add(this.Btn_Save);
            this.Grp_1.Controls.Add(this.Label4);
            this.Grp_1.Controls.Add(this.Btn_Dosya);
            this.Grp_1.Controls.Add(this.Btn_Tara);
            this.Grp_1.Controls.Add(this.ListBox1);
            this.Grp_1.Location = new System.Drawing.Point(0, -2);
            this.Grp_1.Name = "Grp_1";
            this.Grp_1.Size = new System.Drawing.Size(226, 592);
            this.Grp_1.TabIndex = 1;
            this.Grp_1.TabStop = false;
            // 
            // Tmt
            // 
            this.Tmt.Location = new System.Drawing.Point(53, 561);
            this.Tmt.Name = "Tmt";
            this.Tmt.Size = new System.Drawing.Size(100, 20);
            this.Tmt.TabIndex = 9;
            // 
            // Ttt
            // 
            this.Ttt.Location = new System.Drawing.Point(53, 535);
            this.Ttt.Name = "Ttt";
            this.Ttt.Size = new System.Drawing.Size(100, 20);
            this.Ttt.TabIndex = 9;
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new System.Drawing.Point(53, 509);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(100, 20);
            this.TextBox2.TabIndex = 9;
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(53, 483);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(100, 20);
            this.TextBox1.TabIndex = 9;
            // 
            // Btn_Save
            // 
            this.Btn_Save.Location = new System.Drawing.Point(27, 218);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(170, 62);
            this.Btn_Save.TabIndex = 0;
            this.Btn_Save.Text = "KAYDET";
            this.Btn_Save.UseVisualStyleBackColor = true;
            
            // 
            // Btn_Dosya
            // 
            this.Btn_Dosya.Location = new System.Drawing.Point(27, 134);
            this.Btn_Dosya.Name = "Btn_Dosya";
            this.Btn_Dosya.Size = new System.Drawing.Size(170, 62);
            this.Btn_Dosya.TabIndex = 0;
            this.Btn_Dosya.Text = "DOSYADAN AL";
            this.Btn_Dosya.UseVisualStyleBackColor = true;
            this.Btn_Dosya.Click += new System.EventHandler(this.Btn_Dosya_Click);
            // 
            // Btn_Tara
            // 
            this.Btn_Tara.Location = new System.Drawing.Point(27, 59);
            this.Btn_Tara.Name = "Btn_Tara";
            this.Btn_Tara.Size = new System.Drawing.Size(170, 62);
            this.Btn_Tara.TabIndex = 0;
            this.Btn_Tara.Text = "TARAYICI";
            this.Btn_Tara.UseVisualStyleBackColor = true;
            // 
            // Grp_2
            // 
            this.Grp_2.BackColor = System.Drawing.Color.Silver;
            this.Grp_2.Controls.Add(this.Btn_Parlak);
            this.Grp_2.Controls.Add(this.TrackBarBrightness);
            this.Grp_2.Controls.Add(this.Bnt_Sag);
            this.Grp_2.Controls.Add(this.Btn_Sol);
            this.Grp_2.Location = new System.Drawing.Point(1220, -2);
            this.Grp_2.Name = "Grp_2";
            this.Grp_2.Size = new System.Drawing.Size(200, 678);
            this.Grp_2.TabIndex = 2;
            this.Grp_2.TabStop = false;
            // 
            // Btn_Parlak
            // 
            this.Btn_Parlak.Location = new System.Drawing.Point(39, 20);
            this.Btn_Parlak.Name = "Btn_Parlak";
            this.Btn_Parlak.Size = new System.Drawing.Size(127, 23);
            this.Btn_Parlak.TabIndex = 2;
            this.Btn_Parlak.Text = "Parlaklık";
            this.Btn_Parlak.UseVisualStyleBackColor = true;
            // 
            // TrackBarBrightness
            // 
            this.TrackBarBrightness.Location = new System.Drawing.Point(39, 59);
            this.TrackBarBrightness.Name = "TrackBarBrightness";
            this.TrackBarBrightness.Size = new System.Drawing.Size(139, 45);
            this.TrackBarBrightness.TabIndex = 1;
            // 
            // Bnt_Sag
            // 
            this.Bnt_Sag.Location = new System.Drawing.Point(103, 331);
            this.Bnt_Sag.Name = "Bnt_Sag";
            this.Bnt_Sag.Size = new System.Drawing.Size(75, 23);
            this.Bnt_Sag.TabIndex = 0;
            this.Bnt_Sag.Text = "SAG Döndür";
            this.Bnt_Sag.UseVisualStyleBackColor = true;
            // 
            // Btn_Sol
            // 
            this.Btn_Sol.Location = new System.Drawing.Point(19, 331);
            this.Btn_Sol.Name = "Btn_Sol";
            this.Btn_Sol.Size = new System.Drawing.Size(75, 23);
            this.Btn_Sol.TabIndex = 0;
            this.Btn_Sol.Text = "SOL döndür";
            this.Btn_Sol.UseVisualStyleBackColor = true;
            // 
            // RESIM_TARAMA
            // 
            this.RESIM_TARAMA.Location = new System.Drawing.Point(244, 18);
            this.RESIM_TARAMA.Name = "RESIM_TARAMA";
            this.RESIM_TARAMA.Size = new System.Drawing.Size(695, 453);
            this.RESIM_TARAMA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.RESIM_TARAMA.TabIndex = 3;
            this.RESIM_TARAMA.TabStop = false;
            // 
            // Form_Tarama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1410, 688);
            this.Controls.Add(this.Grp_2);
            this.Controls.Add(this.Grp_1);
            this.Controls.Add(this.RESIM_TARAMA);
            this.Name = "Form_Tarama";
            this.Text = "Form_Tarama";
            this.TopMost = true;
            this.Grp_1.ResumeLayout(false);
            this.Grp_1.PerformLayout();
            this.Grp_2.ResumeLayout(false);
            this.Grp_2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RESIM_TARAMA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.ListBox ListBox1;
        private System.Windows.Forms.GroupBox Grp_1;
        private System.Windows.Forms.Button Btn_Tara;
        private System.Windows.Forms.Button Btn_Save;
        private System.Windows.Forms.Button Btn_Dosya;
        private System.Windows.Forms.GroupBox Grp_2;
        private System.Windows.Forms.PictureBox RESIM_TARAMA;
        private System.Windows.Forms.Button Bnt_Sag;
        private System.Windows.Forms.Button Btn_Sol;
        private System.Windows.Forms.TrackBar TrackBarBrightness;
        private System.Windows.Forms.Button Btn_Parlak;
        private System.Windows.Forms.TextBox Tmt;
        private System.Windows.Forms.TextBox Ttt;
        private System.Windows.Forms.TextBox TextBox2;
        private System.Windows.Forms.TextBox TextBox1;
    }
}
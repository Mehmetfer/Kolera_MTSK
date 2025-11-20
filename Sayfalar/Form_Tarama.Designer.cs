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
            this.Button1 = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.ComboBox1 = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Btn_sec = new System.Windows.Forms.Button();
            this.ListBox1 = new System.Windows.Forms.ListBox();
            this.TableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Btn_Tarama = new System.Windows.Forms.Button();
            this.GroupBox1.SuspendLayout();
            this.TableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.TableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Button1.Location = new System.Drawing.Point(3, 3);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(509, 1);
            this.Button1.TabIndex = 0;
            this.Button1.Text = "Start scan";
            this.Button1.UseVisualStyleBackColor = true;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.Btn_Tarama);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.ComboBox1);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.TextBox2);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.TextBox1);
            this.GroupBox1.Controls.Add(this.Btn_sec);
            this.GroupBox1.Controls.Add(this.ListBox1);
            this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBox1.Location = new System.Drawing.Point(3, 3);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(323, 613);
            this.GroupBox1.TabIndex = 1;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Properties";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(6, 24);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(87, 13);
            this.Label4.TabIndex = 8;
            this.Label4.Text = "Select a scanner";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(9, 291);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(68, 13);
            this.Label3.TabIndex = 7;
            this.Label3.Text = "Image format";
            // 
            // ComboBox1
            // 
            this.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.Items.AddRange(new object[] {
            "PNG",
            "JPEG",
            "BMP",
            "GIF",
            "TIFF"});
            this.ComboBox1.Location = new System.Drawing.Point(9, 307);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(194, 21);
            this.ComboBox1.TabIndex = 6;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(9, 238);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(49, 13);
            this.Label2.TabIndex = 5;
            this.Label2.Text = "Filename";
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new System.Drawing.Point(9, 254);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(194, 20);
            this.TextBox2.TabIndex = 4;
            this.TextBox2.Text = "myscan";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(9, 341);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(119, 13);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "Output scanned images";
            // 
            // TextBox1
            // 
            this.TextBox1.Enabled = false;
            this.TextBox1.Location = new System.Drawing.Point(9, 357);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(194, 20);
            this.TextBox1.TabIndex = 2;
            // 
            // Btn_sec
            // 
            this.Btn_sec.Location = new System.Drawing.Point(9, 383);
            this.Btn_sec.Name = "Btn_sec";
            this.Btn_sec.Size = new System.Drawing.Size(194, 38);
            this.Btn_sec.TabIndex = 1;
            this.Btn_sec.Text = "Change output folder";
            this.Btn_sec.UseVisualStyleBackColor = true;
            this.Btn_sec.Click += new System.EventHandler(this.Btn_sec_Click);
            // 
            // ListBox1
            // 
            this.ListBox1.FormattingEnabled = true;
            this.ListBox1.Location = new System.Drawing.Point(9, 46);
            this.ListBox1.Name = "ListBox1";
            this.ListBox1.Size = new System.Drawing.Size(194, 173);
            this.ListBox1.TabIndex = 0;
            // 
            // TableLayoutPanel2
            // 
            this.TableLayoutPanel2.ColumnCount = 1;
            this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel2.Controls.Add(this.Button1, 0, 0);
            this.TableLayoutPanel2.Controls.Add(this.PictureBox1, 0, 1);
            this.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanel2.Location = new System.Drawing.Point(332, 3);
            this.TableLayoutPanel2.Name = "TableLayoutPanel2";
            this.TableLayoutPanel2.RowCount = 2;
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 606F));
            this.TableLayoutPanel2.Size = new System.Drawing.Size(515, 613);
            this.TableLayoutPanel2.TabIndex = 2;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox1.Location = new System.Drawing.Point(3, 10);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(509, 600);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 1;
            this.PictureBox1.TabStop = false;
            this.PictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.ColumnCount = 2;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 521F));
            this.TableLayoutPanel1.Controls.Add(this.GroupBox1, 0, 0);
            this.TableLayoutPanel1.Controls.Add(this.TableLayoutPanel2, 1, 0);
            this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 1;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(850, 619);
            this.TableLayoutPanel1.TabIndex = 4;
            // 
            // Btn_Tarama
            // 
            this.Btn_Tarama.Location = new System.Drawing.Point(227, 87);
            this.Btn_Tarama.Name = "Btn_Tarama";
            this.Btn_Tarama.Size = new System.Drawing.Size(101, 23);
            this.Btn_Tarama.TabIndex = 9;
            this.Btn_Tarama.Text = "Btn_Tarama";
            this.Btn_Tarama.UseVisualStyleBackColor = true;
            this.Btn_Tarama.Click += new System.EventHandler(this.Btn_Tarama_Click);
            // 
            // Form_Tarama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 619);
            this.Controls.Add(this.TableLayoutPanel1);
            this.Name = "Form_Tarama";
            this.Text = "Form_Tarama";
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.TableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.TableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.Button Btn_Tarama;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.ComboBox ComboBox1;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.TextBox TextBox2;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.Button Btn_sec;
        private System.Windows.Forms.ListBox ListBox1;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel2;
        private System.Windows.Forms.PictureBox PictureBox1;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
    }
}
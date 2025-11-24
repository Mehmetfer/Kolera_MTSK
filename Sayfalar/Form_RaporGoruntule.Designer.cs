namespace Tarantula_MTSK.Sayfalar
{
    partial class Form_RaporGoruntule
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.WebBrowser Web_Rapor_Goster;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Web_Rapor_Goster = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // Web_Rapor_Goster
            // 
            this.Web_Rapor_Goster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Web_Rapor_Goster.Location = new System.Drawing.Point(0, 0);
            this.Web_Rapor_Goster.MinimumSize = new System.Drawing.Size(20, 20);
            this.Web_Rapor_Goster.Name = "Web_Rapor_Goster";
            this.Web_Rapor_Goster.Size = new System.Drawing.Size(800, 600);
            this.Web_Rapor_Goster.TabIndex = 0;
            // 
            // Form_RaporGoruntule
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.Web_Rapor_Goster);
            this.Name = "Form_RaporGoruntule";
            this.Text = "Kursiyer Raporu";
           
            this.ResumeLayout(false);
        }
    }
}
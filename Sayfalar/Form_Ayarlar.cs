using System;
using System.Windows.Forms;

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Form_Ayarlar : Form
    {
        private readonly string _connectionString;

        // Ana menüden connection string alan constructor
        public Form_Ayarlar(string connectionString)
        {
            _connectionString = connectionString ??
                                throw new ArgumentNullException(nameof(connectionString));

            InitializeComponent();
            this.Load += Form_Ayarlar_Load;
        }

        private void Form_Ayarlar_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde yapılacak işlemler

            // Örnek: Label'e bilgi basabilirsiniz
            // labelStatus.Text = "Araçlar sayfası yüklendi.";
        }
    }
}

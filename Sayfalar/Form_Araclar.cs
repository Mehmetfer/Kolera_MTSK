using System;
using System.Windows.Forms;

namespace Tarantula_MTSK.Sayfalar
{
    public partial class Form_Araclar : Form
    {
        private readonly string _connectionString;

        // Ana menüden connection string alan constructor
        public Form_Araclar(string connectionString)
        {
            _connectionString = connectionString ??
                                throw new ArgumentNullException(nameof(connectionString));

            InitializeComponent();
            this.Load += Form_Araclar_Load;
        }

        private void Form_Araclar_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde yapılacak işlemler

            // Örnek: Label'e bilgi basabilirsiniz
            // labelStatus.Text = "Araçlar sayfası yüklendi.";
        }
    }
}

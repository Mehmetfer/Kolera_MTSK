using ScannerDemo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIA;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Tarantula_MTSK.Sayfalar
{
    public partial class Form_Tarama : Form
    {
        public Form_Tarama()
        {
            InitializeComponent();
        }
        private void Form1_Tarama_Load(object sender, EventArgs e)
        {
            ListScanners();

            // Set start output folder TMP
            TextBox1.Text = Path.GetTempPath();
            // Set JPEG as default
            ComboBox1.SelectedIndex = 1;

        }
        private void ListScanners()
        {
            // Clear the ListBox.
            ListBox1.Items.Clear();

            // Create a DeviceManager instance
            var deviceManager = new DeviceManager();

            // Loop through the list of devices and add the name to the listbox
            for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
            {
                // Add the device only if it's a scanner
                if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                {
                    continue;
                }

                // Add the Scanner device to the listbox (the entire DeviceInfos object)
                // Important: we store an object of type scanner (which ToString method returns the name of the scanner)
                ListBox1.Items.Add(
                    new Scanner(deviceManager.DeviceInfos[i])
                );
            }
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Btn_Tarama_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(StartScanning).ContinueWith(result => TriggerScan());
        }
        private void TriggerScan()
        {
            Console.WriteLine("Image succesfully scanned");
        }

        public void StartScanning()
        {
            Scanner device = null;

            this.Invoke(new MethodInvoker(delegate ()
            {
                device = ListBox1.SelectedItem as Scanner;
            }));

            if (device == null)
            {
                MessageBox.Show("You need to select first an scanner device from the list",
                                "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (String.IsNullOrEmpty(TextBox2.Text))
            {
                MessageBox.Show("Provide a filename",
                                "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ImageFile image = new ImageFile();
            string imageExtension = "";

            this.Invoke(new MethodInvoker(delegate ()
            {
                switch (ComboBox1.SelectedIndex)
                {
                    case 0:
                        image = device.ScanImage(WIA.FormatID.wiaFormatPNG);
                        imageExtension = ".png";
                        break;
                    case 1:
                        image = device.ScanImage(WIA.FormatID.wiaFormatJPEG);
                        imageExtension = ".jpeg";
                        break;
                    case 2:
                        image = device.ScanImage(WIA.FormatID.wiaFormatBMP);
                        imageExtension = ".bmp";
                        break;
                    case 3:
                        image = device.ScanImage(WIA.FormatID.wiaFormatGIF);
                        imageExtension = ".gif";
                        break;
                    case 4:
                        image = device.ScanImage(WIA.FormatID.wiaFormatTIFF);
                        imageExtension = ".tiff";
                        break;
                }
            }));


            // Save the image
            var path = Path.Combine(TextBox1.Text, TextBox2.Text + imageExtension);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            image.SaveFile(path);

            PictureBox1.Image = new Bitmap(path);
        }

        private void Btn_sec_Click(object sender, EventArgs e)
        {
            using (var folderDlg = new FolderBrowserDialog
            {
                ShowNewFolderButton = true
            })
            {
                DialogResult result = folderDlg.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Seçilen klasör yolu:
                    Console.WriteLine(folderDlg.SelectedPath);
                    // Burada işlemler yapılabilir
                }
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
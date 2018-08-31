using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int toplam = 0;

        public void altdosyalar(string secilidosya)
        {
            string[] dosyalar = Directory.GetDirectories(secilidosya);
            string[] dosyalar2 = Directory.GetFiles(secilidosya);
            foreach (string dosya in dosyalar)
            {
                listBoxDOSYA.Items.Add(dosya);
                altdosyalar(dosya);
            }

            foreach (string dosya2 in dosyalar2)
            {

                if (dosya2.EndsWith(".Designer.cs"))
                {
                    listBoxDOSYA.Items.Add(dosya2);
                }
                else if (dosya2.EndsWith(".cs"))
                {
                    int count = 0;
                    StreamReader oku = File.OpenText(dosya2);
                    while (oku.ReadLine() != null)
                    {
                        count++;
                    }
                    toplam = toplam + count;
                    listBoxDOSYA.Items.Add(dosya2 + "----" + count);
                }
                else
                {
                    listBoxDOSYA.Items.Add(dosya2);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                altdosyalar(fbd.SelectedPath);
            }

            listBoxDOSYA.Items.Add(" Toplam Satır Sayısı =" + toplam);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = " Kaydedilecek yeri seçiniz .";
            saveFileDialog1.Filter = "Text Dosyaları |*.txt";
            saveFileDialog1.FileName = "";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter yaz = File.AppendText(saveFileDialog1.FileName.ToString ());
                foreach (var item in listBoxDOSYA.Items)
                {
                    yaz.WriteLine(item.ToString());
                }
                yaz.Close();
                MessageBox.Show("Kaydedildi");
            }
        }
    }
}

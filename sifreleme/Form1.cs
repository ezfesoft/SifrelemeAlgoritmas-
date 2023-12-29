using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace sifreleme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string hash = "";
        private void button1_Click(object sender, EventArgs e)
        {
           label1.Text= Sifrele(textBox1.Text);
            textBox2.Text = label2.Text;
        }

        public string Sifrele(string text)
        {
            byte[] data = Encoding.Default.GetBytes(text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(Encoding.Default.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider()
                { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDES.CreateEncryptor();
                    byte[] sonuc = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(sonuc, 0, sonuc.Length);
                }

            }
        }

        public string SifreCoz(string text)
        {
            byte[] data = Convert.FromBase64String(text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(Encoding.Default.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider()
                { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDES.CreateDecryptor();
                    byte[] sonuc = transform.TransformFinalBlock(data, 0, data.Length);
                    return Encoding.Default.GetString(sonuc);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            label2.Text=SifreCoz(textBox2.Text);
        }
    }
}

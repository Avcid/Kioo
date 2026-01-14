using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bandara
{
    public partial class Form1 : Form
    {
        BromoAirlinesEntities db = new BromoAirlinesEntities();
        public Form1() 
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 regristrasi = new Form2();
            regristrasi.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Username atau Password harus di isi!");
                return;
            }
            var username = textBox1.Text;
            var password = textBox2.Text;

            var cariPengguna = db.Akun.Where(pengguna => pengguna.Username == username && pengguna.Password == password).FirstOrDefault();

            if (cariPengguna != null)
            {

                MessageBox.Show("Berhasil Masuk");

                Form3 BromoAirlinesAdmin = new Form3();
                BromoAirlinesAdmin.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Gagal Masuk");
            }
        }
    }
}

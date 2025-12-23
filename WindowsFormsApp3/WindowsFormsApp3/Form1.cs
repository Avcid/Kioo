using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{

    public static class UserSession
    {
        public static string NamaLengkap;
    }
    public partial class Form1 : Form
    {

        MiniKasirEntities db = new MiniKasirEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string emailInput = textBox1.Text;
            string passwordInput = textBox2.Text;

            
            if (string.IsNullOrEmpty(emailInput) || string.IsNullOrEmpty(passwordInput))
            {
                MessageBox.Show("Email dan Password harus diisi!");
                return;
            }

            
           
            {
                
                var user = db.Users.FirstOrDefault(u => u.Email == emailInput && u.Password == passwordInput);

                if (user != null)
                {
                    MessageBox.Show("Login Berhasil! Selamat datang " + user.FirstName);
                    UserSession.NamaLengkap = $"{user.FirstName} {user.LastName}";

                    Form3 menu = new Form3();
                    menu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Email atau Password salah!");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
    }
}
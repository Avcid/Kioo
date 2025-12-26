using System;
using System.Linq;
using System.Windows.Forms;

namespace Corporation
{
    public partial class Form1 : Form
    {
        private readonly EsemkaCorporationEntities db = new EsemkaCorporationEntities();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                lblError.Text = "Email and password are required.";
                return;
            }

            var user = db.employees.FirstOrDefault(u =>
                u.deleted_at == null &&
                u.email == email &&
                u.password == password
            );

            if (user == null)
            {
                lblError.Text = "Error";
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            Hide();
            using (var dashboard = new Form2(user.id))
            {
                dashboard.ShowDialog();
            }
            Show();

            txtPassword.Clear();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            db.Dispose();
            base.OnFormClosed(e);
        }

        private void lblError_lick(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

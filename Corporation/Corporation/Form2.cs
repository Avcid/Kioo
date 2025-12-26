using System;
using System.Linq;
using System.Windows.Forms;

namespace Corporation
{
    public partial class Form2 : Form
    {
        private readonly EsemkaCorporationEntities db = new EsemkaCorporationEntities();
        private int _employeeId;

        public Form2(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;

            button3.Click += button3_Click;
        }

        public Form2()
        {
            InitializeComponent();
            _employeeId = -1;

            button3.Click += button3_Click;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (_employeeId <= 0)
            {
                lblWelacome.Text = "Welcome";
                return;
            }

            var employee = db.employees.FirstOrDefault(e1 => e1.id == _employeeId && e1.deleted_at == null);

            if (employee == null)
            {
                MessageBox.Show("Employee data not found.");
                Close();
                return;
            }

            lblWelacome.Text = "Welcome, " + employee.name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var profile = new Form3(_employeeId))
            {
                profile.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var mutation = new Form4(_employeeId))
            {
                mutation.ShowDialog();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            using (var promotion = new Form5(_employeeId))
            {
                promotion.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            db.Dispose();
            base.OnFormClosed(e);
        }
    }
}

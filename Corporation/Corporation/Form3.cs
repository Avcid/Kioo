using System;
using System.Linq;
using System.Windows.Forms;

namespace Corporation
{
    public partial class Form3 : Form
    {
        private EsemkaCorporationEntities db = new EsemkaCorporationEntities();
        private int _employeeId;

        public Form3(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            var employee = db.employees
                .FirstOrDefault(x => x.id == _employeeId && x.deleted_at == null);

            if (employee == null)
            {
                MessageBox.Show("Employee not found");
                Close();
                return;
            }

            var currentPosition = db.positions
                .Where(p => p.employee_id == _employeeId && p.deleted_at == null)
                .OrderByDescending(p => p.created_at)
                .FirstOrDefault();

            txtNama.Text = employee.name ?? "";
            txtEmail.Text = employee.email ?? "";
            txtPhoneNumber.Text = employee.phone_number ?? "";
            txtHireDate.Text = employee.hire_date.ToString("yyyy-MM-dd");

            if (currentPosition == null || currentPosition.job == null)
            {
                txtPosistion.Text = "-";
                txtJoblavel.Text = "-";
                txtDepartmen.Text = "-";

                linkLabel1.Text = "-";
                linkLabel1.Tag = null;

                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
                return;
            }

            txtPosistion.Text = currentPosition.job.name ?? "-";
            txtJoblavel.Text = currentPosition.job.job_level != null ? currentPosition.job.job_level.name : "-";
            txtDepartmen.Text = currentPosition.job.department != null ? currentPosition.job.department.name : "-";

     
            dataGridView2.DataSource = db.positions
                .Where(p => p.employee_id == _employeeId
                            && p.deleted_at == null
                            && p.job != null
                            && p.job.department != null
                            && p.job.job_level != null)
                .OrderByDescending(p => p.job.job_level.id)
                .Select(p => new
                {
                    Job = p.job.name,
                    Department = p.job.department.name,
                    Level = p.job.job_level.name
                })
                .ToList();

            if (currentPosition.job.supervisor_job_id != null)
            {
                int supJobId = currentPosition.job.supervisor_job_id.Value;

                var supervisorPosition = db.positions
                    .Where(p => p.deleted_at == null
                                && p.job_id == supJobId
                                && p.employee != null
                                && p.employee.deleted_at == null)
                    .OrderByDescending(p => p.created_at)
                    .FirstOrDefault();

                if (supervisorPosition != null && supervisorPosition.employee != null)
                {
                    linkLabel1.Text = supervisorPosition.employee.name ?? "-";
                    linkLabel1.Tag = supervisorPosition.employee.id;
                }
                else
                {
                    linkLabel1.Text = "-";
                    linkLabel1.Tag = null;
                }
            }
            else
            {
                linkLabel1.Text = "-";
                linkLabel1.Tag = null;
            }

            dataGridView1.DataSource = db.positions
                .Where(p => p.deleted_at == null
                            && p.job != null
                            && p.job.supervisor_job_id == currentPosition.job_id
                            && p.employee != null
                            && p.employee.deleted_at == null)
                .Select(p => new
                {
                    Name = p.employee.name,
                    Job = p.job.name
                })
                .ToList();

            dataGridView3.DataSource = db.positions
                .Where(p => p.deleted_at == null
                            && p.employee_id != _employeeId
                            && p.job != null
                            && p.employee != null
                            && p.employee.deleted_at == null
                            && p.job.supervisor_job_id == currentPosition.job.supervisor_job_id)
                .Select(p => new
                {
                    Name = p.employee.name,
                    Job = p.job.name
                })
                .ToList();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabel1.Tag == null) return;

            int supervisorId = (int)linkLabel1.Tag;
            using (var supervisorProfile = new Form3(supervisorId))
            {
                supervisorProfile.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close(); // Main
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            db.Dispose();
            base.OnFormClosed(e);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}

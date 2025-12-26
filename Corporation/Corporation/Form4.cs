using System;
using System.Linq;
using System.Windows.Forms;

namespace Corporation
{
    public partial class Form4 : Form
    {
        private readonly EsemkaCorporationEntities db = new EsemkaCorporationEntities();
        private readonly int _employeeId;

        public Form4(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            LoadHeader();
            LoadAvailableMutationJobs();
        }

        private void LoadHeader()
        {
            var employee = db.employees.FirstOrDefault(x => x.id == _employeeId && x.deleted_at == null);
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

            if (currentPosition == null || currentPosition.job == null)
            {
                MessageBox.Show("Current position not found");
                Close();
                return;
            }

            txtName.Text = employee.name ?? "";
            txtCurrentDepartment.Text = currentPosition.job.department != null ? currentPosition.job.department.name : "-";
            txtCurrentPosition.Text = currentPosition.job.name ?? "-";
            txtCurrentJobLevel.Text = currentPosition.job.job_level != null ? currentPosition.job.job_level.name : "-";
        }

        private void LoadAvailableMutationJobs()
        {
            var currentPosition = db.positions
                .Where(p => p.employee_id == _employeeId && p.deleted_at == null)
                .OrderByDescending(p => p.created_at)
                .FirstOrDefault();

            if (currentPosition == null || currentPosition.job == null || currentPosition.job.job_level == null)
            {
                dataGridView1.DataSource = null;
                return;
            }

            int currentJobId = currentPosition.job_id;
            int currentLevelId = currentPosition.job.job_level.id;

            var appliedJobIds = db.mutations
                .Where(m => m.deleted_at == null && m.employee_id == _employeeId && m.status == "Pending")
                .Select(m => m.job_id)
                .ToList();

            var available = db.jobs
                .Where(j => j.deleted_at == null && j.job_level_id == currentLevelId && j.id != currentJobId)
                .Select(j => new
                {
                    JobId = j.id,
                    Department = j.department.name,
                    Position = j.name,
                    Vacancy = j.head_count - db.positions
                        .Where(p => p.deleted_at == null && p.job_id == j.id && p.employee.deleted_at == null)
                        .GroupBy(p => p.employee_id)
                        .Count(),
                    AlreadyApplied = appliedJobIds.Contains(j.id)
                })
                .Where(x => x.Vacancy > 0)
                .Select(x => new
                {
                    x.JobId,
                    x.Department,
                    x.Position,
                    x.AlreadyApplied
                })
                .ToList();

            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Department",
                DataPropertyName = "Department",
                ReadOnly = true
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Position",
                DataPropertyName = "Position",
                ReadOnly = true
            });

            dataGridView1.Columns.Add(new DataGridViewButtonColumn
            {
                HeaderText = "Action",
                Text = "Apply",
                UseColumnTextForButtonValue = true,
                Name = "btnApply"
            });

            dataGridView1.DataSource = available;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                dynamic item = row.DataBoundItem;
                if (item == null) continue;

                if ((bool)item.AlreadyApplied)
                {
                    row.Cells["btnApply"].ReadOnly = true;
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.Gray;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridView1.Columns[e.ColumnIndex].Name != "btnApply") return;

            dynamic rowData = dataGridView1.Rows[e.RowIndex].DataBoundItem;
            if (rowData == null) return;

            int jobId = rowData.JobId;
            bool alreadyApplied = rowData.AlreadyApplied;

            if (alreadyApplied)
            {
                MessageBox.Show("You already applied for this job (Pending).");
                return;
            }

            var m = new mutation
            {
                employee_id = _employeeId,
                job_id = jobId,
                status = "Pending",
                created_at = DateTime.Now
            };

            db.mutations.Add(m);
            db.SaveChanges();

            MessageBox.Show("Mutation applied (Pending).");
            LoadAvailableMutationJobs();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            db.Dispose();
            base.OnFormClosed(e);
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

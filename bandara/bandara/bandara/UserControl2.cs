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
    public partial class UserControl2 : UserControl
    {
        BromoAirlinesEntities db = new BromoAirlinesEntities();
        private int idMaskapai = 0;
        public UserControl2()
        {
            InitializeComponent();
        }

        private void UserControl2_Load(object sender, EventArgs e)
        {
            maskapaiBindingSource.DataSource = db.Maskapai.ToList();
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            richTextBox1.Clear();
            numericUpDown1.Value = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Nama = textBox1.Text;
            string  Perusahaan = textBox2.Text;
            int JumlahKru = (int) numericUpDown1.Value;
            string Deskripsi = richTextBox1.Text;

            if (idMaskapai == 0)
            {
                Maskapai tbhMaskapai = new Maskapai()
                {
                    Nama = Nama,
                    Perusahaan = Perusahaan,
                    JumlahKru = JumlahKru,
                    Deskripsi = richTextBox1.Text,
                };

                db.Maskapai.Add(tbhMaskapai);
                db.SaveChanges();

                MessageBox.Show("Berhasil Menambah Data");

                this.OnLoad(EventArgs.Empty);

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is Maskapai mskpai)
            {
                if (Edit.Index == e.ColumnIndex)
                {
                    var editMaskapai = db.Maskapai.Where(mask => mask.ID == mskpai.ID).FirstOrDefault();
                    textBox1.Text = editMaskapai.Nama;
                    textBox2.Text = editMaskapai.Perusahaan;
                    numericUpDown1.Value = editMaskapai.JumlahKru;
                    richTextBox1.Text = editMaskapai.Deskripsi;
                }

                if (Delete.Index == e.ColumnIndex)
                {
                    var pesan = MessageBox.Show("Apakah Apakah kamu yakin ingin menghapus data reservasi ini?", "Peringatan", MessageBoxButtons.YesNo);

                    if (pesan == DialogResult.Yes)
                    {
                        int maskapaiidToDelete = mskpai.ID;
                        var maskapaiToDelete = db.Maskapai.SingleOrDefault(m => m.ID == maskapaiidToDelete);

                        if (maskapaiToDelete != null)
                        {
                            db.Maskapai.Remove(maskapaiToDelete);
                            db.SaveChanges();

                            this.OnLoad(EventArgs.Empty);
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dataGridView1.Rows[e.ColumnIndex].DataBoundItem is Maskapai mskapai)
            {
                if (Edit.Index == e.ColumnIndex)
                {
                    e.Value = "Ubah";
                }

                if (Delete.Index == e.ColumnIndex)
                {
                    e.Value = "Delete";
                }
            }
        }
    }
}

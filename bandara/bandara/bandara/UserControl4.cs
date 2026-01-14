using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace bandara
{
    public partial class UserControl4 : UserControl
    {
        BromoAirlinesEntities db = new BromoAirlinesEntities();
        private int? kodePromoSedangDiedit = null;

        public UserControl4()
        {
            InitializeComponent();
        }

        private void UserControl4_Load(object sender, EventArgs e)
        {
            LoadData();
            numericUpDown1.Maximum = 100;
            numericUpDown1.Minimum = 0;
            numericUpDown2.Maximum = 999999999;
            numericUpDown2.Minimum = 0;
        }

        private void LoadData()
        {
            db = new BromoAirlinesEntities();
            kodePromoBindingSource.DataSource = db.KodePromo.ToList();
            kodePromoSedangDiedit = null;
            button2.Text = "Simpan";
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var kdPromo = dataGridView1.Rows[e.RowIndex].DataBoundItem as KodePromo;

            if (kdPromo != null)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "MaksimumDiskon")
                {
                    e.Value = kdPromo.MaksimumDiskon.ToString("C0", new CultureInfo("id-ID"));
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "BerlakuSampai")
                {
                    e.Value = kdPromo.BerlakuSampai.ToString("dd-MM-yyyy");
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "Edit")
                {
                    e.Value = "Ubah";
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    e.Value = "Hapus";
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var kdpromo = dataGridView1.Rows[e.RowIndex].DataBoundItem as KodePromo;
            if (kdpromo == null) return;

            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

            if (columnName == "Edit")
            {
                kodePromoSedangDiedit = kdpromo.ID;
                textBox1.Text = kdpromo.Kode;
                dateTimePicker1.Value = kdpromo.BerlakuSampai;
                numericUpDown1.Value = (decimal)kdpromo.PersentaseDiskon;
                numericUpDown2.Value = (decimal)kdpromo.MaksimumDiskon;
                richTextBox1.Text = kdpromo.Deskripsi;
                button2.Text = "Update";
            }

            if (columnName == "Delete")
            {
                var pesan = MessageBox.Show($"Apakah Anda yakin ingin menghapus kode promo {kdpromo.Kode}?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (pesan == DialogResult.Yes)
                {
                    db.KodePromo.Remove(kdpromo);
                    db.SaveChanges();
                    MessageBox.Show("Data berhasil dihapus.");
                    LoadData();
                    button1_Click(null, null);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            dateTimePicker1.Value = DateTime.Now;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            richTextBox1.Clear();
            kodePromoSedangDiedit = null;
            button2.Text = "Simpan";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string kodeInput = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(kodeInput) || string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                MessageBox.Show("Semua data wajib diisi.");
                return;
            }

            var cekKode = db.KodePromo.Any(k => k.Kode == kodeInput && k.ID != (kodePromoSedangDiedit ?? 0));
            if (cekKode)
            {
                MessageBox.Show("Kode promo sudah digunakan, silakan gunakan kode lain.");
                return;
            }

            try
            {
                if (kodePromoSedangDiedit == null)
                {
                    var baru = new KodePromo
                    {
                        Kode = kodeInput,
                        BerlakuSampai = dateTimePicker1.Value,
                        PersentaseDiskon = (int)numericUpDown1.Value,
                        MaksimumDiskon = (double)numericUpDown2.Value,
                        Deskripsi = richTextBox1.Text.Trim()
                    };
                    db.KodePromo.Add(baru);
                }
                else
                {
                    var edit = db.KodePromo.Find(kodePromoSedangDiedit);
                    if (edit != null)
                    {
                        edit.Kode = kodeInput;
                        edit.BerlakuSampai = dateTimePicker1.Value;
                        edit.PersentaseDiskon = (int)numericUpDown1.Value;
                        edit.MaksimumDiskon = (double)numericUpDown2.Value;
                        edit.Deskripsi = richTextBox1.Text.Trim();
                    }
                }

                db.SaveChanges();
                MessageBox.Show("Data berhasil disimpan.");
                LoadData();
                button1_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
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

namespace bandara
{
    public partial class UserControl3 : UserControl
    {
        BromoAirlinesEntities db = new BromoAirlinesEntities();

        private int? jadwaldesangdiedit = null;
        public UserControl3()
        {
            InitializeComponent();
        }

        private void UserControl3_Load(object sender, EventArgs e)
        {
                jadwalPenerbanganBindingSource.DataSource = db.JadwalPenerbangan.ToList();
            bandaraBindingSource.DataSource = db.Bandara.ToList();
            bandaraBindingSource1.DataSource = db.Bandara.ToList();
            maskapaiBindingSource.DataSource = db.Maskapai.ToList();

            numericUpDown1.Maximum = 999999999;
            numericUpDown1.Minimum = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is JadwalPenerbangan jdwlPenerbangan)
            {
                if (Bandara.Index == e.ColumnIndex)
                {
                    e.Value = jdwlPenerbangan.Bandara.Nama;
                }

                if (bandara1.Index == e.ColumnIndex)
                {
                    e.Value = jdwlPenerbangan.Bandara1.Nama;
                }

                if (Maskapai.Index == e.ColumnIndex)
                {
                    e.Value = jdwlPenerbangan.Maskapai.Nama;
                }

                if (DurasiPenerbangan.Index == e.ColumnIndex)
                {
                    int totalMenit = jdwlPenerbangan.DurasiPenerbangan;
                    TimeSpan ts = TimeSpan.FromMinutes(totalMenit);
                    e.Value = $"{ts.Hours:00}:{ts.Minutes:00}";
                }

                if (hargapertiket.Index == e.ColumnIndex)
                {
                    e.Value = jdwlPenerbangan.HargaPerTiket.ToString("C", new CultureInfo("id-ID"));
                }

                if (Delete.Index == e.ColumnIndex)
                {
                    e.Value = "Hapus";
                }

                if (Edit.Index == e.ColumnIndex)
                {
                    e.Value = "Ubah";
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is JadwalPenerbangan jdwlPenerbangan)
            {
                if (Edit.Index == e.ColumnIndex)
                {
                    jadwaldesangdiedit = jdwlPenerbangan.ID;

                    maskedTextBox1.Text = jdwlPenerbangan.KodePenerbangan;

                    comboBox1.SelectedItem = jdwlPenerbangan.Bandara;
                    comboBox2.SelectedItem = jdwlPenerbangan.Bandara1;
                    comboBox3.SelectedItem = jdwlPenerbangan.Maskapai;

                    dateTimePicker1.Value = jdwlPenerbangan.TanggalWaktuKeberangkatan.Date;
                    maskedTextBox2.Text = jdwlPenerbangan.TanggalWaktuKeberangkatan.ToString("HH\\:mm");

                    TimeSpan durasi = TimeSpan.FromMinutes(jdwlPenerbangan.DurasiPenerbangan);
                    maskedTextBox3.Text = $"{durasi.Hours:00}:{durasi.Minutes:00}";

                    numericUpDown1.Value = (decimal)jdwlPenerbangan.HargaPerTiket;
                }

                if (Delete.Index == e.ColumnIndex)
                {
                    var pesan = MessageBox.Show("Apakah kamu yakin ingin menghapus data reservasi ini?", "Peringatan", MessageBoxButtons.YesNo);

                    if (pesan == DialogResult.Yes)
                    {
                        int jadwalidpenerbangan = jdwlPenerbangan.ID;
                        var jadwalpenerbangan = db.JadwalPenerbangan.SingleOrDefault(j => j.ID == jadwalidpenerbangan);

                        if (jadwalpenerbangan != null)
                        {
                            db.JadwalPenerbangan.Remove(jadwalpenerbangan);
                            db.SaveChanges();

                            MessageBox.Show("Data berhasil di hapus");

                            this.OnLoad(EventArgs.Empty);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            maskedTextBox1.Clear();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            maskedTextBox2.Clear();
            maskedTextBox3.Clear();
            numericUpDown1.Value = 0;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(maskedTextBox1.Text))
            {
                MessageBox.Show("Kode penerbangan wajib diisi.");
                return;
            }

            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Bandara asal, bandara tujuan, dan maskapai wajib dipilih.");
                return;
            }

            if (!TimeSpan.TryParseExact(maskedTextBox2.Text, "hh\\:mm",
                CultureInfo.InvariantCulture, out TimeSpan jamBerangkat))
            {
                MessageBox.Show("Format jam keberangkatan harus HH:MM, contoh 09:30");
                return;
            }

            var jam = maskedTextBox3.Text.Substring(0, 2);
            var menit = maskedTextBox3.Text.Substring(5,2);

            if (!TimeSpan.TryParseExact($"{jam}:{menit}", "hh\\:mm",
                CultureInfo.InvariantCulture, out TimeSpan durasiPenerbangan))
            {
                MessageBox.Show("Format durasi harus HH:MM, contoh 01:15");
                return;
            }

            DateTime tanggalBerangkat = dateTimePicker1.Value.Date + jamBerangkat;

            int totalMenitDurasi = (int)durasiPenerbangan.TotalMinutes;
            double harga = Convert.ToDouble(numericUpDown1.Value);

            var bandaraAsal = (Bandara)comboBox1.SelectedItem;
            var bandaraTujuan = (Bandara)comboBox2.SelectedItem;
            var maskapai = (Maskapai)comboBox3.SelectedItem;

            var jadwalBaru = new JadwalPenerbangan
            {
                KodePenerbangan = maskedTextBox1.Text.Trim(),
                BandaraKeberangkatanID = bandaraAsal.ID,
                BandaraTujuanID = bandaraTujuan.ID,
                MaskapaiID = maskapai.ID,
                TanggalWaktuKeberangkatan = tanggalBerangkat,
                DurasiPenerbangan = totalMenitDurasi,
                HargaPerTiket = harga
            };

            db.JadwalPenerbangan.Add(jadwalBaru);
            db.SaveChanges();

            MessageBox.Show("Data jadwal penerbangan berhasil disimpan.");

            jadwalPenerbanganBindingSource.DataSource = db.JadwalPenerbangan.ToList();

            this.OnLoad(EventArgs.Empty);

            button1_Click(null, null);

        }

    }
}

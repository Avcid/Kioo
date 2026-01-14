using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bandara
{
    public partial class UserControl1 : UserControl
    {
        BromoAirlinesEntities db = new BromoAirlinesEntities();
        private int IdBandara = 0;
        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            bandaraBindingSource.DataSource = db.Bandara.ToList();
            negaraBindingSource.DataSource = db.Negara.ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is Bandara bandr)
            {
                if (Edit.Index == e.ColumnIndex)
                {
                    var editBandara = db.Bandara.Where(res => res.ID == bandr.ID).FirstOrDefault();
                    textBox1.Text = editBandara.Nama;
                    textBox2.Text = editBandara.KodeIATA;
                    textBox3.Text = editBandara.Kota;
                    comboBox1.SelectedValue = editBandara.NegaraID;
                    numericUpDown1.Value = editBandara.JumlahTerminal;
                    richTextBox2.Text = editBandara.Alamat;
                }
                if (Delete.Index == e.ColumnIndex)
                {
                    var pesan = MessageBox.Show("Apakah kamu yakin ingin menghapus data reservasi ini?", "Peringatan", MessageBoxButtons.YesNo);
                    if (pesan != DialogResult.Yes) return;
                    if (pesan == DialogResult.Yes)
                    {
                        int bandaraIdToDelete = bandr.ID;

                        var bandaraToDelete = db.Bandara.FirstOrDefault(b => b.ID == bandaraIdToDelete);

                        if (bandaraToDelete != null)
                        {
                            db.Bandara.Remove(bandaraToDelete);
                            db.SaveChanges();

                            MessageBox.Show("Berhasil menghapus data!");

                            this.OnLoad(EventArgs.Empty);
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                if (dataGridView1.Rows[e.RowIndex].DataBoundItem is Bandara Ban)
                {
                    if (Neagara.Index == e.ColumnIndex)
                    {
                        e.Value = Ban.Negara.Nama;
                    }
                    if (Edit.Index == e.ColumnIndex)
                    {
                        e.Value = "Ubah";
                    }

                    if (Delete.Index == e.ColumnIndex)
                    {
                        e.Value = "Hapus";
                    }

                    if (dataGridView1.Columns[e.ColumnIndex].Name == "Negara")
                    {
                        e.Value = Ban.Negara.Nama;
                    }
                }

            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            richTextBox2.Clear();

            comboBox1.SelectedIndex = 0;
            numericUpDown1.Value = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Nama = textBox1.Text;
            string KodelATA = textBox2.Text;
            string Kota = textBox3.Text;
            int Negara = (int)comboBox1.SelectedValue;
            int JumlahBandara = (int)numericUpDown1.Value;

            if (IdBandara == 0)
            {
                Bandara tbhbandara = new Bandara()
                {
                    Nama = Nama,
                    KodeIATA = KodelATA,
                    Kota = Kota,
                    NegaraID = Negara,
                    JumlahTerminal = JumlahBandara,
                    Alamat = richTextBox2.Text,
                };

                db.Bandara.Add(tbhbandara);
                db.SaveChanges();

                MessageBox.Show("Data berhasil di tambah");

                this.OnLoad(EventArgs.Empty);
            }

        }
    }
}

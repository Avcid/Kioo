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
    public partial class UserControl5 : UserControl
    {
        BromoAirlinesEntities db = new BromoAirlinesEntities();

        private int? prbhnstatuspenerbangan = null;
        public UserControl5()
        {
            InitializeComponent();
        }

        private void UserControl5_Load(object sender, EventArgs e)
        {
            jadwalPenerbanganBindingSource.DataSource = db.JadwalPenerbangan.ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is JadwalPenerbangan perubahan)
            {
                if (Edit.Index == e.ColumnIndex)
                {
                    e.Value = "Ubah";
                }
                if (Delete.Index == e.ColumnIndex)
                {
                    e.Value = "Hapus";
                }
                if (Bandara.Index == e.ColumnIndex)
                {
                    e.Value = perubahan.Bandara.Nama;
                }
                if (Bandara1.Index == e.ColumnIndex)
                {
                    e.Value = perubahan.Bandara1.Nama;
                }
                if (Maskapai.Index == e.ColumnIndex)
                {
                    e.Value = perubahan.Maskapai.Nama;
                }
                if (DurasiPenerbangan.Index == e.ColumnIndex)
                {
                    int totamenit = perubahan.DurasiPenerbangan;
                    TimeSpan ts = TimeSpan.FromMinutes(totamenit);
                    e.Value = $"{ts.Hours:00}:{ts.Minutes:00}";
                }
                if (HargaPertiket.Index == e.ColumnIndex)
                {
                    e.Value = perubahan.HargaPerTiket.ToString("C", new CultureInfo("id-ID"));
                }
                if (tglwktKeberangkatan.Index == e.ColumnIndex)
                {
                    e.Value = perubahan.TanggalWaktuKeberangkatan.ToString("dd-MM-yyyy HH:MM");
                }
            }
        }
    }
}

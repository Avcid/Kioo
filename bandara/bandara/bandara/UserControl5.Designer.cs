namespace bandara
{
    partial class UserControl5
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kodePenerbanganDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Maskapai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bandara = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bandara1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BandaraKeberangkatanID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BandaraTujuanID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaskapaiID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tglwktKeberangkatan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DurasiPenerbangan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HargaPertiket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PerubahanStatuspenerbangan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransaksiHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.jadwalPenerbanganBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.perubahanStatusJadwalPenerbanganBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusPenerbanganBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.maskapaiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlEdit = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jadwalPenerbanganBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.perubahanStatusJadwalPenerbanganBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusPenerbanganBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maskapaiBindingSource)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.pnlEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(49, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(418, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ubah Status Penerbangan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(350, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Anda bisa mengubah status penerbangan di sini";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.kodePenerbanganDataGridViewTextBoxColumn,
            this.Maskapai,
            this.Bandara,
            this.Bandara1,
            this.BandaraKeberangkatanID,
            this.BandaraTujuanID,
            this.MaskapaiID,
            this.tglwktKeberangkatan,
            this.DurasiPenerbangan,
            this.HargaPertiket,
            this.PerubahanStatuspenerbangan,
            this.TransaksiHeader,
            this.Edit,
            this.Delete});
            this.dataGridView1.DataSource = this.jadwalPenerbanganBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(56, 96);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1418, 442);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.Visible = false;
            this.iDDataGridViewTextBoxColumn.Width = 150;
            // 
            // kodePenerbanganDataGridViewTextBoxColumn
            // 
            this.kodePenerbanganDataGridViewTextBoxColumn.DataPropertyName = "KodePenerbangan";
            this.kodePenerbanganDataGridViewTextBoxColumn.HeaderText = "KodePenerbangan";
            this.kodePenerbanganDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.kodePenerbanganDataGridViewTextBoxColumn.Name = "kodePenerbanganDataGridViewTextBoxColumn";
            this.kodePenerbanganDataGridViewTextBoxColumn.Width = 150;
            // 
            // Maskapai
            // 
            this.Maskapai.DataPropertyName = "Maskapai";
            this.Maskapai.HeaderText = "Maskapai";
            this.Maskapai.MinimumWidth = 8;
            this.Maskapai.Name = "Maskapai";
            this.Maskapai.Width = 150;
            // 
            // Bandara
            // 
            this.Bandara.DataPropertyName = "Bandara";
            this.Bandara.HeaderText = "BandaraKeberangkatan";
            this.Bandara.MinimumWidth = 8;
            this.Bandara.Name = "Bandara";
            this.Bandara.Width = 150;
            // 
            // Bandara1
            // 
            this.Bandara1.DataPropertyName = "Bandara1";
            this.Bandara1.HeaderText = "BandaraTujuan";
            this.Bandara1.MinimumWidth = 8;
            this.Bandara1.Name = "Bandara1";
            this.Bandara1.Width = 150;
            // 
            // BandaraKeberangkatanID
            // 
            this.BandaraKeberangkatanID.DataPropertyName = "BandaraKeberangkatanID";
            this.BandaraKeberangkatanID.HeaderText = "BandaraKeberangkatanID";
            this.BandaraKeberangkatanID.MinimumWidth = 8;
            this.BandaraKeberangkatanID.Name = "BandaraKeberangkatanID";
            this.BandaraKeberangkatanID.Visible = false;
            this.BandaraKeberangkatanID.Width = 150;
            // 
            // BandaraTujuanID
            // 
            this.BandaraTujuanID.DataPropertyName = "BandaraTujuanID";
            this.BandaraTujuanID.HeaderText = "BandaraTujuanID";
            this.BandaraTujuanID.MinimumWidth = 8;
            this.BandaraTujuanID.Name = "BandaraTujuanID";
            this.BandaraTujuanID.Visible = false;
            this.BandaraTujuanID.Width = 150;
            // 
            // MaskapaiID
            // 
            this.MaskapaiID.DataPropertyName = "MaskapaiID";
            this.MaskapaiID.HeaderText = "MaskapaiID";
            this.MaskapaiID.MinimumWidth = 8;
            this.MaskapaiID.Name = "MaskapaiID";
            this.MaskapaiID.Visible = false;
            this.MaskapaiID.Width = 150;
            // 
            // tglwktKeberangkatan
            // 
            this.tglwktKeberangkatan.DataPropertyName = "TanggalWaktuKeberangkatan";
            this.tglwktKeberangkatan.HeaderText = "TanggalWaktuKeberangkatan";
            this.tglwktKeberangkatan.MinimumWidth = 8;
            this.tglwktKeberangkatan.Name = "tglwktKeberangkatan";
            this.tglwktKeberangkatan.Width = 150;
            // 
            // DurasiPenerbangan
            // 
            this.DurasiPenerbangan.DataPropertyName = "DurasiPenerbangan";
            this.DurasiPenerbangan.HeaderText = "DurasiPenerbangan";
            this.DurasiPenerbangan.MinimumWidth = 8;
            this.DurasiPenerbangan.Name = "DurasiPenerbangan";
            this.DurasiPenerbangan.Width = 150;
            // 
            // HargaPertiket
            // 
            this.HargaPertiket.DataPropertyName = "HargaPerTiket";
            this.HargaPertiket.HeaderText = "HargaPerTiket";
            this.HargaPertiket.MinimumWidth = 8;
            this.HargaPertiket.Name = "HargaPertiket";
            this.HargaPertiket.Width = 150;
            // 
            // PerubahanStatuspenerbangan
            // 
            this.PerubahanStatuspenerbangan.DataPropertyName = "PerubahanStatusJadwalPenerbangan";
            this.PerubahanStatuspenerbangan.HeaderText = "PerubahanStatusJadwalPenerbangan";
            this.PerubahanStatuspenerbangan.MinimumWidth = 8;
            this.PerubahanStatuspenerbangan.Name = "PerubahanStatuspenerbangan";
            this.PerubahanStatuspenerbangan.Width = 150;
            // 
            // TransaksiHeader
            // 
            this.TransaksiHeader.DataPropertyName = "TransaksiHeader";
            this.TransaksiHeader.HeaderText = "TransaksiHeader";
            this.TransaksiHeader.MinimumWidth = 8;
            this.TransaksiHeader.Name = "TransaksiHeader";
            this.TransaksiHeader.Visible = false;
            this.TransaksiHeader.Width = 150;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.MinimumWidth = 8;
            this.Edit.Name = "Edit";
            this.Edit.Width = 150;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Delete";
            this.Delete.MinimumWidth = 8;
            this.Delete.Name = "Delete";
            this.Delete.Width = 150;
            // 
            // jadwalPenerbanganBindingSource
            // 
            this.jadwalPenerbanganBindingSource.DataSource = typeof(bandara.JadwalPenerbangan);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "JadwalPenerbangan";
            this.dataGridViewTextBoxColumn1.HeaderText = "JadwalPenerbangan";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "StatusPenerbangan";
            this.dataGridViewTextBoxColumn2.HeaderText = "StatusPenerbangan";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "StatusPenerbangan";
            this.dataGridViewTextBoxColumn3.HeaderText = "StatusPenerbangan";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Visible = false;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "JadwalPenerbangan";
            this.dataGridViewTextBoxColumn4.HeaderText = "JadwalPenerbangan";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Visible = false;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // perubahanStatusJadwalPenerbanganBindingSource
            // 
            this.perubahanStatusJadwalPenerbanganBindingSource.DataSource = typeof(bandara.PerubahanStatusJadwalPenerbangan);
            // 
            // statusPenerbanganBindingSource
            // 
            this.statusPenerbanganBindingSource.DataSource = typeof(bandara.StatusPenerbangan);
            // 
            // maskapaiBindingSource
            // 
            this.maskapaiBindingSource.DataSource = typeof(bandara.Maskapai);
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.statusPenerbanganBindingSource;
            this.comboBox1.DisplayMember = "Nama";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(72, 8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(226, 28);
            this.comboBox1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Status";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.maskedTextBox1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(518, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(519, 33);
            this.flowLayoutPanel1.TabIndex = 2;
            this.flowLayoutPanel1.Visible = false;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(173, 3);
            this.maskedTextBox1.Mask = "00j\\am00menit";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(256, 26);
            this.maskedTextBox1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Perkiraan durasi delay";
            // 
            // pnlEdit
            // 
            this.pnlEdit.Controls.Add(this.flowLayoutPanel1);
            this.pnlEdit.Controls.Add(this.label3);
            this.pnlEdit.Controls.Add(this.comboBox1);
            this.pnlEdit.Location = new System.Drawing.Point(60, 544);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(1414, 154);
            this.pnlEdit.TabIndex = 3;
            this.pnlEdit.Visible = false;
            // 
            // UserControl5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlEdit);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UserControl5";
            this.Size = new System.Drawing.Size(1771, 737);
            this.Load += new System.EventHandler(this.UserControl5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jadwalPenerbanganBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.perubahanStatusJadwalPenerbanganBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusPenerbanganBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maskapaiBindingSource)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource statusPenerbanganBindingSource;
        private System.Windows.Forms.BindingSource perubahanStatusJadwalPenerbanganBindingSource;
        private System.Windows.Forms.BindingSource maskapaiBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.BindingSource jadwalPenerbanganBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kodePenerbanganDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Maskapai;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bandara;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bandara1;
        private System.Windows.Forms.DataGridViewTextBoxColumn BandaraKeberangkatanID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BandaraTujuanID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaskapaiID;
        private System.Windows.Forms.DataGridViewTextBoxColumn tglwktKeberangkatan;
        private System.Windows.Forms.DataGridViewTextBoxColumn DurasiPenerbangan;
        private System.Windows.Forms.DataGridViewTextBoxColumn HargaPertiket;
        private System.Windows.Forms.DataGridViewTextBoxColumn PerubahanStatuspenerbangan;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransaksiHeader;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Panel pnlEdit;
    }
}

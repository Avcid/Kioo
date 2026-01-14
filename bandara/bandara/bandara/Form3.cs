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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel1Collapsed)
            {
                splitContainer1.Panel1Collapsed = false;
            }
            else
            {
                splitContainer1.Panel1Collapsed = true;
            }
        }

        void setDisplay(UserControl uc)
        {
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(uc);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            UserControl1 MasterBandara = new UserControl1();
            setDisplay(MasterBandara);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserControl2 Mastermaskapai = new UserControl2();
            setDisplay(Mastermaskapai);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UserControl3 MasterJadwalPenerbangan = new UserControl3();
            setDisplay(MasterJadwalPenerbangan);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UserControl4 MaterKodePromo = new UserControl4();
            setDisplay(MaterKodePromo);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UserControl5 Ubahstatuspenerbangan = new UserControl5();
            setDisplay(Ubahstatuspenerbangan);
        }
    }
}

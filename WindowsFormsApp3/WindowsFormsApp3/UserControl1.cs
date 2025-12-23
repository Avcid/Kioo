using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp3
{
    public partial class UserControl1 : UserControl
    {
        private MiniKasirEntities db = new MiniKasirEntities();

        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            label1.Text = UserSession.NamaLengkap;

            var top5item = db.DetailTransactions.Where
            (f => f.Transaction.Date.Month == DateTime.Now.Month).Select(g => new
            {
                item = g.Item.Name,
                quantity = g.CountItem
            }).Take(5).ToList();

            chart1.Controls.Clear();
            var top5chart = chart1.Series.Add("top5Item");
            top5chart.ChartType = SeriesChartType.Pie;
            top5chart.Points.DataBind(top5item, "item", "quantity", null);
            top5chart.IsValueShownAsLabel = true;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

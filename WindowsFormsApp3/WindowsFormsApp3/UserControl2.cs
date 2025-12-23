using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class UserControl2 : UserControl
    {
        // DTO khusus untuk tampilan DataGridView (bukan entity EF)
        private class ItemRow
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int ItemCount { get; set; }
            public string Category { get; set; }
        }

        private List<ItemRow> _allItems = new List<ItemRow>();

        public UserControl2()
        {
            InitializeComponent();
        }

        private void UserControl2_Load(object sender, EventArgs e)
        {
            LoadItems();

            // Bind ke grid via bindingSource
            itemBindingSource.DataSource = _allItems;
            dataGridView1.DataSource = itemBindingSource;

            // Optional: biar tidak muncul dialog error default dari DataGridView
            dataGridView1.DataError += (s, ev) => { ev.ThrowException = false; };
        }

        private void LoadItems()
        {
            using (var db = new MiniKasirEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                var rawItems = db.Items
                    .Select(i => new
                    {
                        i.ID,
                        i.Name,
                        i.Price,
                        i.ItemCount,
                        CategoryName = i.Category.Name
                    })
                    .ToList();

                _allItems = rawItems
                    .Select(i => new ItemRow
                    {
                        ID = i.ID,
                        Name = i.Name,
                        Price = (decimal)i.Price,
                        ItemCount = i.ItemCount,
                        Category = i.CategoryName
                    })
                    .ToList();

                categoryBindingSource.DataSource = db.Categories.ToList();
            }
        }


        // SEARCH by NAMA
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string q = (textBox1.Text ?? "").Trim().ToLower();

            if (string.IsNullOrEmpty(q))
            {
                itemBindingSource.DataSource = _allItems;
                return;
            }

            var filtered = _allItems
                .Where(i => !string.IsNullOrEmpty(i.Name) && i.Name.ToLower().Contains(q))
                .ToList();

            itemBindingSource.DataSource = filtered;
        }

        // Format kolom Price jadi Rupiah
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Price" ||
                dataGridView1.Columns[e.ColumnIndex].HeaderText.Contains("Price"))
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    double val = Convert.ToDouble(e.Value);
                    e.Value = val.ToString("C0", new System.Globalization.CultureInfo("id-ID"));
                    e.FormattingApplied = true;
                }
            }
        }
    }
}

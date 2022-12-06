using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LearningWinFormsApp2
{

    public partial class ResultForm : Form
    {
        public ResultForm(List<List<string>> dataList)
        {
            InitializeComponent();

            LoadInitialData(dataList);

            LoadParsedData(dataList);

        }

        void LoadInitialData(List<List<string>> dataList)
        {
            int maxCellCount = dataList.Max(dl => dl.Count);
            for (int i = 0; i < maxCellCount - 1; i++)
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());

            foreach (var data in dataList)
                dataGridView1.Rows.Add(data.ToArray());
        }

        void LoadParsedData(List<List<string>> dataList)
        {
            var priceItems = PriceListReader.Read(dataList);

            int countOfProperties = typeof(PriceItem).GetProperties().Length;

            for (int i = 0; i < countOfProperties - 1; i++)
                dataGridView2.Columns.Add(new DataGridViewTextBoxColumn());

            foreach (var p in priceItems)
            {
                dataGridView2.Rows.Add(new string[] {
                    p.Id,
                    p.Price == null ? string.Empty : p.Price.Value.ToString(),
                    p.Name,
                    p.Stock ? "есть" : "нет"
                });
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectRowOnDatagrid1(e.RowIndex);
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Down) || e.KeyCode.Equals(Keys.Up))
            {
                SelectRowOnDatagrid1(dataGridView2.SelectedCells[0].RowIndex);
            }

            e.Handled = true;
        }

        private void SelectRowOnDatagrid1(int dataGridView2_RowIndex)
        {
            dataGridView1.MultiSelect = false;
            dataGridView2.Rows[dataGridView2_RowIndex].Selected = true;

            if (dataGridView2_RowIndex >= 0)
            {
                var row = dataGridView2.Rows[dataGridView2_RowIndex];
                string id = row.Cells[0].Value.ToString();

                int dataGridView1_rowIndex = -1;

                DataGridViewRow seekingRow = dataGridView1.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells[2].Value.ToString().Equals(id))
                    .First();

                dataGridView1_rowIndex = seekingRow.Index;

                dataGridView1.Rows[dataGridView1_rowIndex].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView2.SelectedRows[0].Index;
            }
        }
    }
}

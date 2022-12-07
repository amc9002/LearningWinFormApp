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

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            SelectRowOnDatagrid1(dataGridView2.CurrentRow.Cells[0].Value.ToString());
        }

        private void SelectRowOnDatagrid1(string id)
        {
            int dataGridView1_rowIndex = -1;

            DataGridViewRow seekingRow = dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells[2].Value.ToString().Equals(id))
                .First();

            dataGridView1_rowIndex = seekingRow.Index;

            dataGridView1.Rows[dataGridView1_rowIndex].Selected = true;
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
        }
    }
}
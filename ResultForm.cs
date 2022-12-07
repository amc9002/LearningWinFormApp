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

            foreach (var p in priceItems)
            {
                DataGridViewRow row = new();
                row.CreateCells(dataGridView2);

                row.Cells[0].Value = p.Id;
                row.Cells[1].Value = p.Price;
                row.Cells[2].Value = p.Name;
                row.Cells[3].Value = p.Stock ? "есть" : "нет";

                row.Tag = p.NumberStringInitial;

                dataGridView2.Rows.Add(row);
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            SelectRowOnDatagrid1(Convert.ToInt32(dataGridView2.CurrentRow.Tag));
        }

        private void SelectRowOnDatagrid1(int index)
        {
            dataGridView1.Rows[index].Selected = true;
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
        }
    }
}
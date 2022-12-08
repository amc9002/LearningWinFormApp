using NPOI.SS.Formula.Functions;

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
        private const int ITEM_ID = 0;
        private const int ITEM_PRICE = 1;
        private const int ITEM_NAME = 2;
        private const int ITEM_STOCK = 3;

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

            for (int i = 0; i < dataList.Count; i++)
            {
                DataGridViewRow row = new();
                row.CreateCells(dataGridView1);

                for (int j = 0; j < dataList[i].Count; j++)
                {
                    row.Cells[j].Value = dataList[i][j];
                }
                row.Tag = i;

                dataGridView1.Rows.Add(row);
            }
        }

        void LoadParsedData(List<List<string>> dataList)
        {
            var priceItems = PriceListReader.Read(dataList);

            foreach (var p in priceItems)
            {
                DataGridViewRow row = new();
                row.CreateCells(dataGridView2);

                row.Cells[ITEM_ID].Value = p.Id;
                row.Cells[ITEM_PRICE].Value = p.Price;
                row.Cells[ITEM_NAME].Value = p.Name;
                row.Cells[ITEM_STOCK].Value = p.Stock ? "есть" : "нет";

                row.Tag = p.NumberStringInitial;

                dataGridView2.Rows.Add(row);
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            SelectRowOnDatagrid1ByTag(Convert.ToInt32(dataGridView2.CurrentRow.Tag));
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            SelectRowOnDatagrid1ByTag(Convert.ToInt32(dataGridView2.CurrentRow.Tag));
        }

        private void SelectRowOnDatagrid1ByTag(int tag)
        {
            var row = (dataGridView1.Rows.Cast<DataGridViewRow>()
                .First(r => Convert.ToInt32(r.Tag) == tag));

            row.Selected = true;
            dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
        }
    }
}
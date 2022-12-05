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

            string[] headerRow = { "Артикул", "Цена", "Наименование", "Наличие" };
            dataGridView2.Rows.Add(headerRow);

            foreach (var p in priceItems)
            {
                dataGridView2.Rows.Add(new string[] {
                    p.Id,
                    p.Price == null ? string.Empty : p.Price.Value.ToString(),
                    p.Name,
                    p.Stock ? "есть" : "нет"
                });
            }

            dataGridView2.Rows[0].Frozen = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

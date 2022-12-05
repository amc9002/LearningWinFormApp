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
            var priceListReader = new PriceListReader();
            var priceItems = priceListReader.Read(dataList);

            for (int i = 0; i < 3; i++)
                dataGridView2.Columns.Add(new DataGridViewTextBoxColumn());

            foreach (var p in priceItems)
            {
                List<string> priceItem = new();
                priceItem.Add(p.Id);
                priceItem.Add(p.Name);

                string price = String.Empty;
                if (p.Price != null) price = p.Price.ToString();
                priceItem.Add(price);

                string stock = "есть";
                if (!p.Stock) stock = "нет";
                priceItem.Add(stock);

                dataGridView2.Rows.Add(priceItem.ToArray());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

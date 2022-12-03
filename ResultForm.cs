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

            LoadData(dataList);
        }

        void LoadData(List<List<string>> dataList)
        {
            for (int i = 0; i < dataList[0].Count - 1; i++)
            {
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }

            foreach (List<string> data in dataList)
                dataGridView1.Rows.Add(data.ToArray());

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

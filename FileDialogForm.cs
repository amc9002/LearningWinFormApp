using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using System.Data;
using System.IO;

namespace LearningWinFormsApp2
{
    public partial class FileDialogForm : Form
    {
        public FileDialogForm()
        {
            InitializeComponent();

            button1.Click += button1_Click;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excel files(*.xls; *.xlsx)|*.xls; *.xlsx";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string filename = openFileDialog1.FileName;

            List<List<string>> dataList = new();
            ISheet sheet;
            IWorkbook? workbook = null;
            using var stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            {
                stream.Position = 0;

                if (filename.IndexOf(".xlsx") > 0)
                    workbook = new XSSFWorkbook(stream);
 
                else if (filename.IndexOf(".xls") > 0)
                    workbook = new HSSFWorkbook(stream);

                if (workbook == null) return;

                sheet = workbook.GetSheetAt(0);
                IRow headerRow = sheet.GetRow(0);

                int cellCount = headerRow.LastCellNum;

                List<string> headerdataList = new();

                for (int j = 0; j < cellCount; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    
                    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString()))
                    {
                        headerdataList.Add(string.Empty);
                        continue;
                    }

                    headerdataList.Add(cell.ToString());
                }

                dataList.Add(headerdataList);

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    List<string> ordinaryRowList = new();
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (string.IsNullOrEmpty(row.GetCell(j).ToString()) ||
                            string.IsNullOrWhiteSpace(row.GetCell(j).ToString()))
                        {
                            ordinaryRowList.Add(string.Empty);
                            continue;
                        }
                        ordinaryRowList.Add(row.GetCell(j).ToString());

                    }

                    dataList.Add(ordinaryRowList);
                }

            }

            var newResultForm = new ResultForm(dataList);
            newResultForm.Show();

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
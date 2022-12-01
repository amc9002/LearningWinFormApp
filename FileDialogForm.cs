using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using System.Data;

namespace LearningWinFormsApp2
{
    public partial class FileDialogForm : Form
    {
        public FileDialogForm()
        {
            InitializeComponent();
            ShowDialog();
            button1.Click += button1_Click;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<List<string>> dataList = new();
            ISheet sheet;
            using var stream = new FileStream("TestData.xlsx", FileMode.Open);
            {
                stream.Position = 0;
                var xssWorkbook = new XSSFWorkbook(stream);
                sheet = xssWorkbook.GetSheetAt(0);
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


            var newResultForm = new ResultForm();
            newResultForm.Show();

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
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

            int maxCellCount = 1;

            try
            {
                using var stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                {
                    stream.Position = 0;

                    if (filename.IndexOf(".xlsx") > 0)
                        workbook = new XSSFWorkbook(stream);

                    else if (filename.IndexOf(".xls") > 0)
                        workbook = new HSSFWorkbook(stream);

                    if (workbook == null) return;

                    workbook.MissingCellPolicy = MissingCellPolicy.CREATE_NULL_AS_BLANK;
                    sheet = workbook.GetSheetAt(0);

                    for (int i = (sheet.FirstRowNum) - 1; i <= sheet.LastRowNum; i++)
                    {
                        List<string> rowList = new();
                        IRow row = sheet.GetRow(i);

                        if (row == null)
                        {
                            var emptyRow = new List<string>{ string.Empty, string.Empty, string.Empty };
                            rowList.AddRange(emptyRow);
                            dataList.Add(rowList);
                            continue;
                        }

                        int cellCount = row.LastCellNum;
                        if (cellCount > maxCellCount)
                            maxCellCount = cellCount;

                        for (int j = 0; j < cellCount; j++)
                        {
                            ICell cell = row.GetCell(j);

                            if (j == 0) cell.SetCellType(NPOI.SS.UserModel.CellType.String); //if the cell contains formula

                            rowList.Add(cell.ToString());
                        }

                        dataList.Add(rowList);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Exception: ", ex.Message);
                return;
            }

            var newResultForm = new ResultForm(dataList);
            newResultForm.Show();

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
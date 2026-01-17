using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Data.OleDb;
using System.Windows.Forms;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace PrintControl
{
    public static class ExportToExcel
    {
        private static List<string> SelectedColumns = new List<string>();   // Các cột được chọn để in
        private static List<string> AvailableColumns = new List<string>();  // Tất cả các cột

        public static bool DisplayPrintOption = false;
        public static string PrintTitle = "";  // Tiêu đề báo cáo
        public static string PrintTitleDetail1 = ""; // Tiêu đề phụ
        public static string PrintTitleDetail2 = ""; // Tiêu đề phụ
        public static string CompanyName = ""; // Tên công ty được in ở phần đầu của mỗi báo cáo
        public static string CompanyAddress = ""; // Địa chỉ công ty

        public static bool PrintNotice = false;
        public static List<string> Notices = new List<string>();
        public static bool PrintDateTime = false;
        private static string dateTimeString = "Ngày .... tháng .... năm ....";
        private static string personCheck = "Người kiểm tra";
        private static string personReview = "Người duyệt";

        public static int HeaderHeight = 40;
        public static bool IsLanscape = true;
        public static int ZoomSize = 100;

        public static bool FastExport = true;

        // Tắt chương trình excel
        private static void KillExcel()
        {
            try
            {
                Process[] ps = Process.GetProcesses();
                foreach (Process p in ps)
                {
                    if (p.ProcessName.ToLower().Equals("excel"))
                    {
                        p.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR " + ex.Message);
            }
        }

        // Kết xuất dataGridView to excel file
        public static void Export(DataGridView dataGridView)
        {
            // Đảm bảo không có chương trình excel nào đang chạy
            //KillExcel();

            // Save old culture
            System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;

            // Set culture to en-US
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            try
            {
                // Khởi động Excel
                COMExcel.Application exApp = new COMExcel.Application();

                // Thêm file temp excel
                COMExcel.Workbook exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);

                // Thay vì tạo 1 file temp excel thì cũng có thể mở 1 file excel có sẵn
                //string workbookPath = "c:/SomeWorkBook.xls";
                //COMExcel.Workbook exBook = exApp.Workbooks.Open(workbookPath, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

                // Lấy sheet 1
                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exBook.Worksheets[1];

                COMExcel.Range range = (COMExcel.Range)exSheet.Cells[1, 1];
                
                exSheet.PageSetup.TopMargin = 25;
                exSheet.PageSetup.LeftMargin = 10;
                exSheet.PageSetup.RightMargin = 10;
                exSheet.PageSetup.BottomMargin = 30;

                exSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
                exSheet.PageSetup.CenterHorizontally = true;
                exSheet.PageSetup.CenterFooter = "Page &P of &N";
                exSheet.PageSetup.Zoom = ZoomSize;

                if(IsLanscape)
                    exSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
                else
                    exSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;

                // Lấy tất cả các cột của dataGridView
                // chỉ lấy các cột được hiển thị
                AvailableColumns.Clear();
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    if (!column.Visible) continue;
                    AvailableColumns.Add(column.HeaderText.Replace('\n', ' '));
                }

                // Hiển thị bảng chọn các cột để in
                if (DisplayPrintOption)
                {
                    PrintOptions dlg = new PrintOptions(AvailableColumns);
                    //dlg.HidePageSettingOption = true;
                    if (dlg.ShowDialog() != DialogResult.OK) return;

                    SelectedColumns = dlg.GetSelectedColumns(); // Lấy các cột được chọn
                }
                else
                {
                    SelectedColumns = AvailableColumns;
                }

                Splash.Show();

                int columnIndex = 1;
                // In tiêu đề của các cột
                // tiêu đề các cột bắt đầu từ hàng thứ 7,
                // 6 hàng trên cùng để dành cho tiêu đề báo báo
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    if (column.Visible && SelectedColumns.Contains(column.HeaderText.Replace('\n', ' ')))// Chỉ export các cột được hiển thị
                    {
                        range = (COMExcel.Range)exSheet.Cells[7, columnIndex];

                        range.RowHeight = HeaderHeight;
                        range.VerticalAlignment = COMExcel.XlVAlign.xlVAlignCenter;
                        range.Font.Name = dataGridView.ColumnHeadersDefaultCellStyle.Font.Name;
                        range.Font.Bold = true;
                        range.Font.Size = 8;//dataGridView.ColumnHeadersDefaultCellStyle.Font.Size;
                        // Đặt lại chiều rộng của cột
                        //range.ColumnWidth = column.Width / dataGridView.Font.SizeInPoints;
                        // Vẽ boder của cell
                        if (!FastExport)
                        {
                            // Back Color. Cell tiêu đề cột được bôi đen
                            range.Interior.ColorIndex = 15;
                            range.Borders.Weight = COMExcel.XlBorderWeight.xlThin;
                            range.Borders.LineStyle = COMExcel.XlLineStyle.xlContinuous;
                            range.Borders.ColorIndex = COMExcel.XlColorIndex.xlColorIndexAutomatic;
                        }
                        // 
                        range.Value2 = "'" + column.HeaderText;
                        columnIndex++;
                    }
                }

                // In Tên công ty, địa chỉ, tiêu đề báo cáo (tu hang 1 den hang 6)
                for (int i = 1; i <= 6; i++)
                {
                    range = exSheet.get_Range(exSheet.Cells[i, 1], exSheet.Cells[i, columnIndex - 1]);
                    range.Merge(true);
                    range.Font.Name = dataGridView.Font.Name;

                    if (i == 1 && CompanyName != "") // Tên công ty
                    {
                        range.Font.Bold = false;
                        range.Font.Size = 10;
                        range.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignLeft;
                        range.Value2 = CompanyName;
                    }
                    if (i == 2 && CompanyAddress != "") // Địa chỉ công ty
                    {
                        range.Font.Bold = false;
                        range.Font.Size = 10;
                        range.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignLeft;
                        range.Value2 = CompanyAddress;
                    }
                    if (i == 3) // Tiêu đề báo cáo
                    {
                        range.Font.Bold = true;
                        range.Font.Size = 14;
                        range.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                        range.Value2 = PrintTitle;
                    }
                    if (i == 4) // Tiêu đề phụ 1
                    {
                        range.Font.Bold = false;
                        range.Font.Size = 10;
                        range.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignLeft;
                        range.Value2 = PrintTitleDetail1;
                    }
                    if (i == 5) // Tiêu đề phụ 2
                    {
                        range.Font.Bold = false;
                        range.Font.Size = 10;
                        range.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignLeft;
                        range.Value2 = PrintTitleDetail2;
                    }
                }

                string[,] cellValue  = new string[dataGridView.RowCount, columnIndex - 1];

                // Đưa dữ liệu vào các cell
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    columnIndex = 1;
                    foreach (DataGridViewColumn column in dataGridView.Columns)
                    {
                        if (column.Visible && SelectedColumns.Contains(column.HeaderText.Replace('\n', ' ')))
                        {
                            if (!FastExport)
                            {
                                range = (COMExcel.Range)exSheet.Cells[row.Index + 8, columnIndex];

                                range.Font.Name = dataGridView.DefaultCellStyle.Font.Name;
                                range.Font.Size = 8;//dataGridView.DefaultCellStyle.Font.Size;
                                // Back Color. Cell được bôi đen nếu cả dòng hoặc cả cột được bôi đen
                                if (row.DefaultCellStyle.BackColor == System.Drawing.Color.LightGray || column.DefaultCellStyle.BackColor == System.Drawing.Color.LightGray)
                                    range.Interior.ColorIndex = 15;
                                // Vẽ boder của cell
                                range.Borders.Weight = COMExcel.XlBorderWeight.xlThin;
                                range.Borders.LineStyle = COMExcel.XlLineStyle.xlContinuous;
                                range.Borders.ColorIndex = COMExcel.XlColorIndex.xlColorIndexAutomatic;
                                //
                                range.Value2 = "'" + row.Cells[column.Index].Value.ToString();
                            }
                            else
                            {
                                cellValue[row.Index, columnIndex - 1] = row.Cells[column.Index].Value.ToString();
                            }
                            columnIndex++;
                        }
                    }
                }

                if (FastExport)
                {
                    range = (COMExcel.Range)exSheet.get_Range(exSheet.Cells[8, 1], exSheet.Cells[dataGridView.RowCount - 1 + 8, columnIndex - 1]);
                    range.Font.Name = dataGridView.DefaultCellStyle.Font.Name;
                    range.Font.Size = 8;//dataGridView.DefaultCellStyle.Font.Size;
                    range.Value2 = cellValue;
                }

                if (PrintDateTime)
                {
                    range = exSheet.get_Range(exSheet.Cells[dataGridView.RowCount + 9, 24], exSheet.Cells[dataGridView.RowCount + 9, 30]);
                    if(!PrintNotice)
                        range = exSheet.get_Range(exSheet.Cells[dataGridView.RowCount + 9, 3], exSheet.Cells[dataGridView.RowCount + 9, 4]);
                    range.Merge(true);
                    range.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                    range.Font.Name = dataGridView.Font.Name;
                    range.Font.Italic = true;
                    range.Font.Size = 8;
                    range.Value2 = dateTimeString;
                    //
                    range = exSheet.get_Range(exSheet.Cells[dataGridView.RowCount + 10, 24], exSheet.Cells[dataGridView.RowCount + 10, 30]);
                    if (!PrintNotice)
                        range = exSheet.get_Range(exSheet.Cells[dataGridView.RowCount + 10, 3], exSheet.Cells[dataGridView.RowCount + 10, 4]);
                    range.Merge(true);
                    range.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                    range.Font.Name = dataGridView.Font.Name;
                    range.Font.Size = 8;
                    range.Value2 = personReview;

                    range = exSheet.get_Range(exSheet.Cells[dataGridView.RowCount + 9, 33], exSheet.Cells[dataGridView.RowCount + 9, 37]);
                    if (!PrintNotice)
                        range = exSheet.get_Range(exSheet.Cells[dataGridView.RowCount + 9, 6], exSheet.Cells[dataGridView.RowCount + 9, 8]);
                    range.Merge(true);
                    range.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                    range.Font.Name = dataGridView.Font.Name;
                    range.Font.Italic = true;
                    range.Font.Size = 8;
                    range.Value2 = dateTimeString;
                    //
                    range = exSheet.get_Range(exSheet.Cells[dataGridView.RowCount + 10, 33], exSheet.Cells[dataGridView.RowCount + 10, 37]);
                    if (!PrintNotice)
                        range = exSheet.get_Range(exSheet.Cells[dataGridView.RowCount + 10, 6], exSheet.Cells[dataGridView.RowCount + 10, 8]);
                    range.Merge(true);
                    range.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
                    range.Font.Name = dataGridView.Font.Name;
                    range.Font.Size = 8;
                    range.Value2 = personCheck;
                }

                if (PrintNotice)
                {
                    int i = 0;
                    int j = 0;
                    while (i < Notices.Count)
                    {
                        range = exSheet.get_Range(exSheet.Cells[dataGridView.RowCount + 9 + j, 1], exSheet.Cells[dataGridView.RowCount + 9 + j, 10]);
                        range.Merge(true);
                        range.Font.Name = dataGridView.Font.Name;
                        range.Font.Size = 8;
                        range.Value2 = Notices[i];
                        i++;

                        if (Notices.Count > 3)
                        {
                            if (i == Notices.Count)
                                break;
                            range = exSheet.get_Range(exSheet.Cells[dataGridView.RowCount + 9 + j, 11], exSheet.Cells[dataGridView.RowCount + 9 + j, 21]);
                            range.Merge(true);
                            range.Font.Name = dataGridView.Font.Name;
                            range.Font.Size = 8;
                            range.Value2 = Notices[i];
                            i++;
                        }

                        j++;
                    }
                }
                
                // Tự động đặt chiều rộng cột
                exSheet.Columns.AutoFit();
                // Đặt tên sheet
                //exSheet.Name = PrintTitle;
                //exSheet.Activate();

                Splash.Hide();

                // Hiện cửa sổ
                exApp.Visible = true;

                /*
                //Công việc cuối cùng là save file nếu như ko muốn hiện cửa sổ
                // Ẩn chương trình
                exApp.Visible = false;

                // Save file
                exBook.SaveAs("C:\\file.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                null, null, false, false,
                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                false, false, false, false, false);

                // Excel là một đối tượng dạng COM và ngoài sự quản lý của CLR nên tốt nhất cuối cùng nên khử đối tượng này.
                exBook.Close(false, false, false);
                exApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                 * */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                // Return current culture
                System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
            }
        }
    }
}

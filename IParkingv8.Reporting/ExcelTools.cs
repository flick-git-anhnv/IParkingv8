using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using Font = System.Drawing.Font;

namespace IParkingv8.Reporting
{
    public class ExcelTools
    {
        public static string preferPath = @"C:\";
        #region: Styling
        public static SLStyle CreateAlignCenterStyle(SLDocument sl)
        {
            var textCenterStyle = sl.CreateStyle();
            textCenterStyle.SetVerticalAlignment(VerticalAlignmentValues.Center);
            textCenterStyle.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
            return textCenterStyle;
        }
        public static SLStyle CreateTableHeaderStyle(SLDocument sl)
        {
            var tableHeaderStyle = sl.CreateStyle();
            tableHeaderStyle.Font.Bold = true;
            tableHeaderStyle.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
            tableHeaderStyle.SetVerticalAlignment(VerticalAlignmentValues.Center);
            return tableHeaderStyle;
        }

        public static SLStyle CreateAllBorderStyle(SLDocument sl)
        {
            var allBorderStyle = sl.CreateStyle();
            allBorderStyle.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
            allBorderStyle.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
            allBorderStyle.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
            allBorderStyle.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
            return allBorderStyle;
        }

        public static SLStyle CreateHeader1Style(SLDocument sl)
        {
            var header1Type = sl.CreateStyle();
            header1Type.SetFontBold(true);
            header1Type.Font.FontSize = 24;
            header1Type.Font.FontName = "Calibri";
            header1Type.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
            header1Type.SetVerticalAlignment(VerticalAlignmentValues.Center);
            return header1Type;
        }
        public static SLStyle CreateHeader2Style(SLDocument sl)
        {
            var header2Type = sl.CreateStyle();
            header2Type.SetFontBold(true);
            header2Type.Font.FontSize = 16;
            header2Type.Font.FontName = "Calibri";
            return header2Type;
        }

        public static SLStyle SetColorStyle(SLDocument sl, System.Drawing.Color color)
        {
            var style = sl.CreateStyle();
            style.Fill.SetPattern(PatternValues.Solid, color, color);
            return style;
        }
        #endregion: End Styling

        #region:Excel_Export
        public static void CreatReportFile(DataGridView dgvCard, string tittle, List<string> reserverData)
        {
            var sl = new SLDocument();
            //Create
            CreateTableHeader(sl, dgvCard, tittle);
            if (dgvCard.Rows.Count > 0)
            {
                SetCardTableContent(sl, dgvCard, reserverData);
            }
            //Save
            SaveReportFile(sl, tittle);
        }
        public static Size GetTextSize(string data, Font font)
        {
            return TextRenderer.MeasureText(data, font);
        }
        public static char NextCharacter(char input)
        {
            return input == 'z' ? 'a' : (char)(input + 1);
        }
        public static void CreateTableHeader(SLDocument sl, DataGridView dgvData, string tittle)
        {
            var currentChar = 'A';
            var columnIndex = 1;

            foreach (DataGridViewColumn column in dgvData.Columns)
            {
                var excelColumnName = currentChar.ToString() + "2";
                if (column.HeaderText.Trim() == "ID" || column.HeaderText.Trim() == "" ||
                    column.HeaderText.Trim() == "Code" || !column.Visible) continue;
                sl.SetCellValue(excelColumnName, column.HeaderText);
                var textSize = GetTextSize(column.HeaderText, dgvData.ColumnHeadersDefaultCellStyle.Font);
                var columnWidth = Math.Truncate(textSize.Width / 7d * 256) / 256;
                sl.SetColumnWidth(columnIndex, columnWidth);
                currentChar = NextCharacter(currentChar);

                //Set Style
                var style = sl.CreateStyle();
                style.Font.FontSize = dgvData.ColumnHeadersDefaultCellStyle.Font.Size;
                style.Font.Bold = dgvData.Columns[columnIndex - 1].DefaultCellStyle.Font is { Bold: true };
                HorizontalAlignmentValues horizontalAlignment;
                switch (dgvData.Columns[columnIndex - 1].DefaultCellStyle.Alignment)
                {
                    case DataGridViewContentAlignment.MiddleRight:
                    case DataGridViewContentAlignment.TopRight:
                    case DataGridViewContentAlignment.BottomRight:
                        horizontalAlignment = HorizontalAlignmentValues.Right;
                        break;
                    case DataGridViewContentAlignment.MiddleLeft:
                    case DataGridViewContentAlignment.TopLeft:
                    case DataGridViewContentAlignment.BottomLeft:
                        horizontalAlignment = HorizontalAlignmentValues.Left;
                        break;
                    case DataGridViewContentAlignment.MiddleCenter:
                    case DataGridViewContentAlignment.TopCenter:
                    case DataGridViewContentAlignment.BottomCenter:
                        horizontalAlignment = HorizontalAlignmentValues.Center;
                        break;
                    case DataGridViewContentAlignment.NotSet:
                    default:
                        horizontalAlignment = HorizontalAlignmentValues.Left;
                        break;
                }
                style.SetHorizontalAlignment(horizontalAlignment);
                style.SetVerticalAlignment(VerticalAlignmentValues.Center);
                sl.SetColumnStyle(columnIndex, style);
                columnIndex++;
            }
            var wrapStyle = sl.CreateStyle();
            wrapStyle.Font.FontSize = dgvData.ColumnHeadersDefaultCellStyle.Font.Size;
            wrapStyle.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
            wrapStyle.SetVerticalAlignment(VerticalAlignmentValues.Center);

            wrapStyle.SetWrapText(true);

            sl.SetCellStyle("A2", (char)(currentChar - 1) + "2", CreateTableHeaderStyle(sl));
            sl.SetCellStyle("A2", (char)(currentChar - 1) + "2", CreateAllBorderStyle(sl));
            sl.SetCellStyle("A2", (char)(currentChar - 1) + "2", wrapStyle);

            CreateReportTittle(sl, tittle, "A1", (char)(currentChar - 1) + "1");
        }

        public static void CreateReportTittle(SLDocument sl, string tittle, string startCell, string endCell)
        {
            sl.MergeWorksheetCells(startCell, endCell);
            sl.SetCellValue(startCell, tittle);
            sl.SetCellStyle(startCell, CreateHeader1Style(sl));
        }
        public static void SetCardTableContent(SLDocument sl, DataGridView dgvCard, List<string> additionalDatas)
        {
            var currentExcellCollumRow = 3;
            const char startChar = 'A';
            var currentChar = 'A';
            var columnIndex = 1;
            foreach (DataGridViewRow row in dgvCard.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    var cellHeaderText = cell.OwningColumn.HeaderText.Trim();
                    if (!cell.Visible)
                    {
                        continue;
                    }
                    if (cellHeaderText is "ID" or "" or "Code") continue;
                    var cellName = currentChar.ToString() + currentExcellCollumRow;
                    if (cell.Value != null)
                    {
                        sl.SetCellValue(cellName, cell.Value == null ? "" : cell.Value.ToString());
                        var textSize = GetTextSize(cell.Value == null ? "" : cell.Value.ToString() ?? "", dgvCard.ColumnHeadersDefaultCellStyle.Font);
                        var currentWidth = sl.GetColumnWidth(columnIndex);
                        var newWidth = Math.Truncate(textSize.Width / 7d * 256) / 256;
                        if (currentWidth < newWidth)
                            sl.SetColumnWidth(columnIndex, newWidth);
                    }
                    var cellColor = row.DefaultCellStyle.BackColor;
                    if (cellColor.Name != "0")
                    {
                        var rowStyle = SetColorStyle(sl, row.DefaultCellStyle.BackColor);
                        sl.SetCellStyle(cellName, rowStyle);
                    }

                    currentChar = (char)(currentChar + 1);
                    columnIndex++;
                }
                if (dgvCard.Rows.IndexOf(row) >= dgvCard.Rows.Count - 1) continue;
                columnIndex = 1;
                currentChar = startChar;
                currentExcellCollumRow++;
            }
            var lastCellName = ((char)(currentChar - 1)).ToString() + currentExcellCollumRow + "";

            sl.SetCellStyle("A" + 3, lastCellName, CreateAllBorderStyle(sl));
            currentExcellCollumRow++;

            for (int i = 0; i < additionalDatas.Count; i++)
            {
                currentChar = startChar;
                currentExcellCollumRow++;
                var cellName = currentChar.ToString() + currentExcellCollumRow;

                sl.SetCellValue(cellName, additionalDatas[i] == null ? "" : additionalDatas[i]);
            }
        }
        public static void SaveReportFile(SLDocument sl, string fileName)
        {
            try
            {
                var saveFile = new SaveFileDialog();
                saveFile.Filter = @"Data Files (*.xlsx)|*.xlsx";
                saveFile.DefaultExt = "xlsx";
                saveFile.AddExtension = true;
                saveFile.FileName = $"{fileName} {DateTime.Now:yyyy_MM_dd_HH_mm_ss}";
                saveFile.InitialDirectory = preferPath;
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    preferPath = Path.GetDirectoryName(saveFile.FileName);
                    sl.SaveAs(saveFile.FileName);
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion:End Excel_Export
    }
}

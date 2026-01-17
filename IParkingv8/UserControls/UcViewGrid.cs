using iParkingv8.Object.Enums.Bases;
using IParkingv8.UserControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kztek.Control8.UserControls
{
    public partial class UcViewGrid : UserControl
    {
        #region Properties
        public int RowsCount { get; set; } = 2;
        public int ColumnsCount { get; set; } = 2;

        private Point _startArrangeLocation = new(0, 0);
        private Point _endArrangeLocation = new(0, 0);

        public TableLayoutPanel? table;

        // Drag preview state
        private bool _isDragging;
        private Point _dragStartCell = new(-1, -1);   // X=col, Y=row
        private Point _dragHoverCell = new(-1, -1);   // X=col, Y=row
        #endregion

        #region Forms
        public UcViewGrid()
        {
            InitializeComponent();
        }
        #endregion

        #region Controls In Form
        private void Table_MouseDown(object? sender, MouseEventArgs e)
        {
            if (table == null) return;
            if (e.Button != MouseButtons.Left) return;

            Cursor = Cursors.NoMove2D;

            _startArrangeLocation = e.Location;

            _isDragging = true;
            _dragStartCell = GetRowColIndex(table, e.Location);
            _dragHoverCell = _dragStartCell;

            table.Invalidate();
        }

        private void Table_MouseMove(object? sender, MouseEventArgs e)
        {
            if (table == null) return;
            if (!_isDragging) return;

            var hover = GetRowColIndex(table, e.Location);
            if (hover != _dragHoverCell)
            {
                _dragHoverCell = hover;
                table.Invalidate(); // repaint preview
            }
        }

        private void Table_MouseLeave(object? sender, EventArgs e)
        {
            if (table == null) return;

            // Nếu đang drag mà ra khỏi vùng table: vẫn giữ trạng thái,
            // nhưng không đổi hover liên tục nữa.
            if (_isDragging)
            {
                table.Invalidate();
            }
        }

        private void Table_MouseUp(object? sender, MouseEventArgs e)
        {
            if (table == null) return;
            if (e.Button != MouseButtons.Left) return;

            Cursor = Cursors.Default;

            _endArrangeLocation = e.Location;

            // Kết thúc drag preview
            _isDragging = false;
            table.Invalidate();

            ArrageControl(_startArrangeLocation, _endArrangeLocation);

            // Reset cell state
            _dragStartCell = new(-1, -1);
            _dragHoverCell = new(-1, -1);
        }

        private void Table_Paint(object? sender, PaintEventArgs e)
        {
            if (table == null) return;
            if (!_isDragging) return;

            // Vẽ preview: ô bắt đầu + ô đang hover
            using var penStart = new Pen(Color.FromArgb(220, 0, 120, 215), 2);
            using var penHover = new Pen(Color.FromArgb(220, 255, 140, 0), 2);

            using var brushStart = new SolidBrush(Color.FromArgb(50, 0, 120, 215));
            using var brushHover = new SolidBrush(Color.FromArgb(50, 255, 140, 0));

            if (IsValidCell(table, _dragStartCell))
            {
                Rectangle r1 = GetCellRectangle(table, _dragStartCell.X, _dragStartCell.Y);
                e.Graphics.FillRectangle(brushStart, r1);
                e.Graphics.DrawRectangle(penStart, r1);
            }

            if (IsValidCell(table, _dragHoverCell))
            {
                Rectangle r2 = GetCellRectangle(table, _dragHoverCell.X, _dragHoverCell.Y);
                e.Graphics.FillRectangle(brushHover, r2);
                e.Graphics.DrawRectangle(penHover, r2);
            }
        }
        #endregion

        #region Public Function
        public void UpdateRowSetting(int row, int column)
        {
            RowsCount = row;
            ColumnsCount = column;

            if (table != null)
            {
                Controls.Remove(table);

                table.MouseDown -= Table_MouseDown;
                table.MouseMove -= Table_MouseMove;
                table.MouseUp -= Table_MouseUp;
                table.MouseLeave -= Table_MouseLeave;
                table.Paint -= Table_Paint;

                table.Dispose();
                table = null;
            }

            table = new BufferedTableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = column,
                RowCount = row,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

            table.ColumnStyles.Clear();
            table.RowStyles.Clear();

            float colPercent = 100f / Math.Max(1, column);
            float rowPercent = 100f / Math.Max(1, row);

            for (int i = 0; i < column; i++)
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, colPercent));

            for (int i = 0; i < row; i++)
                table.RowStyles.Add(new RowStyle(SizeType.Percent, rowPercent));

            table.MouseDown += Table_MouseDown;
            table.MouseMove += Table_MouseMove;
            table.MouseUp += Table_MouseUp;
            table.MouseLeave += Table_MouseLeave;
            table.Paint += Table_Paint;

            table.Cursor = Cursors.Hand;

            Controls.Add(table);
        }
        #endregion

        #region Private Function

        private static bool IsValidCell(TableLayoutPanel tlp, Point cell)
        {
            return cell.X >= 0 && cell.X < tlp.ColumnCount && cell.Y >= 0 && cell.Y < tlp.RowCount;
        }

        // Tính đúng index ô theo widths/heights thực tế (không giả định widths[0] == widths[i])
        private static Point GetRowColIndex(TableLayoutPanel tlp, Point point)
        {
            int x = Math.Max(0, Math.Min(point.X, tlp.Width - 1));
            int y = Math.Max(0, Math.Min(point.Y, tlp.Height - 1));

            int[] widths = tlp.GetColumnWidths();
            int[] heights = tlp.GetRowHeights();

            int col = 0;
            int acc = 0;
            for (int i = 0; i < widths.Length; i++)
            {
                acc += widths[i];
                if (x < acc)
                {
                    col = i;
                    break;
                }
                col = i;
            }

            int row = 0;
            acc = 0;
            for (int i = 0; i < heights.Length; i++)
            {
                acc += heights[i];
                if (y < acc)
                {
                    row = i;
                    break;
                }
                row = i;
            }

            return new Point(col, row);
        }

        private static Rectangle GetCellRectangle(TableLayoutPanel tlp, int col, int row)
        {
            int[] widths = tlp.GetColumnWidths();
            int[] heights = tlp.GetRowHeights();

            int x = 0;
            for (int c = 0; c < col; c++) x += widths[c];

            int y = 0;
            for (int r = 0; r < row; r++) y += heights[r];

            int w = widths[col];
            int h = heights[row];

            // Trừ nhẹ 1px để viền đẹp hơn
            return new Rectangle(x, y, Math.Max(0, w - 1), Math.Max(0, h - 1));
        }

        private void ArrageControl(Point startLocation, Point endLocation)
        {
            if (table == null) return;

            Point startCell = GetRowColIndex(table, startLocation);
            Point endCell = GetRowColIndex(table, endLocation);

            if (!IsValidCell(table, startCell) || !IsValidCell(table, endCell)) return;
            if (startCell == endCell) return;

            Control? startControl = table.GetControlFromPosition(startCell.X, startCell.Y);
            Control? endControl = table.GetControlFromPosition(endCell.X, endCell.Y);

            if (startControl == null || endControl == null) return;

            table.SuspendLayout();
            try
            {
                // Swap bằng SetCellPosition (ổn định hơn remove/add)
                table.SetCellPosition(startControl, new TableLayoutPanelCellPosition(endCell.X, endCell.Y));
                table.SetCellPosition(endControl, new TableLayoutPanelCellPosition(startCell.X, startCell.Y));

                UpdateLaneColumnOrRowPercents();
                table.Invalidate(true);
            }
            finally
            {
                table.ResumeLayout(true);
            }
        }

        /// <summary>
        /// Rule 40/60 theo Lane_IN hoặc Lane khác.
        /// Nếu có nhiều cột => set ColumnStyles theo từng cột (dựa control ở row 0).
        /// Nếu chỉ có 1 cột => set RowStyles theo từng hàng (dựa control ở col 0).
        /// </summary>
        private void UpdateLaneColumnOrRowPercents()
        {
            if (table == null) return;

            if (table.ColumnCount > 1)
            {
                float[] weights = new float[table.ColumnCount];
                float total = 0;

                for (int col = 0; col < table.ColumnCount; col++)
                {
                    Control? c = table.GetControlFromPosition(col, 0);
                    weights[col] = GetLaneWeight(c);
                    total += weights[col];
                }

                if (total <= 0) return;

                for (int col = 0; col < table.ColumnCount; col++)
                {
                    float percent = (weights[col] / total) * 100f;
                    table.ColumnStyles[col] = new ColumnStyle(SizeType.Percent, percent);
                }
            }
            else if (table.RowCount > 1)
            {
                float[] weights = new float[table.RowCount];
                float total = 0;

                for (int row = 0; row < table.RowCount; row++)
                {
                    Control? c = table.GetControlFromPosition(0, row);
                    weights[row] = GetLaneWeight(c);
                    total += weights[row];
                }

                if (total <= 0) return;

                for (int row = 0; row < table.RowCount; row++)
                {
                    float percent = (weights[row] / total) * 100f;
                    table.RowStyles[row] = new RowStyle(SizeType.Percent, percent);
                }
            }
        }

        private static float GetLaneWeight(Control? control)
        {
            // Default
            const float inWeight = 40f;
            const float outWeight = 60f;

            if (control is ILane lane)
            {
                return lane.Lane.Type == (int)EmLaneType.LANE_IN ? inWeight : outWeight;
            }

            return outWeight;
        }

        #endregion

        #region Helper class: reduce flicker
        private class BufferedTableLayoutPanel : TableLayoutPanel
        {
            public BufferedTableLayoutPanel()
            {
                DoubleBuffered = true;
                ResizeRedraw = true;
            }
        }
        #endregion
    }
}

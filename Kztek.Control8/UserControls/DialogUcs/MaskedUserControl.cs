namespace Kztek.Control8.UserControls.DialogUcs
{
    public partial class MaskedUserControl : Form
    {
        public UserControl? _trackedParentUserControl;
        public MaskedUserControl()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
            this.Opacity = 0.5;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
            this.TopMost = true;
            typeof(Control).InvokeMember("DoubleBuffered",
                                 System.Reflection.BindingFlags.SetProperty |
                                 System.Reflection.BindingFlags.Instance |
                                 System.Reflection.BindingFlags.NonPublic,
                                 null, this, new object[] { true });
        }

        public MaskedUserControl(UserControl parentToTrack) : this() // Gọi constructor mặc định để thiết lập style
        {
            _trackedParentUserControl = parentToTrack;
            if (_trackedParentUserControl != null)
            {
                // Đặt kích thước và vị trí ban đầu
                UpdateMaskPropertiesFromTrackedParent();

                // Đăng ký sự kiện để cập nhật theo Form cha
                _trackedParentUserControl.SizeChanged += Parent_SizeChanged;
                _trackedParentUserControl.LocationChanged += Parent_LocationChanged;
                _trackedParentUserControl.VisibleChanged += Parent_VisibleChanged; // Theo dõi cả trạng thái Visible
            }
        }

        private void UpdateMaskPropertiesFromTrackedParent()
        {
            if (_trackedParentUserControl != null &&
                _trackedParentUserControl.IsHandleCreated &&
                _trackedParentUserControl.Visible)
            {
                this.Size = _trackedParentUserControl.Size;
                this.Location = _trackedParentUserControl.PointToScreen(Point.Empty);
            }
            else
            {
                this.Visible = false;
            }
        }
        private void Parent_VisibleChanged(object? sender, EventArgs e)
        {
            UpdateMaskPropertiesFromTrackedParent();
        }
        private void Parent_LocationChanged(object? sender, EventArgs e)
        {
            if (_trackedParentUserControl != null) // Chỉ cập nhật nếu mask đang hiển thị
            {
                this.Location = _trackedParentUserControl.PointToScreen(Point.Empty);
                if (this.Visible)
                {
                    this.BringToFront();
                }
            }
        }
        private void Parent_SizeChanged(object? sender, EventArgs e)
        {
            if (_trackedParentUserControl != null) // Chỉ cập nhật nếu mask đang hiển thị
            {
                this.Location = _trackedParentUserControl.PointToScreen(Point.Empty);
                this.Size = _trackedParentUserControl.Size;
                if (this.Visible)
                {
                    this.BringToFront();
                }
            }
        }

        /// <summary>
        /// Hiển thị một dialog với một lớp phủ mờ (mask) phía sau.
        /// Mask sẽ được tạo mới và theo dõi kích thước/vị trí của parentFormToMask.
        /// </summary>
        public static async Task<Result?> ShowDialog<Request, Result>(
            Request request,
            UserControl parrentUserControl,
            IDialog<Request, Result> actualDialog)
            where Result : class
            where Request : class
        {
            // Đảm bảo các thao tác UI này chạy trên luồng UI
            if (parrentUserControl.InvokeRequired)
            {
                // Đây là cách gọi đệ quy để chuyển sang luồng UI.
                // Cần cẩn thận với kiểu trả về khi dùng Invoke cho hàm async.
                var tcs = new TaskCompletionSource<Result?>();
                parrentUserControl.Invoke(new Action(async () =>
                {
                    try
                    {
                        tcs.SetResult(await ShowDialog(request, parrentUserControl, actualDialog));
                    }
                    catch (Exception ex)
                    {
                        tcs.SetException(ex);
                    }
                }));
                return await tcs.Task;
            }

            Result? dialogResult = null;
            using (MaskedUserControl mask = new MaskedUserControl(parrentUserControl)) // Mask sẽ theo dõi parentFormToMask
            {
                mask.TopMost = true;
                mask.Show();
                dialogResult = await actualDialog.ShowDialog(request, mask);
            }
            return dialogResult;
        }

        /// <summary>
        /// Hiển thị một dialog với một lớp phủ mờ (mask) phía sau.
        /// Mask sẽ được tạo mới và theo dõi kích thước/vị trí của parentFormToMask.
        /// </summary>
        public static async Task<Result?> ShowKioskDialog<Request, Result>(
            Request request,
            UserControl parrentUserControl,
            IKioskNotifyDialog<Request, Result> actualDialog)
            where Result : class
            where Request : class
        {
            // Đảm bảo các thao tác UI này chạy trên luồng UI
            if (parrentUserControl.InvokeRequired)
            {
                // Đây là cách gọi đệ quy để chuyển sang luồng UI.
                // Cần cẩn thận với kiểu trả về khi dùng Invoke cho hàm async.
                var tcs = new TaskCompletionSource<Result?>();
                parrentUserControl.Invoke(new Action(async () =>
                {
                    try
                    {
                        tcs.SetResult(await ShowKioskDialog(request, parrentUserControl, actualDialog));
                    }
                    catch (Exception ex)
                    {
                        tcs.SetException(ex);
                    }
                }));
                return await tcs.Task;
            }

            Result? dialogResult = null;
            using (MaskedUserControl mask = new MaskedUserControl(parrentUserControl)) // Mask sẽ theo dõi parentFormToMask
            {
                mask.TopMost = true;
                mask.Show();
                dialogResult = await actualDialog.ShowDialog(request, mask);
            }
            return dialogResult;
        }

        // Phiên bản static ShowDialog thứ hai (nhận MaskedDialog đã có)
        // Phiên bản này ít phổ biến hơn nếu bạn muốn mask chỉ xuất hiện khi có dialog.
        // Nó hữu ích nếu bạn có một mask cố định và muốn tái sử dụng.
        // Cần đảm bảo 'maskedDialogInstance' được quản lý vòng đời (tạo, hiển thị, ẩn, dispose) đúng cách bởi người gọi.
        public static async Task<Result?> ShowDialog<Request, Result>(
            MaskedUserControl maskedDialogInstance,
            Request request,
            UserControl parrentUserControl,
            IDialog<Request, Result> actualDialog)
            where Result : class
            where Request : class
        {
            if (maskedDialogInstance.InvokeRequired)
            {
                var tcs = new TaskCompletionSource<Result?>();
                maskedDialogInstance.Invoke(new Action(async () =>
                {
                    try
                    {
                        tcs.SetResult(await ShowDialog(maskedDialogInstance, request, parrentUserControl, actualDialog));
                    }
                    catch (Exception ex)
                    {
                        tcs.SetException(ex);
                    }
                }));
                return await tcs.Task;
            }
            try
            {
                if (parrentUserControl.InvokeRequired)
                {
                    parrentUserControl.Invoke(new Action(() =>
                    {
                        maskedDialogInstance.Size = parrentUserControl.Size;
                        maskedDialogInstance.Location = parrentUserControl.PointToScreen(Point.Empty);
                    }));
                }
                else
                {
                    maskedDialogInstance.Size = parrentUserControl.Size;
                    maskedDialogInstance.Location = parrentUserControl.PointToScreen(Point.Empty);
                }
            }
            catch (Exception ex)
            {
            }

            // Task hiển thị dialog
            var dialogTask = actualDialog.ShowDialog(request, maskedDialogInstance);

            // Nếu dialog xong bình thường
            var result = await dialogTask;
            maskedDialogInstance.Hide();

            if (parrentUserControl.InvokeRequired)
            {
                parrentUserControl.Invoke(new Action(() =>
                {
                    parrentUserControl.FindForm()?.Activate();
                }));
            }
            else
            {
                parrentUserControl.FindForm()?.Activate();
            }
            return result;
        }
    }
}

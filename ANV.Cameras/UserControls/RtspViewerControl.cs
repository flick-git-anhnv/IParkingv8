public class RtspViewerControl : UserControl
{
    private readonly string _rtspUrl;
    private readonly PictureBox _displayBox;
    private VideoStreamDecoderIntptr _decoder;
    private Bitmap? _currentImage;
    private CancellationTokenSource? _cts;

    public RtspViewerControl(string rtspUrl)
    {
        _rtspUrl = rtspUrl;
        //_recognizer = new LicensePlateRecognizer("./tessdata", "eng");

        Width = 640;
        Height = 480;

        _displayBox = new PictureBox
        {
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.StretchImage,
            BackColor = Color.Black
        };
        Controls.Add(_displayBox);
    }

    public void Connect()
    {
        Disconnect();
        _cts = new CancellationTokenSource();
        Task.Run(() => RunStreamLoop(_cts.Token));
    }
    public void Disconnect()
    {
        if (_cts != null && !_cts.IsCancellationRequested)
        {
            _cts.Cancel();
            Thread.Sleep(500); // Chờ vòng lặp thoát hoàn toàn
        }
        _decoder?.Dispose();
        _decoder = null;

        if (_displayBox.Image != null)
        {
            _displayBox.Image.Dispose();
            _displayBox.Image = null;
            _displayBox.Refresh();
        }
        _currentImage?.Dispose();
        _currentImage = null;
    }

    public Bitmap? GetCurrentImage()
    {
        return _currentImage != null ? (Bitmap)_currentImage.Clone() : null;
    }
    private async Task RunStreamLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                _decoder?.Dispose();
                _decoder = new VideoStreamDecoderIntptr(_rtspUrl);
                if (!_decoder.Connect())
                {
                    await Task.Delay(1000);
                    continue;
                }
                DateTime lastFrameTime = DateTime.Now;

                while (!token.IsCancellationRequested)
                {
                    var bmp = _decoder.TryDecodeFrame(token, 5000);
                    if (bmp != null)
                    {
                        lastFrameTime = DateTime.Now;
                        _currentImage?.Dispose();
                        _currentImage = (Bitmap)bmp.Clone();

                        if (_displayBox.InvokeRequired)
                        {
                            _displayBox.Invoke(new Action(() =>
                            {
                                _displayBox.Image?.Dispose();
                                _displayBox.Image = (Bitmap)bmp.Clone();
                                _displayBox.Refresh();
                            }));
                        }
                        else
                        {
                            _displayBox.Image?.Dispose();
                            _displayBox.Image = (Bitmap)bmp.Clone();
                            _displayBox.Refresh();
                        }

                        bmp.Dispose();
                    }

                    if ((DateTime.Now - lastFrameTime).TotalSeconds > 5)
                        throw new Exception("Mất kết nối - không nhận được frame");

                    await Task.Delay(10, token);
                }
            }
            catch
            {
                if (_displayBox.InvokeRequired)
                {
                    _displayBox.Invoke(new Action(() =>
                    {
                        _displayBox.Image = null;
                        _displayBox.Refresh();
                    }));
                }
                else
                {
                    _displayBox.Image = null;
                    _displayBox.Refresh();
                }
                await Task.Delay(3000, token);
            }
        }
    }
}

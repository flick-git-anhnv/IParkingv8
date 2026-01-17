using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;

namespace Kztek.Cameras
{
    public class SnapshotSource
    {
        private string source;
        private string imagePath;
        private Image img;
        private string login;
        private string password;
        private IWebProxy proxy;
        private bool useSeparateConnectionGroup;
        private bool preventCaching = true;
        private int requestTimeout = 5000;
        private Thread thread;
        private ManualResetEvent stopEvent;
        public string Source
        {
            get
            {
                return this.source;
            }
            set
            {
                this.source = value;
            }
        }
        public string ImagePath
        {
            get
            {
                return this.imagePath;
            }
            set
            {
                this.imagePath = value;
            }
        }
        public Image Img
        {
            get { return img; }
        }
        public string Login
        {
            get
            {
                return this.login;
            }
            set
            {
                this.login = value;
            }
        }
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }
        public bool SeparateConnectionGroup
        {
            get
            {
                return this.useSeparateConnectionGroup;
            }
            set
            {
                this.useSeparateConnectionGroup = value;
            }
        }
        public bool PreventCaching
        {
            get
            {
                return this.preventCaching;
            }
            set
            {
                this.preventCaching = value;
            }
        }
        public int RequestTimeout
        {
            get
            {
                return this.requestTimeout;
            }
            set
            {
                this.requestTimeout = value;
            }
        }
        public bool Running
        {
            get
            {
                if (this.thread != null)
                {
                    if (!this.thread.Join(0))
                    {
                        return true;
                    }
                    this.Free();
                }
                return false;
            }
        }
        public SnapshotSource()
        {
        }
        public SnapshotSource(string source, string imagePath)
        {
            this.source = source;
            this.imagePath = imagePath;
        }
        public void Start_GetImage()
        {
            if (this.thread == null && !this.Running)
            {
                this.stopEvent = new ManualResetEvent(false);
                this.thread = new Thread(new ThreadStart(this.WorkerThread));
                this.thread.Name = this.source;
                this.thread.Start();
            }
        }
        public void SignalToStop()
        {
            if (this.thread != null)
            {
                this.stopEvent.Set();
            }
        }
        public void WaitForStop()
        {
            if (this.thread != null)
            {
                this.thread.Join();
                this.Free();
            }
        }
        public void Stop()
        {
            if (this.Running)
            {
                this.WaitForStop();
            }
        }
        private void Free()
        {
            this.thread = null;
            this.stopEvent.Close();
            this.stopEvent = null;
        }
        private void WorkerThread()
        {
            //byte[] buffer = new byte[524288];
            byte[] buffer = new byte[1048576];
            HttpWebRequest httpWebRequest = null;
            WebResponse webResponse = null;
            Stream stream = null;
            Random random = new Random((int)DateTime.Now.Ticks);
            while (!this.stopEvent.WaitOne(0, false))
            {
                int num = 0;
                try
                {
                    DateTime now = DateTime.Now;
                    if (!this.preventCaching)
                    {
                        httpWebRequest = (HttpWebRequest)WebRequest.Create(this.source);
                    }
                    else
                    {
                        httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Concat(new object[]
                        {
                            this.source,
                            (this.source.IndexOf('?') == -1) ? '?' : '&',
                            "fake=",
                            random.Next().ToString()
                        }));
                    }
                    if (this.proxy != null)
                    {
                        httpWebRequest.Proxy = this.proxy;
                    }
                    httpWebRequest.Timeout = this.requestTimeout;
                    if (this.login != null && this.password != null && this.login != string.Empty)
                    {
                        httpWebRequest.Credentials = new NetworkCredential(this.login, this.password);
                    }
                    if (this.useSeparateConnectionGroup)
                    {
                        httpWebRequest.ConnectionGroupName = this.GetHashCode().ToString();
                    }
                    webResponse = httpWebRequest.GetResponse();
                    stream = webResponse.GetResponseStream();
                    stream.ReadTimeout = this.requestTimeout;
                    while (!this.stopEvent.WaitOne(0, false))
                    {
                        if (num > 1048576)//if (num > 784896) //if (num > 523264)
                        {
                            num = 0;
                        }
                        int num2;
                        if ((num2 = stream.Read(buffer, num, 1024)) == 0)
                        {
                            break;
                        }
                        num += num2;
                    }
                    if (!this.stopEvent.WaitOne(0, false))
                    {
                        System.Threading.Thread.Sleep(100);
                        try
                        {
                            img = Image.FromStream(new MemoryStream(buffer, 0, num));
                        }
                        catch { }
                        System.Threading.Thread.Sleep(1);
                        if (img == null)
                        {
                            System.Threading.Thread.Sleep(100);
                            try
                            {
                                img = Image.FromStream(new MemoryStream(buffer, 0, num));
                            }
                            catch
                            {
                            }
                            System.Threading.Thread.Sleep(1);
                            if (img == null)
                            {
                                System.Threading.Thread.Sleep(200);
                                try
                                {
                                    img = Image.FromStream(new MemoryStream(buffer, 0, num));
                                }
                                catch
                                { }
                                System.Threading.Thread.Sleep(1);
                                if (img == null)
                                {
                                    System.Threading.Thread.Sleep(300);
                                    try
                                    {
                                        img = Image.FromStream(new MemoryStream(buffer, 0, num));
                                    }
                                    catch { }
                                    System.Threading.Thread.Sleep(1);
                                    if (img == null)
                                    {
                                        System.Threading.Thread.Sleep(400);
                                        try { img = Image.FromStream(new MemoryStream(buffer, 0, num)); }
                                        catch { }
                                        System.Threading.Thread.Sleep(1);
                                    }
                                }
                            }
                        }

                        if (imagePath != "")
                        {
                            if (img != null)
                            {
                                img.Save(imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                img.Dispose();
                            }                            
                        }                        
                        if (this.stopEvent.WaitOne(100, false))
                            break;
                    }
                }
                catch (ThreadAbortException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(10);
                }
                finally
                {
                    if (httpWebRequest != null)
                    {
                        httpWebRequest.Abort();
                        httpWebRequest = null;
                    }
                    if (stream != null)
                    {
                        stream.Close();
                        stream = null;
                    }
                    if (webResponse != null)
                    {
                        webResponse.Close();
                        webResponse = null;
                    }
                    this.stopEvent.Set();
                    GC.Collect();
                }
                if (this.stopEvent.WaitOne(0, false))
                {
                    break;
                }
            }
        }
    }
}

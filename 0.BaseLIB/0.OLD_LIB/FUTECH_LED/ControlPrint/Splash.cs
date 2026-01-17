using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PrintControl
{
    public class Splash
    {
        public static Thread thread = null;
        public static frmSplash frm = null;

        public static string Status
        {
            set 
            {
                if(frm != null)
                    frm.Status = value; 
            }
        }

        public static void Show()
        {
            Application.DoEvents();
            thread = new Thread(new ThreadStart(WorkerThread));
            //thread.IsBackground = true;
            thread.Start();
        }

        public static void WorkerThread()
        {
            frm = new frmSplash();
            Application.Run(frm);
        }

        public static void Hide()
        {
            Thread.Sleep(100);
            try
            {
                frm.Invoke(new MethodInvoker(frm.Close));
                frm.Close();
                frm = null;
                thread = null;
            }
            catch
            {
 
            }
        }
    }
}

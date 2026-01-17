using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using Helpers;
namespace CopyLib
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Helpers.Compression.ImprovedExtractToDirectory(Application.StartupPath + "\\IngressLib\\IngressLib.zip", Application.StartupPath, Compression.Overwrite.Always);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

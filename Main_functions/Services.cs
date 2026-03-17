using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main_functions
{
    public class Functions : Form
    {
        Thread th;
        public void Open(Form form)
        {
            Thread t = new Thread(() =>
            {
                Application.Run(form);
            });
            t.SetApartmentState(ApartmentState.STA); // WinForms requires STA threads
            t.Start();
        }

     
    }
}

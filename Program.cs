using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        static List<Form> forms = new List<Form>();

        static bool isClosing = true;
        public static void closeAll()
        {

            if (isClosing)
            {
                isClosing = false;
                foreach (var item in forms)
                {

                    item.Close();

                }
                isClosing = true;
            }
        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.SetData("DataDirectory", @"C:\ME\PROJECTS\WINDOWS_FORMS\App");
            forms = new List<Form>() { new loading(),  new Home(),new Intro() ,new Main_home()};
            Application.Run(forms.First());
        }
    }
}

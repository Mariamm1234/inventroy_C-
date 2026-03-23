using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Main_functions;
namespace App
{
    public partial class Intro : Form
    {
        
        Functions func;
        public Intro()
        {
            InitializeComponent();
            func = new Functions();
            this.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Functions.clicked_button = 0;
            func.Open(new Main_home());
        }
       

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Application.Exit();
        }
        private void Intro_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.closeAll();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Functions.clicked_button = 2;
            func.Open(new Main_home());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Functions.clicked_button = 1;
            func.Open(new Main_home());
           
        }
    }
}

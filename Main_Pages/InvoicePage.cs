using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Main_Pages
{
    public partial class InvoicePage : UserControl
    {
        public InvoicePage()
        {
            InitializeComponent();
            this.BackColor = Color.Aqua;

            Label lbl = new Label()
            {
                Text = "🏠 Invoice Page",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(50, 50)
            };

            this.Controls.Add(lbl);
        }
    }
}

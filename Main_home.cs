using App.Main_Pages;
using Main_functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class Main_home : Form
    {
        private TabControl tabControl;
        private Panel topNavbar;
        private Button toggleButton;
        private Timer navbarTimer;
        private bool isCollapsed = true;


        public Main_home()
        {
            InitializeComponent();
            this.Text = "الرئيسيه";
            this.Size = new Size(900, 500);
            this.Icon = Icon = new Icon("C:\\ME\\PROJECTS\\WINDOWS_FORM\\App\\Resources\\homefolderblank_99358.ico");

            // Create TabControl (hidden headers)
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            tabControl.Appearance = TabAppearance.FlatButtons;
            tabControl.ItemSize = new Size(0, 1);
            tabControl.SizeMode = TabSizeMode.Fixed;
           
            // Add pages
            tabControl.TabPages.Add(CreateTab(new ProductsPage()));
            tabControl.TabPages.Add(CreateTab(new InvoicePage()));
            tabControl.TabPages.Add(CreateTab(new ProfilePage()));

            // Create top navbar
            topNavbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40, // collapsed height
                BackColor = Color.FromArgb(45, 45, 48)
            };

            // Toggle button
            toggleButton = new Button
            {
                Text = "☰ القائمه",
                Font= new Font("Segoe UI", 13, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 40,
                FlatStyle = FlatStyle.Popup,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(28, 28, 28)
            };
            toggleButton.FlatAppearance.BorderSize = 0;
            toggleButton.Click += ToggleButton_Click;

            // Add nav buttons (hidden until expanded)
            topNavbar.Controls.Add(CreateNavButton("⚙ المستخدم", 2));
            topNavbar.Controls.Add(CreateNavButton("📊 الفواتير", 1));
            topNavbar.Controls.Add(CreateNavButton("🏠 المنتجات", 0));
            tabControl.SelectedIndex = Functions.clicked_button;

            topNavbar.Controls.Add(toggleButton);

            // Timer for animation
            navbarTimer = new Timer();
            navbarTimer.Interval = 10; // animation speed
            navbarTimer.Tick += NavbarTimer_Tick;

            // Add controls to form
            this.Controls.Add(tabControl);
            this.Controls.Add(topNavbar);
        }

        private TabPage CreateTab(UserControl page)
        {
            var tab = new TabPage();

            page.Dock = DockStyle.Fill;
            tab.Controls.Add(page);
            page.BringToFront();
            return tab;
        }

        private Button CreateNavButton(string text, int tabIndex)
        {
            var btn = new Button
            {
                Text = text,
                Font= new Font("Segoe UI", 15, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(45, 45, 48)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) => tabControl.SelectedIndex = tabIndex;
            return btn;
        }

        private void ToggleButton_Click(object sender, EventArgs e)
        {
            navbarTimer.Start();
        }

        private void NavbarTimer_Tick(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                topNavbar.Height += 10;
                if (topNavbar.Height >= 160) // expanded height
                {
                    navbarTimer.Stop();
                    isCollapsed = false;
                }
            }
            else
            {
                topNavbar.Height -= 10;
                if (topNavbar.Height <= 40) // collapsed height
                {
                    navbarTimer.Stop();
                    isCollapsed = true;
                }
            }
        }

    }
}

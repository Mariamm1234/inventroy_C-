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
    public partial class Home : Form
    {

        Panel sidebar;
        Button btnMenu;
        Button btnHome;
        Button btnDashboard;
        Button btnSettings;
        Panel contentPanel;
        Label contentText;
        Timer sidebarTimer;
        bool sidebarExpand = false;
        
        public Home()
        {
            InitializeComponent();
            this.Width = 900;
            this.Height = 600;
            this.Text = "الرئيسيه";

            CreateSidebar();
            CreateButtons();
            CreateTimer();

            CreateContentPanel();
            CreateContentText();
        }

        void CreateSidebar()
        {
            sidebar = new Panel();

            sidebar.Height = 100;           // Increase navbar height
            sidebar.Dock = DockStyle.Top;  // Place it on top
            sidebar.BackColor = Color.FromArgb(205, 205, 205);

            this.Controls.Add(sidebar);
        }

        void CreateButtons()
        {
            btnMenu = CreateNavButton("☰");
            btnMenu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMenu.Location = new Point(sidebar.Width - btnMenu.Width - 10, 15);
            btnMenu.Width = 50;
            btnMenu.Click += BtnMenu_Click;

            btnHome = CreateNavButton("🏠 الرئيسيه");
            btnHome.Location = new Point(80, 15);

            btnDashboard = CreateNavButton("📊 الفواتير");
            btnDashboard.Location = new Point(220, 15);

            btnSettings = CreateNavButton("⚙ المستخدم");
            btnSettings.Location = new Point(360, 15);

            sidebar.Controls.Add(btnMenu);
            sidebar.Controls.Add(btnHome);
            sidebar.Controls.Add(btnDashboard);
            sidebar.Controls.Add(btnSettings);
        }

        Button CreateNavButton(string text)
        {
            Button btn = new Button();

            btn.Text = text;
            btn.ForeColor = Color.White;
            btn.BackColor = Color.FromArgb(128, 128, 128);
            btn.Height = 70;
            btn.Width = 130;

            btn.FlatStyle = FlatStyle. Popup;
            btn.FlatAppearance.BorderSize = 0;

            return btn;
        }

        void CreateTimer()
        {
            sidebarTimer = new Timer();
            sidebarTimer.Interval = 10;
            sidebarTimer.Tick += SidebarTimer_Tick;
        }

        void CreateContentPanel()
        {
            contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.White;

            this.Controls.Add(contentPanel);
        }

        void CreateContentText()
        {
            contentText = new Label();

            contentText.Text = "Welcome to the Dashboard";
            contentText.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            contentText.AutoSize = true;

            contentText.Location = new Point(30, 200);

            contentPanel.Controls.Add(contentText);
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            //sidebarTimer.Start();
        }

        private void SidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Height -= 10;

                if (sidebar.Height <= 60)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                sidebar.Height += 10;

                if (sidebar.Height >= 200)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }


        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.closeAll();
          
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Application.Exit();
        }
    }
}

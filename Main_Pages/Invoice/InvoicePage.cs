using App.Main_Pages.Invoice;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace App.Main_Pages
{
    public partial class InvoicePage : UserControl
    {
        Button btn1, btn2;
        Panel sidebar, contentPanel, mainContainer;
        Timer animationTimer;
        CashPage cashPage;
        CreditPage creditPage;

        bool isExpanded = false;

        int sidebarTargetWidth = 150;
        int sidebarSpeed = 25;

        public InvoicePage()
        {
            InitializeComponent();
            InitUI();
        }

        void InitUI()
        {
            this.BackColor = Color.White;
            cashPage = new CashPage();
            creditPage = new CreditPage();

            cashPage.Dock = DockStyle.Fill;
            creditPage.Dock = DockStyle.Fill;


            // ✅ MAIN CONTAINER (fixes layout issue)
            mainContainer = new Panel();
            mainContainer.Dock = DockStyle.Fill;
            this.Controls.Add(mainContainer);

            // ✅ SIDEBAR (LEFT)
            sidebar = new Panel();
            sidebar.Width = 0;
            sidebar.Dock = DockStyle.Left;
            sidebar.BackColor = Color.FromArgb(30, 30, 30);

            // ✅ CONTENT PANEL (RIGHT)
            contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.WhiteSmoke;
            contentPanel.Padding = new Padding(20);


            //contentPanel.Controls.Add(cashPage);
            //contentPanel.Controls.Add(creditPage);

            // ✅ Add in correct order
            mainContainer.Controls.Add(contentPanel);
            mainContainer.Controls.Add(sidebar);

            // ✅ BUTTONS (center initially)
            btn1 = CreateButton("كاش");
            btn2 = CreateButton("أجل");

            this.Controls.Add(btn1);
            this.Controls.Add(btn2);

            btn1.BringToFront();
            btn2.BringToFront();

            // Wait until control loads
            this.Load += (s, e) => CenterButtons();

            btn1.Click += (s, e) => SelectOption(btn1, cashPage);
            btn2.Click += (s, e) => SelectOption(btn2, creditPage);

            // ✅ TIMER
            animationTimer = new Timer();
            animationTimer.Interval = 5;
            animationTimer.Tick += AnimateSidebar;
        }

        Button CreateButton(string text)
        {
            return new Button()
            {
                Text = text,
                Size = new Size(140, 45),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
        }

        void CenterButtons()
        {
            if (this.Width == 0 || this.Height == 0) return;

            int centerX = this.Width / 2;

            btn1.Location = new Point(centerX - 160, this.Height / 2 - 30);
            btn2.Location = new Point(centerX + 20, this.Height / 2 - 30);
        }

        void SelectOption(Button selectedBtn, UserControl page)
        {

            if (!isExpanded)
            {
                animationTimer.Start();
                isExpanded = true;
            }
            contentPanel.Controls.Add(page);
            HighlightButton(selectedBtn);
            //ShowContent(contentText);
            ShowPage(page);
        }

        void HighlightButton(Button btn)
        {
            btn1.BackColor = Color.SteelBlue;
            btn2.BackColor = Color.SteelBlue;

            btn.BackColor = Color.DarkOrange;
        }

        void ShowContent(string text)
        {
            contentPanel.Controls.Clear();

            Label lbl = new Label()
            {
                Text = text,
                AutoSize = true,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(20, 20)
            };

            contentPanel.Controls.Add(lbl);
        }


        void ShowPage(UserControl page)
        {
            foreach (Control ctrl in contentPanel.Controls)
                ctrl.Visible = false;

            page.Visible = true;
            page.BringToFront();
        }

        void AnimateSidebar(object sender, EventArgs e)
        {
            if (sidebar.Width < sidebarTargetWidth)
            {
                int remaining = sidebarTargetWidth - sidebar.Width;

                // ✅ Smooth animation
                int step = Math.Max(5, remaining / 2);

                sidebar.Width += step;

                btn1.Left -= step;
                btn2.Left -= step;
            }
            else
            {
                animationTimer.Stop();
                MoveButtonsToSidebar();
            }
        }

        void MoveButtonsToSidebar()
        {
            sidebar.Controls.Add(btn1);
            sidebar.Controls.Add(btn2);

            btn1.Location = new Point(5, 50);
            btn2.Location = new Point(5, 110);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (!isExpanded)
                CenterButtons();
        }
    }
}
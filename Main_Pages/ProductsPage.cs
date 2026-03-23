using App.Data_Model;
using Main_functions;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace App.Main_Pages
{
    public partial class ProductsPage : UserControl
    {

        Panel card;
        TextBox txt;
        ComboBox combo1, combo2, combo3;
        Button btnSubmit;
        Functions fn;
        private readonly IKingBoardRepository _repo;


        public ProductsPage()
        {
            fn = new Functions();
            _repo = new KingBoardRepository(new KingBoardDBEntities1());
            InitializeComponent();
            BuildModernUI();

        }

        private void BuildModernUI()
        {
            // Background
            this.BackColor = Color.FromArgb(240, 242, 245);


            // ===== Main Card (Full Screen) =====
            card = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(40)
            };

            // ===== Layout Panel =====
            TableLayoutPanel layout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 7,
                Padding = new Padding(200, 20, 200, 20)
            };



            layout.RowStyles.Clear();

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80)); // Title
            //layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70)); // Email
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70)); // Role
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70)); // Department
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            //layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70)); // Status
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20)); // Spacer row
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50)); // extra buttons 
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20)); // Spacer row
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50)); // Button (smaller height)



            // ===== Title =====
            Label title = new Label()
            {
                Text = "أداره المنتجات",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40)
            };

            // ===== Inputs =====
            //txtName = CreateModernTextBox("Name");
            txt = CreateModernTextBox("قيمه التعديل");

            combo1 = CreateModernComboBox(_repo.GetAllProducts().Select(p => p.ProductName).ToArray());
            combo2 = CreateModernComboBox(new string[] { "السعر", "الكميه" });
            //combo3 = CreateModernComboBox(new string[] { "Status", "Active", "Inactive" });

            // 👇 Hook the event here
            combo1.SelectedIndexChanged += Combo1_SelectedIndexChanged;
            combo2.SelectedIndexChanged += Combo2_SelectedIndexChanged;

            // Hide dependent controls initially
            txt.Visible = false;
            combo2.Visible = false;
            //combo3.Visible = false;

            // ===== Button =====
            btnSubmit = new Button()
            {
                Text = "تأكيد",
                Dock = DockStyle.None, // 👈 prevents stretching full width
                Size = new Size(150, 40), // 👈 smaller width & height
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Margin = new Padding(0)
            };


            btnSubmit.FlatAppearance.BorderSize = 0;

            // Hover effect
            btnSubmit.MouseEnter += (s, e) => btnSubmit.BackColor = Color.FromArgb(0, 100, 190);
            btnSubmit.MouseLeave += (s, e) => btnSubmit.BackColor = Color.FromArgb(0, 120, 215);


            // ===== Extra Buttons =====
            Button btnInfo = new Button()
            {
                Text = "➕ إضافه منتج",
                Dock = DockStyle.None,
                Size = new Size(150, 40),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 180, 120), // green
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Margin = new Padding(0, 15, 0, 10)
            };
            btnInfo.FlatAppearance.BorderSize = 0;
            btnInfo.Click += (s, e) => fn.ShowAddPopup("Add Item");

            // Second extra button
            Button btnWarning = new Button()
            {
                Text = "🗑 مسح منتج",
                Dock = DockStyle.None,
                Size = new Size(150, 40),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(220, 80, 60), // red
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Margin = new Padding(0, 15, 0, 10)
            };
            btnWarning.FlatAppearance.BorderSize = 0;
            btnWarning.Click += (s, e) => fn.ShowAddPopup("Delete Item");




            // ===== Extra Buttons Row =====
            FlowLayoutPanel extraButtonsPanel = new FlowLayoutPanel()
            {
                AutoSize = true,
                Dock = DockStyle.Fill,         // 👈 prevents stretching full width
                FlowDirection = FlowDirection.LeftToRight,
                Anchor = AnchorStyles.None,    // 👈 keeps centered in its row
                Padding = new Padding(0),
                Margin = new Padding(0),
                WrapContents = true,
            };


            btnInfo.Margin = new Padding(10, 0, 10, 0);   // space around Info
            btnWarning.Margin = new Padding(10, 0, 10, 0); // space around Warning

            extraButtonsPanel.Controls.Add(btnInfo);
            extraButtonsPanel.Controls.Add(btnWarning);


            // ===== Submit Button Row =====
            FlowLayoutPanel submitPanel = new FlowLayoutPanel()
            {
                AutoSize = true,
                Dock = DockStyle.None,
                FlowDirection = FlowDirection.LeftToRight,
                Anchor = AnchorStyles.None,
                WrapContents = true,
            };
            btnSubmit.Margin = new Padding(10, 0, 10, 0);
            submitPanel.Controls.Add(btnSubmit);


            // ===== Add controls to layout =====
            layout.Controls.Add(title, 0, 0);

            layout.Controls.Add(combo1, 0, 1);
            layout.Controls.Add(combo2, 0, 2);
            //layout.Controls.Add(combo3, 0, 3);

            layout.Controls.Add(txt, 0, 3);
            layout.Controls.Add(extraButtonsPanel, 0, 5);
            layout.Controls.Add(submitPanel, 0, 7);


            // ===== Add layout to card =====
            card.Controls.Add(layout);

            // ===== Add card to page =====
            this.Controls.Add(card);


        }

        // ===== Modern TextBox with Placeholder =====
        private TextBox CreateModernTextBox(string placeholder)
        {
            TextBox txt = new TextBox()
            {
                Text = placeholder,
                ForeColor = Color.Gray,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 10, 0, 10),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 13), // bigger text
                MinimumSize = new Size(0, 45), // 👈 control height
                TextAlign = HorizontalAlignment.Left,

            };

            txt.GotFocus += (s, e) =>
            {
                if (txt.Text == placeholder)
                {
                    txt.Text = "";
                    txt.ForeColor = Color.Black;
                }
            };

            txt.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    txt.Text = placeholder;
                    txt.ForeColor = Color.Gray;
                }
            };

            return txt;
        }

        // ===== Modern ComboBox =====
        private ComboBox CreateModernComboBox(string[] items)
        {
            ComboBox combo = new ComboBox()
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 10, 0, 10),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 13),
                MinimumSize = new Size(0, 70) // 👈 height
            };

            combo.Items.AddRange(items);
            combo.SelectedIndex = 0;

            return combo;
        }

        private void Combo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = combo1.SelectedItem.ToString();


            combo2.Visible = false;
            //combo3.Visible = false;

            // Show controls based on selection
            if (selected != null)
            {

                combo2.Visible = true;
                //combo3.Visible = true; // show department
            }

        }
        private void Combo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = combo2.SelectedItem.ToString();


            txt.Visible = false;
            //combo3.Visible = false;

            // Show controls based on selection
            if (selected != null)
            {

                txt.Visible = true;
                //combo3.Visible = true; // show department
            }

        }
    }
}
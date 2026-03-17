using System;
using System.Drawing;
using System.Windows.Forms;

namespace App.Main_Pages
{
    public partial class ProductsPage : UserControl
    {
        Panel card;
        TextBox txtName, txtEmail;
        ComboBox combo1, combo2, combo3;
        Button btnSubmit;

        public ProductsPage()
        {
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

            // Equal row spacing
            //for (int i = 0; i < 7; i++)
            //{
            //    layout.RowStyles.Add(new RowStyle(SizeType.Percent, 14f));
            //}

            layout.RowStyles.Clear();

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80)); // Title
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20)); // Button
            layout.Padding = new Padding(250, 30, 250, 30);

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
            txtName = CreateModernTextBox("Name");
            txtEmail = CreateModernTextBox("Email");

            combo1 = CreateModernComboBox(new string[] { "Role", "Admin", "User", "Guest" });
            combo2 = CreateModernComboBox(new string[] { "Department", "HR", "IT", "Sales" });
            combo3 = CreateModernComboBox(new string[] { "Status", "Active", "Inactive" });

            // ===== Button =====
            btnSubmit = new Button()
            {
                    Text = "Submit",
    Dock = DockStyle.Fill,
    MinimumSize = new Size(0, 10), // 👈 taller button
    FlatStyle = FlatStyle.Flat,
    BackColor = Color.FromArgb(0, 120, 215),
    ForeColor = Color.White,
    Font = new Font("Segoe UI", 13, FontStyle.Bold),
    Margin = new Padding(0, 15, 0, 10)
            };

            btnSubmit.FlatAppearance.BorderSize = 0;

            // Hover effect
            btnSubmit.MouseEnter += (s, e) => btnSubmit.BackColor = Color.FromArgb(0, 100, 190);
            btnSubmit.MouseLeave += (s, e) => btnSubmit.BackColor = Color.FromArgb(0, 120, 215);

            // ===== Add controls to layout =====
            layout.Controls.Add(title, 0, 0);
            layout.Controls.Add(txtName, 0, 1);
            layout.Controls.Add(txtEmail, 0, 2);
            layout.Controls.Add(combo1, 0, 3);
            layout.Controls.Add(combo2, 0, 4);
            layout.Controls.Add(combo3, 0, 5);
            layout.Controls.Add(btnSubmit, 0, 6);

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
            TextAlign=HorizontalAlignment.Left,
            
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
                MinimumSize = new Size(0, 50) // 👈 height
            };

            combo.Items.AddRange(items);
            combo.SelectedIndex = 0;

            return combo;
        }
    }
}
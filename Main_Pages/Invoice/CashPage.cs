using App.Data_Model;
using App.Main_functions;
using Main_functions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace App.Main_Pages.Invoice
{
    public partial class CashPage : UserControl
    {
        Panel card;
        Functions fn;

        FlowLayoutPanel selectedItemsPanel;

        Button btnRoundAdd;
        Button btnAddToInvoice;

        ComboBox comboItem;
        TextBox txtQty;

        List<ReceiptItem> invoiceItems;

        private readonly IKingBoardRepository _repo;


        public CashPage()
        {
            _repo = new KingBoardRepository(new KingBoardDBEntities1());
            fn = new Functions();
           invoiceItems = new List<ReceiptItem>();
            InitializeComponent();
            BuildModernUI();
        }

        private void BuildModernUI()
        {
            this.BackColor = Color.FromArgb(240, 242, 245);

            card = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(40)
            };

            TableLayoutPanel layout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                Padding = new Padding(350, 20, 350, 20)
            };

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));

            // ===== Title =====
            Label title = new Label()
            {
                Text = "أداره المنتجات",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 18, FontStyle.Bold)
            };

            // ===== Buttons Row =====

            btnRoundAdd = new Button()
            {
                Text = "+",
                Width = 45,
                Height = 45,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold)
            };

            btnRoundAdd.FlatAppearance.BorderSize = 0;
            btnRoundAdd.Click += BtnRoundAdd_Click;

            btnAddToInvoice = new Button()
            {
                Text = "إضافة للفاتورة",
                Height = 45,
                Width = 150,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(16, 172, 132),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            btnAddToInvoice.FlatAppearance.BorderSize = 0;
            btnAddToInvoice.Click += btnAddToInvoice_Click;

            FlowLayoutPanel topButtons = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft
            };

            topButtons.Controls.Add(btnRoundAdd);
            topButtons.Controls.Add(btnAddToInvoice);

            // ===== Dropdown =====

            comboItem = CreateModernComboBox(_repo.GetAllProducts().Select(p=>p.ProductName).ToArray());

            comboItem.Dock = DockStyle.Fill;
            comboItem.Visible = false;

            // ===== Quantity =====

            txtQty = CreateModernTextBox("الكمية");
            txtQty.Dock = DockStyle.Fill;
            txtQty.Visible = false;

            // ===== Items Panel =====

            selectedItemsPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };

            // ===== Add/Delete Buttons =====

            // ===== Extra Buttons =====
            Button btnInfo = new Button()
            {
                Text = "💾 حفظ",
                Dock = DockStyle.None,
                Size = new Size(150, 40),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 180, 120), // green
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Margin = new Padding(0, 15, 0, 10)
            };
            btnInfo.FlatAppearance.BorderSize = 0;
            btnInfo.Click += (s, e) =>
            {
                // Create a bitmap for the receipt
                fn.SearchClientPopup("البحث عن عميل");
                if (Functions.obj != null)
                {
                    Functions.recipies_num++;
                    string invNUM = $"INV-0000{Functions.recipies_num}";
                    fn.DrawReceipt(invoiceItems, invNUM,Functions.obj);
                    MessageBox.Show("تم حفظ الايصال ");
                }
            };


            // Second extra button
            Button btnPrint = new Button()
            {
                Text = "🖨 أطبع إيصال",
                Dock = DockStyle.None,
                Size = new Size(150, 40),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(220, 80, 60), // red
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Margin = new Padding(0, 15, 0, 10)
            };
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.Click += (s, e) =>
            {

                fn.PringRecepit();
                //MessageBox.Show($"جاري طباعه الايصال");
            };





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
            btnPrint.Margin = new Padding(10, 0, 10, 0); // space around Warning

            extraButtonsPanel.Controls.Add(btnInfo);
            extraButtonsPanel.Controls.Add(btnPrint);

            // ===== Submit =====

            Button btnSubmit = new Button()
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

            // ===== Add to layout =====

            layout.Controls.Add(title, 0, 0);
            layout.Controls.Add(topButtons, 0, 1);
            layout.Controls.Add(comboItem, 0, 2);
            layout.Controls.Add(txtQty, 0, 3);
            layout.Controls.Add(selectedItemsPanel, 0, 4);
            layout.Controls.Add(extraButtonsPanel, 0, 5);
            layout.Controls.Add(submitPanel, 0, 6);

            card.Controls.Add(layout);
            this.Controls.Add(card);
        }

        private void BtnRoundAdd_Click(object sender, EventArgs e)
        {
            comboItem.Visible = true;
            txtQty.Visible = true;

            comboItem.BringToFront();
            txtQty.BringToFront();
        }

        private void btnAddToInvoice_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtQty.Text))
                return;

            // create data item
            ReceiptItem dataItem = new ReceiptItem()
            {
                Name = comboItem.Text,
                Qty = int.Parse(txtQty.Text),
                Price = 0   // you can fill later if you add price textbox
            };

            // add to list
            invoiceItems.Add(dataItem);

            Panel item = new Panel()
            {
                Width = selectedItemsPanel.Width - 25,
                Height = 45,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 5, 0, 5),
                Tag = dataItem // IMPORTANT
            };

            Label lbl = new Label()
            {
                Text = $"{dataItem.Name} - {dataItem.Qty}",
                AutoSize = true,
                Location = new Point(10, 12),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            Button remove = new Button()
            {
                Text = "x",
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(30, 25),
                Location = new Point(item.Width - 40, 10)
            };

            remove.FlatAppearance.BorderSize = 0;

            remove.Click += (s, ev) =>
            {
                // remove from UI
                selectedItemsPanel.Controls.Remove(item);

                // remove from list
                invoiceItems.Remove((ReceiptItem)item.Tag);
            };

            item.Controls.Add(lbl);
            item.Controls.Add(remove);

            selectedItemsPanel.Controls.Add(item);

            comboItem.Visible = false;
            txtQty.Visible = false;
            txtQty.Text = "";
        }
        private TextBox CreateModernTextBox(string placeholder)
        {
            TextBox txt = new TextBox()
            {
                Text = placeholder,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 12),
                Height = 40
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

        private ComboBox CreateModernComboBox(string[] items)
        {
            ComboBox combo = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 12),
                Height = 40
            };

            combo.Items.AddRange(items);
            combo.SelectedIndex = 0;

            return combo;
        }
    }
}
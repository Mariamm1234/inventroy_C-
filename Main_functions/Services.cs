using App.Data_Model;
using App.Main_functions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
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
        public static int clicked_button = 0;
        public static int recipies_num = 0;
        private Bitmap _lastReceiptImage;

        private readonly IKingBoardRepository _repo;

        public static GetCustomer_Result obj;
        public Functions()
        {
            _repo = new KingBoardRepository(new KingBoardDBEntities1());
        }


        public void Open(Form form)
        {
            Thread t = new Thread(() =>
            {
                Application.Run(form);
            });
            t.SetApartmentState(ApartmentState.STA); // WinForms requires STA threads
            t.Start();
        }

        //Main_Drop down handler
        public void ShowAddPopup(string txt)
        {
            // Create popup form
            Form popup = new Form()
            {
                Text = txt,
                Size = new Size(400, 200),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.WhiteSmoke,
                FormBorderStyle = FormBorderStyle.FixedDialog, // 👈 fixed dialog style
                MaximizeBox = false,                           // 👈 no maximize
                MinimizeBox = false                            // 👈 no minimize
            };

            // Layout for dropdowns + button
            TableLayoutPanel layout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(20),
                AutoSize = true
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            // First dropdown
            ComboBox comboA = new ComboBox()
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 12),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            comboA.Items.AddRange(_repo.GetAllProducts().Select(p=>p.ProductName).ToArray());
            comboA.SelectedIndex = 0;

            // Second dropdown
            ComboBox comboB = new ComboBox()
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 12),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            comboB.Items.AddRange(_repo.GetUnits().ToArray());
            comboB.SelectedIndex = 0;

            if (txt.Split(' ')[0] == "Delete")
               txt = "🗑 مسح المنتج ";
            else
                txt = "➕ أضف المنتج ";


            // First textbox
            TextBox txtBox1 = new TextBox()
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Gray,
                Text = "السعر"
            };

            txtBox1.GotFocus += (s, e) =>
            {
                if (txtBox1.ForeColor == Color.Gray)
                {
                    txtBox1.Text = "";
                    txtBox1.ForeColor = Color.Black;
                }
            };

            txtBox1.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtBox1.Text))
                {
                    txtBox1.Text = "السعر";
                    txtBox1.ForeColor = Color.Gray;
                }
            };


            // Second textbox
            TextBox txtBox2 = new TextBox()
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Gray,
                Text = "الكميه"
            };

            txtBox2.GotFocus += (s, e) =>
            {
                if (txtBox2.ForeColor == Color.Gray)
                {
                    txtBox2.Text = "";
                    txtBox2.ForeColor = Color.Black;
                }
            };

            txtBox2.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtBox2.Text))
                {
                    txtBox2.Text = "الكميه";
                    txtBox2.ForeColor = Color.Gray;
                }
            };



            // Add button inside popup
            Button btnAdd = new Button()
            {

                Text = txt,
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            btnAdd.Click += (s, e) =>
            {
                MessageBox.Show($"Selected: {comboA.SelectedItem} + {comboB.SelectedItem}", "Added");
                popup.Close();
            };

            // Add controls to layout
            layout.Controls.Add(comboA, 0, 0);
            layout.Controls.Add(comboB, 1, 0);
            layout.Controls.Add(txtBox1, 2, 0);
            layout.Controls.Add(txtBox2, 3, 0);
            layout.Controls.Add(btnAdd, 4, 1);
            layout.SetColumnSpan(btnAdd, 2); // button spans both columns

            popup.Controls.Add(layout);

            popup.ShowDialog();
        }

        public void SearchClientPopup(string title)
        {
            // Create popup form
            Form popup = new Form()
            {
                Text = title,
                Size = new Size(400, 200),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.WhiteSmoke,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // Layout with 2 rows: textbox + button
            TableLayoutPanel layout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(20),
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70)); // text wider
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30)); // button narrower
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 60));       // textbox row
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 40));       // button row

            // Textbox centered
            TextBox txtBox = new TextBox()
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 12),
                TextAlign = HorizontalAlignment.Center
            };

            // Button on right
            Button btnOk = new Button()
            {
                Text = "✔ موافق",
                Dock = DockStyle.Right,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Width = 100
            };
            btnOk.Click += (s, e) =>
            {
              obj= _repo.GetCustomerByPhone(txtBox.Text).FirstOrDefault();

                if (obj != null)
                {
                    // Customer already exists → show message on popup
                    MessageBox.Show($"⚠ العميل موجود بالفعل: {obj.CustomerName}",
                                    "تنبيه",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    popup.Close();
                }
                else
                {
                    // Customer not found → proceed normally
                    MessageBox.Show($"تم إدخال: {txtBox.Text}", "Info");
                    popup.Close();
                }

            };

            // Add controls
            layout.Controls.Add(txtBox, 0, 0);
            layout.SetColumnSpan(txtBox, 2); // textbox spans both columns
            layout.Controls.Add(btnOk, 1, 1); // button in right column

            popup.Controls.Add(layout);
            popup.ShowDialog();
        }

        public void DrawReceipt(List<ReceiptItem> items, string invoiceNumber,GetCustomer_Result client)
        {
            int width = 420;
            int height = 800;

            using (Bitmap bmp = new Bitmap(width, height))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                var headerFont = new Font("Segoe UI", 18, FontStyle.Bold);
                var boldFont = new Font("Segoe UI", 12, FontStyle.Bold);
                var normalFont = new Font("Segoe UI", 11);

                StringFormat rtl = new StringFormat()
                {
                    FormatFlags = StringFormatFlags.DirectionRightToLeft,
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Center
                };
                StringFormat center = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                int y = 20;

                // ===== HEADER =====
                g.DrawString("إيصال شراء", headerFont, Brushes.Black,
                    new RectangleF(0, y, width - 20, 40), center);

                y += 45;

                g.DrawString($"رقم الفاتورة : {invoiceNumber}", normalFont,
                    Brushes.Black, new RectangleF(0, y, width - 20, 25), center);

                y += 25;

                g.DrawString($"التاريخ : {DateTime.Now:yyyy/MM/dd}", normalFont,
                    Brushes.Black, new RectangleF(0, y, width - 20, 25), center);

                y += 35;

                // ===== COLUMN POSITIONS (RTL adjusted) =====
                int nameX = width - 200;   // product name on the right
                int nameWidth = 160;       // enough space for wrapping

                int qtyX = nameX - 80;     // quantity column to the left of product
                int priceX = 20;           // price column on the far left

                // ===== TABLE HEADER =====
                g.DrawString("المنتج", boldFont, Brushes.Black,
                    new RectangleF(nameX, y, nameWidth, 25), rtl);

                g.DrawString("الكمية", boldFont, Brushes.Black,
                    new RectangleF(qtyX, y, 80, 25), center);

                g.DrawString("السعر", boldFont, Brushes.Black,
                    new RectangleF(priceX, y, 80, 25), center);

                y += 25;
                g.DrawLine(Pens.Black, 20, y, width - 20, y);
                y += 8;

                // ===== ITEMS =====
                decimal total = 0;
                foreach (var item in items)
                {
                    // product name (right side, wrapped)
                    SizeF nameSize = g.MeasureString(item.Name, normalFont, nameWidth);
                    float nameHeight = Math.Max(25, nameSize.Height);

                    g.DrawString(item.Name, normalFont, Brushes.Black,
                        new RectangleF(nameX, y, nameWidth, nameHeight), rtl);

                    // quantity (middle column)
                    g.DrawString(item.Qty.ToString("0.00"), normalFont, Brushes.Black,
                        new RectangleF(qtyX, y, 80, 25), center);

                    // price (left column)
                    decimal lineTotal = item.Price * item.Qty;
                    g.DrawString(lineTotal.ToString("0.00"), normalFont, Brushes.Black,
                        new RectangleF(priceX, y, 80, 25), center);

                    total += lineTotal;
                    y += (int)nameHeight + 5;
                }

                y += 5;
                g.DrawLine(Pens.Black, 20, y, width - 20, y);

                y += 15;

                // ===== TOTAL =====
                g.DrawString($"الإجمالي : {total:0.00}",
                    new Font("Segoe UI", 13, FontStyle.Bold),
                    Brushes.Black,
                    new RectangleF(0, y, width - 20, 30),
                    center);

                y += 35;

                // ===== CUSTOMER INFO =====
                string customerName = $"اسم العميل: {client.CustomerName}";   // <-- pass or fetch dynamically
                string customerPhone = $"رقم الهاتف: {client.Phone}"; // <-- pass or fetch dynamically

                g.DrawString(customerName,
                    normalFont,
                    Brushes.Black,
                    new RectangleF(0, y, width - 20, 25),
                    center);

                y += 25;

                g.DrawString(customerPhone,
                    normalFont,
                    Brushes.Black,
                    new RectangleF(0, y, width - 20, 25),
                    center);

                y += 35;

                // ===== FOOTER =====
                g.DrawString("شكراً لتعاملكم معنا",
                    normalFont,
                    Brushes.Black,
                    new RectangleF(0, y, width - 20, 25),
                    center);


                string path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"receipt_{invoiceNumber}.png");

                bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png);

                _lastReceiptImage = (Bitmap)bmp.Clone();
            }
        }
        public void PringRecepit()
        {
            if (_lastReceiptImage == null)
            {
                MessageBox.Show("لا يوجد إيصال للطباعة. الرجاء حفظ إيصال أولاً.");
                return;
            }

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (s, ev) =>
            {
                // Center the image on the page
                int x = (ev.PageBounds.Width - _lastReceiptImage.Width) / 2;
                int y = (ev.PageBounds.Height - _lastReceiptImage.Height) / 2;
                ev.Graphics.DrawImage(_lastReceiptImage, x, y);
            };

            PrintDialog dlg = new PrintDialog();
            dlg.Document = pd;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }

        }

    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data_Model
{
    public class KingBoardRepository : IKingBoardRepository
    {
        private readonly KingBoardDBEntities1 _db;

        public KingBoardRepository(KingBoardDBEntities1 db)
        {
            _db = db;
        }

        public int AddCustomer(string customerName, string customerType, string phone)
        {
            return _db.AddCustomer(customerName, customerType, phone);
        }

        public int AddInvoiceItem(int? invoiceID, int? productID, decimal? quantity, decimal? unitPrice)
        {
            return _db.AddInvoiceItem(invoiceID, productID, quantity, unitPrice);
        }

        public int AddProduct(string productName, int? unitID, decimal? stockQuantity,
                              decimal? wholesalePrice, decimal? retailPrice, decimal? minStockLevel)
        {
            return _db.AddProduct(productName, unitID, stockQuantity, wholesalePrice, retailPrice, minStockLevel);
        }

        public int AddStock(int? productID, decimal? quantity)
        {
            return _db.AddStock(productID, quantity);
        }

        public decimal? CreateInvoice(string invoiceNumber, int? customerID, string invoiceType, int? createdBy)
        {
            return _db.CreateInvoice(invoiceNumber, customerID, invoiceType, createdBy).FirstOrDefault();
        }

        public IEnumerable<GetAllProducts_Result> GetAllProducts()
        {
            return _db.GetAllProducts().ToList();
        }

        public int PayInvoice(int? invoiceID, decimal? amount)
        {
            return _db.PayInvoice(invoiceID, amount);
        }
       

        public ObjectResult<GetCustomer_Result> GetCustomerByPhone(string phone)
        {
            return _db.GetCustomer(phone);
        }

        public List<string> GetUnits()
        {
           return _db.GetUnits().ToList();
        }
    }
}

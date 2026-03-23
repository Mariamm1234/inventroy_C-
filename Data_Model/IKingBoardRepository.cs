using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data_Model
{
    internal interface IKingBoardRepository
    {
        int AddCustomer(string customerName, string customerType, string phone);
        int AddInvoiceItem(int? invoiceID, int? productID, decimal? quantity, decimal? unitPrice);
        int AddProduct(string productName, int? unitID, decimal? stockQuantity,
                       decimal? wholesalePrice, decimal? retailPrice, decimal? minStockLevel);
        int AddStock(int? productID, decimal? quantity);
        decimal? CreateInvoice(string invoiceNumber, int? customerID, string invoiceType, int? createdBy);
        IEnumerable<GetAllProducts_Result> GetAllProducts();
        int PayInvoice(int? invoiceID, decimal? amount);
        ObjectResult<GetCustomer_Result> GetCustomerByPhone(string phone);

        List<string> GetUnits();

    }
}

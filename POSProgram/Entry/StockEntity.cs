using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProgram.Entry
{
    class StockEntity
    {
        private string stockId;
        private string categoryId;
        private string productId;
        private string unit;
        private string stockDate;
        private string quantity;
        private string employeeId;
        private string bal;

        public StockEntity(string stockId, string categoryId, string productId, string unit, string stockDate, string quantity, string employeeId, string bal)
        {
            this.stockId = stockId;
            this.categoryId = categoryId;
            this.productId = productId;
            this.unit = unit;
            this.stockDate = stockDate;
            this.quantity = quantity;
            this.employeeId = employeeId;
            this.bal = bal;
        }

        public string StockID { get => stockId; set => stockId = value; }
        public string CategoryId { get => categoryId; set => categoryId = value; }
        public string ProductId { get => productId; set => productId = value; }
        public string Unit { get => unit; set => unit = value; }
        public string StockDate { get => stockDate; set => stockDate = value; }
        public string Quantity { get => quantity; set => quantity = value; }
        public string EmployeeId { get => employeeId; set => employeeId = value; }
        public string Bal { get => bal; set => bal = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProgram.Entry
{
    class CartEntity
    {
        private int Id;
        private string transactionno;
        private string productId;
        private double price;
        private string quantity;
        private double discount;
        private double total;
        private string date;
        private string status;

        public CartEntity(int id, string transactionno, string productId, double price, string quantity, double discount, double total, string date, string status)
        {
            Id = id;
            this.transactionno = transactionno;
            this.productId = productId;
            this.price = price;
            this.quantity = quantity;
            this.discount = discount;
            this.total = total;
            this.date = date;
            this.status = status;
        }

        public int Id1 { get => Id; set => Id = value; }
        public string Transactionno { get => transactionno; set => transactionno = value; }
        public string ProductId { get => productId; set => productId = value; }
        public double Price { get => price; set => price = value; }
        public string Quantity { get => quantity; set => quantity = value; }
        public double Discount { get => discount; set => discount = value; }
        public double Total { get => total; set => total = value; }
        public string Date { get => date; set => date = value; }
        public string Status { get => status; set => status = value; }
    }
}

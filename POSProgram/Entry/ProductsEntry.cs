using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProgram.Entry
{
    class ProductsEntry
    {
        private int Id;
        private string productId;
        private string productName;
        private string SupplierId;
        private string categoryId;
        private string brandId;
        private string Unit;
        private string price;
        

        public ProductsEntry(int Id,string productId, string productName, string supplierID, string categoryId, string brandId, string unit, string price)
        {
            this.Id = Id;
            this.productId = productId;
            this.productName = productName;
            this .SupplierId = supplierID;
            this.categoryId = categoryId;
            this.brandId = brandId;
            this.Unit = unit;
            this.price = price;
           
        }
        public ProductsEntry() { }
        public int ID { get => Id; set => Id = value; }
        public string ProductId { get => productId; set => productId = value; }
        public string ProductName { get => productName; set => productName = value; }
        public string SupplierID { get => SupplierId; set => SupplierId = value; }
        public string CategoryId { get => categoryId; set => categoryId = value; }
        public string BrandId { get => brandId; set => brandId = value; }
        public string unit { get => Unit; set => Unit = value; }
        public string Price { get => price; set => price = value; }
       
    }
}

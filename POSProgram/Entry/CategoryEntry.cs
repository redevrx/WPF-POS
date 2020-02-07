using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProgram.Entry
{
    class CategoryEntry
    {
        private int Id;
        private string categoryId;
        private string categoryName;
        private string description;



        public CategoryEntry(int Id , string categoryId, string categoryName, string description)
        {
            this.Id = Id;
            this.categoryId = categoryId;
            this.categoryName = categoryName;
            this.description = description;
        }
        public CategoryEntry() { }
        public int ID { get => Id; set => Id = value; }
        public string CategoryId { get => categoryId; set => categoryId = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
        public string Description { get => description; set => description = value; }

    }
}

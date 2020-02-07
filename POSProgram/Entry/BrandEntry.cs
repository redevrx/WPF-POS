using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProgram.Entry
{
    class BrandEntry
    {
        private int Id;
        private string brandId;
        private string brandName;

        public BrandEntry() { }

        public BrandEntry(int id, string brandId, string brandName)
        {
            this.Id = id;
            this.brandId = brandId;
            this.brandName = brandName;
        }

        public int ID { get => Id; set => Id = value; }
        public string BrandId { get => brandId; set => brandId = value; }
        public string BrandName { get => brandName; set => brandName = value; }



        /*   public class BrandEntryMap : EntityMap<BrandEntry>
           {
               public BrandEntryMap()
               {
                   Map(p => p.ID).ToColumn("ID");
               }
           }*/

    }
   
}

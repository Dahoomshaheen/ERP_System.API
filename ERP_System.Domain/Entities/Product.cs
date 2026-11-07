using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_System.Domain.Entities
{
   public class Product
    { public int Id { set; get; }
      public string EnName { set; get; }
      public string ArName { set; get; }
      public decimal Price { set; get; }

      public decimal CostPrice { set; get; }
         public int UnitId { set; get; }
        public Unit Unit { set; get; }
        public int GroupId { set; get; }
        public Group Group  { set; get; }

        public int Warehouseid { set; get; }
        public Warehouse Warehouse { set; get; }



    }
}

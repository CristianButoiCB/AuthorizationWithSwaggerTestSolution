using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Item
    {
        public Item()
        {
            PurchaseOrdersLines = new HashSet<PurchaseOrdersLine>();
            SaleOrdersLines = new HashSet<SaleOrdersLine>();
        }

        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public bool? Active { get; set; }

        public virtual ICollection<PurchaseOrdersLine> PurchaseOrdersLines { get; set; }
        public virtual ICollection<SaleOrdersLine> SaleOrdersLines { get; set; }
    }
}

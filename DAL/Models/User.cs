using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class User
    {
        public User()
        {
            PurchaseOrderCreatedByNavigations = new HashSet<PurchaseOrder>();
            PurchaseOrderLastUpdatedByNavigations = new HashSet<PurchaseOrder>();
            PurchaseOrdersLineCreatedByNavigations = new HashSet<PurchaseOrdersLine>();
            PurchaseOrdersLineLastUpdatedByNavigations = new HashSet<PurchaseOrdersLine>();
            SaleOrderCreatedByNavigations = new HashSet<SaleOrder>();
            SaleOrderLastUpdatedByNavigations = new HashSet<SaleOrder>();
        }

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool? Active { get; set; }

        public virtual ICollection<PurchaseOrder> PurchaseOrderCreatedByNavigations { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrderLastUpdatedByNavigations { get; set; }
        public virtual ICollection<PurchaseOrdersLine> PurchaseOrdersLineCreatedByNavigations { get; set; }
        public virtual ICollection<PurchaseOrdersLine> PurchaseOrdersLineLastUpdatedByNavigations { get; set; }
        public virtual ICollection<SaleOrder> SaleOrderCreatedByNavigations { get; set; }
        public virtual ICollection<SaleOrder> SaleOrderLastUpdatedByNavigations { get; set; }
    }
}

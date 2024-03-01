using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class BusinessPartner
    {
        public BusinessPartner()
        {
            PurchaseOrders = new HashSet<PurchaseOrder>();
            SaleOrders = new HashSet<SaleOrder>();
        }

        public string Bpcode { get; set; } = null!;
        public string Bpname { get; set; } = null!;
        public string Bptype { get; set; } = null!;
        public bool? Active { get; set; }

        public virtual Bptype BptypeNavigation { get; set; } = null!;
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
    }
}

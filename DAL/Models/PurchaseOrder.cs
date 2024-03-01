using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            PurchaseOrdersLines = new HashSet<PurchaseOrdersLine>();
        }

        public int Id { get; set; }
        public string Bpcode { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }

        public virtual BusinessPartner BpcodeNavigation { get; set; } = null!;
        public virtual User CreatedByNavigation { get; set; } = null!;
        public virtual User? LastUpdatedByNavigation { get; set; }
        public virtual ICollection<PurchaseOrdersLine> PurchaseOrdersLines { get; set; }
    }
}

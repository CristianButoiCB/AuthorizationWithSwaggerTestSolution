using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class PurchaseOrdersLine
    {
        public int LineId { get; set; }
        public int DocId { get; set; }
        public string ItemCode { get; set; } = null!;
        public decimal Quantity { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; } = null!;
        public virtual PurchaseOrder Doc { get; set; } = null!;
        public virtual Item ItemCodeNavigation { get; set; } = null!;
        public virtual User? LastUpdatedByNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class SaleOrdersLine
    {
        public SaleOrdersLine()
        {
            SaleOrdersLinesComments = new HashSet<SaleOrdersLinesComment>();
        }

        public int LineId { get; set; }
        public int DocId { get; set; }
        public string ItemCode { get; set; } = null!;
        public decimal Quantity { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }

        public virtual SaleOrder Doc { get; set; } = null!;
        public virtual Item ItemCodeNavigation { get; set; } = null!;
        public virtual ICollection<SaleOrdersLinesComment> SaleOrdersLinesComments { get; set; }
    }
}

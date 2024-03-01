using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class SaleOrdersLinesComment
    {
        public int CommentLineId { get; set; }
        public int DocId { get; set; }
        public int LineId { get; set; }
        public string Comment { get; set; } = null!;

        public virtual SaleOrder Doc { get; set; } = null!;
        public virtual SaleOrdersLine Line { get; set; } = null!;
    }
}

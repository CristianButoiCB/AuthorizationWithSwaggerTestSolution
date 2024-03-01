using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsTest.DAL.Models
{
    public enum DocumentType
    {
        PurchaseOrder=1,
        SaleOrder=2,        
    }
    public partial class RequestDocument
    {
        public RequestDocument()
        {
            Lines = new HashSet<RequestLine>();
        }

        public int? Id { get; set; }
        public int? DocumentType { get; set; }
        public string? Bpcode { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }       
        public ICollection<RequestLine> Lines { get; set; }

        public object Copy()
        {
            return this.MemberwiseClone();
        }
    }

    public partial class RequestLine
    {
        public int LineId { get; set; }
        public int DocId { get; set; }
        public string ItemCode { get; set; } = null!;
        public decimal Quantity { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }

     
    }
}

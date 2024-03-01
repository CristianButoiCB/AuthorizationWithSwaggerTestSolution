using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsTest.DAL.Models
{
    public partial class ResponseDocument
    {
        public ResponseDocument()
        {
            Lines = new HashSet<ResponseLine>();
        }

        public int? Id { get; set; }
        public int? DocumentType { get; set; }
        public string? Bpcode { get; set; }

        public string? BPName { get; set; }

        public bool? Active { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? CreatedBy { get; set; }

        public string? FullNameCreatedBy { get; set; }

        public string? FullNameLastUpdateddBy { get; set; }

        public int? LastUpdatedBy { get; set; }
        public ICollection<ResponseLine> Lines { get; set; }

        public object Copy()
        {
            return this.MemberwiseClone();
        }
    }

    public partial class ResponseLine
    {
        public int LineId { get; set; }
        public int DocId { get; set; }
        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public bool Active { get; set; }
        public decimal Quantity { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }


    }
}


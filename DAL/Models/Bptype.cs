using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Bptype
    {
        public Bptype()
        {
            BusinessPartners = new HashSet<BusinessPartner>();
        }

        public string TypeCode { get; set; } = null!;
        public string TypeName { get; set; } = null!;

        public virtual ICollection<BusinessPartner> BusinessPartners { get; set; }
    }
}

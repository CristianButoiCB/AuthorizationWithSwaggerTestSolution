using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsTest.DAL.Models
{
    public class ResponseBusinessPartnersModel
    {
        public IEnumerable<BusinessPartner>? Pagedlist { get; set; }
        public int? TotalPages { get; set; }
    }
}

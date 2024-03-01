using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsTest.DAL.Models
{
    public class ResponseItemsModel
    {
        public IEnumerable<Item>? Pagedlist { get; set; }
        public int ? TotalPages { get; set; }
    }
}

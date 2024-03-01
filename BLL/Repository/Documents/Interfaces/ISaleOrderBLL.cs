using DAL.Models;
using GoodsTest.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository.Documents.Interfaces
{
    public interface ISaleOrderBLL
    {
        public SaleOrder? Insert(RequestDocument req, ref string strError, ref bool blnOk);
        public SaleOrder? Update(RequestDocument model, ref string strError, ref bool blnOk);
        public bool Delete(int id);
        ResponseDocument? Get(int id);
    }
}

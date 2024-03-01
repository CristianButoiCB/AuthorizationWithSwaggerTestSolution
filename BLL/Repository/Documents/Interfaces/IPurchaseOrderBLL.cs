using DAL.Models;

using GoodsTest.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository.Documents.Interfaces
{
    public interface IPurchaseOrderBLL
    {
        PurchaseOrder? Insert(RequestDocument req, ref string strError, ref bool blnOk);
        PurchaseOrder? Update(RequestDocument model, ref string strError, ref bool blnOk);
        bool Delete(int id);
        ResponseDocument? Get(int id);
    }
}

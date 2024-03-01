using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BLL.Repository.Documents.Interfaces;
using GoodsTest.DAL.Models;
using DAL;
using System.Reflection;
using System.Reflection.Metadata;
namespace BLL.Repository.Documents
{
    public class SaleOrderBLL : ISaleOrderBLL
    {
        private bool disposedValue;
        private readonly GoodsTestContext _context;
        public SaleOrderBLL(GoodsTestContext context)
        {
            _context = context;
        }
        public SaleOrder Insert(SaleOrder so)
        {
            try
            {
                _context.SaleOrders.Add(so);
                _context.SaveChanges();
                return so;

            }
            catch (blException ex)
            {
                throw (new blException("SaleOrderBLL - Insert: " + ex.Message));
            }

        }

        public SaleOrder? Insert(RequestDocument req, ref string strError, ref bool blnOk)
        {
            try
            {
                //validate It’s not possible to add a document of an unknown type
                if (!req.DocumentType.HasValue || (req.DocumentType.HasValue && (req.DocumentType.Value != (int)DocumentType.PurchaseOrder && req.DocumentType != (int)DocumentType.SaleOrder)))
                {
                    strError = "Invalid Document Type";
                    blnOk = false;
                    return null;
                }
                //validate It’s not possible to add a document for a business partner that is not active
                var bp = _context.BusinessPartners.FirstOrDefault(b => b.Bpcode == req.Bpcode);
                if (bp == null || (!bp.Active.HasValue) || (bp.Active.HasValue && bp.Active.Value == false))
                {
                    strError = "It’s not possible to add a document for a business partner that is not active";
                    blnOk = false;
                    return null;
                }
                //validate It’s not possible to add a sale document for a business partner of type V
                if (bp.Bptype == "V")
                {
                    strError = "It’s not possible to add a sale document for a business partner of type V";
                    blnOk = false;
                    return null;
                }
                //validate  It’s not possible to add a document without lines
                if (req.Lines == null || req.Lines.Count == 0)
                {
                    strError = "It’s not possible to add a document without lines";
                    blnOk = false;
                    return null;
                }
                //validate It’s not possible to sale / purchase an item that is not active
                foreach (var req_line in req.Lines)
                {
                    var item_line = _context.Items.FirstOrDefault(i => i.ItemCode == req_line.ItemCode);
                    if (item_line == null || (!item_line.Active.HasValue) || (item_line.Active.HasValue && item_line.Active.Value == false))
                    {
                        strError = "It’s not possible to sale / purchase an item that is not active";
                        blnOk = false;
                        return null;
                    }
                }
                var requestEntity = req.AutoMap(new SaleOrder());
                //we are going to map the content in this why since the lists properties are called differently
                foreach (var item in req.Lines)
                {
                    SaleOrdersLine line = item.AutoMap(new SaleOrdersLine());
                    line.CreatedBy = requestEntity.CreatedBy;
                    line.CreateDate = requestEntity.CreateDate;
                    line.LastUpdatedBy = null;
                    line.LastUpdateDate = null;
                    line.DocId = 0;
                    requestEntity.SaleOrdersLines.Add(line);
                }
                requestEntity.LastUpdateDate = null;
                requestEntity.LastUpdatedBy = null;
                _context.SaleOrders.Add(requestEntity);
                _context.SaveChanges();

                strError = "";
                blnOk = true;
                return requestEntity;

            }
            catch (blException ex)
            {
                throw (new blException("SaleOrderBLL - Insert: " + ex.Message));
            }

        }

        public void Update(SaleOrder so)
        {
            try
            {
                SaleOrder? dbpo = _context.SaleOrders.Find(so.Id);
                if (dbpo != null)
                {
                    dbpo.Bpcode = so.Bpcode;
                    dbpo.LastUpdateDate = so.LastUpdateDate;
                    dbpo.LastUpdatedBy = so.LastUpdatedBy;

                    _context.SaveChanges();
                }
            }
            catch (blException ex)
            {
                throw (new blException("SaleOrdersBLL - Update:" + ex.Message));
            }
        }

        public SaleOrder? Update(RequestDocument model, ref string strError, ref bool blnOk)
        {
            var existingso = _context.SaleOrders
                .Where(p => p.Id == model.Id)
                .Include(p => p.SaleOrdersLines)
                .SingleOrDefault();
            //validate It’s not possible to update a document that doesn’t exists
            if (existingso == null)
            {
                strError = "It’s not possible to update a document that doesn’t exists";
                blnOk = false;
                return null;
            }
            //• It’s not possible to change the document type
            if (model.DocumentType != (int)DocumentType.SaleOrder)
            {
                strError = "It’s not possible to change the document type";
                blnOk = false;
                return null;
            }
          
            //validate  It’s not possible to add a document without lines
            if (model.Lines == null || model.Lines.Count == 0)
            {
                strError = "It’s not possible to add a document without lines";
                blnOk = false;
                return null;
            }
            //validate It’s not possible to add a document for a business partner that is not active
            var bp = _context.BusinessPartners.FirstOrDefault(b => b.Bpcode == model.Bpcode);
            if (bp == null || (!bp.Active.HasValue) || (bp.Active.HasValue && bp.Active.Value == false))
            {
                strError = "It’s not possible to add a document for a business partner that is not active";
                blnOk = false;
                return null;
            }
            //validate It’s not possible to add a sale document for a business partner of type V
            if (bp.Bptype == "V")
            {
                strError = "It’s not possible to add a sale document for a business partner of type V";
                blnOk = false;
                return null;
            }
           
            //validate It’s not possible to sale / purchase an item that is not active
            foreach (var req_line in model.Lines)
            {
                var item_line = _context.Items.FirstOrDefault(i => i.ItemCode == req_line.ItemCode);
                if (item_line == null || (!item_line.Active.HasValue) || (item_line.Active.HasValue && item_line.Active.Value == false))
                {
                    strError = "It’s not possible to sale / purchase an item that is not active";
                    blnOk = false;
                    return null;
                }
            }
            //TO DO add transactions!
            if (existingso != null)
            {
                // Update parent
                //make sure that any not passed values are preserved during the update
                model.CreatedBy = existingso.CreatedBy;
                model.CreateDate = existingso.CreateDate;
                _context.Entry(existingso).CurrentValues.SetValues(model);

                // Delete PurchaseOrderLines
                foreach (var existingLine in existingso.SaleOrdersLines.ToList())
                {
                    if (!model.Lines.Any(c => c.LineId == existingLine.LineId))
                        _context.SaleOrdersLines.Remove(existingLine);
                }

                // Update and Insert children PurchaseOrderLines from model
                foreach (var childModel in model.Lines)
                {
                    var existingChild = existingso.SaleOrdersLines
                        .Where(c => c.LineId == childModel.LineId && c.LineId != default(int))
                        .SingleOrDefault();

                    if (existingChild != null)
                    {
                        childModel.LastUpdatedBy = existingso.LastUpdatedBy;
                        childModel.LastUpdateDate = existingso.LastUpdateDate;
                        childModel.CreatedBy = existingso.CreatedBy;
                        childModel.CreateDate = existingso.CreateDate;
                        // Update PurchaseOrdersLine
                        _context.Entry(existingChild).CurrentValues.SetValues(childModel);
                    }
                    else
                    {
                        // Insert new PurchaseOrdersLine

                        SaleOrdersLine newLine = childModel.AutoMap(new SaleOrdersLine());
                        newLine.LineId = 0;
                        newLine.LastUpdatedBy = existingso.LastUpdatedBy;
                        newLine.LastUpdateDate = existingso.LastUpdateDate;
                        newLine.CreatedBy = existingso.CreatedBy;
                        newLine.CreateDate = existingso.CreateDate;
                        newLine.DocId = existingso.Id;
                        existingso.SaleOrdersLines.Add(newLine);

                    }
                }

                _context.SaveChanges();

            }
            blnOk = true;
            strError = string.Empty;
            return existingso;
        }
        public bool Delete(int id)
        {
            try
            {
                SaleOrder? dbpo = _context.SaleOrders.Find(id);
                if (dbpo != null)
                {
                    var lines = _context.SaleOrdersLines.Where(l => l.DocId == id);
                    _context.RemoveRange(lines);
                    _context.Remove(dbpo);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (blException ex)
            {
                throw (new blException("SaleOrderBLL - Update:" + ex.Message));
            }

        }

        public ResponseDocument? Get(int id)
        {
            var existingpo = _context.SaleOrders
                .Where(p => p.Id == id)
                .Include(p => p.SaleOrdersLines)
                .ThenInclude(i => i.ItemCodeNavigation)
                .Include(bp => bp.BpcodeNavigation)
                .Include(u => u.CreatedByNavigation)
                .Include(u => u.LastUpdatedByNavigation)

                .SingleOrDefault();
            //some of the properties are coming wiuth a different name we will mape them manually
            ResponseDocument? response=null;
            if (existingpo != null)
            {
                response = existingpo.AutoMap(new ResponseDocument());

                response.BPName = existingpo.BpcodeNavigation?.Bpname;
                response.FullNameCreatedBy = existingpo.CreatedByNavigation.FullName;
                response.FullNameLastUpdateddBy = existingpo.LastUpdatedByNavigation?.FullName;
                foreach (var line in _context.PurchaseOrdersLines)
                {
                    var responseline = line.AutoMap(new ResponseLine());
                    responseline.ItemName = line.ItemCodeNavigation.ItemName;
                    response.Lines.Add(responseline);
                }
            }
            return response;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CompanyBLL()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

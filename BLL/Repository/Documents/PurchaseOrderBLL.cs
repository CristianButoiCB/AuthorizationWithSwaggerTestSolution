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
using System.Threading.Channels;
using System.Xml.Linq;
using System.Runtime.Intrinsics.X86;
namespace BLL.Repository.Documents
{
    public class PurchaseOrderBLL: IPurchaseOrderBLL
    {
        private bool disposedValue;
        private readonly GoodsTestContext _context;
        public PurchaseOrderBLL(GoodsTestContext context)
        {
            _context = context;
        }
        public PurchaseOrder Insert(PurchaseOrder po)
        {
            try
            {
                _context.PurchaseOrders.Add(po);
                _context.SaveChanges();
                return po;

            }
            catch (blException ex)
            {
                throw (new blException("PurchaseOrderBLL - Insert: " + ex.Message));
            }

        }

        public PurchaseOrder? Insert(RequestDocument req, ref string strError, ref bool blnOk)
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
                if (bp==null || (!bp.Active.HasValue) ||(bp.Active.HasValue && bp.Active.Value==false)) 
                {
                    strError = "It’s not possible to add a document for a business partner that is not active";
                    blnOk = false;
                    return null;
                }
                //validate It’s not possible to add a purchase document for a business partner of type S
                if (bp.Bptype=="S")
                {
                    strError = "It’s not possible to add a purchase document for a business partner of type S";
                    blnOk = false;
                    return null;
                }
                //validate  It’s not possible to add a document without lines
                if (req.Lines==null || req.Lines.Count == 0)
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
                var requestEntity = req.AutoMap(new PurchaseOrder());
                //we are going to map the content in this why since the lists properties are called differently
                foreach(var item in req.Lines)
                {
                    PurchaseOrdersLine line =item.AutoMap(new PurchaseOrdersLine());
                   
                    line.LastUpdatedBy = null;
                    line.LastUpdateDate = null;
                    line.CreatedBy = requestEntity.CreatedBy;
                    line.CreateDate = requestEntity.CreateDate;

                    requestEntity.PurchaseOrdersLines.Add(line);
                }
                requestEntity.LastUpdateDate = null;
                requestEntity.LastUpdatedBy = null;
               _context.PurchaseOrders.Add(requestEntity);
               _context.SaveChanges();
                requestEntity = _context.PurchaseOrders.Find(requestEntity.Id);
                strError = "";
                blnOk = true;
               return requestEntity;

            }
            catch (blException ex)
            {
                throw (new blException("PurchaseOrderBLL - Insert: " + ex.Message));
            }

        }

        public void Update(PurchaseOrder po)
        {
            try
            {
                PurchaseOrder dbpo = _context.PurchaseOrders.Find(po.Id);
                if (dbpo != null)
                {
                    dbpo.Bpcode = po.Bpcode;
                    dbpo.LastUpdateDate = po.LastUpdateDate;
                    dbpo.LastUpdatedBy = po.LastUpdatedBy;

                    _context.SaveChanges();
                }
            }
            catch (blException ex)
            {
                throw (new blException("PurchaseOrderBLL - Update:" + ex.Message));
            }
        }

        public PurchaseOrder? Update(RequestDocument model, ref string strError, ref bool blnOk)
        {
            var existingpo = _context.PurchaseOrders
                .Where(p => p.Id == model.Id)
                .Include(p => p.PurchaseOrdersLines)
                .SingleOrDefault();
            //validate It’s not possible to update a document that doesn’t exists
            if (existingpo == null)
            {
                strError = "It’s not possible to update a document that doesn’t exists";
                blnOk = false;
                return null;
            }
            //• It’s not possible to change the document type
            if (model.DocumentType != (int)DocumentType.PurchaseOrder)
            {
                strError = "It’s not possible to update a document that doesn’t exists";
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
            //validate It’s not possible to add a purchase document for a business partner of type S
            if (bp.Bptype == "S")
            {
                strError = "It’s not possible to add a purchase document for a business partner of type S";
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

            //TO DO add transactions!
            if (existingpo != null)
            {
                // Update parent
                //make sure that any not passed values are preserved during the update
                model.CreatedBy = existingpo.CreatedBy;
                model.CreateDate = existingpo.CreateDate; 
                _context.Entry(existingpo).CurrentValues.SetValues(model);

                // Delete PurchaseOrderLines
                foreach (var existingLine in existingpo.PurchaseOrdersLines.ToList())
                {
                    if (!model.Lines.Any(c => c.LineId == existingLine.LineId))
                        _context.PurchaseOrdersLines.Remove(existingLine);
                }

                // Update and Insert children PurchaseOrderLines from model
                foreach (var childModel in model.Lines)
                {
                    var existingChild = existingpo.PurchaseOrdersLines
                        .Where(c => c.LineId == childModel.LineId && c.LineId != default(int))
                        .SingleOrDefault();

                    if (existingChild != null)
                    {
                        childModel.LastUpdatedBy = existingpo.LastUpdatedBy;
                        childModel.LastUpdateDate = existingpo.LastUpdateDate;
                        childModel.CreatedBy= existingpo.CreatedBy;
                        childModel.CreateDate = existingpo.CreateDate;
                        // Update PurchaseOrdersLine
                        _context.Entry(existingChild).CurrentValues.SetValues(childModel);
                    }
                    else
                    {
                        // Insert new PurchaseOrdersLine

                        PurchaseOrdersLine newLine = childModel.AutoMap(new PurchaseOrdersLine());
                        newLine.LineId = 0;
                        newLine.LastUpdatedBy = existingpo.LastUpdatedBy;
                        newLine.LastUpdateDate = existingpo.LastUpdateDate;
                        newLine.CreatedBy = existingpo.CreatedBy;
                        newLine.CreateDate = existingpo.CreateDate;
                        newLine.DocId = existingpo.Id;
                        existingpo.PurchaseOrdersLines.Add(newLine);

                    }
                }

                _context.SaveChanges();
               
            }
            blnOk = true;
            strError = string.Empty;
            return existingpo;
        }

        public bool Delete (int id)
        {
            try
            {
                PurchaseOrder? dbpo = _context.PurchaseOrders.Find(id);
                if (dbpo != null)
                {
                    var lines = _context.PurchaseOrdersLines.Where(l => l.DocId == id);
                    _context.RemoveRange(lines);
                    _context.Remove(dbpo);
                    _context.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            catch (blException ex)
            {
                throw (new blException("PurchaseOrderBLL - Delete:" + ex.Message));
            }
        
        }


        public ResponseDocument? Get(int id)
        {
            var existingpo = _context.PurchaseOrders
                .Where(p => p.Id == id)
                .Include(p => p.PurchaseOrdersLines)
                .ThenInclude(i => i.ItemCodeNavigation)
                .Include(bp=>bp.BpcodeNavigation)                
                .Include(u=>u.CreatedByNavigation)
                .Include(u => u.LastUpdatedByNavigation)
                
                .SingleOrDefault();
            //some of the properties are coming wiuth a different name we will mape them manually
            ResponseDocument? response = null;
            if (existingpo != null)
            {
                response = existingpo.AutoMap(new ResponseDocument());
                response.BPName = existingpo.BpcodeNavigation.Bpname;
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

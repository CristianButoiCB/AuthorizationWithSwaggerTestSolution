using BLL.Repository.Users.Interfaces;
using GoodsTest.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Repository;
using BLL.Repository.Documents.Interfaces;
using BLL.Repository.Items.Interfaces;
using BLL.Repository.Users;
using System.Security.Claims;
using System.Security.Principal;
using GoodsTest.DAL.Models;
using Microsoft.Data.SqlClient;
namespace GoodsTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        #region methods
        protected int GetUserId()
        {
            return int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
        }
        #endregion
        #region Fields
        private readonly IPurchaseOrderBLL _PurchaseOrderBLL;
        private readonly ISaleOrderBLL _SaleOrderBLL;

        #endregion
        #region Constructor
        public DocumentController(IPurchaseOrderBLL purchaseorderbll,ISaleOrderBLL saleorderbll)
        {
            _PurchaseOrderBLL = purchaseorderbll;
            _SaleOrderBLL = saleorderbll;
        }

        #endregion
        #region API Endpoints
        [Authorize]
        [Route("adddocument")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]       
        public IActionResult Post([FromBody] DAL.Models.RequestDocument el)
        {
            try
            {
                int _userId = -1;
                string strError = "";
                bool blnOk = false;

                _userId=GetUserId();
                

                el.CreatedBy = _userId;
                el.CreateDate = DateTime.Now;
                if (el.DocumentType == (int)DocumentType.PurchaseOrder)
                {
                    var po=_PurchaseOrderBLL.Insert(el, ref strError, ref blnOk);
                   
                }
                else
                {
                    var so=_SaleOrderBLL.Insert(el, ref strError, ref blnOk);
                   
                }

                if (blnOk)
                {
                    return Ok(el);
                }
                else
                {
                    return BadRequest(strError);
                }

            }
            catch (Exception ex)
            {               

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [Route("editdocument")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromBody] DAL.Models.RequestDocument el)
        {
            try
            {
                int _userId = -1;
                string strError = "";
                bool blnOk = false;

                _userId = GetUserId();
                el.LastUpdatedBy = _userId;
                el.LastUpdateDate = DateTime.Now;
                if (el.DocumentType == (int)DocumentType.PurchaseOrder)
                {
                    var po = _PurchaseOrderBLL.Update(el, ref strError, ref blnOk);

                }
                else
                {
                    var so = _SaleOrderBLL.Update(el, ref strError, ref blnOk);

                }

                if (blnOk)
                {
                    return Ok(el);
                }
                else
                {
                    return BadRequest(strError);
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [Route("deletedocument")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromBody] DAL.Models.RequestDocument el)
        {
            try
            {
                
                bool blnOk = false;

                if (!el.Id.HasValue) return BadRequest("Invalid Paramater");

                if (el.DocumentType == (int)DocumentType.PurchaseOrder)
                {
                    blnOk = _PurchaseOrderBLL.Delete(el.Id.Value);

                }
                else
                {
                    blnOk = _SaleOrderBLL.Delete(el.Id.Value);

                }

                if (blnOk)
                {
                    return Ok(el);
                }
                else
                {//TO DO add any pertinent info
                    return BadRequest("An error occured whend deleting the document.");
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



        [Authorize]
        [Route("getdocument")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get(int id, int ObjectType)
        {
            try
            {

                bool blnOk = false;
                ResponseDocument? response = null;
                

                if (ObjectType == (int)DocumentType.PurchaseOrder)
                {
                    response = _PurchaseOrderBLL.Get(id);

                }
                else
                {
                    response = _SaleOrderBLL.Get(id);
                }
                blnOk = response != null;
                if (blnOk)
                {
                    return Ok(response);
                }
                else
                {//TO DO add any pertinent info
                    return BadRequest("An error occured whend getting the document.");
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}

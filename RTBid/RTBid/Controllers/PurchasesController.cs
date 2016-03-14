using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RTBid.Core.Domain;
using RTBid.Data.Infrastructure;
using RTBid.Infrastructure;
using RTBid.Core.Models;
using RTBid.Core.Infrastructure;
using RTBid.Core.Repository;
using AutoMapper;

namespace RTBid.Controllers
{
    [Authorize]
    public class PurchasesController : BaseApiController
    {
        //private RTBidDataContext db = new RTBidDataContext();
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PurchasesController(IPurchaseRepository purchaseRepository, IUnitOfWork unitOfWork, IRTBidUserRepository rtbidUserRepository) : base(rtbidUserRepository)
        {
            _purchaseRepository = purchaseRepository;
            _unitOfWork = unitOfWork;
        }
        // GET: api/Purchases
        public IEnumerable<PurchaseModel> GetPurchases()
        {
            return Mapper.Map<IEnumerable<PurchaseModel>>(_purchaseRepository.GetAll());
        }

        // GET: api/Purchases/5
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult GetPurchase(int id)
        {
            Purchase purchase = _purchaseRepository.GetById(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PurchaseModel>(purchase));
        }

        // PUT: api/Purchases/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchase(int id, PurchaseModel purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchase.PurchaseId)
            {
                return BadRequest();
            }

            //db.Entry(purchase).State = EntityState.Modified;
            var dbPurchase = _purchaseRepository.GetById(id);
            dbPurchase.Update(purchase);
            _purchaseRepository.Update(dbPurchase);
            try
            {
                //db.SaveChanges();
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!PurchaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Purchases
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult PostPurchase(PurchaseModel purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbPurchase = new Purchase(purchase);

            dbPurchase.UserId = CurrentUser.Id;
            _purchaseRepository.Add(dbPurchase);
            _unitOfWork.Commit();

            purchase.PurchaseId = dbPurchase.PurchaseId;
            purchase.CreatedDate = dbPurchase.CreatedDate;
            //db.Purchases.Add(purchase);
            //db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchase.PurchaseId }, purchase);
        }

        // DELETE: api/Purchases/5
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult DeletePurchase(int id)
        {
            Purchase purchase = _purchaseRepository.GetById(id);
            if (purchase == null)
            {
                return NotFound();
            }

            //db.Purchases.Remove(purchase);
            //db.SaveChanges();

            _purchaseRepository.Delete(purchase);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<PurchaseModel>(purchase));
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool PurchaseExists(int id)
        {
            return _purchaseRepository.Any(e => e.PurchaseId == id);
        }
    }
}
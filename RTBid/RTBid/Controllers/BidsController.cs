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
using RTBid.Core.Repository;
using RTBid.Core.Infrastructure;
using AutoMapper;
using RTBid.Core.Models;

namespace RTBid.Controllers
{
    [Authorize]
    public class BidsController : BaseApiController
    {
        private readonly IBidRepository _bidRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BidsController(IBidRepository bidRepository, IUnitOfWork unitOfWork, IRTBidUserRepository rtbidUserRepository) : base(rtbidUserRepository)
        {
            _bidRepository = bidRepository;
            _unitOfWork = unitOfWork;
        }
        //private RTBidDataContext db = new RTBidDataContext();

        // GET: api/Bids
        public IEnumerable<BidModel> GetBids()
        {
            return Mapper.Map<IEnumerable<BidModel>>(_bidRepository.GetAll());
        }

        // GET: api/Bids/5
        [ResponseType(typeof(Bid))]
        public IHttpActionResult GetBid(int id)
        {
            Bid bid = _bidRepository.GetById(id);
            if (bid == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<BidModel>(bid));
        }

        // PUT: api/Bids/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBid(int id, BidModel bid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bid.BidId)
            {
                return BadRequest();
            }

            //db.Entry(bid).State = EntityState.Modified;
            var dbBid = _bidRepository.GetById(id);
            dbBid.Update(bid);
            _bidRepository.Update(dbBid);

            try
            {
                _unitOfWork.Commit();
                //db.SaveChanges();
            }
            catch (Exception)
            {
                if (!BidExists(id))
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

        // POST: api/Bids
        [ResponseType(typeof(Bid))]
        public IHttpActionResult PostBid(BidModel bid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.Bids.Add(bid);
            //db.SaveChanges();
            var dbBid = new Bid(bid);

            dbBid.UserId = CurrentUser.Id;
            _bidRepository.Add(dbBid);
            _unitOfWork.Commit();

            bid.BidId = dbBid.BidId;
            bid.TimeStamp = dbBid.TimeStamp;

            return CreatedAtRoute("DefaultApi", new { id = bid.BidId }, bid);
        }

        // DELETE: api/Bids/5
        [ResponseType(typeof(Bid))]
        public IHttpActionResult DeleteBid(int id)
        {
            Bid bid = _bidRepository.GetById(id);
            if (bid == null)
            {
                return NotFound();
            }

            _bidRepository.Delete(bid);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<BidModel>(bid));
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool BidExists(int id)
        {
            return _bidRepository.Any(e => e.BidId == id);
        }
    }
}
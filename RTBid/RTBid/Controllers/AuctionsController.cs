﻿using System;
using System.Collections.Generic;
using System.Data;
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
using RTBid.Data.Repository;
using RTBid.Core.Models;
using AutoMapper;
using System.Collections;

namespace RTBid.Controllers
{
    [Authorize]
    public class AuctionsController : BaseApiController
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuctionsController(IAuctionRepository auctionRepository, IUnitOfWork unitOfWork, IRTBidUserRepository rtbidUserRepository) : base(rtbidUserRepository)
        {
            _auctionRepository = auctionRepository;
            _unitOfWork = unitOfWork;
        }


        // GET: api/Auctions
        public IEnumerable<AuctionModel> GetAuctions()
        {
            //return Mapper.Map<IEnumerable<AuctionModel>>(_auctionRepository.GetWhere(a => a.Product.RTBidUser.UserName == User.Identity.Name));
            return Mapper.Map<IEnumerable<AuctionModel>>(_auctionRepository.GetAll());
        }
        // GET: api/Auctions/5
        [ResponseType(typeof(Auction))]
        public IHttpActionResult GetAuction(int id)
        {
            Auction auction = _auctionRepository.GetById(id);
            if (auction == null)
            {
                return NotFound();
            }

            //if (auction.ActualClosedTime <= DateTime.Now) // Find a way to not allow the user to access the aution before it statrs???
            //{

            //    return NotFound();
            //}


            return Ok(Mapper.Map<AuctionModel>(auction));
        }

        // GET: api/auctionsByCategory/id
        [Route("api/auctionsByCategory/id={id}")]
        public IEnumerable GetCategoryByName(int id)
        {
            return Mapper.Map<IEnumerable<AuctionModel>>(_auctionRepository.GetWhere(a => a.Product.CategoryId == id));
        }

        // PUT: api/Auctions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuction(int id, AuctionModel auction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != auction.AuctionId)
            {
                return BadRequest();
            }

            var dbAuction = _auctionRepository.GetById(id);
            dbAuction.Update(auction);
            _auctionRepository.Update(dbAuction);
            //db.Entry(auction).State = EntityState.Modified;

            try
            {
                //db.SaveChanges();
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!AuctionExists(id))
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

        // POST: api/Auctions
        [ResponseType(typeof(Auction))]
        public IHttpActionResult PostAuction(AuctionModel auction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbAuction = new Auction(auction);

            dbAuction.RTBidUsers.Add(new UserAuction { RTBidUser = CurrentUser });// attach the user to the auction 
            dbAuction.ClosedTime = dbAuction.StartTime.AddHours(1);// Set the expected end time if no bid happened

            dbAuction.OpenSoon = true;
            dbAuction.InAction = false;
            dbAuction.ItemSold = false;
            dbAuction.Rescheduled = false;

            _auctionRepository.Add(dbAuction);
            _unitOfWork.Commit();

            auction.AuctionId = dbAuction.AuctionId;
            //auction.DateSubmitted = dbResponse.DateSubmitted;

            return CreatedAtRoute("DefaultApi", new { id = auction.AuctionId }, auction);
        }

        // DELETE: api/Auctions/5
        [ResponseType(typeof(Auction))]
        public IHttpActionResult DeleteAuction(int id)
        {
            //Auction auction = db.Auctions.Find(id);
            Auction auction = _auctionRepository.GetById(id);
            if (auction == null)
            {
                return NotFound();
            }

            //db.Auctions.Remove(auction);
            //db.SaveChanges();
            _auctionRepository.Delete(auction);
            _unitOfWork.Commit();

            return Ok(Mapper.Map <AuctionModel> (auction));
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool AuctionExists(int id)
        {
            return _auctionRepository.Any(e => e.AuctionId == id);
        }
    }
}
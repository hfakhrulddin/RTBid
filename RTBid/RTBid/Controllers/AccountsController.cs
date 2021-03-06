﻿using AutoMapper;
using RTBid.Core.Domain;
using RTBid.Core.Infrastructure;
using RTBid.Core.Models;
using RTBid.Core.Repository;
using RTBid.Core.Services.Finance;
using RTBid.Infrastructure;
using RTBid.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace RTBid.Controllers
{
    public class AccountsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly IAuthorizationRepository _authRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRTBidUserRepository _rtbidUserRepository;

        public AccountsController(IPaymentService paymentService, IAuthorizationRepository authRepository, IRTBidUserRepository rtbidUserRepository, IUnitOfWork unitOfWork) : base(rtbidUserRepository)
        {
            _paymentService = paymentService;
            _authRepository = authRepository;
            _unitOfWork = unitOfWork;
            _rtbidUserRepository = rtbidUserRepository;
        }

        [AllowAnonymous]
        [Route("api/accounts/register")]
        public async Task<IHttpActionResult> Register(RegistrationModel registration)
        {
            //Server Side Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Pass the Registration onto AuthRepository
            var result = await _authRepository.RegisterUser(registration);

            //Check to see the Registration was Successful
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Registration form was invalid.");
            }
        }

        [Route("api/accounts/currentuser")]
        [HttpGet]
        [ResponseType(typeof(RTBidUserModel.Profile))]
        public IHttpActionResult GetCurrentUser()
        {
            return Ok(Mapper.Map<RTBidUserModel.Profile>(CurrentUser));
        }

        [Route("api/accounts/user/{userId}")]
        [ResponseType(typeof(RTBidUserModel.Profile))]
        public IHttpActionResult GetUser(int userId)
        {
            return Ok(Mapper.Map<RTBidUserModel>(_rtbidUserRepository.GetById(userId)));
        }

        [Route("api/accounts/currentuser")]
        [HttpPut]
        public IHttpActionResult UpdateCurrentUser(string id, RTBidUserModel.Profile user)
        {
            // check to see that id == user.Id

            // check to see that user.Id == CurrentUser.Id

            // update the user

            // call repository.update

            // call unitofwork.commit

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/accounts/payforkeys")]
        public IHttpActionResult PayForKeys(PaymentRequest request)
        {
            _paymentService.BuyItem(CurrentUser, request.token, request.numberOfKeys);

            return Ok();
        }

        [AllowAnonymous]
        [Route("api/accounts/id={id}")]
        public IHttpActionResult GetAccountById(int id)
        {
            RTBidUser rtBidUser = _rtbidUserRepository.GetById(id);

            if (rtBidUser == null)
            {
                return NotFound();
            }

            return Ok(rtBidUser);
        }
    }
}

using System;
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
using RTBid.Core.Models;
using RTBid.Core.Repository;
using RTBid.Core.Infrastructure;
using AutoMapper;

namespace RTBid.Controllers
{
    [Authorize]
    public class CommentsController : BaseApiController
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommentsController(ICommentRepository commentRepository, IUnitOfWork unitOfWork, IRTBidUserRepository rtbidUserRepository) : base(rtbidUserRepository)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }
        //private RTBidDataContext db = new RTBidDataContext();

        // GET: api/Comments
        public IEnumerable<CommentModel> GetComments()
        {
            //return db.Comments;
            return Mapper.Map<IEnumerable<CommentModel>>(_commentRepository.GetAll());
        }

        // GET: api/Comments/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult GetComment(int id)
        {
            Comment comment = _commentRepository.GetById(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<CommentModel>(comment));
        }

        // PUT: api/Comments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComment(int id, CommentModel comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            //db.Entry(comment).State = EntityState.Modified;
            var dbComment = _commentRepository.GetById(id);
            dbComment.Update(comment);
            _commentRepository.Update(dbComment);

            try
            {
                //db.SaveChanges();
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        [ResponseType(typeof(Comment))]
        public IHttpActionResult PostComment(CommentModel comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbComment = new Comment(comment);

            dbComment.UserId = CurrentUser.Id;
            _commentRepository.Add(dbComment);
            _unitOfWork.Commit();

            comment.CommentId = dbComment.CommentId;
            comment.TimeStamp = dbComment.TimeStamp;
       

            return CreatedAtRoute("DefaultApi", new { id = comment.CommentId }, comment);
        }

        // DELETE: api/Comments/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult DeleteComment(int id)
        {
            Comment comment = _commentRepository.GetById(id);
            if (comment == null)
            {
                return NotFound();
            }

            //db.Comments.Remove(comment);
            //db.SaveChanges();
            _commentRepository.Delete(comment);
            _unitOfWork.Commit();

            return Ok(Mapper.Map <CommentModel > (comment));
        }

        

        private bool CommentExists(int id)
        {
            return _commentRepository.Any(e => e.CommentId == id);
        }
    }
}
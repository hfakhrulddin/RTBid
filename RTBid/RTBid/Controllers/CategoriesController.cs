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
using RTBid.Core.Models;
using AutoMapper;

namespace RTBid.Controllers
{
    [Authorize]
    public class CategoriesController : BaseApiController
    {
        //private RTBidDataContext db = new RTBidDataContext();
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IRTBidUserRepository rtbidUserRepository) : base(rtbidUserRepository)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Categories
        public IEnumerable<CategoryModel> GetCategories()
        {
            //return db.Categories;
            return Mapper.Map<IEnumerable<CategoryModel>>(_categoryRepository.GetAll());
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(int id, CategoryModel category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            //db.Entry(category).State = EntityState.Modified;
            var dbCategory = _categoryRepository.GetById(id);
            dbCategory.Update(category);
            _categoryRepository.Update(dbCategory);

            try
            {
                //db.SaveChanges();
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(CategoryModel category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.Categories.Add(category);
            //db.SaveChanges();
            var dbCategory = new Category(category);

            //dbCategory = CurrentUser.Id;
            _categoryRepository.Add(dbCategory);
            _unitOfWork.Commit();

            category.CategoryId = dbCategory.CategoryId;
            dbCategory.CreatedDate = dbCategory.CreatedDate;

            return CreatedAtRoute("DefaultApi", new { id = category.CategoryId }, category);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            //db.Categories.Remove(category);
            //db.SaveChanges();
            _categoryRepository.Delete(category);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<CategoryModel>(category));
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool CategoryExists(int id)
        {
            return _categoryRepository.Any(e => e.CategoryId == id);
        }
    }
}
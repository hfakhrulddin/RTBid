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
using RTBid.Core.Repository;
using RTBid.Core.Infrastructure;
using AutoMapper;

namespace RTBid.Controllers
{
    [Authorize]
    public class ProductsController : BaseApiController
    {
        //private RTBidDataContext db = new RTBidDataContext();
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IProductRepository productRepository, IUnitOfWork unitOfWork, IRTBidUserRepository rtbidUserRepository) : base(rtbidUserRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        // GET: api/Products
        public IEnumerable<ProductModel> GetProducts()
        {
            return Mapper.Map<IEnumerable<ProductModel>>(_productRepository.GetAll());
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ProductModel>(product));
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, ProductModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            //db.Entry(product).State = EntityState.Modified;
            var dbProduct = _productRepository.GetById(id);
            dbProduct.Update(product);
            _productRepository.Update(dbProduct);

            try
            {
                //db.SaveChanges();
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(ProductModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.Products.Add(product);
            //db.SaveChanges();
            var dbProduct = new Product(product);
            //Product.UserId = CurrentUser.Id;
            _productRepository.Add(dbProduct);
            _unitOfWork.Commit();

            product.ProductId = dbProduct.ProductId;
            //product. = dbResponse.DateSubmitted;

            return CreatedAtRoute("DefaultApi", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            //db.Products.Remove(product);
            //db.SaveChanges();
            _productRepository.Delete(product);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<ProductModel>(product));
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool ProductExists(int id)
        {
            return _productRepository.Any(e => e.ProductId == id);
        }
    }
}
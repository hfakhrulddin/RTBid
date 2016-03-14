using RTBid.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Core.Domain
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }


        public Category()
        {
        }

        public Category(CategoryModel model)
        {
            this.Update(model);
            this.CreatedDate = DateTime.Now;
        }

        public void Update(CategoryModel model)
        {
            CategoryId = model.CategoryId;
            CategoryName = model.CategoryName;
            CategoryDescription = model.CategoryDescription;
        }
    }
}

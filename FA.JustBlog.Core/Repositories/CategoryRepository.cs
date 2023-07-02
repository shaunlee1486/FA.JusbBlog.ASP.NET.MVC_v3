using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.IRepositories;

namespace FA.JustBlog.Core.Repositories
{

    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public void DeleteCategory(Category category)
        {
            var item = base.Context.Categories.Find(category);
            base.Context.Categories.Remove(item);
            base.Context.SaveChanges();
        }

    }
}

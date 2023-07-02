﻿using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.IRepositories
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        //Category Find(int categoryId);
        //void AddCategory(Category category);
        //void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        //void DeleteCategory(int categoryId);
        //IList<Category> GetAllCategories();

    }
}

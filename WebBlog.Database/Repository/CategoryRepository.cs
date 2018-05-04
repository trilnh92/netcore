using System;
using System.Collections.Generic;
using System.Text;
using WebBlog.Database.Data;
using WebBlog.Database.IRepository;
using WebBlog.Database.Models;

namespace WebBlog.Database.Repository
{
    public class CategoryRepository : BaseRepository<Category, WebBlogDbContext>, ICategoryRepository
    {
        public CategoryRepository(WebBlogDbContext context) : base(context)
        {
        }
    }
}

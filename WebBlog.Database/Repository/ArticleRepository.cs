using System;
using System.Collections.Generic;
using System.Text;
using WebBlog.Database.Data;
using WebBlog.Database.IRepository;
using WebBlog.Database.Models;

namespace WebBlog.Database.Repository
{
    public class ArticleRepository : BaseRepository<Article, WebBlogDbContext>, IArticleRepository
    {
        public ArticleRepository(WebBlogDbContext context) : base(context)
        {
        }
    }
}

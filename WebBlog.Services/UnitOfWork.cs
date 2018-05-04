using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Database.Data;
using WebBlog.Database.IRepository;
using WebBlog.Database.Repository;

namespace WebBlog.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private WebBlogDbContext _context;

        public ICategoryRepository CategoryRepository { get; private set; }
        public IArticleRepository ArticleRepository { get; private set; }
        public ICommentRepository CommentRepository { get; private set; }
        public IMediaRepository MediaRepository { get; private set; }
        

        public UnitOfWork(WebBlogDbContext context)
        {
            _context = context;

            CategoryRepository = new CategoryRepository(_context);
            ArticleRepository = new ArticleRepository(_context);
            CommentRepository = new CommentRepository(_context);
            MediaRepository = new MediaRepository(_context);
           
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    
        public void Dispose()
        {
        }
    }
}

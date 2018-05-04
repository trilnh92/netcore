using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Database.IRepository;

namespace WebBlog.Services
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IArticleRepository ArticleRepository { get; }
        ICommentRepository CommentRepository { get; }
        IMediaRepository MediaRepository { get; }
        
        int Commit();
        Task<int> CommitAsync();
    }
}

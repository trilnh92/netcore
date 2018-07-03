using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Database.Models;

namespace WebBlog.Services.IServices
{
    public interface ICommentService
    {
        bool CommentExists(int id);
        Task<Comment> GetByIdAsync(int id);
        Task CreateAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(Comment comment);
        IOrderedQueryable<Comment> GetAllOrderByCreatedBy();
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<IEnumerable<Comment>> GetAllByArticleIdAsync(int articleId);
        Task<IEnumerable<Comment>> GetAllRemovedAsync();
    }
}

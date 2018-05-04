using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Database.Models;

namespace WebBlog.Services.IServices
{
    public interface IArticleService
    {
        bool ArticleExists(int id);
        Task<Article> GetByIdAsync(int id);
        Task CreateAsync(Article article);
        Task UpdateAsync(Article article);
        Task DeleteAsync(Article article);
        Task<IEnumerable<Article>> GetAllAsync();
        Task<IEnumerable<Article>> GetAllRemovedAsync();
    }
}

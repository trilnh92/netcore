using System;
using System.Collections.Generic;
using System.Linq;
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
        IOrderedQueryable<Article> GetAllOrderByTitle();
        IOrderedQueryable<Article> GetAllVisibleOrderByCreatedDate();
        IOrderedQueryable<Article> GetAllByCategoryNameOrderByCreatedDate(string category);
        IOrderedQueryable<Article> GetAllBySearchOrderByCreatedDate(string search);
        IOrderedQueryable<Article> GetAllByUserEmailOrderByCreatedDate(string email);
        Task<IEnumerable<Article>> GetAllAsync();
        Task<IEnumerable<Article>> GetAllByUserEmailAsync(string email);
        Task<IEnumerable<Article>> GetAllByCategoryNameAsync(string category);        
        Task<IEnumerable<Article>> GetAllVisibleAsync();
        Task<IEnumerable<Article>> GetAllRemovedAsync();
    }
}

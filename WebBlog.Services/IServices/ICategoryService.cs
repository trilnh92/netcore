using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Database.Models;

namespace WebBlog.Services.IServices
{
    public interface ICategoryService
    {
        bool CategoryExists(int id);
        IEnumerable<Category> GetAllByLanguage(string language);
        IOrderedQueryable<Category> GetAllOrderByName();

        Task<Category> GetByIdAsync(int id);
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<Category>> GetAllByLanguageAsync(string language);
    }
}

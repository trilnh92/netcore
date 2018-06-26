using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.Database.Data;
using WebBlog.Database.Models;
using WebBlog.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace WebBlog.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> GetAllByLanguage(string language)
        {
            return _unitOfWork.CategoryRepository.FindAll(c => c.Language.Equals(language));
        }

        public IOrderedQueryable<Category> GetAllOrderByName()
        {
            return _unitOfWork.CategoryRepository.Query.AsNoTracking().OrderBy(p => p.Name);
        }
        
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _unitOfWork.CategoryRepository.FindAllAsync(p=>true);
        }

        public async Task<IEnumerable<Category>> GetAllByLanguageAsync(string language)
        {
            return await _unitOfWork.CategoryRepository.FindAllAsync(c => c.Language.Equals(language));
        }

        public async Task CreateAsync(Category category)
        {
            await _unitOfWork.CategoryRepository.InsertAsync(category);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _unitOfWork.CategoryRepository.FindAsync(x => x.CategoryId == id);
        }

        public async Task UpdateAsync(Category category)
        {
            await _unitOfWork.CategoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(Category category)
        {
            await _unitOfWork.CategoryRepository.DeleteAsync(category);
        }

        public bool CategoryExists(int id)
        {
            return _unitOfWork.CategoryRepository.Query.Any(x => x.CategoryId == id);
        }
    }
}

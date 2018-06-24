using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebBlog.Database.Models;
using WebBlog.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace WebBlog.Services.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool ArticleExists(int id)
        {            
            return _unitOfWork.ArticleRepository.Query.Any(e => e.ArticleId == id);
        }

        public async Task CreateAsync(Article article)
        {
            await _unitOfWork.ArticleRepository.InsertAsync(article);
        }

        public async Task DeleteAsync(Article article)
        {
            await _unitOfWork.ArticleRepository.DeleteAsync(article);
        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await _unitOfWork.ArticleRepository.FindAllAsync(x => !x.IsDeleted);
        }

        public async Task<IEnumerable<Article>> GetAllVisibleAsync()
        {
            //return await _unitOfWork.ArticleRepository.FindAllAsync(x => x.IsVisible && !x.IsDeleted);
            return await _unitOfWork.ArticleRepository.Query.Where(x => x.IsVisible && x.IsDeleted == false).OrderByDescending(y => y.CreatedDate).ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetAllByUserEmailAsync(string email)
        {
            //return await _unitOfWork.ArticleRepository.FindAllAsync(x => x.CreatedBy == email && !x.IsDeleted);
            return await _unitOfWork.ArticleRepository.Query.Where(x => x.CreatedBy == email && x.IsDeleted == false).OrderByDescending(y => y.CreatedDate).ToListAsync();
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            return await _unitOfWork.ArticleRepository.FindAsync(x => x.ArticleId == id);
        }

        public async Task UpdateAsync(Article article)
        {
            await _unitOfWork.ArticleRepository.UpdateAsync(article);
        }

        public async Task<IEnumerable<Article>> GetAllRemovedAsync()
        {
            return await _unitOfWork.ArticleRepository.FindAllAsync(x => x.IsDeleted);
        }
    }
}

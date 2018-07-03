using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.Database.Models;
using WebBlog.Services.IServices;

using Microsoft.EntityFrameworkCore;

namespace WebBlog.Services.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }       

        public bool CommentExists(int id)
        {
            return _unitOfWork.CommentRepository.Query.Any(c => c.CommentId == id);
        }

        public async Task CreateAsync(Comment comment)
        {
            await _unitOfWork.CommentRepository.InsertAsync(comment);
        }

        public async Task DeleteAsync(Comment comment)
        {
            await _unitOfWork.CommentRepository.DeleteAsync(comment);
        }

        public IOrderedQueryable<Comment> GetAllOrderByCreatedBy()
        {
            return _unitOfWork.CommentRepository.Query.AsNoTracking().Where(x=>!x.IsDeleted).OrderBy(p => p.CreatedBy);
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _unitOfWork.CommentRepository.FindAllAsync(x => !x.IsDeleted);
        }

        public async Task<IEnumerable<Comment>> GetAllByArticleIdAsync(int articleId)
        {
            return await _unitOfWork.CommentRepository.Query.Where(x => x.ArticleId == articleId && x.IsDeleted == false).OrderByDescending(y => y.CreatedDate).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllRemovedAsync()
        {
            return await _unitOfWork.CommentRepository.FindAllAsync(x => x.IsDeleted);
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _unitOfWork.CommentRepository.FindAsync(x => x.CommentId == id);
        }

        public async Task UpdateAsync(Comment comment)
        {
            await _unitOfWork.CommentRepository.UpdateAsync(comment);
        }
    }
}

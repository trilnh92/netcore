using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBlog.Database.Data;
using WebBlog.Database.Models;
using WebBlog.Services.IServices;

namespace WebBlog.Areas.Administrator.Controllers
{
    [Authorize]
    [Area("Administrator")]
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentServicce;

        public CommentsController(ICommentService commentServicce)
        {
            _commentServicce = commentServicce;
        }

        // GET: Administrator/Comments
        public async Task<IActionResult> Index()
        {
            return View(await _commentServicce.GetAllAsync());
        }

        // GET: Administrator/Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _commentServicce.GetByIdAsync(id.Value);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Administrator/Comments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
     [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Type,ParentId,CreatedDate,CreatedBy,IsDeleted,DeletedDate,DeletedBy,UpdatedDate,UpdatedBy,IsApproved,Content,Ext,ArticleId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                await _commentServicce.CreateAsync(comment);
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        // GET: Administrator/Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _commentServicce.GetByIdAsync(id.Value);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: Administrator/Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
     [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentId,Type,ParentId,CreatedDate,CreatedBy,IsDeleted,DeletedDate,DeletedBy,UpdatedDate,UpdatedBy,IsApproved,Content,Ext,ArticleId")] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _commentServicce.UpdateAsync(comment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.CommentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        // GET: Administrator/Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _commentServicce.GetByIdAsync(id.Value);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Administrator/Comments/Delete/5
        [HttpPost, ActionName("Delete")]
     [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _commentServicce.GetByIdAsync(id);
            comment.IsDeleted = true;
            await _commentServicce.UpdateAsync(comment);

            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _commentServicce.CommentExists(id);
        }
    }
}

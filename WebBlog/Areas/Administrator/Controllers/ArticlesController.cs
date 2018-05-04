using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBlog.Common;
using WebBlog.Database.Data;
using WebBlog.Database.Models;
using WebBlog.Services.IServices;

namespace WebBlog.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly WebBlogDbContext _context;

        public ArticlesController(IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
        }

        // GET: Administrator/Articles
        public async Task<IActionResult> Index()
        {
            var result = await _articleService.GetAllAsync();
            return View(result);
        }

        // GET: Administrator/Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .SingleOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Administrator/Articles/Create
        public IActionResult Create()
        {
            var categoryList = new List<SelectListItem>();
            _categoryService.GetAllByLanguage(Constants.DefaultLanguage).ToList().ForEach(c => {
                categoryList.Add(
                    new SelectListItem()
                    {
                        Text = c.Name,
                        Value = c.CategoryId.ToString()
                    });
            });
            ViewBag.CategoryList = categoryList;

            return View();
        }

        // POST: Administrator/Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,CategoryArticleId,CategoryArticleName,ViewCount,Image,Title,BriefContent,FullContent,Source,Index,IsVisible,SEName,SEOTitle,SEODescription,SEOKeywords,IsDeleted,IsHot,DeletedDate,DeletedBy,UpdatedDate,UpdatedBy,CreatedBy,CreatedDate,Position,Ext,Ext1,Ext2,Ext3")] Article article)
        {
            if (ModelState.IsValid)
            {
                await _articleService.CreateAsync(article);
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Administrator/Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoryList = new List<SelectListItem>();
            _categoryService.GetAllByLanguage(Constants.DefaultLanguage).ToList().ForEach(c => {
                categoryList.Add(
                    new SelectListItem()
                    {
                        Text = c.Name,
                        Value = c.CategoryId.ToString()
                    });
            });
            ViewBag.CategoryList = categoryList;

            var article = await _articleService.GetByIdAsync(id.Value);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: Administrator/Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,CategoryArticleId,CategoryArticleName,ViewCount,Image,Title,BriefContent,FullContent,Source,Index,IsVisible,SEName,SEOTitle,SEODescription,SEOKeywords,IsDeleted,IsHot,DeletedDate,DeletedBy,UpdatedDate,UpdatedBy,CreatedBy,CreatedDate,Position,Ext,Ext1,Ext2,Ext3")] Article article)
        {
            if (id != article.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _articleService.UpdateAsync(article);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.ArticleId))
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
            return View(article);
        }

        // GET: Administrator/Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _articleService.GetByIdAsync(id.Value);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Administrator/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _articleService.GetByIdAsync(id);
            article.IsDeleted = true;
            await _articleService.UpdateAsync(article);
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.ArticleId == id);
        }
    }
}

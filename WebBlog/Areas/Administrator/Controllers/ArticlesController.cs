using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebBlog.Common;
using WebBlog.Database.Data;
using WebBlog.Database.Models;
using WebBlog.Services.IServices;

namespace WebBlog.Areas.Administrator.Controllers
{
    [Authorize]
    [Area("Administrator")]
    public class ArticlesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;        
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger _logger;

        public ArticlesController(IArticleService articleService, ICategoryService categoryService, UserManager<ApplicationUser> userManager, ILogger<ArticlesController> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _articleService = articleService;
            _categoryService = categoryService;
            _logger = logger;
            _configuration = configuration;
        }

        // GET: Administrator/Articles
        public async Task<IActionResult> Index()
        {
            var result = await _articleService.GetAllAsync();
            ViewBag.ClientUrl = _configuration["ClientUIUrl"].ToString();
            return View(result);
        }

        // GET: Administrator/Articles/Details/5
        public async Task<IActionResult> Details(int? id)
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
        public async Task<IActionResult> Create([Bind("ArticleId,CategoryArticleId,CategoryArticleName,ViewCount,Image,Title,BriefContent,FullContent,Source,Index,IsVisible,SEName,SEOTitle,SEODescription,SEOKeywords,IsDeleted,IsHot,DeletedDate,DeletedBy,UpdatedDate,UpdatedBy,CreatedBy,CreatedDate,Position,Ext,Ext1,Ext2,Ext3,PresentImage")] ArticleViewModel article)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    _logger.LogWarning("User is NULL");
                    throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }                

                article.CreatedBy = article.UpdatedBy =  user.Email;

                var file = article.PresentImage;

                if (file != null && file.Length != 0)
                {
                    var path = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot", "Articles", "Uploaded");

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                    var physicalFileName = Path.Combine(path, fileName);

                    using (var stream = new FileStream(physicalFileName, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var serverUrl = _configuration["ServerAdminUrl"].ToString();
                    var imageSrc = string.Format("{0}{1}/{2}/{3}", serverUrl, "Articles", "Uploaded", fileName);

                    article.Image = imageSrc;
                }

                var createArticle = new Article()
                {
                    ArticleId = article.ArticleId,
                    BriefContent = article.BriefContent,
                    CategoryArticleId = article.CategoryArticleId,
                    CategoryArticleName = article.CategoryArticleName,
                    CreatedBy = article.CreatedBy,
                    CreatedDate = DateTime.Now,
                    DeletedBy = article.DeletedBy,
                    DeletedDate = article.DeletedDate,
                    Ext = article.Ext,
                    Ext1 = article.Ext1,
                    Ext2 = article.Ext2,
                    Ext3 = article.Ext3,
                    FullContent = article.FullContent,
                    Image = article.Image,
                    Index = article.Index,
                    IsDeleted = article.IsDeleted,
                    IsHot = article.IsHot,
                    IsVisible = article.IsVisible,
                    Position = article.Position,
                    SEName = article.SEName,
                    SEODescription = article.SEODescription,
                    SEOKeywords = article.SEOKeywords,
                    SEOTitle = article.SEOTitle,
                    Source = article.Source,
                    Title = article.Title,
                    UpdatedBy = article.UpdatedBy,
                    UpdatedDate = DateTime.Now,
                    ViewCount = article.ViewCount
                };
                
                await _articleService.CreateAsync(createArticle);
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

            var updateArticle = new ArticleViewModel()
            {
                ArticleId = article.ArticleId,
                BriefContent = article.BriefContent,
                CategoryArticleId = article.CategoryArticleId,
                CategoryArticleName = article.CategoryArticleName,
                CreatedBy = article.CreatedBy,
                CreatedDate = article.CreatedDate,
                DeletedBy = article.DeletedBy,
                DeletedDate = article.DeletedDate,
                Ext = article.Ext,
                Ext1 = article.Ext1,
                Ext2 = article.Ext2,
                Ext3 = article.Ext3,
                FullContent = article.FullContent,
                Image = article.Image,
                Index = article.Index,
                IsDeleted = article.IsDeleted,
                IsHot = article.IsHot,
                IsVisible = article.IsVisible,
                Position = article.Position,
                SEName = article.SEName,
                SEODescription = article.SEODescription,
                SEOKeywords = article.SEOKeywords,
                SEOTitle = article.SEOTitle,
                Source = article.Source,
                Title = article.Title,
                UpdatedBy = article.UpdatedBy,
                UpdatedDate = article.UpdatedDate,
                ViewCount = article.ViewCount
            };
            return View(updateArticle);
        }

        // POST: Administrator/Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
     [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,CategoryArticleId,CategoryArticleName,ViewCount,Image,Title,BriefContent,FullContent,Source,Index,IsVisible,SEName,SEOTitle,SEODescription,SEOKeywords,IsDeleted,IsHot,DeletedDate,DeletedBy,UpdatedDate,UpdatedBy,CreatedBy,CreatedDate,Position,Ext,Ext1,Ext2,Ext3, PresentImage")] ArticleViewModel article)
        {
            if (id != article.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user == null)
                    {
                        _logger.LogWarning("User is NULL");
                        throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                    }

                    article.UpdatedBy = user.Email;

                    var file = article.PresentImage;

                    if (file != null && file.Length != 0)
                    {
                        var path = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot", "Articles", "Uploaded");

                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                        var physicalFileName = Path.Combine(path, fileName);

                        using (var stream = new FileStream(physicalFileName, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var serverUrl = _configuration["ServerAdminUrl"].ToString();
                        var imageSrc = string.Format("{0}{1}/{2}/{3}", serverUrl, "Articles", "Uploaded", fileName);

                        article.Image = imageSrc;
                    }

                    var updateArticle = new Article()
                    {
                        ArticleId = article.ArticleId,
                        BriefContent = article.BriefContent,
                        CategoryArticleId = article.CategoryArticleId,
                        CategoryArticleName = article.CategoryArticleName,
                        CreatedBy = article.CreatedBy,
                        CreatedDate = article.CreatedDate,
                        DeletedBy = article.DeletedBy,
                        DeletedDate = article.DeletedDate,
                        Ext = article.Ext,
                        Ext1 = article.Ext1,
                        Ext2 = article.Ext2,
                        Ext3 = article.Ext3,
                        FullContent = article.FullContent,
                        Image = article.Image,
                        Index = article.Index,
                        IsDeleted = article.IsDeleted,
                        IsHot = article.IsHot,
                        IsVisible = article.IsVisible,
                        Position = article.Position,
                        SEName = article.SEName,
                        SEODescription = article.SEODescription,
                        SEOKeywords = article.SEOKeywords,
                        SEOTitle = article.SEOTitle,
                        Source = article.Source,
                        Title = article.Title,
                        UpdatedBy = article.UpdatedBy,
                        UpdatedDate = DateTime.Now,
                        ViewCount = article.ViewCount
                    };

                    await _articleService.UpdateAsync(updateArticle);
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
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("User is NULL");
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var article = await _articleService.GetByIdAsync(id);
            article.IsDeleted = true;
            article.DeletedBy = user.Email;
            article.DeletedDate = DateTime.Now;

            await _articleService.UpdateAsync(article);
            return RedirectToAction(nameof(Index));
        }

        // GET: Administrator/Articles/Recycle
        public async Task<IActionResult> Recycle()
        {
            var result = await _articleService.GetAllRemovedAsync();
            return View(result);
        }

        // POST: Administrator/Articles/Restore/5
        [HttpGet, ActionName("Restore")]        
        public async Task<IActionResult> Restore(int id)
        {
            var article = await _articleService.GetByIdAsync(id);
            article.IsDeleted = false;
            await _articleService.UpdateAsync(article);
            return RedirectToAction(nameof(Index));
        }

        // POST: Administrator/Articles/Delete/5
        [HttpGet, ActionName("DeleteForever")]        
        public async Task<IActionResult> DeleteForever(int id)
        {
            var article = await _articleService.GetByIdAsync(id);            
            await _articleService.DeleteAsync(article);
            return RedirectToAction(nameof(Recycle));
        }

        private bool ArticleExists(int id)
        {
            return _articleService.ArticleExists(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using WebBlog.Common;
using WebBlog.Database.Data;
using WebBlog.Database.Models;
using WebBlog.Services.IServices;

namespace WebBlog.Areas.Administrator.Controllers
{
    [Authorize]
    [Area("Administrator")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Administrator/Categories
        public async Task<IActionResult> Index(int page = 1, string sortExpression = "Name")
        {
            var qry = _categoryService.GetAllOrderByName();
            var model = await PagingList.CreateAsync(qry, 10, page, sortExpression, "Name");
            return View(model);
        }

        // GET: Administrator/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetByIdAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Administrator/Categories/Create
        public IActionResult Create()
        {
            ViewBag.Type = EnumCategoryType.ARTICLE;
            ViewBag.Language = Constants.DefaultLanguage;
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

        // POST: Administrator/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
     [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,ParentId,ParentName,Name,Type,Language,Description,Image,Url,HasChildren,HasUrl,Index")] Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Administrator/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetByIdAsync(id.Value);

            ViewBag.Type = EnumCategoryType.ARTICLE;
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

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Administrator/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
     [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,ParentId,ParentName,Name,Type,Language,Description,Image,Url,HasChildren,HasUrl,Index")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateAsync(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            return View(category);
        }

        // GET: Administrator/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetByIdAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Administrator/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
     [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            await _categoryService.DeleteAsync(category);
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _categoryService.CategoryExists(id);
        }
    }
}

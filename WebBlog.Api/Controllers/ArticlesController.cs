using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebBlog.Database.Data;
using WebBlog.Database.Models;
using WebBlog.Database.Models.UserViewModels;
using WebBlog.Services.IServices;

namespace WebBlog.Api.Controllers
{
    [EnableCors("AllowAllHeaders")]
    [Produces("application/json")]
    [Route("api/Articles")]
    public class ArticlesController : Controller
    {        
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IConfiguration _configuration;

        public ArticlesController(IArticleService articleService, ICategoryService categoryService, IConfiguration configuration)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _configuration = configuration;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<IEnumerable<Article>> Index()
        {
            return await _articleService.GetAllAsync();
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle([FromRoute] int id)        
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var article = await _articleService.GetByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);            
        }

        // PUT: api/Articles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle([FromRoute] int id, [FromBody] Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != article.ArticleId)
            {
                return BadRequest();
            }

            try
            {
                await _articleService.UpdateAsync(article);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Articles
        [HttpPost]
        public async Task<IActionResult> PostArticle([FromBody] CreateArticleViewModel createArticle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            //var tags = string.Empty;
            //createArticle.Tags?.ToList().ForEach(x => {
            //    tags += (x + ",");
            //});
            //if (tags.Length > 1)
            //    tags = tags.Substring(0, tags.Length - 1);

            var tags = createArticle.Tags;

            var article = new Article()
            {
                BriefContent = createArticle.FullContent.Length > 50 ? createArticle.FullContent.Substring(0, 50) : "",
                CategoryArticleId = -1, //No category
                CreatedBy = createArticle.CreatedBy,
                CreatedDate = DateTime.Now,
                Ext = tags,
                FullContent = createArticle.FullContent,
                Image = createArticle.Image,
                IsDeleted = false,
                IsHot = false,
                IsVisible = true,
                Title = createArticle.Title,
                UpdatedBy = createArticle.CreatedBy,
                UpdatedDate = DateTime.Now
            };

            try
            {
                await _articleService.CreateAsync(article);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }

        [HttpPost]
        [Route("GetArticlesByUser")]
        public async Task<IEnumerable<Article>> GetArticlesByUser([FromBody] UserViewModel user)
        {
            var email = user.Email;
            return await _articleService.GetAllByUserEmailAsync(email);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var article = await _articleService.GetByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            article.IsDeleted = true;
            await _articleService.UpdateAsync(article);

            return Ok(article);
        }

        [HttpPost]
        [Route("UploadPhoto")]
        public async Task<object> UploadPhoto()
        {
            var file = Request.Form.Files[0];
            var imagePath = string.Empty;
            if (file != null && file.Length != 0)
            {
                try
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

                    var serverUrl = _configuration["ServerAPIUrl"].ToString();
                    var imageSrc = string.Format("{0}{1}/{2}/{3}", serverUrl, "Articles", "Uploaded", fileName);

                    imagePath = imageSrc;

                    return Json(new
                    {
                        data = imagePath,
                        success = true
                    });
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        errorMessage = ex.Message,
                        success = false
                    });
                }
            }
            else
            {
                return Json(new
                {
                    errorMessage = "File is NULL",
                    success = false
                });
            }          
        }

        private bool ArticleExists(int id)
        {
            return _articleService.ArticleExists(id);
        }
    }
}
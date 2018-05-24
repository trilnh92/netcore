using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBlog.Database.Data;
using WebBlog.Database.Models;
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

        public ArticlesController(IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
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
        public async Task<IActionResult> PostArticle([FromBody] Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await _articleService.CreateAsync(article);            

            return CreatedAtAction("GetArticle", new { id = article.ArticleId }, article);
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

        private bool ArticleExists(int id)
        {
            return _articleService.ArticleExists(id);
        }
    }
}
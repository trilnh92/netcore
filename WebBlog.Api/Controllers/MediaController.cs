using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBlog.Database.Data;
using WebBlog.Database.Models;

namespace WebBlog.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Media")]
    public class MediaController : Controller
    {
        private readonly WebBlogDbContext _context;

        public MediaController(WebBlogDbContext context)
        {
            _context = context;
        }

        // GET: api/Media
        [HttpGet]
        public IEnumerable<Media> GetMedias()
        {
            return _context.Medias;
        }

        // GET: api/Media/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedia([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var media = await _context.Medias.SingleOrDefaultAsync(m => m.MediaId == id);

            if (media == null)
            {
                return NotFound();
            }

            return Ok(media);
        }

        // PUT: api/Media/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedia([FromRoute] int id, [FromBody] Media media)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != media.MediaId)
            {
                return BadRequest();
            }

            _context.Entry(media).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MediaExists(id))
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

        // POST: api/Media
        [HttpPost]
        public async Task<IActionResult> PostMedia([FromBody] Media media)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Medias.Add(media);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedia", new { id = media.MediaId }, media);
        }

        // DELETE: api/Media/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedia([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var media = await _context.Medias.SingleOrDefaultAsync(m => m.MediaId == id);
            if (media == null)
            {
                return NotFound();
            }

            _context.Medias.Remove(media);
            await _context.SaveChangesAsync();

            return Ok(media);
        }

        private bool MediaExists(int id)
        {
            return _context.Medias.Any(e => e.MediaId == id);
        }
    }
}
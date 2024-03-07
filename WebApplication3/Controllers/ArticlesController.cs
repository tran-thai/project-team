using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;
using WebApplication3.Models.Services;

namespace WebApplication3.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly Web01Context _context;
        private readonly ISearch _search;
        private readonly ICommentsService _commentsService;



        public ArticlesController(Web01Context context, ISearch search, ICommentsService commentsService)
        {
            _context = context;
            _search = search;
            _commentsService = commentsService;
        }

        // GET: Articles
        /*public async Task<IActionResult> Index()
        {
            var web01Context = _context.Articles.Include(a => a.Author).Include(a => a.Category);
            return View(await web01Context.ToListAsync());
        }*/

        /*public async Task<IActionResult> Index(string searchString)
        {
            var applicationDbContext = _search.GetAll();

            
            if (!string.IsNullOrEmpty(searchString))
            {
                applicationDbContext = applicationDbContext.Where(a => a.Title.Contains(searchString));
            }
            var listings = applicationDbContext.ToList();

            return View(listings);
        }*/

        public async Task<IActionResult> Index(string searchString)
        {
            var applicationDbContext = _search.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                // Kiểm tra searchString có phải là số không
                if (int.TryParse(searchString, out int authorId))
                {
                    // Tìm kiếm theo author Id trước
                    applicationDbContext = applicationDbContext.Where(a => a.AuthorId == authorId);
                }
                else
                {
                    // Nếu không phải là author id, sẽ tìm kiếm theo Title
                    applicationDbContext = applicationDbContext.Where(a => a.Title.Contains(searchString));
                }
            }

            var listings = applicationDbContext.ToList();

            return View(listings);
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Author)
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Users, "UserId", "UserId");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,Title,Content,AuthorId,CategoryId")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "UserId", "UserId", article.AuthorId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", article.CategoryId);
            return View(article);
        }

        //Comment
        [HttpPost]
        public async Task<ActionResult> AddComment([Bind("CommentId, CommentContent, UserId, ArticleId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                await _commentsService.Add(comment);
            }
            var listing = await _search.GetById(comment.ArticleId);
            return View("Details", listing);


        }

        

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "UserId", "UserId", article.AuthorId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", article.CategoryId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,Title,Content,AuthorId,CategoryId")] Article article)
        {
            if (id != article.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
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
            ViewData["AuthorId"] = new SelectList(_context.Users, "UserId", "UserId", article.AuthorId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", article.CategoryId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Author)
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Articles == null)
            {
                return Problem("Entity set 'Web01Context.Articles'  is null.");
            }
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
          return (_context.Articles?.Any(e => e.ArticleId == id)).GetValueOrDefault();
        }
    }
}

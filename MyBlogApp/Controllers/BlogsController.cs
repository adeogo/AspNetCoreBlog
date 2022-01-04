using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlogApp.Data;
using MyBlogApp.Models;


namespace MyBlogApp.Controllers
{

    public class BlogsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public BlogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Blogs
        public async Task<IActionResult> Index()
        {

            string UserId = "";

            if (User.Identity.IsAuthenticated)
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }


            ViewData["UserId"] = UserId;
            return View(await _context.Blogs
                                        .Where(b => b.IsDraft.Equals("no"))
                                        .Include(b => b.User)
                                        .Include(b => b.Category)
                                        .OrderByDescending(b => b.BlogId)
                                        .ToListAsync());

        }


        [HttpGet("category/{slug}")]
        public IActionResult Category(string slug)
        {
            return Ok(slug);
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            string UserId = "";

            if (User.Identity.IsAuthenticated)
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            return View(blog);
        }

        // GET: Blogs/Create
        [Authorize]
        public IActionResult Create()
        {

            IEnumerable<Category> categories = _context.Categories.ToList();


            ViewBag.Categories = categories;

            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,IsDraft, CategoryId")] Blog blog)
        {
            if (ModelState.IsValid)
            {

                String UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                String CreatedDate = DateTime.Now.ToString("dd, MMMM yyyy");

                blog.UserId = UserId;
                blog.CreatedDate = CreatedDate;

                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            IEnumerable<Category> categories = _context.Categories.ToList();


            ViewBag.Categories = categories;

            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Blog blog)
        {
            if (id == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Blog blogInDb = _context.Blogs.Where(b => b.BlogId == id).FirstOrDefault();

                    blogInDb.Title = blog.Title;
                    blogInDb.Content = blog.Content;
                    blogInDb.CategoryId = blog.CategoryId;
                    blogInDb.IsDraft = blog.IsDraft;



                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.BlogId))
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
            return View(blog);
        }


        // GET: Blogs/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.BlogId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlogApp.Data;
using MyBlogApp.Models;

namespace MyBlogApp.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/


        public IActionResult Index()
        {
            IEnumerable<Category> categories = _context.Categories;

            return View(categories);
        }

        [Authorize]

        public IActionResult Create(string slug)
        {
            Category category = null;

            if (slug != null)
            {
                category = _context.Categories.Where(c => c.Slug.Equals(slug)).FirstOrDefault();


            }

            return View(category);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateCategory(Category category)
        {

            if (ModelState.IsValid)
            {

                string categoryName = category.CategoryName;

                categoryName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(categoryName);

                // if exists

                Category dbCategory = _context.Categories.Where(b => b.CategoryName.Equals(categoryName)).FirstOrDefault();

                category.Slug = category.Slug.ToLower();




                if (dbCategory != null)
                {
                    ViewData["errMsg"] = "Category Name already exists";
                    return View("Create");

                }
                else
                {
                    _context.Add(category);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }





            }

            return View("Create");
        }


        [Authorize]
        [HttpGet("categories/edit/{slug?}")]
        public IActionResult Edit(string slug)
        {
            Category category = null;

            if (slug != null)
            {
                category = _context.Categories.Where(c => c.Slug.Equals(slug)).FirstOrDefault();


            }

            return View(category);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditCategory(Category category)
        {

            if (ModelState.IsValid)
            {

                string categoryName = category.CategoryName;

                categoryName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(categoryName);

                category.Slug = category.Slug.ToLower();

                if (category.Id != 0)
                {
                    try
                    {
                        _context.Categories.Update(category);
                        _context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CategoryExists(category.Id))
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

            }

            return View();
        }


        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }


}

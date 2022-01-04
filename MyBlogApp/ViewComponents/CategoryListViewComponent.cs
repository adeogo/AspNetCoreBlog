using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlogApp.Data;

namespace MyBlogApp.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {

        private readonly ApplicationDbContext _context;

        public CategoryListViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories.ToListAsync();

            return View("NavbarDropdown", categories);
        }
    }
}

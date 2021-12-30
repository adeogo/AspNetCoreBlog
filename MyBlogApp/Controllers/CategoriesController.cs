using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MyBlogApp.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: /<controller>/


        public IActionResult Index()
        {
            return View();
        }
    }
}

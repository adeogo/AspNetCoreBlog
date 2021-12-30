using System;
namespace MyBlogApp.Models
{
    public class Category
    {

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }

        public Category()
        {
        }
    }
}

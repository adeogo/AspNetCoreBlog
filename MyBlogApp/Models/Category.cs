using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlogApp.Models
{
    [Table("Categories")]
    public class Category
    {

        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }

        public Category()
        {
        }
    }
}

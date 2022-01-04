using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlogApp.Models
{
    [Table("Categories")]
    public class Category
    {

        [Key]
        public int Id { get; set; }

        [DisplayName("Category")]
        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string Slug { get; set; }

        public Category()
        {
        }


    }
}

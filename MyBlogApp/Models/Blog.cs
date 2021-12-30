using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MyBlogApp.Models
{
    public class Blog
    {

        public int BlogId { get; set; }

        [ForeignKey("AspNetUsers")]
        [DisplayName("Created By")]
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string IsDraft { get; set; }

        [ForeignKey("Categories")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        [DisplayName("Date")]
        public string CreatedDate { get; set; }

        public virtual IdentityUser User { get; set; }

        public Blog()
        {
        }
    }
}

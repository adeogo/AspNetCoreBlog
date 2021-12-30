using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlogApp.Models
{

    [Table("AspNetUsers")]
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }


        public User()
        {
        }
    }
}

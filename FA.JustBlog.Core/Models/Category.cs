using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.JustBlog.Core.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(255)]
        public string CategoryName { get; set; }

        [StringLength(255)]
        public string UrlSlug { get; set; }
        
        [StringLength(1024)]
        public string Description { get; set; }

        public virtual IList<Post> Posts { get; set; }
    }
}





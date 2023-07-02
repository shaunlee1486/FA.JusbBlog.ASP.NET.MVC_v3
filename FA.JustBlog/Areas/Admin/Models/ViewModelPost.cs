using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FA.JustBlog.Areas.Admin.Models
{
    public class ViewModelPost
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title name is required.")]
        [StringLength(255)]
        [Display(Name = "Post title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Short Description is required.")]
        [StringLength(1024)]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Content")]
        public string PostContent { get; set; }

        [StringLength(255)]
        [Display(Name = "Url")]
        public string UrlSlug { get; set; }

        [Display(Name = "Is Published")]
        public bool Published { get; set; }

        [Display(Name = "Posted On")]
        public DateTime PostedOn { get; set; }

        public DateTime? Modified { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string[]  TagMulti { get; set; }

       
    }
}
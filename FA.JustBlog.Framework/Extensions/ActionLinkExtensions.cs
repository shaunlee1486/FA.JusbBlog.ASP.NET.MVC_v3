using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FA.JustBlog.Framework.Extensions
{
    public static class ActionLinkExtensions
    {
        public static MvcHtmlString PostLink(this HtmlHelper helper, Post post)
        {
            var linkPost = helper.ActionLink(post.Title, "Post", "Blog", 
                new { year = post.PostedOn.Year, month = post.PostedOn.Month, title = post.UrlSlug }, 
                new { title = post.Title});

            return linkPost;
        }

        public static MvcHtmlString CategoryLink(this HtmlHelper helper, Category category)
        {
            var linkCategory = helper.ActionLink(category.CategoryName, "Category", "Post",
                new { category = category.UrlSlug },
                new { title = string.Format("See all posts in {0}", category.CategoryName) });

            return linkCategory;
        }

        public static MvcHtmlString TagLink(this HtmlHelper helper, Tag tag)
        {
            var linkTag = helper.ActionLink(tag.Name, "Tag", "Post",
                new { title = string.Format("See all posts in {0}", tag.Name), style = "color: #ffffff" });

            return linkTag;
        }
    }
}

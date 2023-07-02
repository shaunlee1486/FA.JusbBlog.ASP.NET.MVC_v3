using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Mvc;

namespace FA.JustBlog.Framework.Extensions
{
    public static class TagExtensions
    {
        public static MvcHtmlString PostTags(this HtmlHelper helper, Post post)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Tag: ");
            foreach (Tag tag in post.Tags)
            {
                stringBuilder.AppendFormat("<span class='w3-tag w3-margin-bottom w3-padding'>{0}</span>&nbsp;", helper.TagLink(tag));
            }

            return new MvcHtmlString(stringBuilder.ToString());
        }
    }
}

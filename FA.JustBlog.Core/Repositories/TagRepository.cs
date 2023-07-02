using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.IRepositories;

namespace FA.JustBlog.Core.Repositories
{

    public class TagRepository: GenericRepository<Tag>, ITagRepository
    {
        public void DeleteTag(Tag tag)
        {
            var item = base.Context.Tags.Find(tag.Name);
            base.Context.Tags.Remove(item);
            base.Context.SaveChanges();
        }

        public Tag FindTag(string name)
        {
            return base.Context.Tags.Find(name);
        }

        public Tag GetTagByUrlSlug(string urlSlug)
        {
            var tag = base.Context.Tags.FirstOrDefault<Tag>(t => t.UrlSlug == urlSlug);
            return tag;
        }

    }
}

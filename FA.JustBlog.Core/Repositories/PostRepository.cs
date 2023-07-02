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
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public void AddPost(Post post)
        {
            base.Context.Posts.Add(post);
            post.Tags = new List<Tag>();
            if (!string.IsNullOrEmpty(post.TagValues))
            {
                var tags = post.TagValues.Split(new[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string tag in tags)
                {
                    var keyword = tag.Trim();
                    var tagObject = base.Context.Tags.Find(keyword);
                    if (tagObject == null)
                    {
                        var newTag = new Tag()
                        {
                            Name = keyword,
                            UrlSlug = keyword,
                            Count = 1,
                            Description = keyword
                        };
                        newTag = base.Context.Tags.Add(newTag);
                        post.Tags.Add(newTag);
                    }
                    else
                    {
                        tagObject.Count += 1;
                        base.Context.Entry(tagObject).State = EntityState.Modified;
                        post.Tags.Add(tagObject);
                    }
                }
            }

            base.Context.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            base.Context.Entry(post).State = EntityState.Modified;
            base.Context.SaveChanges();

            var newPost = Context.Posts.Include("Tags").FirstOrDefault(x=>x.Id == post.Id);
            newPost.Tags.Clear();

            if (!string.IsNullOrEmpty(post.TagValues))
            {
                var tags = post.TagValues.Split(new[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string tag in tags)
                {
                    var keyword = tag.Trim();
                    var tagObject = base.Context.Tags.Find(keyword);
                    if (tagObject == null)
                    {
                        var newTag = new Tag()
                        {
                            Name = keyword,
                            UrlSlug = keyword,
                            Count = 1,
                            Description = keyword
                        };
                        newTag = base.Context.Tags.Add(newTag);
                        newPost.Tags.Add(newTag);
                    }
                    else
                    {
                        tagObject.Count += 1;
                        base.Context.Entry(tagObject).State = EntityState.Modified;
                        newPost.Tags.Add(tagObject);
                    }
                }
            }
            base.Context.SaveChanges();
        } 

        public int CountPostsForCategory(string category)
        {
            return base.Context.Posts.Count(p => p.Category.CategoryName == category);
        }

        public int CountPostsForTag(string tag)
        {
            return base.Context.Posts.Count(p => p.Tags.Any(t => t.Name == tag));
        }

        public void DeletePost(Post post)
        {
            var item = base.Context.Posts.Find(post.Id);
            base.Context.Posts.Remove(item);
            base.Context.SaveChanges();
        }


        public Post FindPost(int year, int month, string urlSlug)
        {
            var post = base.Context.Posts.Include("Category").SingleOrDefault<Post>(p => p.Published && p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug == urlSlug);
            return post;
        }

        public IQueryable<Post> GetAllPosts()
        {
            return base.Context.Posts;
        }

        public IList<Post> GetPublisedPosts()
        {
            return base.Context.Posts.Where(p => p.Published).OrderByDescending(p => p.PostedOn).ToList();
        }

        public IList<Post> GetUnpublisedPosts()
        {
            return base.Context.Posts.Where(p => !p.Published).OrderByDescending(p => p.Modified).ToList();
        }

        public IList<Post> GetHighestPosts(int size)
        {
            return base.Context.Posts.OrderByDescending(p => p.Rate).Take(size).ToList();
        }

        public IList<Post> GetLatestPost(int size)
        {
            return base.Context.Posts.OrderByDescending(p => p.PostedOn).Take(size).ToList();
        }

        public IList<Post> GetMostViewedPost(int size)
        {
            return base.Context.Posts.OrderByDescending(p => p.ViewCount).Take(size).ToList();
        }

        public IList<Post> GetPostsByCategory(string category)
        {
            return base.Context.Posts.Where(p => p.Category.CategoryName == category).ToList();
        }

        public IList<Post> GetPostsByMonth(DateTime monthYear)
        {
            var fromDate = new DateTime(monthYear.Year, monthYear.Month, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);
            return base.Context.Posts.Where(p => p.PostedOn >= fromDate && p.PostedOn <= toDate).ToList();
        }

        public IList<Post> GetPostsByTag(string tag)
        {
            return base.Context.Posts.Where(p => p.Tags.Any(t => t.UrlSlug == tag)).ToList();
        }

    }
}

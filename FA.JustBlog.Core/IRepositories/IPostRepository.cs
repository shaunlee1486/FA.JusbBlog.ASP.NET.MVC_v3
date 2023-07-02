using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.IRepositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        //Post FindPost(int postId);
        //void AddPost(Post post);
        //void UpdatePost(Post post);
        //void DeletePost(int postId);
        //IList<Post> GetAllPosts();

        //
        void DeletePost(Post post);
        void AddPost(Post post);
        void UpdatePost(Post post);
        IQueryable<Post> GetAllPosts();
        Post FindPost(int year, int month, string urlSlug);
        IList<Post> GetPublisedPosts();
        IList<Post> GetUnpublisedPosts();
        IList<Post> GetLatestPost(int size);
        IList<Post> GetPostsByMonth(DateTime monthYear);
        int CountPostsForCategory(string category);
        IList<Post> GetPostsByCategory(string category);
        int CountPostsForTag(string tag);
        IList<Post> GetPostsByTag(string tag);
        IList<Post> GetMostViewedPost(int size);
        IList<Post> GetHighestPosts(int size);
    }
}

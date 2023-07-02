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

    public class CommentRepository: GenericRepository<Comment>, ICommentRepository
    {
        public void AddComment(Comment comment)
        {
            if(comment.CommentTime == null)
            {
                comment.CommentTime = DateTime.Now;
            }
            
            base.Context.Comments.Add(comment);
            base.Context.Entry(comment).State = EntityState.Added;
            base.Context.SaveChanges();
        }

        public void AddComment(int postId, string commentName, string commentEmail, string commentTitle, string commentBody)
        {
            //var post = postRepository.FindPost(postId);
            var post = base.Context.Posts.Find(postId);
            Comment comment = new Comment();
            comment.Name = commentName;
            comment.Email= commentEmail;
            comment.CommentHeader = commentTitle;
            comment.CommentText = commentBody;
            comment.Post = post;
            comment.CommentTime = DateTime.Now;
            base.Context.Comments.Add(comment);
            base.Context.SaveChanges();
        }

        public void DeleteComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public IList<Comment> GetCommentsForPost(int postId)
        {
            return base.Context.Posts.Find(postId).Comments.ToList();
        }

        public IList<Comment> GetCommentsForPost(Post post)
        {
            return post.Comments.ToList();
        }

    }
}

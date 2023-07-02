using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.IRepositories
{
    public interface ICommentRepository: IGenericRepository<Comment>
    {
        //Comment Find(int commentId);
        //void AddComment(Comment comment);       
        //void UpdateComment(Comment comment);
        //void DeleteComment(Comment comment);
        //void DeleteComment(int commendId);
        //IList<Comment> GetAllComments();

        //
        void AddComment(Comment comment);
        void AddComment(int postId, string commentName, string commentEmail, string commentTitle, string commentBody);
        IList<Comment> GetCommentsForPost(int postId);
        IList<Comment> GetCommentsForPost(Post post);
    }
}

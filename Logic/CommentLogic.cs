using DAL;
using Model.Models;
using MongoDB.Bson;
using System.Security.Policy;

namespace Logic
{
    public class CommentLogic
    {
        private readonly CommentDao commentDao = new CommentDao();
        private readonly TicketDao ticketDao = new TicketDao();
        private readonly UserDao userDao = new UserDao();

        public void AddComment(Comment comment)
        {
            commentDao.InsertComment(comment);
        }

        public List<Comment> GetCommentsForTicket(ObjectId ticketId)
        {
            return commentDao.GetCommentsByTicketId(ticketId);
        }

        public List<Comment> GetCommentsByIds(List<ObjectId> commentIds)
        {
            return commentDao.GetCommentsByIds(commentIds);
        }

    }
}
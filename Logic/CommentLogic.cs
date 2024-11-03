using DAL;
using Model.Models;
using MongoDB.Bson;

namespace Logic
{
    public class CommentLogic
    {
        private readonly CommentDao commentDao = new CommentDao();

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
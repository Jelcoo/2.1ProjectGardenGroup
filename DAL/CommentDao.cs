using Model.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DAL
{
    public class CommentDao : DAO
    {
        public void InsertComment(Comment comment)
        {
            Db.GetCollection<Comment>("comments").InsertOne(comment);
        }

        public List<Comment> GetCommentsByTicketId(ObjectId ticketId)
        {
            return Db.GetCollection<Comment>("comments")
                     .Find(c => c.ticketId == ticketId)
                     .ToList();
        }

        public List<Comment> GetCommentsByIds(List<ObjectId> commentIds)
        {
            return Db.GetCollection<Comment>("comments")
                     .Find(Builders<Comment>.Filter.In(c => c._id, commentIds))
                     .ToList();
        }
    }
}
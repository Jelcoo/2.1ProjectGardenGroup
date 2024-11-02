using Model.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CommentDao : DAO
    {
        public void InsertComment(Comment comment)
        {

            Db.GetCollection<Comment>("comments").InsertOne(comment);
        }

        //krijg comments bij id
        public List<Comment> GetCommentsByIds(List<ObjectId> commentIds)
        {
            return Db.GetCollection<Comment>("comments")
                .Find(Builders<Comment>.Filter.In(c => c._id, commentIds))
                .ToList();
        }

        public List<Comment> GetCommentsByTicketId(ObjectId ticketId)
        {
            var filter = Builders<Comment>.Filter.Eq(comment => comment.ticketId, ticketId);
            return Db.GetCollection<Comment>("comments").Find(filter).ToList();
        }
    }
}

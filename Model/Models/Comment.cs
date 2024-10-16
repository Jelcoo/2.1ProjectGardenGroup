using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class Comment
    {
        public string Message;
        public PartialUser CommentedBy;
        public DateTime CommentedAt;
    }
}

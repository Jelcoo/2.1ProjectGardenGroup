using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class Comment
    {
        public string message;
        public PartialUser commented_by;
        public DateTime commented_at;
    }
}

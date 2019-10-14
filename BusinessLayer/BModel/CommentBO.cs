using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BModel
{
    public class CommentBO
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? UserId { get; set; }
        public string Text { get; set; }

    }
}

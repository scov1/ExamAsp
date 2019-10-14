using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamAsp.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? UserId { get; set; }
        public string Text { get; set; }
    }
}
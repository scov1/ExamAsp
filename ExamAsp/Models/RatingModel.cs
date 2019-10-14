using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamAsp.Models
{
    public class RatingModel
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? Votes { get; set; }
    }
}
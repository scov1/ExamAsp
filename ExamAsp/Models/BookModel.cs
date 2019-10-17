using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamAsp.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public string Title { get; set; }
        public int? Pages { get; set; }
        public int? GenreId { get; set; }
        public byte[] ImageData { get; set; }


    }
}
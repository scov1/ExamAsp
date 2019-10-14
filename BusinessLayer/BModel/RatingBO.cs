using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BModel
{
    public class RatingBO
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? Votes { get; set; }

    }
}

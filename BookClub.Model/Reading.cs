using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Model
{
    public class Reading
    {
        
        public int Id { get; set; }

        public int ReaderId { get; set; }

        public int BookId { get; set; }

        public DateTime? Date { get; set; }

        public bool IsAudioBook { get; set; }

        public string Notes { get; set; }

        public virtual Book Book { get; set; }

        public virtual Reader Reader { get; set; }

    }
}

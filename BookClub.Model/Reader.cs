using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Model
{
    public class Reader
    {

        public Reader()
        {
            Readings = new List<Reading>();
        }
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(80)]
        public string Email { get; set; }

        [MaxLength(30)]
        public string Phone { get; set; }

        public virtual ICollection<Reading> Readings { get; set; }

    }
}

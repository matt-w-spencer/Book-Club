using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Model
{
    public class Author
    {
        public Author()
        {
            Books = new List<Book>();
        }

        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public DateTime? Dob { get; set; }

        [MaxLength(50)]
        public string Notes { get; set; }

        public string Country { get; set; }

        public virtual ICollection<Book> Books { get; set; }

    }
}

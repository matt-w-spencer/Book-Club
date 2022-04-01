using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Model
{
   public class Book
    {
        public Book()
        {
            Authors = new List<Author>();
            Readings = new List<Reading>();
        }
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        public short? PublicationYear { get; set; }

        [MaxLength(50)]
        public string Publisher { get; set; }

        public int? Pages { get; set; }

        [MaxLength(100)]
        public string RecordedBy { get; set; }

        [MaxLength(50)]
        public string AudioProvider { get; set; }


        public int? RecordingMinutes { get; set; }

        public string Notes { get; set; }

        public string First2Authors
        {
            get
            {
                switch (Authors.Count)
                {
                    case 0:
                        return $"N.A.";
                    case 1:
                        return $"{Authors[0].Name}";
                    case 2:
                        return $"{Authors[0].Name} and {Authors[1].Name}";
                    default:
                        return $"{Authors[0].Name}, {Authors[1].Name} et al.";
                }
            }
        }

        public string RecordingTime
        {
            get
            {
                if (RecordingMinutes  != null)
                {
                    int? x = RecordingMinutes;
                    return $"{(x/60).ToString()}h {(x%60).ToString()}min" ;
                }
                else
                {
                    return "N.A.";
                }

            }
        }

        public virtual IList<Author> Authors { get; set; }

        public virtual ICollection<Reading> Readings { get; set; }


    }
}

using Pri.Api.Music.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Music.Core.Entities
{
    public class Record : BaseEntity
    {
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public int? GenreId { get; set; }
        public Artist Artist { get; set; }
        public int? ArtistId { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public ICollection<Property> Properties { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}

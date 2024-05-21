using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Music.Core.Services.Models
{
    public class RecordCreateRequestModel
    {
        public string Title { get; set; }
        public int GenreId{ get; set; }
        public int ArtistId{ get; set; }
        public decimal Price { get; set; }
        public IEnumerable<int> PropertyIds{ get; set; }
        public string Image { get; set; }
    }
}

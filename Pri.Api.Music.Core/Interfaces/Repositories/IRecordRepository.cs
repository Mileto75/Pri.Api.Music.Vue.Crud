using Pri.CleanArchitecture.Music.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Music.Core.Interfaces.Repositories
{
    public interface IRecordRepository : IBaseRepository<Record>
    {
        Task<IEnumerable<Record>> GetRecordsByGenreIdAsync(int genreId);
        Task<IEnumerable<Record>> GetRecordsByArtistIdAsync(int artistId);
    }
}

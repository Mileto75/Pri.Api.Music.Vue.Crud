using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pri.CleanArchitecture.Music.Core.Entities;
using Pri.CleanArchitecture.Music.Core.Interfaces.Repositories;
using Pri.CleanArchitecture.Music.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Music.Infrastructure.Repositories
{
    public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(ApplicationDbContext applicationDbContext, ILogger<IBaseRepository<Artist>> logger) 
            : base(applicationDbContext, logger)
        {
        }

        public override async Task<Artist> GetByIdAsync(int id)
        {
            return await _table.
                Include(a => a.Records).FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}

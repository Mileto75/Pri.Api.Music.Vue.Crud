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
    public class RecordRepository : BaseRepository<Record>, IRecordRepository
    {
        public RecordRepository(ApplicationDbContext applicationDbContext, ILogger<IBaseRepository<Record>> logger) 
            : base(applicationDbContext, logger)
        {
        }

        public override IQueryable<Record> GetAll()
        {
            return _table
                .Include(r => r.Artist)
                .Include(r => r.Genre)
                .Include(r => r.Properties)
                .AsQueryable();
        }

        public async override Task<IEnumerable<Record>> GetAllAsync()
        {
            return await _table.Include(r => r.Artist)
                .Include(r => r.Genre)
                .Include(r => r.Properties)
                .ToListAsync();


        }

        public async override Task<Record> GetByIdAsync(int id)
        {
            return await _table.Include(r => r.Artist)
                .Include(r => r.Genre)
                .Include(r => r.Properties)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Record>> GetRecordsByArtistIdAsync(int artistId)
        {
            return await GetAll().Where(r => r.ArtistId == artistId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Record>> GetRecordsByGenreIdAsync(int genreId)
        {
            return await GetAll().Where(r => r.GenreId == genreId )
                .ToListAsync();
        }
    }
}

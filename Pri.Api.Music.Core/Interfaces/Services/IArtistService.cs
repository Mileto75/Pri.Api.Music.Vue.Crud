using Pri.CleanArchitecture.Music.Core.Entities;
using Pri.CleanArchitecture.Music.Core.Interfaces.Repositories;
using Pri.CleanArchitecture.Music.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Api.Music.Core.Interfaces.Services
{
    public interface IArtistService
    {
        Task<ResultModel<IEnumerable<Artist>>> GetAllAsync();
        Task<ResultModel<Artist>> GetByIdAsync(int id);
        Task<ResultModel<Artist>> CreateAsync(string name);
        Task<ResultModel<Artist>> DeleteAsync(int id);
        Task<ResultModel<Artist>> UpdateAsync(int id,string name);
    }
}

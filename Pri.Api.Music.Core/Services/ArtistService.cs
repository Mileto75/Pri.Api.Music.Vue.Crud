using Microsoft.EntityFrameworkCore;
using Pri.Api.Music.Core.Interfaces.Services;
using Pri.CleanArchitecture.Music.Core.Entities;
using Pri.CleanArchitecture.Music.Core.Interfaces.Repositories;
using Pri.CleanArchitecture.Music.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Api.Music.Core.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<ResultModel<Artist>> CreateAsync(string name)
        {
            var artist = new Artist()
            {
                Name = name
            };
            //check if artist exists
            if (await _artistRepository.GetAll().AnyAsync(a => a.Name.ToUpper().Equals(name.ToUpper())))
            {
                return new ResultModel<Artist>
                {
                    IsSucces = false,
                    Errors = new List<string> { "Artist name exists!" }
                };  
            }
            if(await _artistRepository.AddAsync(artist))
            {
                return new ResultModel<Artist>()
                {
                    IsSucces = true,
                    Value = artist
                };
            }
            return new ResultModel<Artist>()
            {
                IsSucces = false,
                Errors = new List<string>() { "Artist not saved!" }
            };
        }

        public async Task<ResultModel<Artist>> DeleteAsync(int id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            if(await _artistRepository.DeleteAsync(artist))
            {
                return new ResultModel<Artist>
                {
                    IsSucces = true,
                    Value = artist
                };
            }
            return new ResultModel<Artist>
            {
                IsSucces = false
            };
        }

        public async Task<ResultModel<IEnumerable<Artist>>> GetAllAsync()
        {
            var artists = await _artistRepository.GetAllAsync();
            if (artists.Count() > 0)
            {
                return new ResultModel<IEnumerable<Artist>>()
                {
                    IsSucces = true,
                    Value = artists
                };
            }
            return new ResultModel<IEnumerable<Artist>>()
            {
                IsSucces = false,
                Errors = new List<string>() { "No artists found!" }
            };
        }

        public async Task<ResultModel<Artist>> GetByIdAsync(int id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            if (artist == null)
            {
                return new ResultModel<Artist>
                {
                    IsSucces = false,
                    Errors = new List<string>() { "Artist not found!" }
                };
            }
            return new ResultModel<Artist>
            {
                IsSucces = true,
                Value = artist
            };
        }

        public async Task<ResultModel<Artist>> UpdateAsync(int id, string name)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            artist.Name = name;
            if(await _artistRepository.UpdateAsync(artist))
            {
                return new ResultModel<Artist> { IsSucces = true };
            }
            return new ResultModel<Artist> { IsSucces = false};
        }
    }
}

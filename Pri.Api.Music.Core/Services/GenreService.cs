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
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<ResultModel<IEnumerable<Genre>>> GetAllAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            if(genres.Count() > 0)
            {
                return new ResultModel<IEnumerable<Genre>>()
                { 
                    IsSucces = true,
                    Value = genres
                };
            }
            return new ResultModel<IEnumerable<Genre>>()
            {
                IsSucces = false,
                Errors = new List<string>() {"No genres found!"}
            };
        }
    }
}

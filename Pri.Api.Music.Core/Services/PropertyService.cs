using Pri.Api.Music.Core.Interfaces.Repositories;
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
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<ResultModel<IEnumerable<Property>>> GetAllAsync()
        {
            var properties = await _propertyRepository.GetAllAsync();
            if (properties.Count() > 0)
            {
                return new ResultModel<IEnumerable<Property>>()
                {
                    IsSucces = true,
                    Value = properties
                };
            }
            return new ResultModel<IEnumerable<Property>>()
            {
                IsSucces = false,
                Errors = new List<string>() { "No properties found!" }
            };
        }
    }
}

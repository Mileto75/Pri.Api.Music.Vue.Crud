using Pri.CleanArchitecture.Music.Core.Entities;
using Pri.CleanArchitecture.Music.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Api.Music.Core.Interfaces.Services
{
    public interface IPropertyService
    {
        Task<ResultModel<IEnumerable<Property>>> GetAllAsync();
    }
}

using Microsoft.Extensions.Logging;
using Pri.Api.Music.Core.Interfaces.Repositories;
using Pri.CleanArchitecture.Music.Core.Entities;
using Pri.CleanArchitecture.Music.Core.Interfaces.Repositories;
using Pri.CleanArchitecture.Music.Infrastructure.Data;
using Pri.CleanArchitecture.Music.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Api.Music.Infrastructure.Repositories
{
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(ApplicationDbContext applicationDbContext, ILogger<IBaseRepository<Property>> logger) : base(applicationDbContext, logger)
        {
        }
    }
}

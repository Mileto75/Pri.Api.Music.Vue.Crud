using Pri.CleanArchitecture.Music.Core.Entities;
using Pri.CleanArchitecture.Music.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Music.Core.Interfaces.Services
{
    public interface IRecordService
    {
        Task<ResultModel<IEnumerable<Record>>> GetAllAsync();
        Task<ResultModel<Record>> GetByIdAsync(int id);
        Task<ResultModel<IEnumerable<Record>>> SearchByTitleAsync(string title);
        Task<ResultModel<IEnumerable<Record>>> SearchByPropertyAsync(string name);
        Task<ResultModel<IEnumerable<Record>>> SearchByArtistAsync(string name);
        Task<ResultModel<IEnumerable<Record>>> GetRecordsByGenreIdAsync(int genreId);
        Task<ResultModel<Record>> CreateRecordAsync(RecordCreateRequestModel recordCreateRequestModel);
        Task<ResultModel<Record>> UpdateRecordAsync(RecordUpdateRequestModel recordUpdateRequestModel);
        Task<ResultModel<Record>> DeleteRecordAsync(int id);
        Task<bool> CheckIfExistsAsync(int id);
    }
}

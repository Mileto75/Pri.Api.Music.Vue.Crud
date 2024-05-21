using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Music.Core.Services.Models
{
    public class RecordUpdateRequestModel : RecordCreateRequestModel
    {
        public int Id { get; set; }
    }
}

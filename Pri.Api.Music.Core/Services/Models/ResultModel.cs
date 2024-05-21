using Pri.CleanArchitecture.Music.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Music.Core.Services.Models
{
    public class ResultModel<T>
    {
        public bool IsSucces { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public T Value { get; set; }
    }
}

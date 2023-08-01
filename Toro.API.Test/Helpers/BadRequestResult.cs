using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toro.API.Test.Helpers
{
    public record BadRequestResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}

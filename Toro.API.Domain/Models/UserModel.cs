using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toro.API.Domain.Models
{
    public class UserModel
    {
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}

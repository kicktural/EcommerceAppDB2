using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concreate
{
    public class User : AppUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhotoUrl { get; set; }
        public string? Address { get; set; }
    }
}

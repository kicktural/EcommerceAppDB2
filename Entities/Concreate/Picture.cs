﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concreate
{
    public class Picture
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

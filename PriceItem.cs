﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningWinFormsApp2
{
    class PriceItem
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; } = null;
        public bool Stock { get; set; }
    }
}

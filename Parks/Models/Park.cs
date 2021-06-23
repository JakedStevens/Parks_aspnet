﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parks.Models
{
	public class Park
	{  
        public string ParkId { get; set; }
        public string ParkName { get; set; }
        public string SanctuaryName { get; set; }
        public string Borough { get; set; }
        public double Acres { get; set; }
        public string Directions { get; set; }
        public string Description { get; set; }
        public string HabitatType { get; set; }

        //public Park (string parkid, string parkname, string sanctuar)
    }
}

﻿using System.Collections.Generic;

namespace Bus.Models
{
    public class Subroute
    {
        public string SubRouteUID { get; set; }
        public string SubRouteID { get; set; }
        public List<string> OperatorIDs { get; set; }
        public SubrouteName SubRouteName { get; set; }
        public string Headsign { get; set; }
        public int Direction { get; set; }
    }
}
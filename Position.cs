using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rosteras
{
    public class Position
    {
        public String name { get; set; }
        public String row { get; set; }

        public Position(String name, String row)
        {
            this.name = name;
            this.row = row;
        }
    }
}
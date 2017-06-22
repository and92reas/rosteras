using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rosteras
{
    public class Info
    {
        

        public string image { get; set; }
        public string cssid { get; set; }
        public string capitals { get; set; }

        public Info(string image, string cssid, string capitals)
        {
            this.image = image;
            this.cssid = cssid;
            this.capitals = capitals;
        }

    }
    }

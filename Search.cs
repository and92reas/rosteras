using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rosteras
{
    public abstract class Search
    {
        String nam;

        public Search(String nam)
        {
            this.nam = nam;
        }

        public abstract String displaySearchedElements();
    }
}
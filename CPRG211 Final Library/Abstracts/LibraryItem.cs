using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG211_Final_Library.Abstracts
{
    public abstract class LibraryItem
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }

        public int ID { get; set; }
    }
}

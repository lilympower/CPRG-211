using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPRG211_Final_Library.Abstracts;


namespace CPRG211_Final_Library.Entities
{
    public class Book : LibraryItem
    {
        public int NumberOfPages { get; set; }
        
        public bool Borrowed { get; set; }

    }
}

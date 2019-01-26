using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class CatsByOwnersGender
    {
        public string OwnerGender { get; set; }
        public IEnumerable<string> Cats { get; set; }
    }
}

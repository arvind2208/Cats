using System.Collections.Generic;

namespace Models
{
    public class GetCatsByOwnersGenderResponse
    {
        public IEnumerable<CatsByOwnersGender> CatsByOwnersGenders { get; set; }
    }
}

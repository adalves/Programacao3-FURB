using System;
using System.Collections.Generic;

namespace pokemonAPI.Domain.Models
{
    public partial class Moves
    {
        public int CdMove { get; set; }
        public string NmMove { get; set; }
        public int CdType { get; set; }
        public string NmCategory { get; set; }
        public int? NrPwr { get; set; }
        public int? NrAcc { get; set; }
        public int NrPp { get; set; }

        public virtual Types CdTypeNavigation { get; set; }
    }
}

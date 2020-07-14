using System;
using System.Collections.Generic;

namespace pokemonAPI.Domain.Models
{
    public partial class TypeEffectiveness
    {
        public int CdAtkType { get; set; }
        public int CdDefType { get; set; }
        public decimal? NrEffectiveness { get; set; }

        public virtual Types CdAtkTypeNavigation { get; set; }
        public virtual Types CdDefTypeNavigation { get; set; }
    }
}

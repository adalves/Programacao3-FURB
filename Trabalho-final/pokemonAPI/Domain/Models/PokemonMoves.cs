using System;
using System.Collections.Generic;

namespace pokemonAPI.Domain.Models
{
    public partial class PokemonMoves
    {
        public int CdPoke { get; set; }
        public int CdMove { get; set; }

        public virtual Moves CdMoveNavigation { get; set; }
        public virtual Pokemon CdPokeNavigation { get; set; }
    }
}

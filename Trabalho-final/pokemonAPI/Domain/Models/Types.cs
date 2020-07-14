using System;
using System.Collections.Generic;

namespace pokemonAPI.Domain.Models
{
    public partial class Types
    {
        public Types()
        {
            Moves = new HashSet<Moves>();
            PokemonCdType1Navigation = new HashSet<Pokemon>();
            PokemonCdType2Navigation = new HashSet<Pokemon>();
        }

        public int CdType { get; set; }
        public string NmType { get; set; }

        public virtual ICollection<Moves> Moves { get; set; }
        public virtual ICollection<Pokemon> PokemonCdType1Navigation { get; set; }
        public virtual ICollection<Pokemon> PokemonCdType2Navigation { get; set; }
    }
}

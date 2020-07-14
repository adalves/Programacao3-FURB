using System;
using System.Collections.Generic;

namespace pokemonAPI.Domain.Models
{
    public partial class ListPokemon
    {
        public int _ { get; set; }
        public string Name { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
    }
}

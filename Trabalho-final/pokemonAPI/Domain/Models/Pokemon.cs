using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace pokemonAPI.Domain.Models
{
    public partial class Pokemon
    {
        public int CdPoke { get; set; }
        public string NmPoke { get; set; }
        public int CdType1 { get; set; }
        public int CdType2 { get; set; }


        public virtual Types CdType1Navigation { get; set; }
        public virtual Types CdType2Navigation { get; set; }
    }
}

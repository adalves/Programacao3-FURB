using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pokemonAPI.Domain.Models;

namespace pokemonAPI.Domain.Interfaces
{
    public interface IPokemonRepository
    {
        List<PokemonDTO> ListAll();
        PokemonDTO GetByID(string id);
        void Add(PokemonDTO pokemon);
        void Update(PokemonDTO pokemon);
        void DeleteByID(string id);
        MovesDTO GetByName(string name);
        List<MovesDTO> ListMoves(string id);
        void AddMove(string nameMove, string idPkmn);        
        void DeleteMove(string nameMove, string idPkmn);  
        Dictionary<string,string> ListTypes(string id);
    }
}

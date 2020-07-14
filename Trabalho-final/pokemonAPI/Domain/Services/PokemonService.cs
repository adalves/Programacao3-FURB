using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pokemonAPI.Domain.Interfaces;
using pokemonAPI.Domain.Models;

namespace pokemonAPI.Domain.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository repository;
        public PokemonService(IPokemonRepository repository)
        {
            this.repository = repository;
        }

        public List<PokemonDTO> ListAll()
        {
            return repository.ListAll();
        }

        public PokemonDTO GetByID(string id)
        {
            return repository.GetByID(id);
        }

        public void Add(PokemonDTO pokemon)
        {
            repository.Add(pokemon);
        }

        public void Update(PokemonDTO pokemon)
        {
            repository.Update(pokemon);
        }

        public void DeleteByID(string id)
        {
            repository.DeleteByID(id);
        }

        public MovesDTO GetByName(string name) {
            return repository.GetByName(name);
        }

        public List<MovesDTO> ListMoves(string id)
        {
            return repository.ListMoves(id);
        }
        
        public void AddMove(string nameMove, string idPkmn)
        {
            repository.AddMove(nameMove, idPkmn);
        }

        public void DeleteMove(string nameMove, string idPkmn)
        {
            repository.DeleteMove(nameMove, idPkmn);
        }
        
        public List<EffectivenessDTO>  ListTypes(string id) 
        {
            return repository.ListTypes(id);
        }
    }
}

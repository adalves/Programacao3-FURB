using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pokemonAPI.Domain.Interfaces;
using pokemonAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace pokemonAPI.Persistence.MariaDB
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokemonContext context;
        public PokemonRepository(PokemonContext context) {
            this.context = context;
        }

        public List<PokemonDTO> ListAll()
        {
            var pokemon = (from obj in context.Pokemon
                            .Include(p => p.CdType1Navigation)
                            .Include(p => p.CdType2Navigation)
                            .OrderBy(x => x.CdPoke)
                            select new PokemonDTO()
                            {
                                ID = obj.CdPoke.ToString(),
                                Name = obj.NmPoke,
                                Type1 = obj.CdType1Navigation.NmType,
                                Type2 = obj.CdType2Navigation.NmType
                            }).ToList();
            return (List<PokemonDTO>)pokemon;
        }

        public PokemonDTO GetByID(string id)
        {
            var pokemon = (from obj in context.Pokemon
                            .Include(p => p.CdType1Navigation)
                            .Include(p => p.CdType2Navigation)
                            select new PokemonDTO()
                            {
                                ID = obj.CdPoke.ToString(),
                                Name = obj.NmPoke,
                                Type1 = obj.CdType1Navigation.NmType,
                                Type2 = obj.CdType2Navigation.NmType
                            }).SingleOrDefault(x => x.ID.Equals(id));
            return pokemon;
        }

        public void Add(PokemonDTO pokemon)
        {

            Types type1 = context.Types.SingleOrDefault(t => t.NmType == pokemon.Type1);
            Types type2 = context.Types.SingleOrDefault(t => t.NmType == pokemon.Type2);

            Pokemon newPoke = new Pokemon() { CdPoke = int.Parse(pokemon.ID), NmPoke = pokemon.Name, 
                                            CdType1 = type1.CdType, CdType2 = type2.CdType,
                                            CdType1Navigation = type1, CdType2Navigation = type2 };

            context.Pokemon.Add(newPoke);
            context.SaveChanges();
        }

        public void Update(PokemonDTO pokemon)
        {
            var persisted = context.Pokemon.SingleOrDefault(x => x.CdPoke.ToString().Equals(pokemon.ID));
            if (persisted == null)
                return;
            
            persisted.NmPoke = pokemon.Name;

            var type1 = context.Types.SingleOrDefault(t => t.NmType == pokemon.Type1);
            var type2 = context.Types.SingleOrDefault(t => t.NmType == pokemon.Type2);

            persisted.CdType1 = type1.CdType;
            persisted.CdType2 = type2.CdType;
            persisted.CdType1Navigation = type1;
            persisted.CdType2Navigation = type2;

            context.SaveChanges();
        }

        public void DeleteByID(string id)
        {
            var pokemon = context.Pokemon.SingleOrDefault(x => x.CdPoke.ToString().Equals(id));
            if (pokemon == null)
                return;
            List<MovesDTO> moves = ListMoves(id);
            foreach(MovesDTO move in moves) {
                DeleteMove(move.Name, id);
            }
            context.Pokemon.Remove(pokemon);
            context.SaveChanges();
        }
        
        public MovesDTO GetByName(string name) {
            var move = (from obj in context.Moves
                            .Include(m => m.CdTypeNavigation)
                            select new MovesDTO()
                            {
                                Name = obj.NmMove,
                                Type = obj.CdTypeNavigation.NmType,
                                Category = obj.NmCategory,
                                Power = obj.NrPwr.ToString(),
                                Accuracy = obj.NrAcc.ToString(),
                                PP = obj.NrPp.ToString()
                            }).SingleOrDefault(obj => obj.Name.Equals(name));
            return move;
        }
        public List<MovesDTO> ListMoves(string id)
        {
            var moves = (from obj in context.PokemonMoves
                            .Include(pm => pm.CdMoveNavigation)
                            .Include(pm => pm.CdPokeNavigation)
                            .Include(pm => pm.CdMoveNavigation.CdTypeNavigation)
                            .Where (pm => pm.CdPoke.ToString().Equals(id))
                            select new MovesDTO()
                            {
                                Name = obj.CdMoveNavigation.NmMove,
                                Type = obj.CdMoveNavigation.CdTypeNavigation.NmType,
                                Category = obj.CdMoveNavigation.NmCategory,
                                Power = obj.CdMoveNavigation.NrPwr.ToString(),
                                Accuracy = obj.CdMoveNavigation.NrAcc.ToString(),
                                PP = obj.CdMoveNavigation.NrPp.ToString()
                            }).ToList();
            return (List<MovesDTO>)moves;
        }

        public void AddMove(string nameMove, string idPkmn)
        {
            Pokemon pokemon = context.Pokemon.SingleOrDefault(p => p.CdPoke.ToString().Equals(idPkmn));
            Moves move = context.Moves.SingleOrDefault(m => m.NmMove.Equals(nameMove));

            PokemonMoves pkmnMove = new PokemonMoves() { CdPoke = pokemon.CdPoke, CdMove = move.CdMove,
                                                        CdMoveNavigation = move, CdPokeNavigation = pokemon };
            
            context.PokemonMoves.Add(pkmnMove);
            context.SaveChanges();
        }

        public void DeleteMove(string nameMove, string idPkmn)
        {            
            Moves move = context.Moves.SingleOrDefault(m => m.NmMove.Equals(nameMove));

            var pkmnMove = context.PokemonMoves.SingleOrDefault(p => (p.CdMove == move.CdMove && p.CdPoke == int.Parse(idPkmn)));
            
            context.PokemonMoves.Remove(pkmnMove);
            context.SaveChanges();
        }
        
        public List<EffectivenessDTO> ListTypes(string id) 
        {
            Pokemon pokemon = context.Pokemon.SingleOrDefault(p => p.CdPoke.ToString().Equals(id));

            var type1 = (from obj in context.TypeEffectiveness
                            .Include(te => te.CdAtkTypeNavigation)
                            .Include(te => te.CdDefTypeNavigation)
                            .Where (te => (te.CdDefType == pokemon.CdType1 && te.CdAtkType != 19))
                            select new EffectivenessDTO()
                            {
                                Type = obj.CdAtkTypeNavigation.NmType,
                                Effectiveness = (decimal)obj.NrEffectiveness
                            }).ToList();
                            
            var type2 = (from obj in context.TypeEffectiveness
                            .Include(te => te.CdAtkTypeNavigation)
                            .Include(te => te.CdDefTypeNavigation)
                            .Where (te => (te.CdDefType == pokemon.CdType2 && te.CdAtkType != 19))
                            select new EffectivenessDTO()
                            {
                                Type = obj.CdAtkTypeNavigation.NmType,
                                Effectiveness = (decimal)obj.NrEffectiveness
                            }).ToList();

            for (int i = 0; i < type1.Count(); ++i) {
                type1[i].Effectiveness *= type2[i].Effectiveness;
            }

            return type1;
        }
    }
}

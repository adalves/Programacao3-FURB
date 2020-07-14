using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pokemonAPI.Domain.Services;
using pokemonAPI.Domain.Models;

namespace pokemonAPI.Controllers
{
    [Route("api/pokemon")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService service;
        public PokemonController(IPokemonService service)
        {
            this.service = service;
        }


        //List all pokemon
        // GET: api/pokemon
        [HttpGet]
        public List<PokemonDTO> Get()
        {
            List<PokemonDTO> pokemon = service.ListAll();
            return pokemon;
        }

        //Get pokemon by ID
        // GET: api/pokemon/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<PokemonDTO> Get(string id)
        {
            PokemonDTO pokemon = service.GetByID(id);
            if (pokemon != null) return pokemon;
            return NotFound();
        }

        //Add a pokemon
        // POST: api/pokemon
        [HttpPost]
        public ActionResult Post([FromBody] PokemonDTO pokemon)
        {
            PokemonDTO found = service.GetByID(pokemon.ID);
            if (found != null) return Conflict("Duplicate entry.");
            service.Add(pokemon);
            return Ok(pokemon);
        }

        //Update pokemon by ID
        // PUT: api/pokemon/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] PokemonDTO pokemon)
        {
            if (id != pokemon.ID) return BadRequest("Pokemon ID doesn't match URI.");
            PokemonDTO found = service.GetByID(id);
            if (found == null) return NotFound();
            service.Update(pokemon);
            return Ok(pokemon);
        }

        //Delete pokemon by ID
        // DELETE: api/pokemon/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            PokemonDTO found = service.GetByID(id);            
            if (found == null) return NotFound();
            service.DeleteByID(id);
            return NoContent();
        }

        //Get moves by pokemon ID
        // GET: api/pokemon/5/moves
        [HttpGet("{id}/moves")]
        public ActionResult GetMoves(string id)
        {
            PokemonDTO found = service.GetByID(id);            
            if (found == null) return NotFound();
            List<MovesDTO> moves = service.ListMoves(id);            
            return Ok(moves);
        }        

        //Add a move to a pokemon by ID 
        // POST: api/pokemon/5/moves
        [HttpPost("{id}/moves")]
        public ActionResult PostMove([FromRoute] string id, [FromBody] object moveName)
        {
            PokemonDTO pkmnFound = service.GetByID(id);            
            if (pkmnFound == null) return NotFound();

            JsonElement element = (JsonElement)moveName;
            string json = element.GetProperty("move_name").ToString();
            MovesDTO moveFound = service.GetByName(json);            
            if (moveFound == null) return NotFound();

            service.AddMove(json, id);
            return Ok(moveFound);
        }

        //Remove move from pokemon by ID
        // DELETE: api/pokemon/5/moves
        [HttpDelete("{id}/moves")]
        public ActionResult DeleteMove([FromRoute] string id, [FromBody] object moveName)
        {
            
            PokemonDTO pkmnFound = service.GetByID(id);            
            if (pkmnFound == null) return NotFound();

            JsonElement element = (JsonElement)moveName;
            string json = element.GetProperty("move_name").ToString();
            MovesDTO moveFound = service.GetByName(json);            
            if (moveFound == null) return NotFound();
            
            service.DeleteMove(json, id);
            return NoContent();
        }

        //Get type effectiveness by pokemon ID
        // GET: api/pokemon/5/types
        [HttpGet("{id}/types")]
        public ActionResult GetTypes(string id)
        {
            PokemonDTO found = service.GetByID(id);            
            if (found == null) return NotFound();
            
            Dictionary<string,string> types = service.ListTypes(id);
            return Ok(types);
        }   
    }
}

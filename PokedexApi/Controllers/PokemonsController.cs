using Microsoft.AspNetCore.Mvc;
using PokedexApi.Services;
using PokedexApi.Dtos;
using PokedexApi.Mappers;

namespace PokedexApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PokemonsController : ControllerBase 
{
    private readonly IPokemonService _pokemonService;

    public PokemonsController(IPokemonService pokemonService) 
    {
        _pokemonService = pokemonService;
    }


    //localhost/api/v1/pokemons/12971293-1283812
    [HttpGet("{id}")]
    public async Task<ActionResult<PokemonResponse>> GetPokemonById(Guid id, CancellationToken cancellationToken) 
    {
        var pokemon = await _pokemonService.GetPokemonByIdAsync(id, cancellationToken);
        if (pokemon is null) {
            return NotFound();
        }
        return Ok(pokemon.ToDto());
    }

    //localhost:port/api/v1/pokemons/ID
    //localhost:port/api/v1/pokemons
    //OK - 200
    //[pokemons..]
    //[]
    [HttpGet]
    public async Task<ActionResult<List<PokemonResponse>>> GetPokemonsByName([FromQuery] string name, CancellationToken cancellationToken) {
        return Ok();
    }

    //404 - NotFound
    //204 - NoContent (Se encontro y se elimino el pokemon de manera correcta pero
    // el body de respuesta esta vacio)
    //200 - OK (Se encontro y se elimino y en el body de respuesta
    // se manda un mensaje de exito)
    // {status: "success"}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePokemonById(Guid id, CancellationToken cancellationToken) {
        var deleted = await _pokemonService.DeletePokemonByIdAsync(id, cancellationToken);
        if (deleted) {
            return NoContent(); //204
        }
        return NotFound(); //404
    }
}

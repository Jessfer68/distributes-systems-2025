using Microsoft.AspNetCore.Mvc;
using PokedexApi.Services;
using PokedexApi.Dtos;
using PokedexApi.Mappers;
using PokedexApi.Exceptions;

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

    //400 - BadRequest (Usuario ingreso un valor incorrecto)
    //409 - Conflict (Ya existe el recurso que se quiere crear)
    //200 - Ok (Objeto de respuesta Pokemon creado)
    //201 - Created (Pokemon creado, en headers de respuesta url para consultar el pokemon creado)
    [HttpPost]
    public async Task<ActionResult<PokemonResponse>> CreatePokemon([FromBody] CreatePokemonRequest pokemon, CancellationToken cancellationToken)
    {
        try 
        {
            var createdPokemon = await _pokemonService.CreatePokemonAsync(pokemon.ToModel(), cancellationToken);
            return CreatedAtAction(nameof(GetPokemonById), new {id = createdPokemon.Id}, createdPokemon.ToDto());
        }
        catch(PokemonValidationException ex) 
        {
            return BadRequest(new {message=ex.Message});
        }
        catch(PokemonConflictException) {
            return Conflict(new {message=$"Pokemon already exists with the name: {pokemon.Name}"});
        }
    }

    //PUT - localhost:port/api/v1/pokemons/ID
    //Request body {Name, Type, Level, Stats, ......}
    //404 - Not Found (No existe el pokemon con el id que se manda en la url)
    //400 - BadRequest (Usuario ingreso un valor incorrecto)
    //409 - Conflict (Ya existe el pokemon con el mismo nombre)
    //204 - NoContent
    //200 - OK (Pokemon actualizado)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePokemon(Guid id, [FromBody] UpdatePokemonRequest pokemon, CancellationToken cancellationToken)
    {
        try 
        {
            await _pokemonService.UpdatePokemonAsync(id, pokemon.ToModel(), cancellationToken);
            return NoContent();
        }
        catch(PokemonConflictException) {
            return Conflict(new {message=$"Pokemon already exists with the name: {pokemon.Name}"});
        }
        catch(PokemonValidationException ex) 
        {
            return BadRequest(new {message=ex.Message});
        }
        catch(PokemonNotFoundException) 
        {
            return NotFound();
        }
    }


    //PATCH - localhost:port/api/v1/pokemons/ID
    //Request body {Level:10}
    //404 - Not Found (No existe el pokemon con el id que se manda en la url)
    //400 - BadRequest (Usuario ingreso un valor incorrecto)
    //409 - Conflict (Ya existe el pokemon con el mismo nombre)
    //200 - OK (Pokemon actualizado)
    //[HttpPatch("{id}")]
}

using PokedexApi.Exceptions;
using PokedexApi.Models;
using PokedexApi.Repositories;

namespace PokedexApi.Services;

public class PokemonService : IPokemonService
{
    private readonly IPokemonRepository _pokemonRepository;

    public PokemonService(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<Pokemon?> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _pokemonRepository.GetPokemonByIdAsync(id, cancellationToken);
    }

    public async Task<bool> DeletePokemonByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _pokemonRepository.DeletePokemonByIdAsync(id, cancellationToken);
    }

    public async Task<Pokemon> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        var pokemons = await _pokemonRepository.GetPokemonByNameAsync(pokemon.Name, cancellationToken);
        if (pokemons.Any(s => s.Name.ToLower() == pokemon.Name.ToLower()))
        {
            throw new PokemonConflictException();
        }
        return await _pokemonRepository.CreatePokemonAsync(pokemon, cancellationToken);
    }

    public async Task UpdatePokemonAsync(Guid id, Pokemon pokemon, CancellationToken cancellationToken) 
    {
        //{Name: Pikachu {2}, Name: Charmander {1}}
        //Request: {Pikachu {2}, Name: Pikachu, Level: 3}
        var pokemons = await _pokemonRepository.GetPokemonByNameAsync(pokemon.Name, cancellationToken);
        if(pokemons.Any(s => s.Name.ToLower() == pokemon.Name.ToLower() && s.Id != id)) {
            throw new PokemonConflictException();
        }

        if(pokemon.Level <= 0) 
        {
            throw new PokemonValidationException("Level must be greater than 0");
        }

        pokemon.Id = id;
        await _pokemonRepository.UpdatePokemonAsync(pokemon, cancellationToken);
    }
}

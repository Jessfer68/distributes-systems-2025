using System.ServiceModel;
using PokemonApi.Dtos;
using PokemonApi.Mappers;
using PokemonApi.Repositories;

namespace PokemonApi.Services;

public class PokemonService : IPokemonService 
{
    private readonly IPokemonRepository _pokemonRepository;

    public PokemonService(IPokemonRepository pokemonRepository) {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<PokemonResponseDto> GetPokemonById(Guid id, CancellationToken cancellationToken) {
        var pokemon = await _pokemonRepository.GetByIdAsync(id, cancellationToken);
        if(pokemon is null) {
            throw new FaultException("Pokemon not found :(");
        }
        return pokemon.ToDto();
    }

    public async Task<bool> DeletePokemon(Guid id, CancellationToken cancellationToken) {
        var pokemon = await _pokemonRepository.GetByIdAsync(id, cancellationToken);
        if(pokemon is null) {
            throw new FaultException("Pokemon not found :(");
        }

        await _pokemonRepository.DeleteAsync(pokemon, cancellationToken);
        return true;
    }

    public async Task<PokemonResponseDto> CreatePokemon(CreatePokemonDto pokemon, CancellationToken cancellationToken) {
        return null;
    }
}

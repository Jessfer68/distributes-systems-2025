using System.ServiceModel;
using PokemonApi.Dtos;
using PokemonApi.Mappers;
using PokemonApi.Repositories;
using PokemonApi.Validators;

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
        var pokemonToCreate = pokemon.ToModel();

        pokemonToCreate.ValidateName().ValidateLevel().ValidateType();

        await _pokemonRepository.AddAsync(pokemonToCreate, cancellationToken);
        return pokemonToCreate.ToDto();
    }

    public async Task<PokemonResponseDto> UpdatePokemon(UpdatePokemonDto pokemon, CancellationToken cancellationToken) {
        var pokemonToUpdate = await _pokemonRepository.GetByIdAsync(pokemon.Id, cancellationToken);
        if(pokemonToUpdate is null) {
            throw new FaultException("Pokemon not found ...");
        }

        pokemonToUpdate.Name = pokemon.Name;
        pokemonToUpdate.Level = pokemon.Level;
        pokemonToUpdate.Type = pokemon.Type;
        pokemonToUpdate.Stats.Attack = pokemon.Stats.Attack;
        pokemonToUpdate.Stats.Defense = pokemon.Stats.Defense;
        pokemonToUpdate.Stats.Speed = pokemon.Stats.Speed;

        await _pokemonRepository.UpdateAsync(pokemonToUpdate, cancellationToken);
        return pokemonToUpdate.ToDto();
    }
}

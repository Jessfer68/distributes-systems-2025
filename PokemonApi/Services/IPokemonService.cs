using System.ServiceModel;
using PokemonApi.Dtos;

namespace PokemonApi.Services;

[ServiceContract(Name = "PokemonService", Namespace = "http://pokemon-api/pokemon-service")]
public interface IPokemonService
{
    [OperationContract]
    Task<PokemonResponseDto> GetPokemonById(Guid id, CancellationToken cancellationToken);

    [OperationContract]
    Task<IEnumerable<PokemonResponseDto>> GetPokemonByName(string name, CancellationToken cancellationToken);

    [OperationContract]
    Task<bool> DeletePokemon(Guid id, CancellationToken cancellationToken);

    [OperationContract]
    Task<PokemonResponseDto> CreatePokemon(CreatePokemonDto createPokemon, CancellationToken cancellationToken);

    [OperationContract]
    Task<PokemonResponseDto> UpdatePokemon(UpdatePokemonDto pokemon, CancellationToken cancellationToken);
}

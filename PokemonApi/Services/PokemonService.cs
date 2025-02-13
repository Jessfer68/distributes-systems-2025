using PokemonApi.Dtos;

namespace PokemonApi.Services;

public class PokemonService : IPokemonService 
{
    public Task<PokemonResponseDto> GetPokemonById(Guid id, CancellationToken cancellationToken) {
        return null;
    }
}
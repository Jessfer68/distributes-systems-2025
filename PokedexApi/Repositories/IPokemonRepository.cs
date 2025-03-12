using PokedexApi.Models;

namespace PokedexApi.Repositories;

public interface IPokemonRepository
{
    Task<Pokemon?> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken);
}
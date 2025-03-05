using System.ServiceModel;
using PokemonApi.Models;

namespace PokemonApi.Validators;

public static class PokemonValidator {
    public static Pokemon ValidateName(this Pokemon pokemon) =>
        string.IsNullOrEmpty(pokemon.Name) ? 
        throw new FaultException("Pokemon name is required") : pokemon; 

    public static Pokemon ValidateType(this Pokemon pokemon) =>
        string.IsNullOrEmpty(pokemon.Type) ?
        throw new FaultException("Pokemon type is required") : pokemon;

    public static Pokemon ValidateLevel(this Pokemon pokemon) =>
        pokemon.Level <= 0 ? throw new FaultException("Pokemon level is required") : pokemon;
}
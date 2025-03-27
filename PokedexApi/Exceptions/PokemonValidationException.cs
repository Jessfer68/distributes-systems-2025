namespace PokedexApi.Exceptions;

public class PokemonValidationException : Exception {
    public PokemonValidationException(string message) : base(message) {

    }
}
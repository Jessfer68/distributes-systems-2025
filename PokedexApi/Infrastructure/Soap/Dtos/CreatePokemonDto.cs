using System.Runtime.Serialization;

namespace PokedexApi.Infrastructure.Soap.Dtos;

[DataContract(Name="CreatePokemonDto", Namespace="http://pokemon-api/pokemon-service")]
public class CreatePokemonDto : PokemonCommonDto {
}

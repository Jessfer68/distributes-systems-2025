using System.Runtime.Serialization;

namespace PokemonApi.Dtos;

[DataContract(Name="CreatePokemonDto", Namespace="http://pokemon-api/pokemon-service")]
public class CreatePokemonDto : PokemonCommonDto {
}

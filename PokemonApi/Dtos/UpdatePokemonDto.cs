using System.Runtime.Serialization;

namespace PokemonApi.Dtos;

[DataContract(Name = "UpdatePokemonDto", Namespace = "http://pokemon-api/pokemon-service")]
public class UpdatePokemonDto : PokemonCommonDto 
{
    [DataMember(Name = "Id", Order = 5)]
    public Guid Id {get;set;}
}
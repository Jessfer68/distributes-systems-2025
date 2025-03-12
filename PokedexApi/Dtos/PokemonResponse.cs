namespace PokedexApi.Dtos;

//TU RUTA/swagger/index.html

public class PokemonResponse 
{
    public Guid Id {get;set;}
    public required string Name {get;set;}
    public required string Type {get;set;}
    public int Level {get;set;}
    public required StatsResponse Stats {get;set;}
}

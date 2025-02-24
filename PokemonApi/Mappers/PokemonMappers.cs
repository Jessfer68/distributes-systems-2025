using PokemonApi.Dtos;
using PokemonApi.Infrastructure.Entities;
using PokemonApi.Models;

namespace PokemonApi.Mappers;

public static class PokemonMapper {
    public static PokemonEntity ToEntity(this Pokemon pokemon) {
        return new PokemonEntity {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Level = pokemon.Level,
            Type = pokemon.Type,
            Attack = pokemon.Stats.Attack,
            Defense = pokemon.Stats.Defense,
            Speed = pokemon.Stats.Speed
        };
    }

    public static Pokemon ToModel(this PokemonEntity entity) {
        if(entity is null) {
            return null;
        }

        return new Pokemon {
            Id = entity.Id,
            Name = entity.Name,
            Level = entity.Level,
            Type = entity.Type,
            Stats = new Stats {
                Attack = entity.Attack,
                Defense = entity.Defense,
                Speed = entity.Speed
            }
        };
    }

    public static PokemonResponseDto ToDto(this Pokemon pokemon) {
        return new PokemonResponseDto {
            Id = pokemon.Id,
            Level = pokemon.Level,
            Name = pokemon.Name,
            Type = pokemon.Type,
            Stats = new StatsDto {
                Attack = pokemon.Stats.Attack,
                Speed = pokemon.Stats.Speed,
                Defense = pokemon.Stats.Defense,
            }
        };
    }
}

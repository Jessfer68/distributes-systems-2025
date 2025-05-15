using Google.Protobuf.WellKnownTypes;
using MongoDB.Driver;
using TrainerApi.Infrastructure.Documents;
using TrainerApi.Models;

namespace TrainerApi.Mappers;

public static class TrainerMapper {
    public static Trainer? ToModel(this TrainerDocument trainer) {
        if (trainer is null) {
            return null;
        }

        return new Trainer {
            Id = trainer.Id,
            Age = trainer.Age,
            Name = trainer.Name,
            Birthdate = trainer.Birthdate,
            CreatedAt = trainer.CreatedAt,
            Medals = trainer.Medals.Select(s => 
            new Medal {
                Region = s.Region,
                Type = (Models.MedalType)(int) s.Type
            }).ToList()
        };
    }

    public static Trainer ToModel(this CreateTrainerRequest trainer) {
        return new Trainer{
            Name = trainer.Name,
            Age = trainer.Age,
            Birthdate = trainer.Birthdate.ToDateTime(),
            CreatedAt = DateTime.UtcNow,
            Medals = trainer.Medals.Select(s => new Medal {
                Region = s.Region,
                Type = (Models.MedalType)(int) s.Type
            }).ToList()
        };
    }

    public static TrainerDocument ToDocument(this Trainer trainer) 
    {
        return new TrainerDocument {
            Id = trainer.Id,
            Name = trainer.Name,
            Age = trainer.Age,
            Birthdate = trainer.Birthdate,
            CreatedAt = trainer.CreatedAt,
            Medals = trainer.Medals.Select(s => new MedalDocument {
                Region = s.Region,
                Type = (Infrastructure.Documents.MedalType) s.Type
            }).ToList()
        };
    }

    public static TrainerResponse ToResponse(this Trainer trainer)
    {
        return new TrainerResponse {
            Id = trainer.Id,
            Name = trainer.Name,
            Age = trainer.Age,
            Birthdate = Timestamp.FromDateTime(trainer.Birthdate.ToUniversalTime()),
            CreatedAt = Timestamp.FromDateTime(trainer.CreatedAt.ToUniversalTime()),
            Medals = {trainer.Medals.Select(s => new TrainerApi.Medals {
                Region = s.Region,
                Type = (MedalType)(int) s.Type
            })}
        };
    }
}
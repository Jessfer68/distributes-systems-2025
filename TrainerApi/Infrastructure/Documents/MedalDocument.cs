using MongoDB.Bson.Serialization.Attributes;

namespace TrainerApi.Infrastructure.Documents;

public class MedalDocument 
{
    [BsonElement("region")]
    public string Region {get;set;}

    [BsonElement("type")]
    public MedalType Type {get;set;}
}

public enum MedalType {
    Unknown = 0,
    Gold = 1,
    Silver = 2,
    Bronze = 3
}
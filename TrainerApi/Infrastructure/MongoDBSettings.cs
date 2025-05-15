namespace TrainerApi.Infrastructure;

public class MongoDBSettings {
    public string ConnectionString {get;set;}
    public string DatabaseName {get;set;}
    public string TrainersCollectionName {get;set;}
}
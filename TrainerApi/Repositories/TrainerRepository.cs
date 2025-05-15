using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TrainerApi.Infrastructure;
using TrainerApi.Infrastructure.Documents;
using TrainerApi.Mappers;
using TrainerApi.Models;

namespace TrainerApi.Repositories;

public class TrainerRepository : ITrainerRepository 
{
    private readonly IMongoCollection<TrainerDocument> _trainersCollection;

    public TrainerRepository(IMongoDatabase database, 
    IOptions<MongoDBSettings> settings) {
        _trainersCollection = 
        database.GetCollection<TrainerDocument>(settings.Value.TrainersCollectionName);
    }

    public async Task<Trainer?> GetByIdAsync(string id, CancellationToken cancellationToken) 
    {
        var trainer = await _trainersCollection.Find(t => t.Id == id).FirstOrDefaultAsync(cancellationToken);
        return trainer?.ToModel();
    }

    public async Task<Trainer> CreateAsync(Trainer trainer, CancellationToken cancellationToken) {
        var document = trainer.ToDocument();
        await _trainersCollection.InsertOneAsync(document, cancellationToken);
        return document.ToModel();
    }

    public async Task<List<Trainer>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var trainers = await _trainersCollection.
        Find(s => s.Name.Contains(name)).
        ToListAsync(cancellationToken);

        return trainers.Select(s => s.ToModel()).ToList();
    }
}
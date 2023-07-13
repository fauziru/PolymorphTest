using PolymorphTest.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PolymorphTest.FStatisticInterval;

public class StatisticIntervalService
{
    private readonly IMongoCollection<StatisticInterval> _statisticCollection;

    public StatisticIntervalService(
        IOptions<MainDatabase> mainDatabase)
    {
        var mongoClient = new MongoClient(
            mainDatabase.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            mainDatabase.Value.DatabaseName);

        _statisticCollection = mongoDatabase.GetCollection<StatisticInterval>(
            mainDatabase.Value.StatisticCollectionName);
    }

    public async Task<List<StatisticInterval>> GetAsync(string type) =>
        await _statisticCollection.Find(x => x.type == type).ToListAsync();

    public async Task<StatisticInterval?> DetailsAsync(string id) =>
        await _statisticCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(StatisticInterval newStatistic) =>
        await _statisticCollection.InsertOneAsync(newStatistic);

    // public async Task UpdateAsync(string id, StatisticInterval updatedStatist) =>
    //     await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    // public async Task RemoveAsync(string id) =>
    //     await _booksCollection.DeleteOneAsync(x => x.Id == id);
}
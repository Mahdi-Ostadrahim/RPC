using GrpcGreeter.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace GrpcGreeter.Services
{
    public class WorkersService
    {
        private readonly IMongoCollection<Worker> _workersCollection;
        public WorkersService(string connectionString, string databaseName, string workersCollectionName)
        {
            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseName);
            _workersCollection = mongoDatabase.GetCollection<Worker>(workersCollectionName);
        }
        public WorkersService(IOptions<WorkersListDatabaseSettings> workersListDataBaseSettings)
        {
            var mongoClient = new MongoClient(workersListDataBaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(workersListDataBaseSettings.Value.DatabaseName);
            _workersCollection = mongoDatabase.GetCollection<Worker>(workersListDataBaseSettings.Value.WorkersCollectionName);
        }
        public async Task<List<Worker>> GetAsync() =>
       await _workersCollection.Find(_ => true).ToListAsync();

        public async Task<Worker?> GetAsync(string id) =>
            await _workersCollection.Find(x => x._nationalID == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Worker newBook) =>
            await _workersCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Worker updatedBook) =>
            await _workersCollection.ReplaceOneAsync(x => x._nationalID == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _workersCollection.DeleteOneAsync(x => x._nationalID == id);
    }
}

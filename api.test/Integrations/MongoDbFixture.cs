/*
 * @file MongoDbFixture
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-03
 *
 * This file contains the fixture with db connection
 */

// Installed Utils
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

// Namespace for Integrations
namespace Api.Test.Integrations;

/// <summary>
/// Interface for TestRepository
/// </summary>
public interface ITestRepository
{
 
    /// <summary>
    /// Save Data
    /// </summary>
    /// <param name="entity">Data to save</param>
    void InsertData(DataEntity entity);

    /// <summary>
    /// Delete Data
    /// </summary>
    /// <param name="dataName">Data name used as identifier</param>
    void DeleteData(string dataName);
}

/// <summary>
/// Test Repository
/// </summary>
public class TestRepository : ITestRepository
{
    /// <summary>
    /// Container for Collection
    /// </summary>
    private readonly IMongoCollection<DataEntity> _collection;

    /// <summary>
    /// Test Repository Constructor
    /// </summary>
    /// <param name="collection">Database collection</param>
    /// <exception cref="ArgumentNullException">If collection missing</exception>
    public TestRepository(IMongoCollection<DataEntity> collection)
    {
        _collection = collection;
    }

    /// <summary>
    /// Save Data
    /// </summary>
    /// <param name="entity">Data to save</param>
    public void InsertData(DataEntity entity)
    {
        _collection.InsertOne(entity);
    }

    /// <summary>
    /// Delete Data
    /// </summary>
    /// <param name="dataName">Data name used as identifier</param>
    public void DeleteData(string dataName)
    {
        var filter = Builders<DataEntity>.Filter.Eq(d => d.DataName, dataName);
        _collection.DeleteMany(filter);
    }

}

/// <summary>
/// Data Entity Class
/// </summary>
public class DataEntity
{
    /// <summary>
    /// Data ID Field
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("data_id")]
    public ObjectId DataId { get; set; }

    /// <summary>
    /// Data Name Field
    /// </summary>
    [BsonElement("data_name")]
    public string? DataName { get; set; }

    /// <summary>
    /// Data Value Field
    /// </summary>
    [BsonElement("data_value")]
    public string? DataValue { get; set; }    
}

/// <summary>
/// Test Db Context
/// </summary>
public class TestDbContext
{

    /// <summary>
    /// Database Implementation Container
    /// </summary>
    private readonly IMongoDatabase _database;

    /// <summary>
    /// Mongo Db Constructor
    /// </summary>
    /// <param name="connectionString">Mongo Db connection string</param>
    /// <param name="databaseName">Database Name</param>
    public TestDbContext(string connectionString, string databaseName)
    {
        // Create a new instance with connection string
        MongoClient client = new(connectionString);

        // Get the database
        _database = client.GetDatabase(databaseName);
    }

    /// <summary>
    /// Register the Data Collection
    /// </summary>
    public IMongoCollection<DataEntity> Data => _database.GetCollection<DataEntity>("Data");

}

/// <summary>
/// MongoDbFixture Class
/// </summary>
public class MongoDbFixture : IDisposable
{

    /// <summary>
    /// Class Constructor
    /// </summary>
    public MongoDbFixture()
    {

        // Create connection to MongoDB server
        MongoClient client = new (Configuration.mongoDbConnection);

        // Set DB Name
        _database = client.GetDatabase(Configuration.mongoDbName);

        // Set the testing collection
        _collection = _database.GetCollection<DataEntity>("Data");

    }

    /// <summary>
    /// Database Container
    /// </summary>
    public readonly IMongoDatabase _database;

    /// <summary>
    /// Collection Container
    /// </summary>
    public readonly IMongoCollection<DataEntity> _collection;

    /// <summary>
    /// Get the Test Repository
    /// </summary>
    /// <returns>Test Repository with collection</returns>
    public TestRepository GetRepository()
    {
        return new TestRepository(_collection);
    }

    /// <summary>
    /// Delete the collection
    /// </summary>
    public void Dispose()
    {

        // Clean up resources if needed
        if (_database != null)
        {
            // Delete the collection before disposing
            _database.DropCollection("Data");

        }

    }

}
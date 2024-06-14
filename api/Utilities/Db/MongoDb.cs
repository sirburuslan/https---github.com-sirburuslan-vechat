/*
 * @class MongoDb
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-09
 *
 * This class is used to create the db connection and register the collection
 */

// System Namespaces
using MongoDB.Driver;

// App Namespaces
using api.Models.Entities.Users;
using api.Models.Entities.Settings;

// Namespace for Db
namespace api.Utilities.Db;

/// <summary>
/// Mongo Db Class
/// </summary>
public class MongoDb
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
    public MongoDb(string connectionString, string databaseName)
    {
        // Create a new instance with connection string
        MongoClient client = new(connectionString);

        // Get the database
        _database = client.GetDatabase(databaseName);
    }

    /// <summary>
    /// Register the Users Collection
    /// </summary>
    public IMongoCollection<UserEntity> Users => _database.GetCollection<UserEntity>("Users");

    /// <summary>
    /// Register the Users Options Collection
    /// </summary>
    public IMongoCollection<UserOptionEntity> UsersOptions => _database.GetCollection<UserOptionEntity>("UsersOptions");

    /// <summary>
    /// Register the Settings Collection
    /// </summary>
    public IMongoCollection<OptionEntity> Settings => _database.GetCollection<OptionEntity>("Settings");
}
/*
 * @class MongoDbTests
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-03
 *
 * This class contains the methods used for mongo db testing
 */

// Installed Utils
using MongoDB.Driver;
using Xunit;

// Namespace for Integrations
namespace Api.Test.Integrations;

/// <summary>
/// MongoDbTests Class
/// </summary>
public class MongoDbTests
{

    /// <summary>
    /// Test Mongo Db database
    /// </summary>
    [Fact]
    public void TestDb()
    {

        /*
           GET MONGODB CONNECTION WITH ITS DEPENDENCIES
        */

        // Arrange
        using var fixture = new MongoDbFixture();



        /*
           SAVE DATA IN A COLLECTION
        */

        // Get the repository
        TestRepository repository = fixture.GetRepository();

        // Create a data entity to insert
        DataEntity entity = new ()
        {
            DataName = "Test Name",
            DataValue = "Test Value"
        };

        // Act
        repository.InsertData(entity);




        /*
           TEST READ DATA FROM A COLLECTION
        */


        // Find data
        var firstResult = fixture._collection.FindSync(Builders<DataEntity>.Filter.Empty).SingleOrDefault();

        // Test if data exists
        Assert.NotNull(firstResult);

        // Test if is the saved data
        Assert.Equal("Test Value", firstResult.DataValue);



        /*
           TEST DELETE DATA FROM A COLLECTION
        */
        

        // Delete data
        repository.DeleteData("Test Name");

        // Search for data
        var secondResult = fixture._collection.FindSync(Builders<DataEntity>.Filter.Empty).SingleOrDefault();

        // Test if the data was deleted
        Assert.Null(secondResult);

    }

}
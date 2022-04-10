using System;
using Database;
using Database.Client;
using Database.DAO;
using DatabaseTest.TestEntities;
using MongoDB.Driver;

namespace DatabaseTest.Fixtures
{
    public class MongoDBFixture : IDisposable
    {
        public MongoDBClient client { get; init; }
        public MongoDBGenericDao<MongoDBTestEntity> dao {get;init;}

		public MongoDBFixture()
		{
            this.client = (MongoDBClient)ClientFactory.GetClient(ClientFactory.Database.MONGO, "mongodb://localhost:27017", "lesson");
            this.client.Connect();
            this.client.SelectDatabase();
            this.dao = new MongoDBGenericDao<MongoDBTestEntity>(client);


        }

        public void Dispose()
        {
            
        }
    }
}


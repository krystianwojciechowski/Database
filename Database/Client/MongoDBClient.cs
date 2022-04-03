using System;
using MongoDB.Driver;

namespace Database.Client {

	public class MongoDBClient : Client
	{
		private IMongoClient client;
		public IMongoDatabase Database { get; internal set; }

	    public MongoDBClient(string dbUrl, string dbName) : base(dbUrl,dbName)
		{
		}

		public override void Connect()
		{
			this.client = new MongoClient(DbUrl);
		}

        public override void SelectDatabase()
        {
			this.Database = this.client.GetDatabase(this.DbName);
		}
        public override void SelectDatabase(string dbName)
        {
			this.Database = this.client.GetDatabase(dbName);
			this.DbName = dbName;
        }

    }
}
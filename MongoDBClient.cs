using System;

namespace Database.Client {

	public class MongoDBClient : Client
	{
		private  client;

	public MongoDBClient()
		{
		}

		public override void connect()
		{
			this.client = new MongoDBClient(DbUrl);
			
	
	}
	}
}
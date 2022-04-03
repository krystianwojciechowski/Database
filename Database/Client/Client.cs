using System;

namespace Database.Client
{
	public abstract class Client
	{
		
		protected string DbUrl;
		protected string DbName;

		public abstract void Connect();

		public abstract void SelectDatabase(string dbName);
		public abstract void SelectDatabase();
		public Client(string dbUrl, string dbName)
		{
			this.DbUrl = dbUrl;
			this.DbName = dbName;	
		}
	}

}

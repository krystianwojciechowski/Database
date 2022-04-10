using System;
namespace Database.Client.Connection
{
	public class ConnectionPool<T>
	{
		private string dbUrl { get; init; }
		private string dbUser { get; init; }
		private string dbPassword { get; init; }
        private HashMap<string, T> connections;

        public ConnectionPool(string dbUrl, string dbUser, string dbPassword)
        {
            this.dbUrl = dbUrl;
            this.dbUser = dbUser;
            this.dbPassword = dbPassword;
        }


    }
}


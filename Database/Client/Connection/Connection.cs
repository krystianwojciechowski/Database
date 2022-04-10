using System;
namespace Database.Client.Connection
{
	internal abstract class Connection<T> : IDisposable
	{
		private string dbUrl { get; init; }
		private string dbUser { get; init; }
		private string dbPassword { get; init; }
		private T connection { get; set; }

		public Connection(string dbUrl, string dbUser, string dbPassword)
		{
			this.dbUrl = dbUrl;
			this.dbUser = dbUser;
			this.dbPassword = dbPassword;
		}

		public void Close()
        {

        }

		public void Dispose()
        {

        }

	}
}


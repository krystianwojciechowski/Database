using System;

namespace Database.Client
{
	public abstract class Client
	{

		protected string DbUrl;
		protected string DbName;

		public void connect();
		public Client()
		{
		}
	}

}

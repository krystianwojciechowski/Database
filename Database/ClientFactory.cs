using Database.Client;

namespace Database
{
    public class ClientFactory
    {
        public enum Database
        {
            MONGO,
        }

        public static Client.Client GetClient(Database type,string url, string dbName)
        {
            switch (type)
            {
                case Database.MONGO:
                    return new MongoDBClient(url,dbName);
                default:
                    return null;
            }
        }




    }
}
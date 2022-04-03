using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
namespace Database.Client
{
    internal class MysqlClient : Client
    {

        private MySqlConnection mysql;

        public MysqlClient(string dbUrl, string dbName) : base(dbUrl, dbName)
        {
        }

        public override void Connect()
        {
            this.mysql = new MySqlConnection(this.DbUrl);
        }

        public override void SelectDatabase()
        {
            this.mysql.ChangeDatabase(this.DbName);
        }
        public override void SelectDatabase(string dbName)
        {
           if(this.mysql.Database != this.DbName)
            {
                this.mysql.ChangeDatabase(dbName);
                this.DbName = dbName;
            }
        }
    }
}

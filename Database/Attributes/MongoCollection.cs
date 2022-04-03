using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Attributes
{
    internal class MongoCollection : Attribute
    {
        public string CollectionName { get; set; }

        public MongoCollection(string collectionName)
        {
            CollectionName = collectionName;    
        }


    }
}

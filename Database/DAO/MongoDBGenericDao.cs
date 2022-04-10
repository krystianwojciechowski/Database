using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Client;
using Database.Attributes;
using Database.Exceptions;
using MongoDB.Driver;
using Database.DAO.Filters;
using System.Reflection;
using Database.BaseEntities;
using MongoDB.Bson;
using System.Reflection.Metadata;
using MongoDB.Bson.Serialization.Attributes;

namespace Database.DAO
{
   
    public class MongoDBGenericDao<T,F>: DAO<T, F> 
        where T : BaseMongoEntity 
        where F : FilterDefinition<T>
    {
        private MongoDBClient client;

        private string getCollectionName()
        {
            var attribute = typeof(T).GetCustomAttributes(typeof(MongoCollection), true).Cast<MongoCollection>().FirstOrDefault();

            if(attribute == null)
            {
                throw new InvalidEntityDeclarationException(typeof(T).Name, typeof(MongoCollection).Name);
            }

            return attribute.CollectionName;

        }

        private MongoCollectionBase<T> GetCollection()
        {
            var collectionName = this.getCollectionName();
            return (MongoCollectionBase<T>) this.client.Database.GetCollection<T>(collectionName);
        }

        public MongoDBGenericDao(MongoDBClient client) 
        {
            this.client = client;
        }

        private FilterDefinition<T> GetFilter(T entity)
        {
            return Builders<T>.Filter.Eq("_id", entity.Id);
        }

        public override void Delete(params T[] elements)
        {
            var collection = this.GetCollection();
            if (elements.Length == 1)
            {
                var filter = this.GetFilter(elements[0]);
                collection.DeleteOne(filter);
            } else if (elements.Length == 0 || elements == null)
            {
                throw new Exception("The elements parameter can't be empty");
            } else
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    collection.DeleteOne(this.GetFilter(elements[i]));
                }
                
            }
        }

        public override T Get(Filter<F> filter, T element)
        {
            throw new NotImplementedException();
        }

        public override void Insert(params T[] elements)
        {
            var collection = this.GetCollection();
            if (elements.Length == 1)
            {
             
              collection.WithWriteConcern(WriteConcern.Acknowledged).InsertOne(elements[0]);
             
            }
            else if (elements == null || elements.Length == 0) {
                throw new Exception("The elements parameter can't be empty");
            }
            else
            {
                collection.WithWriteConcern(WriteConcern.Acknowledged).InsertMany(elements);
            }

     
        }
        
        public override void Update(params T[] elements)
        {
            var collection = this.GetCollection();

            if (elements != null && elements.Length > 0)
            {
                foreach (T element in elements) {
                    collection.ReplaceOne<T>(doc => doc.Id == element.Id, element);
                }
            }
            else
            {
                throw new Exception("The elements parameter can't be empty");
            }
        }
    }
}

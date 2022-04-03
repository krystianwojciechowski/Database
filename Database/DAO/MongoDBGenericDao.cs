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

        public override void Delete(Filter<F> filter, params T[] elements)
        {
            var collection = this.GetCollection();
           
            if(elements.Length == 1)
            {
                collection.DeleteOne(filter.filter);
            } else if(elements.Length == 0 || elements == null)
            {
                throw new Exception("The elements parameter can't be empty");
            } else
            {
                collection.DeleteMany(filter.filter);
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
                collection.InsertOne(elements[0]);
            }
            else if (elements == null || elements.Length == 0) {
                throw new Exception("The elements parameter can't be empty");
            }
            else
            {
                collection.InsertMany(elements);
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

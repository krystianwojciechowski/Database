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
using System.Collections;

namespace Database.DAO
{
   
    public class MongoDBGenericDao<T>: DAO<T> 
        where T : BaseMongoEntity 
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

        private FilterDefinition<T> GetFilter(T entity,bool byId = false, bool findNulls = false)
        {

            if (byId)
            {
                return Builders<T>.Filter.Eq("_id", entity.Id);
            }

            var list = new List<FilterDefinition<T>>();

            foreach(PropertyInfo property in typeof(T).GetProperties())
            {
                var attribute = property.GetCustomAttribute<BsonElementAttribute>();

                if (attribute != null && attribute.ElementName != "_id" && (findNulls || property.GetValue(entity) != null)) { 
                 var fieldName = attribute.ElementName;
                    list.Add(Builders<T>.Filter.Eq(fieldName, property.GetValue(entity).ToString()));
                }

            }
            return Builders<T>.Filter.And(list.ToArray<FilterDefinition<T>>());
        }

        public override void Delete(params T[] elements)
        {
            var collection = this.GetCollection();
            if (elements.Length == 0 || elements == null)
            {
                throw new Exception("The elements parameter can't be empty");
            } else
            {
                //@todo perform performance tests
                var filters = new List<FilterDefinition<T>>();
                for (int i = 0; i < elements.Length; i++)
                {
                    filters.Add(this.GetFilter(elements[i]));
                }
                collection.DeleteMany(Builders<T>.Filter.And(filters.ToArray<FilterDefinition<T>>()));

            }
        }



        public override void Delete(T element)
        {
            var collection = this.GetCollection();
            if ( element == null)
            {
                throw new Exception("The element parameter can't be null");
            }
            else
            {
                    collection.DeleteOne(this.GetFilter(element));

            }
        }

        public override List<T> Get(T element)
        {
            var collection = this.GetCollection();

            var filter = this.GetFilter(element);
            return collection.Find<T>(filter).ToList<T>();
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

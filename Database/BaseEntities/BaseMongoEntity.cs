using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Database.BaseEntities
{
    public class BaseMongoEntity
    {
        [BsonElement("_id")]
        [BsonId]
        public ObjectId Id { get; set; }


        //Two entities are the same when they share id. When field was edited, entity remains the same entity just with differen value for field
        public override bool Equals(object? obj)
        {
            var item = obj as BaseMongoEntity;

            if (item == null)
            {
                return false;
            }

            return item.Id.Equals(Id);
        }
    }
}

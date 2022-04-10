using System;
using Database.Attributes;
using Database.BaseEntities;
using MongoDB.Bson.Serialization.Attributes;

namespace DatabaseTest.TestEntities
{
	[MongoCollection("lesson")]
	public class MongoDBTestEntity : BaseMongoEntity
	{
		public MongoDBTestEntity()
		{
		}


		[BsonElement("name")]
		[BsonRepresentation(MongoDB.Bson.BsonType.String)]
		public string Name { get; set; }


		[BsonElement("text")]
		[BsonRepresentation(MongoDB.Bson.BsonType.String)]
		public string Text { get; set; }
	}
}


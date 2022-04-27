using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.BaseDTO
{
    internal record MongoBaseDTO
    {
        public ObjectId Id { get; init; }
    }
}

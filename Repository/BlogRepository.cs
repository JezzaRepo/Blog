using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BlogRepository : IBlogRepository
    {
        private MongoClient _client = null;
        private IMongoDatabase _database = null;
        private IMongoCollection<BsonDocument> _collection = null;

        public BlogRepository()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _database = _client.GetDatabase("foo");
            _collection = _database.GetCollection<BsonDocument>("users");
        }

        public long GetUserCount()
        {
            return _collection.Find(new BsonDocument(), null).Count();
        }

        public bool DeleteAllUsers()
        {
            DeleteResult result = _collection.DeleteMany(new BsonDocument());
            return result.DeletedCount >= 0;
        }

        public List<string> GetUserNames()
        {
            List<BsonDocument> allDocs = _collection.Find(new BsonDocument(), null).ToList(); //"{'_id', 'test@mail.com'"
            List<string> results = new List<string>();
            allDocs.ForEach(x => results.Add(x["username"].ToString()));
            return results;
        }

        public string InsertUser(string email, string password)
        {
            BsonDocument doc = CreateUserJson(email, password);
            _collection = _database.GetCollection<BsonDocument>("users");
            _collection.InsertOne(doc);
            return "test";
        }

        private BsonDocument CreateUserJson(string email, string password)
        {
            BsonDocument document = new BsonDocument
            {
                { "_id", email },
                { "type", "user" },
                { "username", email },
                { "password", password },
                { "roles", "['admin', 'user']"}
            };
            return document;
        }


        public bool LoginUser(string username, string password)
        {
            try
            {
                _collection = _database.GetCollection<BsonDocument>("users");
                var builder = Builders<BsonDocument>.Filter;
                var filter = builder.Eq("username", username) & builder.Eq("password", password);
                var result = _collection.Find(filter).FirstOrDefault();
                return (result != null);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

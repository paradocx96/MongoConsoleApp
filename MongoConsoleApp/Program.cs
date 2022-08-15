using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // CRUD Operations

            // Create
            // createPerson();

            // Read
            readPerson();

            // Delete
            // deletePerson();

            // Update
            // updatePerson();

            // Read All
            readAllPerson();
        }

        static MongoClient dbConnection()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://root:it19180526@ead.vuzt9we.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);

            return client;
        }

        static void createPerson()
        {
            var database = dbConnection().GetDatabase("person_db");
            var dbCollection = database.GetCollection<PersonModel>("Person");

            var nimal = new PersonModel { firstName = "Nimal", lastName = "Susantha" };
            var kamal = new PersonModel { firstName = "Kamal", lastName = "Shantha" };
            var sunil = new PersonModel { firstName = "Sunil", lastName = "Appuhami" };

            dbCollection.InsertOne(nimal);
            dbCollection.InsertOne(kamal);
            dbCollection.InsertOne(sunil);
        }

        static void readPerson()
        {
            var database = dbConnection().GetDatabase("person_db");
            var dbCollection = database.GetCollection<PersonModel>("Person");

            var selectQuery = dbCollection.Find(x => x.firstName == "Kamal").ToList();

            Console.WriteLine("ID: " + selectQuery[0].id);
            Console.WriteLine("Name: " + selectQuery[0].firstName + " " + selectQuery[0].lastName);
            Console.ReadLine();
        }

        static void deletePerson()
        {
            var database = dbConnection().GetDatabase("person_db");
            var dbCollection = database.GetCollection<PersonModel>("Person");

            dbCollection.DeleteOne(x => x.firstName == "Sunil");
        }

        static void updatePerson()
        {
            var database = dbConnection().GetDatabase("person_db");
            var dbCollection = database.GetCollection<PersonModel>("Person");

            var filter = Builders<PersonModel>.Filter.Eq("firstName", "Nimal");
            var update = Builders<PersonModel>.Update.Set("Updated", DateTime.Now);

            dbCollection.UpdateOne(filter, update);
        }

        static void readAllPerson()
        {
            var database = dbConnection().GetDatabase("person_db");
            var dbCollection = database.GetCollection<BsonDocument>("Person");

            var documents = dbCollection.Find(new BsonDocument()).ToList();

            foreach (BsonDocument doc in documents)
            {
                Console.WriteLine(doc.ToString());
            }

            Console.ReadLine();
        }
    }
}
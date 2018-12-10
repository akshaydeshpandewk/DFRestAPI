using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace DFRestAPI
{
    public class CosmosDBHelper
    {
        private static string DBName = ConfigurationManager.AppSettings["database"];
        private static string CollectionName = ConfigurationManager.AppSettings["collection"];
        public static Uri DatabaseUri => UriFactory.CreateDatabaseUri(DBName);
        public static Uri CollectionUri => UriFactory.CreateDocumentCollectionUri(DBName, CollectionName);

        private static string endPoint = ConfigurationManager.AppSettings["CosmosdbEndpoint"];
        private static string masterKey = ConfigurationManager.AppSettings["CosmosDbMasterKey"];

        public async static Task CreateDB()
        {
            using (var client = new DocumentClient(new Uri(endPoint), masterKey))
            {
                await CreateDatabase(client);
            }
        }

        public async static Task CreateCollection()
        {
            using (var client = new DocumentClient(new Uri(endPoint), masterKey))
            {
                await CreateCollection(client, CollectionName);
            }
        }

        public async static Task CreateDocument()
        {
            using (var client = new DocumentClient(new Uri(endPoint), masterKey))
            {
                await CreateDocuments(client);
            }
        }

        private async static Task CreateDatabase(DocumentClient client)
        {
            Database database = new Database()
            {
                Id = DBName
            };
            await client.CreateDatabaseAsync(database);
            
           // Console.WriteLine("Database created ->" + result.Resource.Id);
        }
        private async static Task CreateCollection(DocumentClient client, string collectionId,
            int reservedRUs = 400, string partitionKey = "/partitionKey")
        {
            var p = new PartitionKeyDefinition();
            p.Paths.Add(partitionKey);

            var collectionDef = new DocumentCollection
            {
                Id = collectionId,
                PartitionKey = p
            };

            var options = new RequestOptions { OfferThroughput = reservedRUs };

            var result = await client.CreateDocumentCollectionAsync(DatabaseUri, collectionDef, options);
            var collection = result.Resource;

           // Console.WriteLine("Collection created ->" + result.Resource.Id);

            // ViewCollections(client);
        }
        private async static Task CreateDocuments(DocumentClient client)
        {
            dynamic newDocument = new
            {
                name = "Akshay Deshpande",
                address = new
                {
                    addressType = "Main Office",
                    location = new
                    {
                        city = "Pune"
                    },
                    PinCode = "411001"
                }
            };

            var result = await client.CreateDocumentAsync(CollectionUri, newDocument);

           // Console.WriteLine("Document created ->" + result.Resource.Id);
        }
    }
}
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Nexus.Link.Eav.Repository.Service.Logic;
using System.Threading.Tasks;
using Nexus.Link.Eav.Repository.Contract;
using Nexus.Link.Libraries.Crud.MemoryStorage;
using Nexus.Link.Eav.Repository.MemoryStorage;

namespace Nexus.Link.Eav.Repository.UnitTest
{
    [TestClass]
    public class SchemaUnitTest
    {
        [TestMethod]
        public async Task CreateAndReadSchemaAsync()
        {
            var personSchemaAsString =
                "  { \"title\": \"Person\" , \"type\": \"object\", \"eav-primary-key\":\"myid\", \"properties\": { \"myid\": { }, \"firstName\": { }, \"lastName\": { }, } } ";

            var personSchema = JObject.Parse(personSchemaAsString);
            var eav = new EavMemoryStorage();
            var service = new GenericStorage( eav );
            await service.Schema.CreateWithSpecifiedIdAsync( (string)personSchema["title"], personSchema);

            var readSchema = await service.Schema.ReadAsync((string)personSchema["title"]);

            Assert.IsNotNull(readSchema);
            Assert.AreEqual(5, readSchema.Count);
        }

        [TestMethod]
        public async Task CreateAndReadSchemaAsync_AttributeWithType()
        {
            var personSchemaAsString =
                " { \"title\": \"Person\" , \"type\": \"object\", \"eav-primary-key\":\"myid\", \"properties\": { \"myid\": { \"type\": \"Guid\"}, \"firstName\": { \"type\": \"string\" }, \"lastName\": { \"type\": \"string\"  },  \"age\": { \"type\": \"number\"  } } } ";

            var personSchema = JObject.Parse(personSchemaAsString);

            var eav = new EavMemoryStorage();
            var service = new GenericStorage(eav);
            await service.Schema.CreateWithSpecifiedIdAsync((string)personSchema["title"], personSchema);

            var readSchema = await service.Schema.ReadAsync((string)personSchema["title"]);

            Assert.IsNotNull(readSchema);
            Assert.AreEqual(5, readSchema.Count);

        }

        [TestMethod]
        public async Task CreateAndReadSchema_AttributeType_Integer()
        {
            var personSchemaAsString =
                " { \"title\": \"Person\" , \"type\": \"object\", \"eav-primary-key\":\"myid\", \"properties\": { \"myid\": { \"type\": \"Guid\"}, \"firstName\": { \"type\": \"string\" }, \"lastName\": { \"type\": \"string\"  },  \"age\": { \"type\": \"integer\"  } } } ";

            var personSchema = JObject.Parse(personSchemaAsString);

            var eav = new EavMemoryStorage();
            var service = new GenericStorage(eav);
            await service.Schema.CreateWithSpecifiedIdAsync((string) personSchema["title"], personSchema);

            var readSchema = await service.Schema.ReadAsync((string) personSchema["title"]);

            Assert.IsNotNull(readSchema);
            Assert.AreEqual(5, readSchema.Count);
            int result = 0;

            // Looping the Properties schema
            var propertySchema = readSchema["properties"];

            Assert.IsNotNull(propertySchema);
            Assert.AreEqual(4, propertySchema.Count());

            foreach (var rSchema in propertySchema)
            {
                Assert.IsNotNull(rSchema);
                Assert.AreEqual(1, rSchema.Count());

                foreach (var attributeType in rSchema)
                {
                    if (attributeType["type"].ToString() == "integer")
                        result = result + 1;
                }
                
            }
            Assert.AreEqual( 1 , result);
        }

        [TestMethod]
        public async Task CreateAndReadSchema_AttributeType_String()
        {
            var personSchemaAsString =
                " { \"title\": \"Person\" , \"type\": \"object\", \"eav-primary-key\":\"myid\", \"properties\": { \"myid\": { \"type\": \"Guid\"}, \"firstName\": { \"type\": \"string\" }, \"lastName\": { \"type\": \"string\"  },  \"age\": { \"type\": \"integer\"  } } } ";

            var personSchema = JObject.Parse(personSchemaAsString);

            var eav = new EavMemoryStorage();
            var service = new GenericStorage(eav);
            await service.Schema.CreateWithSpecifiedIdAsync((string)personSchema["title"], personSchema);

            var readSchema = await service.Schema.ReadAsync((string)personSchema["title"]);

            Assert.IsNotNull(readSchema);
            Assert.AreEqual(5, readSchema.Count);
            int result = 0;

            // Looping the Properties schema
            var propertySchema = readSchema["properties"];

            Assert.IsNotNull(propertySchema);
            Assert.AreEqual(4, propertySchema.Count());

            foreach (var rSchema in propertySchema)
            {
                Assert.IsNotNull(rSchema);
                Assert.AreEqual(1, rSchema.Count());

                foreach (var attributeType in rSchema)
                {
                    if (attributeType["type"].ToString() == "string")
                        result = result + 1;
                }

            }
            Assert.AreEqual(2, result);
        }


        [TestMethod]
        public async Task CreateAndReadSchema_AttributeType_Boolean()
        {
            var personSchemaAsString =
                " { \"title\": \"Person\" , \"type\": \"object\", \"eav-primary-key\":\"myid\", \"properties\": { \"myid\": { \"type\": \"Guid\"}, \"firstName\": { \"type\": \"string\" }, \"lastName\": { \"type\": \"string\"  },  \"age\": { \"type\": \"integer\" },  \"isActive\": { \"type\": \"boolean\" } } } ";

            var personSchema = JObject.Parse(personSchemaAsString);

            var eav = new EavMemoryStorage();
            var service = new GenericStorage(eav);
            await service.Schema.CreateWithSpecifiedIdAsync((string)personSchema["title"], personSchema);

            var readSchema = await service.Schema.ReadAsync((string)personSchema["title"]);

            Assert.IsNotNull(readSchema);
            Assert.AreEqual(5, readSchema.Count);
            int result = 0;

            // Looping the Properties schema
            var propertySchema = readSchema["properties"];

            Assert.IsNotNull(propertySchema);
            Assert.AreEqual(5, propertySchema.Count());

            foreach (var rSchema in propertySchema)
            {
                Assert.IsNotNull(rSchema);
                Assert.AreEqual(1, rSchema.Count());

                foreach (var attributeType in rSchema)
                {
                    if (attributeType["type"].ToString() == "boolean")
                        result = result + 1;
                }

            }
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task CreateAndReadSchema_Withid()
        {
            var personSchemaAsString =
                " { \"$id\": \"https://example.com/person.schema.json \", " +
                " \"$schema\": \"http://json-schema.org/draft-07/schema# \", " +
                " \"title\": \"Person\" , " +
                " \"type\": \"object\", " +
                " \"eav-primary-key\":\"myid\",  " +
                " \"properties\": { \"myid\": { \"type\": \"Guid\"}, \"firstName\": { \"type\": \"string\" }, \"lastName\": { \"type\": \"string\"  },  \"age\": { \"type\": \"number\"  } } } ";

            var personSchema = JObject.Parse(personSchemaAsString);

            // Memory Storage 
            var eav = new EavMemoryStorage();
            var service = new GenericStorage(eav);
            await service.Schema.CreateWithSpecifiedIdAsync((string)personSchema["title"], personSchema);

            var readSchema = await service.Schema.ReadAsync((string)personSchema["title"]);

            Assert.IsNotNull(readSchema);
            Assert.AreEqual(6, readSchema.Count);

        }
    }
}

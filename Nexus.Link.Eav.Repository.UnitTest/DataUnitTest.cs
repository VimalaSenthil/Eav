using System;
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
    public class DataUnitTest
    {

        [TestMethod]
        public async Task CreateAndReadSchemaAsync()
        {
            var personSchemaAsString =
                " { \"title\": \"Person\" , \"type\": \"object\", \"eav-primary-key\":\"id\", \"properties\": { \"id\": { }, \"firstName\": { } , \"lastName\": { }, } } ";

            var personSchema = JObject.Parse(personSchemaAsString);

            var eav = new EavMemoryStorage();
            var service = new GenericStorage(eav);
            await service.Schema.CreateWithSpecifiedIdAsync((string)personSchema["title"], personSchema);

            var personObjectAsString = " {\"firstName\" : \"Lars\", \"lastName\": \"Lindberg\" } ";

            var personObject = JObject.Parse(personObjectAsString);
            var objectId = await service.Object.CreateAsync( (string)personSchema["title"], personObject);

            var readData = await service.Object.ReadAsync((string)personSchema["title"], objectId);
            Assert.IsNotNull(readData);
            Assert.AreEqual(2, readData.Count);

        }

        [TestMethod]
        public async Task CreateAndReadSchemaAsync_WithAttributeType()
        {
            var personSchemaAsString =
                " { \"title\": \"Person\" , \"type\": \"object\", \"eav-primary-key\":\"myid\", \"properties\": { \"myid\": { \"type\": \"Guid\"}, \"firstName\": { \"type\": \"string\" }, \"lastName\": { \"type\": \"string\"  },  \"age\": { \"type\": \"number\"  } } } ";

            var personSchema = JObject.Parse(personSchemaAsString);

            var eav = new EavMemoryStorage();
            var service = new GenericStorage(eav);
            await service.Schema.CreateWithSpecifiedIdAsync((string)personSchema["title"], personSchema);

            var personObjectAsString = " {\"firstName\" : \"Lars\", \"lastName\": \"Lindberg\" , \"age\": 47 } ";

            var personObject = JObject.Parse(personObjectAsString);
            var objectId = await service.Object.CreateAsync((string)personSchema["title"], personObject);

            var readData = await service.Object.ReadAsync((string)personSchema["title"], objectId);
            Assert.IsNotNull(readData);
            Assert.AreEqual(3, readData.Count);

        }

        [TestMethod]
        public async Task CreateAndReadSchemaAsync_WithTypeInteger()
        {
            var personSchemaAsString =
                " { \"title\": \"Person\" , \"type\": \"object\", \"eav-primary-key\":\"myid\", \"properties\": { \"myid\": { \"type\": \"Guid\"}, \"firstName\": { \"type\": \"string\" }, \"lastName\": { \"type\": \"string\"  },  \"age\": { \"type\": \"integer\"  } } } ";

            var personSchema = JObject.Parse(personSchemaAsString);

            var eav = new EavMemoryStorage();
            var service = new GenericStorage(eav);
            await service.Schema.CreateWithSpecifiedIdAsync((string)personSchema["title"], personSchema);

            var personObjectAsString = " { \"age\": 47 } ";

            var personObject = JObject.Parse(personObjectAsString);
            var objectId = await service.Object.CreateAsync((string)personSchema["title"], personObject);

            var readData = await service.Object.ReadAsync((string)personSchema["title"], objectId);
            Assert.IsNotNull(readData);
            Assert.AreEqual(1, readData.Count);
            Assert.AreEqual( 47 , readData["age"]);

        }

        [TestMethod]
        public async Task CreateAndReadSchemaAsync_WithTypeNumber()
        {
            var personSchemaAsString =
                " { \"title\": \"Person\" , \"type\": \"object\", \"eav-primary-key\":\"myid\", \"properties\": { \"myid\": { \"type\": \"Guid\"}, \"firstName\": { \"type\": \"string\" }, \"lastName\": { \"type\": \"string\"  },  \"age\": { \"type\": \"number\"  } } } ";

            var personSchema = JObject.Parse(personSchemaAsString);

            var eav = new EavMemoryStorage();
            var service = new GenericStorage(eav);
            await service.Schema.CreateWithSpecifiedIdAsync((string)personSchema["title"], personSchema);

            var personObjectAsString = " { \"age\": 47 } ";

            var personObject = JObject.Parse(personObjectAsString);
            var objectId = await service.Object.CreateAsync((string)personSchema["title"], personObject);

            var readData = await service.Object.ReadAsync((string)personSchema["title"], objectId);
            Assert.IsNotNull(readData);
            Assert.AreEqual(1, readData.Count);
            Assert.AreEqual(47.0, readData["age"]);

        }

        [TestMethod]
        public async Task CreateAndReadSchemaAsync_WithTypeString()
        {
            var personSchemaAsString =
                " { \"title\": \"Person\" , \"type\": \"object\", \"eav-primary-key\":\"myid\", \"properties\": { \"myid\": { \"type\": \"Guid\"}, \"firstName\": { \"type\": \"string\" }, \"lastName\": { \"type\": \"string\"  },  \"age\": { \"type\": \"number\"  } } } ";

            var personSchema = JObject.Parse(personSchemaAsString);

            var eav = new EavMemoryStorage();
            var service = new GenericStorage(eav);
            await service.Schema.CreateWithSpecifiedIdAsync((string)personSchema["title"], personSchema);

            var personObjectAsString = " {  \"firstName\" : \"Lars\" } ";

            var personObject = JObject.Parse(personObjectAsString);
            var objectId = await service.Object.CreateAsync((string)personSchema["title"], personObject);

            var readData = await service.Object.ReadAsync((string)personSchema["title"], objectId);
            Assert.IsNotNull(readData);
            Assert.AreEqual(1, readData.Count);
            Assert.AreEqual("Lars", readData["firstName"]);

        }

        [TestMethod]
        public async Task CreateAndReadSchemaAsync_WithTypeBoolean()
        {
            var personSchemaAsString =
                " { \"title\": \"Person\" , \"type\": \"object\", \"eav-primary-key\":\"myid\", \"properties\": { \"myid\": { \"type\": \"Guid\"}, \"firstName\": { \"type\": \"string\" }, \"lastName\": { \"type\": \"string\"  },  \"age\": { \"type\": \"integer\" },  \"isActive\": { \"type\": \"boolean\" } } } ";

            var personSchema = JObject.Parse(personSchemaAsString);

            var eav = new EavMemoryStorage();
            var service = new GenericStorage(eav);
            await service.Schema.CreateWithSpecifiedIdAsync((string)personSchema["title"], personSchema);

            var personObjectAsString = " { \"isActive\": \"false\" } ";

            var personObject = JObject.Parse(personObjectAsString);
            var objectId = await service.Object.CreateAsync((string)personSchema["title"], personObject);

            var readData = await service.Object.ReadAsync((string)personSchema["title"], objectId);
            Assert.IsNotNull(readData);
            Assert.AreEqual(1, readData.Count);
            Assert.AreEqual(false, readData["isActive"]);

        }



        //[TestMethod]
        //public async Task ReadSchemaAsync()
        //{
        //    var personSchemaAsString =
        //        " { 
        //    "$id": "https://example.com/person.schema.json",
        //    "$schema": "http://json-schema.org/draft-07/schema#",\"title\": \"Person\" , \"type\": \"object\", \"properties\": { \"firstName\": { }, \"lastName\": { }, } } ";

        //    var personSchema = JObject.Parse(personSchemaAsString);

        //    var eav = new EavMemoryStorage();
        //    var service = new GenericStorage(eav);
        //    await service.Schema.CreateWithSpecifiedIdAsync((string)personSchema["title"], personSchema);
        //    var readSchema = await service.Schema.ReadAsync((string)personSchema["title"]);

        //    Assert.IsNotNull(readSchema);
        //    Assert.AreEqual(3, readSchema.Count);
        //    Assert.AreEqual(personSchema, readSchema);
        //}
    }
}

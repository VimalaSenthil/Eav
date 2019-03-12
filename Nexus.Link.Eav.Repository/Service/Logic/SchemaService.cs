using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Nexus.Link.Eav.Repository.Contract;
using Nexus.Link.Eav.Repository.Model;
using Nexus.Link.Libraries.Core.Storage.Model;
using Nexus.Link.Libraries.Crud.Interfaces;
using Nexus.Link.Libraries.Crud.Model;
using Attribute = Nexus.Link.Eav.Repository.Model.Attribute;

namespace Nexus.Link.Eav.Repository.Service.Logic
{
    public class SchemaService : ICrud<JObject, string>
    {
        private IEav _eav;

        public SchemaService(IEav eav )
        {
            _eav = eav;
        }
        public Task<Lock<string>> ClaimLockAsync(string id, CancellationToken token = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }

        public Task<JObject> CreateAndReturnAsync(JObject item, CancellationToken token = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }

        public Task<string> CreateAsync(JObject item, CancellationToken token = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }

        public Task<JObject> CreateWithSpecifiedIdAndReturnAsync(string id, JObject item, CancellationToken token = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateWithSpecifiedIdAsync(string id, JObject item, CancellationToken token = default(CancellationToken))
        {
            // Create Entity Object
            var entity = new Entity();
            entity.Name = id;
            entity.SchemaId = (item.HasValues && (item["$id"].Type != JTokenType.Null))
                            ? item["$id"].ToString()
                            : string.Empty;
            // Create New Entity
            var entityId = await _eav.EntityStorage.CreateAsync(entity, token);
            
            
            var properties = (JObject)item["properties"];
            foreach (var property in properties)
            {
                // Create Attributes Object
                var attribute = new Attribute();
                attribute.Name = property.Key;
                attribute.EntityId = entityId;
                attribute.IsPrimaryKey = ( item["eav-primary-key"].ToString() == property.Key) ? true : false;
                attribute.Type = (property.Value.HasValues && property.Value.HasValues &&
                                 (property.Value["type"].Type != JTokenType.Null))
                        ? property.Value["type"].ToString()
                        : "string";

                // Create New Attributes
                await _eav.AttributeStorage.CreateAsync(entityId, attribute, token);
            }
            
        }

        public Task DeleteAllAsync(CancellationToken token = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(string id, CancellationToken token = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<JObject>> ReadAllAsync(int limit = int.MaxValue, CancellationToken token = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }

        public Task<PageEnvelope<JObject>> ReadAllWithPagingAsync(int offset, int? limit = null, CancellationToken token = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }

        public async Task<JObject> ReadAsync(string id, CancellationToken token = default(CancellationToken))
        {
            // Read Entity
            var entity = await _eav.EntityStorage.ReadByNameAsync((string)id, token);

            // Create Return Object
            var jObject = new JObject();
            if( ! string.IsNullOrWhiteSpace(entity.SchemaId)) 
                jObject.Add("$id" , entity.SchemaId);

            jObject.Add("$schema", "http://json-schema.org/draft-07/schema#");
            jObject.Add("title" , entity.Name);
            jObject.Add("type", "object");
            
            // Create Attribute Object
            var jAttributeObject = new JObject();
            var attributes = await _eav.AttributeStorage.ReadChildrenAsync(entity.Id, int.MaxValue, token);

            // Looping the Attributes
            foreach (var attribute in attributes)
            {
                if( attribute.IsPrimaryKey == true )
                    jObject.Add("eav-primary-key" , attribute.Name );

                var tObject = new JObject();
                    tObject.Add("type" , string.IsNullOrWhiteSpace(attribute.Type) ? "string" : attribute.Type );

                jAttributeObject.Add(attribute.Name, tObject);
            }

            jObject.Add("properties" , jAttributeObject);
            return jObject;
        }


        public Task ReleaseLockAsync(string id, string lockId, CancellationToken token = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }

        public Task<JObject> UpdateAndReturnAsync(string id, JObject item, CancellationToken token = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(string id, JObject item, CancellationToken token = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }
    }
}
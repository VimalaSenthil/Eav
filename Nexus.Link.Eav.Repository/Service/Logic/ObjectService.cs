using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Nexus.Link.Eav.Repository.Contract;
using Nexus.Link.Eav.Repository.Model;
using Nexus.Link.Libraries.Core.Crud.Model;
using Nexus.Link.Libraries.Core.Storage.Model;
using Nexus.Link.Libraries.Crud.Interfaces;
using Nexus.Link.Libraries.Crud.Model;
using Object = Nexus.Link.Eav.Repository.Model.Object;

namespace Nexus.Link.Eav.Repository.Service.Logic
{
    public class ObjectService : ICrudSlaveToMaster<JObject , string>
    {
        private IEav _eav;
        public ObjectService(IEav eav)
        {
            _eav = eav;
        }

        public Task<SlaveLock<string>> ClaimLockAsync(string masterId, string slaveId, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<JObject> CreateAndReturnAsync(string masterId, JObject item, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreateAsync(string masterId, JObject item, CancellationToken token = default(CancellationToken))
        {
            var entity = await _eav.EntityStorage.ReadByNameAsync(masterId, token);
            var o = new Object();
            o.EntityId = entity.Id;

            var objectId = await _eav.ObjectStorage.CreateAsync(entity.Id, o, token);

            var attributes = await _eav.AttributeStorage.ReadChildrenAsync(entity.Id, int.MaxValue, token);

            foreach (var attribute in attributes)
            {
                var data = new Data();
                data.AttributeId = attribute.Id;
                data.ObjectId = objectId;

                if (item[attribute.Name] == null)
                    continue;

                data.Value = item[attribute.Name].Value<string>();

                await _eav.DataStorage.CreateAsync(objectId, data, token);
            }

            return objectId.ToString();

        }

        public Task<JObject> CreateWithSpecifiedIdAndReturnAsync(string masterId, string slaveId, JObject item, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public async Task CreateWithSpecifiedIdAsync(string masterId, string slaveId, JObject item, CancellationToken token = default(CancellationToken))
        {
            var entity = await _eav.EntityStorage.ReadByNameAsync(masterId, token);

            var objectId = Guid.Parse(slaveId);
            var o = new Object();
            o.Id = objectId;
            o.EntityId = entity.Id;
            
            await _eav.ObjectStorage.CreateWithSpecifiedIdAsync(entity.Id, objectId , o , token );

            var attributes = await _eav.AttributeStorage.ReadChildrenAsync(entity.Id, int.MaxValue, token);

            foreach (var attribute in attributes)
            {
                var data = new Data();
                data.AttributeId = attribute.Id;
                data.ObjectId = objectId;

                if(item[attribute.Name] == null )
                    continue;

                data.Value = item[attribute.Name].Value<string>();
                
                await _eav.DataStorage.CreateAsync(objectId, data, token);
            }
           
        }

        public Task DeleteAsync(string masterId, string slaveId, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task DeleteChildrenAsync(string parentId, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<JObject> ReadAsync(SlaveToMasterId<string> id, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> ReadAsync(string masterId, string slaveId, CancellationToken token = default(CancellationToken))
        {
            var entity = await _eav.EntityStorage.ReadByNameAsync(masterId, token);
            var objectId = Guid.Parse(slaveId);
            var jObject = new JObject();
            
            var attributes = await _eav.AttributeStorage.ReadChildrenAsync(entity.Id, int.MaxValue, token);
            var data = await _eav.DataStorage.ReadChildrenAsync(objectId, int.MaxValue, token);

            foreach (var datum in data)
            {
                var attribute = attributes.FirstOrDefault(i => i.Id == datum.AttributeId);
                switch (attribute.Type)
                {
                    case "number":
                        var num = double.Parse(datum.Value);
                        jObject.Add(attribute.Name, num );
                        break;
                    case "integer":
                        var inte = int.Parse(datum.Value);
                        jObject.Add(attribute.Name , inte);
                        break;
                    case "string":
                        var str = datum.Value.ToString();
                        jObject.Add(attribute.Name, str);
                        break;
                    case "boolean":
                        var b = Boolean.Parse(datum.Value);
                        jObject.Add(attribute.Name, b);
                        break;

                }
            }
            return jObject;
        }

        public Task<IEnumerable<JObject>> ReadChildrenAsync(string parentId, int limit = int.MaxValue, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<PageEnvelope<JObject>> ReadChildrenWithPagingAsync(string parentId, int offset, int? limit = null, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task ReleaseLockAsync(string masterId, string slaveId, string lockId, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<JObject> UpdateAndReturnAsync(string masterId, string slaveId, JObject item, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string masterId, string slaveId, JObject item, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}

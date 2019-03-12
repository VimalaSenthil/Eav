using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nexus.Link.Eav.Repository.Contract;
using Nexus.Link.Eav.Repository.Model;
using Nexus.Link.Libraries.Core.Storage.Model;
using Nexus.Link.Libraries.Crud.MemoryStorage;
using Nexus.Link.Libraries.Crud.Model;

namespace Nexus.Link.Eav.Repository.MemoryStorage
{
    public class EntityMemoryStorage : CrudMemory<Entity, Guid>, IEntityStorage
    {
        public async Task<Entity> ReadByNameAsync(string name, CancellationToken token)
        {
            var entities = await ReadAllAsync(int.MaxValue, token);
            return entities.FirstOrDefault(i => i.Name == name);
        }
    }
}

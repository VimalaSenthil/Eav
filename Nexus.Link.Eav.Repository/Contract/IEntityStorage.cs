using System;
using System.Threading.Tasks;
using Nexus.Link.Eav.Repository.Model;
using Nexus.Link.Libraries.Crud.Interfaces;

namespace Nexus.Link.Eav.Repository.Contract
{
    public interface IEntityStorage : ICrud<Entity , Guid>
    {
        Task<Entity> ReadByNameAsync(string name, System.Threading.CancellationToken token);
    }
}
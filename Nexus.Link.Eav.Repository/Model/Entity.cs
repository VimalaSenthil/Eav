using System;
using Nexus.Link.Libraries.Core.Storage.Model;

namespace Nexus.Link.Eav.Repository.Model
{
    public class Entity : IUniquelyIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SchemaId { get; set; }

    }
}

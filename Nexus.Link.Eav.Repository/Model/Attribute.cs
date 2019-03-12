using Nexus.Link.Libraries.Core.Storage.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nexus.Link.Eav.Repository.Model
{
    public class Attribute: IUniquelyIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsPrimaryKey { get; set; }
        public Guid EntityId { get; set; }

    }
}

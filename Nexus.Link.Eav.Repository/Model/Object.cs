using System;
using System.Collections.Generic;
using System.Text;
using Nexus.Link.Libraries.Core.Storage.Model;

namespace Nexus.Link.Eav.Repository.Model
{
    public class Object : IUniquelyIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }


    }
}

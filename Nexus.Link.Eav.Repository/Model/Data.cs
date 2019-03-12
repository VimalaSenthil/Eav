using System;
using System.Collections.Generic;
using System.Text;
using Nexus.Link.Libraries.Core.Storage.Model;

namespace Nexus.Link.Eav.Repository.Model
{
    public class Data : IUniquelyIdentifiable<Guid>

    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public Guid AttributeId { get; set; }
        public Guid ObjectId { get; set; }

    }
}

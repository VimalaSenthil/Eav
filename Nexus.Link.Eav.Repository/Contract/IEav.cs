using System;
using System.Collections.Generic;
using System.Text;
using Nexus.Link.Eav.Repository.Model;
using Nexus.Link.Libraries.Crud.Interfaces;
using Attribute = Nexus.Link.Eav.Repository.Model.Attribute;
using Object = Nexus.Link.Eav.Repository.Model.Object;

namespace Nexus.Link.Eav.Repository.Contract
{
    public interface IEav
    {
        IEntityStorage EntityStorage { get; set; } 

        ICrudSlaveToMaster<Attribute , Guid> AttributeStorage { get; set; }

        ICrudSlaveToMaster<Object, Guid> ObjectStorage { get; set; }

        ICrudSlaveToMaster<Data , Guid> DataStorage { get; set; }

    }
}

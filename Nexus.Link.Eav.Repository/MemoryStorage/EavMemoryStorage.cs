using System;
using System.Collections.Generic;
using System.Text;
using Nexus.Link.Eav.Repository.Contract;
using Nexus.Link.Libraries.Crud.MemoryStorage;
using Nexus.Link.Eav.Repository.Model;
using Nexus.Link.Libraries.Crud.Interfaces;
using Attribute = Nexus.Link.Eav.Repository.Model.Attribute;
using Object = Nexus.Link.Eav.Repository.Model.Object;

namespace Nexus.Link.Eav.Repository.MemoryStorage
{
    public class EavMemoryStorage : IEav
    {
        public EavMemoryStorage()
        {
            EntityStorage = new EntityMemoryStorage();
            AttributeStorage = new SlaveToMasterMemory<Attribute, Guid>();
            ObjectStorage = new SlaveToMasterMemory<Object, Guid>();
            DataStorage = new SlaveToMasterMemory<Data, Guid>();


        }

        public IEntityStorage EntityStorage { get; set; }
        public ICrudSlaveToMaster<Attribute, Guid> AttributeStorage { get; set; }
        public ICrudSlaveToMaster<Object, Guid> ObjectStorage { get; set; }
        public ICrudSlaveToMaster<Data, Guid> DataStorage { get; set; }
       
    }
}

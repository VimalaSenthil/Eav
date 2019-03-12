using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Nexus.Link.Eav.Repository.Contract;
using Nexus.Link.Eav.Repository.ServiceContract;
using Nexus.Link.Libraries.Crud.Interfaces;
using Nexus.Link.Libraries.Crud.MemoryStorage;
using Object = Nexus.Link.Eav.Repository.Model.Object;

namespace Nexus.Link.Eav.Repository.Service.Logic
{
    public class GenericStorage : IGenericStorage
    {
        public GenericStorage(IEav eav )
        {
            Schema = new SchemaService( eav );
            Object = new ObjectService(eav);
        }
        public ICrud<JObject, string> Schema { get; set; }

        public ICrudSlaveToMaster<JObject , string> Object { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Newtonsoft.Json.Linq;
using Nexus.Link.Libraries.Crud.Interfaces;

namespace Nexus.Link.Eav.Repository.ServiceContract
{
    public interface IGenericStorage
    {
        ICrud<JObject, string> Schema { get; set; }

        ICrudSlaveToMaster<JObject , string> Object { get; set; }

    }
}

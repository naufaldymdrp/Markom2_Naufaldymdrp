using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Markom2.Web.Extensions
{
    public class JsonDataId
    {
        public string DataId { get; set; }
    }

    public enum FormPartialMode
    {
        Add,
        Detail,
        Edit,
        Delete
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAppTest
{
    public static class MethodEnum
    {
        public enum Methods
        {
            GetValues,
            GetExtentValues,
            GetRelatedVlaues,
            Unknown
        }

        public static Methods GetMethodEnum(string method)
        {
            switch (method)
            {
                case "GetValues":
                    return Methods.GetValues;
                case "GetExtentValues":
                    return Methods.GetExtentValues;
                case "GetRelatedVlaues":
                    return Methods.GetRelatedVlaues;
                default:
                    return Methods.Unknown;
            }
        }
    }
}

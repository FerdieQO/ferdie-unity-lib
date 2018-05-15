using System;
using System.Collections.Generic;
using System.Text;

namespace FerdieUnityLib.Helpers
{
    public static class SystemHelpers
    {
        public static Type GetEnumType(string enumName)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var type = assembly.GetType(enumName);
                if (type == null)
                    continue;
                if (type.IsEnum)
                    return type;
            }
            return null;
        }
    }
}

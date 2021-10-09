using System.Collections.Generic;

namespace Canopy.Provider.Extensions
{
    public static class DictionaryExtensions
    {
        public static void TryAddParam(this Dictionary<string, string> dict, string name, object value)
        {
            if (value != null)
                dict.Add(name, value.ToString());
        }
    }
}

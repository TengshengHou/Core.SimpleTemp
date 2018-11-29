using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.SimpleTemp.Common
{
    public static class JsonSerializerExtensions
    {
        public static string Serialize(this JsonSerializer jsonSerializer, object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonWriter = new JsonTextWriter(stringWriter))
                {
                    jsonSerializer.Serialize(jsonWriter, obj);
                    return stringWriter.ToString();
                }
            }
        }
    }
}

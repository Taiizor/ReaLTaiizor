using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

namespace ReaLTaiizor.UI.Helpers
{
    public class SerializerHelper
    {
        public static void Serialize<T>(T obj, string file)
        {
            using StreamWriter fs = File.CreateText(file);
            JsonSerializer serializer = new();
            serializer.Converters.Add(new StringEnumConverter());
            serializer.Formatting = Formatting.Indented;

            serializer.Serialize(fs, obj);
        }

        public static T Deserialize<T>(string file) where T : class
        {
            using StreamReader fs = File.OpenText(file);
            JsonSerializer serializer = new();
            serializer.Converters.Add(new StringEnumConverter());

            object result = serializer.Deserialize(fs, typeof(T));
            return result as T;
        }
    }
}
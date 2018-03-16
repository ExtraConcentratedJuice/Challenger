using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace Challenger.API
{
    public class ChallengerConfiguration
    {
        internal Dictionary<string, object> values;
        private string configPath;

        internal ChallengerConfiguration(string name)
        {
            configPath = $"{Environment.CurrentDirectory}/Plugins/{name}/{name}.yaml";

            var d = new Deserializer();
            values = d.Deserialize<Dictionary<string, object>>(File.ReadAllText(configPath));
        }

        public void Reload()
        {
            var d = new Deserializer();
            values = d.Deserialize<Dictionary<string, object>>(File.ReadAllText(configPath));
        }

        public object GetValue(string key) => values.TryGetValue(key, out object v) ? v : null;
        public T GetValue<T>(string key) => values.TryGetValue(key, out object v) ? (T)v : default(T);
        public string GetString(string key) => values.TryGetValue(key, out object v) ? (string)v : null;
        public int? GetInt(string key) => values.TryGetValue(key, out object v) ? (int)v : (int?)null;
        public double? GetDouble(string key) => values.TryGetValue(key, out object v) ? (double)v : (double?)null;
        public List<T> GetList<T>(string key) => values.TryGetValue(key, out object v) ? (List<T>)v : null;
    }
}

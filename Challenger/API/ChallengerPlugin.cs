using Challenger.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using YamlDotNet.Serialization;

namespace Challenger.API
{
    public class ChallengerPlugin : MonoBehaviour
    {
        public ChallengerConfiguration Configuration { get; private set; }

        public ChallengerPlugin()
        {
            if (!Directory.Exists($"{Environment.CurrentDirectory}/Plugins/{GetType().Name}"))
                Directory.CreateDirectory($"{Environment.CurrentDirectory}/Plugins/{GetType().Name}");

            if (this is IConfigurable c)
            {
                string configPath = $"{Environment.CurrentDirectory}/Plugins/{GetType().Name}/{GetType().Name}.yaml";

                if (!File.Exists(configPath))
                {
                    using (StreamWriter s = new StreamWriter(configPath))
                    {
                        Serializer sz = new Serializer();
                        sz.Serialize(s, c.ConfigurationValues());
                    }
                }

                Configuration = new ChallengerConfiguration(GetType().Name);
            }

            LogTool.Log($"{GetType().Name} loaded!", LogTool.Severity.INFO);
        }

        protected internal virtual void OnEnable()
        {
        }

        protected internal virtual void OnDisable()
        {
        }
    }
}

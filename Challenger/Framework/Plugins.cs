using Challenger.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Challenger.Framework
{
    public class Plugins
    {
        private string pluginDirectory;
        private List<ChallengerPlugin> loadedPlugins;

        public Plugins()
        {
            pluginDirectory = $"{Environment.CurrentDirectory}\\Plugins\\";
            loadedPlugins = new List<ChallengerPlugin>();

            LoadPlugins();
        }

        internal void LoadPlugins()
        {
            foreach (string s in Directory.GetFiles(pluginDirectory))
                LoadPlugin(Path.GetFileNameWithoutExtension(s));
        }

        internal void UnloadPlugins()
        {
            foreach (ChallengerPlugin plugin in loadedPlugins.Reverse<ChallengerPlugin>())
                UnloadPlugin(plugin);
        }

        internal void UnloadPlugin(ChallengerPlugin plugin)
        {
            LogTool.Log($"Unloading plugin: {plugin.GetType().Name}", LogTool.Severity.INFO);
            plugin.OnDisable();
            loadedPlugins.Remove(plugin);
        }

        internal void LoadPlugin(string name)
        {
            LogTool.Log($"Loading plugin: {GetType().Name}", LogTool.Severity.INFO);
            var a = Assembly.LoadFrom(pluginDirectory + name + ".dll");
            Type p = a.GetTypes().FirstOrDefault(x => x.IsSubclassOf(typeof(ChallengerPlugin)));
            if (p != null)
            {
                var plugin = (ChallengerPlugin)Activator.CreateInstance(p);
                plugin.OnEnable();
                loadedPlugins.Add(plugin);
            }
            else
            {
                LogTool.Log($"Error loading {a.GetName().Name}.", LogTool.Severity.ERROR);
            }
        }
    }
}

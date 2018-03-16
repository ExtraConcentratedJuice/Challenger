using Challenger.Framework;
using SDG.Framework.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Challenger
{
    public class ChallengerFramework : IModuleNexus
    {
        public static ChallengerFramework Instance { get; private set; }
        public Plugins Plugins { get; private set; }

        #region Initialization/Shutdown
        public void initialize()
        {
            Instance = this;

            #region Text
            Console.Clear();
            Console.WriteLine("#############################################################################");
            Console.WriteLine(@" _______ _     _ _______               _______ __   _  ______ _______  ______
 |       |_____| |_____| |      |      |______ | \  | |  ____ |______ |_____/
 |_____  |     | |     | |_____ |_____ |______ |  \_| |_____| |______ |    \_
                                                                             ");
            Console.WriteLine("v. 0.0.69");
            Console.WriteLine("#############################################################################");
            #endregion

            LogTool.Log("LOADING Plugins..", LogTool.Severity.INFO);
            Plugins = new Plugins();
            LogTool.Log("LOADED Plugins.", LogTool.Severity.INFO);
            LogTool.Log("Challenger Framework 0.0.69 Successfully Loaded!", LogTool.Severity.INFO);
        }

        public void shutdown()
        {
            Plugins.UnloadPlugins();
        }
        #endregion
    }
}

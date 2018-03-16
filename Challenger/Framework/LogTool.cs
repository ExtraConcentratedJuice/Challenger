using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Challenger.Framework
{
    public static class LogTool
    {
        private static readonly string logPath = $"{Environment.CurrentDirectory}\\log.log";

        public enum Severity { LOG, INFO, WARNING, ERROR, CRITICAL }

        static LogTool()
        {
            if (File.Exists(logPath))
            {
                DateTime dt = DateTime.Now;
                File.Move(logPath, $"{Environment.CurrentDirectory}\\log-{dt.Month}{dt.Day}{dt.Year}-{dt.Hour}{dt.Minute}{dt.Second}.log");
            }
        }

        public static void Log(string m, Severity s)
        {
            ConsoleColor color;

            switch (s)
            {
                case Severity.CRITICAL:
                    color = ConsoleColor.DarkRed;
                    break;
                case Severity.ERROR:
                    color = ConsoleColor.Red;
                    break;
                case Severity.WARNING:
                    color = ConsoleColor.Yellow;
                    break;
                case Severity.INFO:
                    color = ConsoleColor.Cyan;
                    break;
                case Severity.LOG:
                    color = ConsoleColor.White;
                    break;
                default:
                    color = ConsoleColor.White;
                    break;
            }

            WriteConsole(m, color);
            WriteLogs(m, s);
        }

        public static void Log(string m) => Log(m, Severity.LOG);

        private static void WriteConsole(string m, ConsoleColor c)
        {
            Console.ForegroundColor = c;
            Console.WriteLine(m);
            Console.ResetColor();
        }

        private static void WriteLogs(string m, Severity s)
        {
            string level;

            switch(s)
            {
                case Severity.CRITICAL:
                    level = "Critial";
                    break;
                case Severity.ERROR:
                    level = "Error";
                    break;
                case Severity.WARNING:
                    level = "Warning";
                    break;
                case Severity.INFO:
                    level = "Info";
                    break;
                default:
                    level = "Log";
                    break;
            }

            using (StreamWriter sw = File.AppendText(logPath))
                sw.WriteLine($"[{DateTime.Now} | {level} ] {m}");
        }
    }
}

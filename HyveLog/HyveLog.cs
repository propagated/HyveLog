using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HyveLog
{
    public class Logger
    {
        public enum LogTarget { Console, File, Both }

        private string _logPath = String.Empty;
        private LogTarget _type;

        /// <summary>
        /// Initialize HyveLog and let it determine where to log.
        /// </summary>
        public Logger()
        {
            _type = SetApplicationType();
        }
        /// <summary>
        /// Initialize HyveLog with a specified type of logging
        /// </summary>
        /// <param name="Type"></param>
        public Logger(LogTarget Type)
        {
            _type = Type;
        }
        /// <summary>
        /// Initialize HyveLog with a target logfile.
        /// </summary>
        /// <param name="LogFilePath"></param>
        public Logger(String LogFilePath) : this()
        {
            _logPath = LogFilePath;
        }
        /// <summary>
        /// Initialize HyveLog with a specified type of logging and a target logfile.
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="LogFilePath"></param>
        public Logger(LogTarget Type, String LogFilePath) : this(Type)
        {
            _logPath = LogFilePath;
        }
        

        public void Log(String Message)
        {
            //approach 1, switch based on enum?
            switch (_type)
            {
                case LogTarget.Console:
                {
                    WriteToLog.Console(Message);
                    break;
                }
                case LogTarget.File:
                {
                    WriteToLog.File(_logPath, Message);
                    break;
                }
                default:
                {
                    WriteToLog.Both(_logPath, Message);
                    break;
                }
            }
        }

        /// <summary>
        /// determine calling assembly's type to target the log correctly.
        /// </summary>
        /// <returns></returns>
        private LogTarget SetApplicationType()
        {
            if (Environment.UserInteractive)
            {
                return LogTarget.Console;
            }
            else
            {
                return LogTarget.File;
            }
        }
    }

    internal static class WriteToLog
    {
        public static void Console(String Message)
        {
            System.Console.WriteLine(Message);
        }
        public static void File(String FilePath, String Message)
        {
            //check if directory
            if (Directory.Exists(FilePath))
            {
                FilePath = Path.Combine(FilePath, "ErrorLog.txt");
            }
            System.IO.File.AppendAllText(FilePath, Message);
        }
        public static void Both(String FilePath, String Message)
        {
            Console(Message);
            File(FilePath, Message);
        }
    }
}

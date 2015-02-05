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
                    //default to user directory\errors if no path specified
                    if (String.IsNullOrEmpty(_logPath))
                    {
                        _logPath = GetDefaultPath();
                    }
                    _logPath = ValidatePath(_logPath);
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

        public void Log(String Message, String FilePath)
        {
            _logPath = FilePath;
            Log(Message);
        }

        private static String ValidatePath(String FilePath)
        {
            var FileDirectory = Path.GetDirectoryName(FilePath);
            //check if directory exists
            if (!Directory.Exists(FileDirectory))
            {
                Directory.CreateDirectory(FileDirectory);
            }
            //check if filename was specified
            if (String.IsNullOrEmpty(Path.GetFileName(FilePath)))
            {
                FilePath = Path.Combine(FilePath, "ErrorLog.txt");
            }
            return FilePath;
        }

        /// <summary>
        /// Get the location of the users appdata directory and create a default folder to write a file to.
        /// </summary>
        /// <returns></returns>
        private static String GetDefaultPath()
        {
            var UserPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Errors");
            return UserPath;
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
            System.IO.File.AppendAllText(FilePath, Message);
        }
        public static void Both(String FilePath, String Message)
        {
            Console(Message);
            File(FilePath, Message);
        }
    }
}

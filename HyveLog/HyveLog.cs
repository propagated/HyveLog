using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HyveLog
{
    public class Logger
    {
        public enum ApplicationType { Console, Service }
        private string logPath = String.Empty;

        private ApplicationType _type;
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
        public Logger(ApplicationType Type)
        {
            _type = Type;
        }

        public void Log(String Message)
        {
            //approach 1, switch based on enum?
            switch (_type)
            {
                case ApplicationType.Console:
                {
                    Console.WriteLine(Message);
                    break;
                }
                case ApplicationType.Service:
                {
                    //TODO determine logging location
                    File.WriteAllText(Path.Combine(logPath, "ErrorLog.txt"), Message);
                    break;
                }
                default:
                {
                    //do both
                    break;
                }
            }
        }

        /// <summary>
        /// determine calling assembly's type to target the log correctly.
        /// </summary>
        /// <returns></returns>
        private ApplicationType SetApplicationType()
        {
            if (Environment.UserInteractive)
            {
                return ApplicationType.Console;
            }
            else
            {
                return ApplicationType.Service;
            }
        }
    }
}

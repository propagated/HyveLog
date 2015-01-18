using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HyveLog
{
    public class HyveLog
    {
        public enum ApplicationType { Console, Service }

        private ApplicationType _type;
        /// <summary>
        /// Initialize HyveLog and let it determine where to log.
        /// </summary>
        public HyveLog()
        {
            _type = SetApplicationType();
        }
        /// <summary>
        /// Initialize HyveLog with a specified type of logging
        /// </summary>
        /// <param name="Type"></param>
        public HyveLog(ApplicationType Type)
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
                    
                    break;
                }
                case ApplicationType.Service:
                {
                    
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
            return ApplicationType.Console;
        }
    }

    internal static class Logger
    {
        public static void WriteToConsole(String Message)
        {
            Console.WriteLine("Hello Log. " + Message);
        }
        public static void WriteToErrorLog(String Message)
        {
            File.WriteAllText("LogTest\\LogTest.txt","Hello Log. " + Message);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyveLog;

using System.Timers;

namespace HyveLogTest
{
    public class Service
    {
        public void Run()
        {
            var timer = new Timer(500);
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;
            //Console.WriteLine("Hello World");
            var logger = new Logger(Logger.LogTarget.File);
            logger.Log("Test error message");
            Console.Read();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var logger = new Logger();
            logger.Log("test error message");
        }


        public void Close()
        {
            //Service Application is exiting. This is where your cleanup code should be. For example, a socket server would need "mySocketListener.Close();"
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyveLogTest
{
    public class Service
    {
        public void Run()
        {
            Console.WriteLine("Hello World");
            Console.Read();
        }


        public void Close()
        {
            //Service Application is exiting. This is where your cleanup code should be. For example, a socket server would need "mySocketListener.Close();"
        }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;

namespace HyveLogTest
{
    public partial class ServiceWrapper : ServiceBase
    {
        public ServiceWrapper()
        {
            
        }

        protected override void OnStart(string[] args)
        {
            //init and start Service Method
            var serv = new Service();
            serv.Run();
        }

        protected override void OnStop()
        {
            
        }

    }
    
}

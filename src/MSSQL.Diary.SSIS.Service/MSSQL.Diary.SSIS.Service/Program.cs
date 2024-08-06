using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MSSQL.Diary.SSIS.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceHandler serviceHandler = new ServiceHandler();
            serviceHandler.Start();

            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new SSISPackageService()
            //};
            //ServiceBase.Run(ServicesToRun);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
namespace MSSQL.Diary.SSIS.Service
{
    public partial class SSISPackageService : ServiceBase
    {
        public SSISPackageService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Timer timer = new Timer();
            timer.Interval = 10 * 6000; ;
            timer.Enabled = true;
            ServiceHandler serviceHandler = new ServiceHandler();
            serviceHandler.Start();

        } 
        protected override void OnStop()
        {
        }
    }
}

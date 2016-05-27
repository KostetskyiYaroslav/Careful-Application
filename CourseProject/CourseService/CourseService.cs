using CourseDBProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CourseService
{
    public partial class CourseService : ServiceBase
    {
        ServiceHost sh = null;

        public CourseService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            sh = new ServiceHost(typeof(Contract));
            sh.Open();
            Logger.Log("The Course Service is successfully started!");
        }

        protected override void OnStop()
        {
            Logger.Log("The Course Service is successfully stopped!");
            sh.Close();
        }
    }
}

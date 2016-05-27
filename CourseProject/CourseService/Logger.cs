using CourseDBProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseService
{
    public class Logger
    {
        public static String Log_TXT_Path = @"E:\CourseLogger.txt";

        public static void Log(String _message)
        {
            try
            {
                DataAccess.GetInstance().Save_Logs(new tbl_Logs { date = DateTime.Now, message = _message });
                
                StreamWriter file = new StreamWriter(Log_TXT_Path, true);
                file.WriteLine(_message);
                file.Close();
            }
            catch (Exception ex)
            {
                StreamWriter file = new StreamWriter(Log_TXT_Path, true);
                Exception e = ex;

                while (e != null)
                {
                    file.WriteLine(e.Message);
                    e = e.InnerException;
                }

                file.Close();
            }
        }
    }
}

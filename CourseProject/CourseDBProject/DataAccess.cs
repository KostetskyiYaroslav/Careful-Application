using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDBProject
{
    public class DataAccess 
    {
        private CourseDBEntities DB = null;
        private static DataAccess Instance = null;

        private DataAccess() 
        {
            this.DB = new CourseDBEntities();
        }

        public static DataAccess GetInstance()
        {
            if (Instance == null)
            {
                Instance = new DataAccess();
            }

            return Instance;
        }

        public bool Save_Logs(tbl_Logs _model)
        {
            this.DB.tbl_Logs.Add(_model);
            this.DB.SaveChanges();

            return true;
        }

        public bool Save_Fail(tbl_Fail _model)
        {
            this.DB.tbl_Fail.Add(_model);
            this.DB.SaveChanges();

            return true;
        }

        public bool Save_Status(tbl_Status _model)
        {
            this.DB.tbl_Status.Add(_model);
            this.DB.SaveChanges();

            return true;
        }

        public List<tbl_Cities> Get_Cities()
        {
            return this.DB.tbl_Cities.ToList();
        }

        public tbl_Cities Get_City(String _name)
        {
            List<tbl_Cities> City = this.DB.tbl_Cities.Where(x => x.Name == _name).ToList();
            return City[0];
        }

        public List<tbl_Fail> Get_Fail()
        {
            return this.DB.tbl_Fail.ToList();
        }

        public List<tbl_Logs> Get_Logs()
        {
            return this.DB.tbl_Logs.ToList();
        }
    }
}

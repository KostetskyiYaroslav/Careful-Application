using CourseDBProject;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    [ServiceContract]
    public interface iContract
    {

        [OperationContract]
        bool Login(User _model);

        [OperationContract]
        bool Logout();

        [OperationContract]
        List<tbl_Cities> Get_Cities();

        [OperationContract]
        tbl_Cities Get_City(String _name);

        [OperationContract]
        bool Change_Status(tbl_Cities _city);
    }
}

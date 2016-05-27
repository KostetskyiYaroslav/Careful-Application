using CourseDBProject;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CourseService
{
    [ServiceContract]
    public interface iContract
    {
       
        [OperationContract]
        ///<summary>
        ///Function to authorization
        ///</summary>
        ///<param name="_user">tbl_Users which try to log in</param>
        bool Login(User _model);

        [OperationContract]

        ///<summary>
        ///Function to logout
        ///</summary>
        bool Logout();

        [OperationContract]
        ///<summary>
        ///Function to get all cities
        ///</summary>
        List<tbl_Cities> Get_Cities();

        [OperationContract]
        ///<summary>
        ///Function to get city by name
        ///</summary>
        ///<param name="_name">name of city</param>
        tbl_Cities Get_City(String _name);

        [OperationContract]
        ///<summary>
        ///Function to start new there to change Client status
        ///</summary>
        bool Change_Status(tbl_Cities _city);
    }
}

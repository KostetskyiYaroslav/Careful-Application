using CourseDBProject;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace CourseService
{
    class Contract : iContract
    {
        private         ulong        AppId   = 5478250;
        private static  int          ChatId  = 44;
        private         DataAccess   DA      = DataAccess.GetInstance();
        public  static  SingleAPI    sAPI    = SingleAPI.GetInstance();

        public Contract() { }

        public bool Login(User _user)
        {
            try
            {
                sAPI.API.Authorize(new ApiAuthParams
                {
                    ApplicationId = AppId,
                    Login = _user.Login,
                    Password = _user.Password,
                    Settings = Settings.All
                });
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                return false;
            }

            Logger.Log(String.Format("User, {0}, has successfully loged in!", _user.Login));
            return true;
        }

        public bool Logout()
        {
            Logger.Log("User has been logged out!");

            return true;
        }
       
        public List<tbl_Cities> Get_Cities()
        {
            return DA.Get_Cities();
        }

        public bool Change_Status(tbl_Cities _city)
        {
            try
            {
                Thread t = new Thread(Start_Change_Status);
                t.Start(_city);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                return false;
            }

            return true;
        }

        public tbl_Cities Get_City(String _name)
        {
            return DA.Get_City(_name);
        }

        ///<summary>
        ///Function - timer for update Client status 
        ///</summary>
        ///<param name="_model">Object as tbl_Cities</param>
        private static void Start_Change_Status(Object _model = null)
        {
            tbl_Cities _city = _model as tbl_Cities;
            String newStatus = "";

            System.Timers.Timer aTimer = new System.Timers.Timer();

            aTimer.Elapsed += new ElapsedEventHandler(delegate
            {
                try
                {
                    String temperature = Contract.Get_Date(_city.Site_Name);
                    Int32 Int_Temp = Int32.Parse(temperature);

                    newStatus = String.Format("At {0} the temperature in my city({2}) is {1} °С", DateTime.Now, temperature, _city.Name);

                    #region Worrisome
                    if (Int_Temp < 20)
                    {
                        Send_Temp_Cold_Warning();
                    }
                    else if (Int_Temp > 25)
                    {
                        Send_Temp_Hot_Warning();
                    }
                    #endregion

                    #region Change & Log Status
                    if (sAPI.API.Status.Set(newStatus))
                    {

                        Logger.Log(String.Format("Status Updated!"));
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message);
                }

            });

            aTimer.Interval = 10000;
            aTimer.Enabled = true;
        }

        #region Get Temperature
        ///<summary>
        ///Function to get temperature from sinoptic.ua
        ///</summary>
        ///<param name="_city_url">link of City URL on the Sinoptic website</param>
        private static String Get_Date(String _city_url)
        {
            StringBuilder sb_temp = new StringBuilder(999);
            string urlAddress = _city_url;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();

                StreamReader readStream = (response.CharacterSet == null) ? new StreamReader(receiveStream) : new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                string data = readStream.ReadToEnd();
                data = data.Replace(" ", "");
                data = data.Replace(">", "");
                data = data.Replace("<", "");

                string find = "class=\"today-temp\"";
                int pos = data.IndexOf(find) - 1;

                for (int i = 1; i < 4; i++)
                {
                    sb_temp.Append(data[pos + find.Length + i]);
                }

                response.Close();
                readStream.Close();
            }

            return sb_temp.ToString();
        }

        #endregion

        #region Worrisome Functions

        ///<summary>
        ///Function to send notif to inform Client about Cold weather
        ///</summary>
        private static void Send_Temp_Cold_Warning()
        {
            sAPI.API.Messages.Send(new MessageSendParams
            {
                ChatId = Contract.ChatId,
                Message = String.Format("Message #{0}: Outside is cold, don't forget wear something warm!", new Random().Next(666666, 999999))
            });
        }

        ///<summary>
        ///Function to send notif to inform Client about Hot weather
        ///</summary>
        private static void Send_Temp_Hot_Warning()
        {
            sAPI.API.Messages.Send(new MessageSendParams
            {
                ChatId = 44,
                Message = String.Format("Message #{0}: Outside is hot, don't wear something warm!", new Random().Next(666666, 999999))
            });
        }
        #endregion
    }
}

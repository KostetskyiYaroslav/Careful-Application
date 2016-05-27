using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;

namespace CourseService
{
    public class SingleAPI
    {
        public VkApi API = null;

        private static SingleAPI Instance = null;

        private SingleAPI()
        {
            this.API = new VkApi();
        }
        public static SingleAPI GetInstance()
        {
            if (Instance == null)
            {
                Instance = new SingleAPI();
            }

            return Instance;
        }

    }
}

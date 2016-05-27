using CourseDBProject;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProject.Pages
{
    /// <summary>
    /// Interaction logic for Select_Location.xaml
    /// </summary>
    public partial class Select_Location : UserControl
    {
        private iContract channel = null;

        public Select_Location()
        {
            InitializeComponent();

            Uri address = new Uri("http://localhost:8000/Service");
            BasicHttpBinding binding = new BasicHttpBinding();

            EndpointAddress endpoint = new EndpointAddress(address);
            ChannelFactory<iContract> factory = new ChannelFactory<iContract>("myContract");

            channel = factory.CreateChannel();
            this.Load_Cities();
        }

        private void Load_Cities()
        {
            List<tbl_Cities> Cities = this.channel.Get_Cities();

            foreach (tbl_Cities City in Cities)
            {
                this.Cities_LB.Items.Add(City.Name);
            }

            this.Cities_LB.SelectedIndex = 0;
        }

        private void ChangeLocation_B_Click(object sender, RoutedEventArgs e)
        {
            String CityName = this.Cities_LB.SelectedValue.ToString();

            if (!String.IsNullOrWhiteSpace(CityName) && !String.IsNullOrEmpty(CityName))
            {
                tbl_Cities NewCity = this.channel.Get_City(CityName);
                this.channel.Change_Status(NewCity);
            }
        }
    }
}

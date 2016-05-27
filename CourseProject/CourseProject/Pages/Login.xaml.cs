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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        private iContract channel = null;

        public Login()
        {
            InitializeComponent();

            Uri address = new Uri("http://localhost:8000/Service");
            BasicHttpBinding binding = new BasicHttpBinding();

            EndpointAddress endpoint = new EndpointAddress(address);
            ChannelFactory<iContract> factory = new ChannelFactory<iContract>("myContract");

            channel = factory.CreateChannel();
        }

        private void Login_B_Click(object sender, RoutedEventArgs e)
        {
            string Login = this.Login_TB.Text;
            string Password = this.Password_PB.Password;
      
            User model = new User
            {
                Login = Login,
                Password = Password
            };
            
            try
            {
                if (this.channel.Login(model))
                {
                    MessageBox.Show("Welcome, " + Login + "!");
                }
                else
                {
                    MessageBox.Show("User dose not exist or login/password don't correct!\nTry agine or Sing Up!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.ResetField();
            }
        }

        private void ResetField()
        {
            this.Login_TB.Text = "";
            this.Password_PB.Password = "";
        }

    }
}

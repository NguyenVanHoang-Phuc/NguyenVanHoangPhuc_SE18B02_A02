using BusinessOjects;
using Microsoft.Extensions.Configuration;
using Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly string adminEmail;
        private readonly string adminPassword;

        public LoginWindow()
        {
            InitializeComponent();
            _customerRepository = new CustomerRepository();
            var configuration = LoadConfiguration();
            adminEmail = configuration["DefaultAdminAccount:Email"];
            adminPassword = configuration["DefaultAdminAccount:Password"];
        }

        private IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtUser.Text;
            string password = txtPass.Password;

            if (email.Equals(adminEmail, StringComparison.OrdinalIgnoreCase) && password == adminPassword)
            {
                this.Hide();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                return;
            }

            Customer? account = _customerRepository.GetCustomerByEmail(email);

            if (account != null && account.Password == password)
            {
                this.Hide();
                CustomerProfile customerProfile = new CustomerProfile(account);
                customerProfile.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password!", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

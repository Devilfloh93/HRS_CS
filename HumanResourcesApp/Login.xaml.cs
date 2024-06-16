using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Security.Cryptography;
using System.Security.Policy;
using Microsoft.Data.Sqlite;
using System.Xml.Linq;
using System.IO;
using HumanResourcesApp.Dao;

namespace HumanResourcesApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly LoginDao loginDao = new();

        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {
            if (CalculateSha256Hash(textBoxPassword.Password) == loginDao.GetPassword(textBoxUsername.Text))
            {
                MainWindow mainWindow = new(loginDao.GetEmployeeID(textBoxUsername.Text));
                this.Close();
            }
            else
                Debug.WriteLine("Wrong Username or Password!");
        }

        private string CalculateSha256Hash(string rawData)
        {
            // Create a SHA256
            using SHA256 sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        private void Button_Click_ResetPassword(object sender, RoutedEventArgs e)
        {
            ResetPassword resetPasswordWindow = new();
            resetPasswordWindow.Show();
            this.Close();
        }
    }
}
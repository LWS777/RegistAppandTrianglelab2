using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ReggApp
{
    
    public partial class MainWindow : Window
    {
        
        public string LogFile = @"F:\logi.txt";
        public List<string> existingLogins = new List<string> { "admin", "user1", "user2" };

        public MainWindow()
        {
            InitializeComponent();



        }

        public void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            bool isValid = ValidateRegistration(login, password, confirmPassword, out string message);

            if (isValid)
            {
                RegisterUser(login, password);

                MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                LogSuccessfulRegistration(login, password);

                ClearFields();

                // Обновление лога успешных регистраций на ListBox
                SuccessfulRegistrationsListBox.Items.Add($"Login: {login}, Password: {password}");
            }
            else
            {
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                LogFailedRegistration(login, password, message);

                // Обновление лога неудачных регистраций на ListBox
                FailedRegistrationsListBox.Items.Add($"Login: {login}, Password: {password}, Error: {message}");
            }
        }

        public bool ValidateRegistration(string login, string password, string confirmPassword, out string message)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                message = "Пожалуйста, заполните все поля!";
                return false;
            }

            if (!IsValidLogin(login))
            {
                message = "Неправильный формат логина!";
                return false;
            }

            if (existingLogins.Contains(login))
            {
                message = "Логин уже существует!";
                return false;
            }

            if (password != confirmPassword)
            {
                message = "Пароли не совпадают!";
                return false;
            }

            if (!IsValidPassword(password))
            {
                message = "Неправильный формат пароля!";
                return false;
            }

            message = string.Empty;
            return true;
        }

        public bool IsValidLogin(string login)
        {
            return IsValidEmail(login) || IsValidPhone(login) || IsValidString(login);
        }

        

        public bool IsValidEmail(string email)
        {
            // Проверка формата email
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        public bool IsValidPhone(string phone)
        {
            // Проверка формата телефона
            string pattern = @"^\+(\d{1,3})-\d{3}-\d{3}-\d{4}$";
            return Regex.IsMatch(phone, pattern);
        }

        public bool IsValidString(string str)
        {
            // Проверка формата строки
            string pattern = @"^[A-Za-z0-9_]{5,}$";
            return Regex.IsMatch(str, pattern);
        }

        public bool IsValidPassword(string password)
        {
            // Проверка формата пароля
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=])(?=.*[^\da-zA-Z]).{7,}$";
            return Regex.IsMatch(password, pattern);
        }

        public void RegisterUser(string login, string password)
        {

            if (!File.Exists(LogFile))
            {
                File.Create(LogFile).Close();
            }

            using (StreamWriter writer = File.AppendText(LogFile))
            {
                writer.WriteLine($"Login: {login}, Password: {password}");
            }
            existingLogins.Add(login);
        }

        public void LogSuccessfulRegistration(string login, string password)
        {
            // Логирование успешной регистрации
            string logEntry = $"{DateTime.Now}: Login: {login}, Password: {MaskPassword(password)}, Confirmation: {MaskPassword(ConfirmPasswordBox.Password)}, Success";

            LogToFile(logEntry);
            LogToConsole(logEntry);
        }

        public void LogFailedRegistration(string login, string password, string errorMessage)
        {
            string logEntry = $"{DateTime.Now}: Login: {login}, Password: {MaskPassword(password)}, Confirmation: {MaskPassword(ConfirmPasswordBox.Password)}, Error: {errorMessage}";

            LogToFile(logEntry);
            LogToConsole(logEntry);
        }

        public void LogToFile(string logEntry)
        {
            using (StreamWriter writer = File.AppendText(LogFile))
            {
                writer.WriteLine(logEntry);
            }
        }

        public void LogToConsole(string logEntry)
        {
            Console.WriteLine(logEntry);
        }

        public string MaskPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public void ClearFields()
        {
            LoginTextBox.Text = string.Empty;
            PasswordBox.Password = string.Empty;
            ConfirmPasswordBox.Password = string.Empty;
        }


    }
}
using NUnit.Framework;
using ReggApp;
using System.ComponentModel.DataAnnotations;
using System.Configuration;


namespace ReggAppTest
{

    [TestFixture]
    public class MainWindowTests
    {
        
        [Test]
        [Apartment(ApartmentState.STA)]
        public void IsValidLogin_ValidLogin_ReturnsTrue()
        {
            // Arrange
            var mainWindow = new MainWindow();

            // Act
            bool isValid = mainWindow.IsValidLogin("test@example.com");

            // Assert
            Assert.True(isValid);
        }

        [Test]
        [Apartment(ApartmentState.STA)]

        [TestCase("test@example.com")]
        [TestCase("username")]
        [TestCase("+7-952-934-2171")]
        public void IsValidLogin_ValidLogin_ReturnsTrue(string login)
        {
            // Arrange
            var mainWindow = new MainWindow();

            // Act
            bool isValid = mainWindow.IsValidLogin(login);

            // Assert
            Assert.True(isValid);
        }

        [Test]
        [Apartment(ApartmentState.STA)]

        [TestCase("")]
        [TestCase("leosh@")]
        [TestCase("+79529342171")]
        public void IsValidLogin_InvalidLogin_ReturnsFalse(string login)
        {
            // Arrange
            var mainWindow = new MainWindow();

            // Act
            bool isValid = mainWindow.IsValidLogin(login);

            // Assert
            Assert.False(isValid);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void ValidateRegistration_WithValidInput_ReturnsTrue()
        {
            // Arrange
            var mainWindow = new MainWindow();
            string login = "test@example.com";
            string password = "Passw0rd@";
            string confirmPassword = "Passw0rd@";
            string message;

            // Act
            bool result = mainWindow.ValidateRegistration(login, password, confirmPassword, out message);

            // Assert
            Assert.IsTrue(result);
            Assert.IsEmpty(message);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void ValidateRegistration_WithEmptyLogin_ReturnsFalseAndErrorMessage()
        {
            // Arrange
            var mainWindow = new MainWindow();
            string login = string.Empty;
            string password = "Passw0rd";
            string confirmPassword = "Passw0rd";
            string message;

            // Act
            bool result = mainWindow.ValidateRegistration(login, password, confirmPassword, out message);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Пожалуйста, заполните все поля!", message);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void ValidateRegistration_WithInvalidLoginFormat_ReturnsFalseAndErrorMessage()
        {
            // Arrange
            var mainWindow = new MainWindow();
            string login = "234@kkk";
            string password = "Passw0rd@";
            string confirmPassword = "Passw0rd@";
            string message;

            // Act
            bool result = mainWindow.ValidateRegistration(login, password, confirmPassword, out message);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Неправильный формат логина!", message);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void ValidateRegistration_WithExistingLogin_ReturnsFalseAndErrorMessage()
        {
            // Arrange
            var mainWindow = new MainWindow();
            string login = "admin";
            string password = "Passw0rd@";
            string confirmPassword = "Passw0rd@";
            string message;

            // Act
            bool result = mainWindow.ValidateRegistration(login, password, confirmPassword, out message);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Логин уже существует!", message);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void ValidateRegistration_WithMismatchedPasswords_ReturnsFalseAndErrorMessage()
        {
            // Arrange
            var mainWindow = new MainWindow();
            string login = "test@example.com";
            string password = "Passw0rd";
            string confirmPassword = "password";
            string message;

            // Act
            bool result = mainWindow.ValidateRegistration(login, password, confirmPassword, out message);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Пароли не совпадают!", message);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void ValidateRegistration_WithInvalidPasswordFormat_ReturnsFalseAndErrorMessage()
        {
            // Arrange
            var mainWindow = new MainWindow();
            string login = "test@example.com";
            string password = "password";
            string confirmPassword = "password";
            string message;

            // Act
            bool result = mainWindow.ValidateRegistration(login, password, confirmPassword, out message);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Неправильный формат пароля!", message);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void IsValidEmail_ValidEmail_ReturnsTrue()
        {
            // Arrange
            var mainWindow = new MainWindow();
            string email = "leosh06@mail.ru";

            // Act
            bool isValid = mainWindow.IsValidEmail(email);

            // Assert
            Assert.IsTrue(isValid);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void IsValidEmail_InvalidEmail_ReturnsFalse()
        {
            // Arrange
            var mainWindow = new MainWindow();
            string email = "777777";

            // Act
            bool isValid = mainWindow.IsValidEmail(email);

            // Assert
            Assert.IsFalse(isValid);
        }


        [Test]
        [Apartment(ApartmentState.STA)]
        [TestCase("test@example")]
        [TestCase("test.example.com")]
        [TestCase("test@.com")]
        public void IsValidEmail_InvalidEmailFormats_ReturnsFalse(string email)
        {
            var mainWindow = new MainWindow();
            // Act
            bool isValid = mainWindow.IsValidEmail(email);

            // Assert
            Assert.IsFalse(isValid);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void IsValidPhone_ValidPhone_ReturnsTrue()
        {
            var mainWindow = new MainWindow();
            // Arrange
            string phone = "+7-952-934-2171";

            // Act
            bool isValid = mainWindow.IsValidPhone(phone);

            // Assert
            Assert.IsTrue(isValid);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void IsValidPhone_InvalidPhone_ReturnsFalse()
        {
            var mainWindow = new MainWindow();
            // Arrange
            string phone = "123-456-7890"; // Missing country code

            // Act
            bool isValid = mainWindow.IsValidPhone(phone);

            // Assert
            Assert.IsFalse(isValid);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        [TestCase("+1-1234-456-7890")] // Invalid middle part length
        [TestCase("+1-123-4567-7890")] // Invalid middle part length
        [TestCase("+1-123-456-78901")] // Invalid last part length
        [TestCase("12345")] // Invalid format
        [TestCase("+1-123-456")] // Invalid format
        public void IsValidPhone_InvalidPhoneFormats_ReturnsFalse(string phone)
        {
            var mainWindow = new MainWindow();
            // Act
            bool isValid = mainWindow.IsValidPhone(phone);

            // Assert
            Assert.IsFalse(isValid);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void IsValidString_ValidString_ReturnsTrue()
        {
            // Arrange
            var mainWindow = new MainWindow();
            string str = "AbcDef_123";

            // Act
            bool isValid = mainWindow.IsValidString(str);

            // Assert
            Assert.IsTrue(isValid);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void IsValidString_InvalidString_ReturnsFalse()
        {
            // Arrange
            var mainWindow = new MainWindow();
            string str = "Abc12$%";

            // Act
            bool isValid = mainWindow.IsValidString(str);

            // Assert
            Assert.IsFalse(isValid);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        [TestCase("abc")]
        [TestCase("abc_")]
        [TestCase("abc_def_ghi-")]
        public void IsValidString_InvalidStringFormats_ReturnsFalse(string str)
        {
            var mainWindow = new MainWindow();
            // Act
            bool isValid = mainWindow.IsValidString(str);

            // Assert
            Assert.IsFalse(isValid);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void IsValidPassword_ValidPassword_ReturnsTrue()
        {
            // Arrange
            var mainWindow = new MainWindow();
            string password = "AbcDef123@";

            // Act
            bool isValid = mainWindow.IsValidPassword(password);

            // Assert
            Assert.IsTrue(isValid);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void IsValidPassword_InvalidPassword_ReturnsFalse()
        {
            // Arrange
            var mainWindow = new MainWindow();
            string password = "abc123"; 

            // Act
            bool isValid = mainWindow.IsValidPassword(password);

            // Assert
            Assert.IsFalse(isValid);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        [TestCase("abcdefg")] 
        [TestCase("ABCDEFG")] 
        [TestCase("1234567")] 
        [TestCase("!@#$%^&*")]
        [TestCase("p@ssword_123")] 
        [TestCase("PASSWORD_123")] 
        [TestCase("Password123")] 
        [TestCase("Password123!")] 
        public void IsValidPassword_InvalidPasswordFormats_ReturnsFalse(string password)
        {
            var mainWindow = new MainWindow();
            // Act
            bool isValid = mainWindow.IsValidPassword(password);

            // Assert
            Assert.IsFalse(isValid);
        }

        
    }

}
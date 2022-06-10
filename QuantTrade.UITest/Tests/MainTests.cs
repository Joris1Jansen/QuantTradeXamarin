using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace QuantTrade.UITest.Tests
{
    public class MainTests : BaseTest
    {
        const string _correctEmail = "test@test.com";
        const string _wrongEmail = "test";
        const string _correctPassword = "password123";
        const string _wrongPassword = "123";
        
        public MainTests(Platform platform) : base(platform)
        {
        }

        [Test]
        public void EnterWrongEmail()
        {
            //Arrange
            var wrongEmail = _wrongEmail;
            var emailError = "Invalid Email";

            //Act
            MainPage.EnterEmail(wrongEmail);

            //Assert
            Assert.AreEqual(MainPage.GetEmailLabelText(), emailError);
            Assert.IsFalse(MainPage.GetLoginButtonEnabled());
        }
        
        [Test]
        public void EnterWrongPassword()
        {
            //Arrange
            var wrongEmail = _wrongPassword;
            var passwordError = "Passwords too short, 6 characters required";

            //Act
            MainPage.EnterPassword(wrongEmail);

            //Assert
            Assert.AreEqual(MainPage.GetPasswordLabelText(), passwordError);
            Assert.IsFalse(MainPage.GetLoginButtonEnabled());
        }
        
        [Test]
        public void TryLoginWithoutPassword()
        {
            //Arrange
            var correctEmail = _correctEmail;

            //Act
            MainPage.EnterEmail(correctEmail);

            //Assert
            Assert.IsFalse(MainPage.GetLoginButtonEnabled());
        }
        
        [Test]
        public void TryLoginWithoutEmail()
        {
            //Arrange
            var correctPassword = _correctPassword;

            //Act
            MainPage.EnterPassword(correctPassword);

            //Assert
            Assert.IsFalse(MainPage.GetLoginButtonEnabled());
        }
        
        [Test]
        public void TryLogin()
        {
            //Arrange
            var correctEmail = _correctEmail;
            var correctPassword = _correctPassword;

            //Act
            MainPage.EnterEmail(correctEmail);
            MainPage.EnterPassword(correctPassword);

            //Assert
            Assert.IsTrue(MainPage.GetLoginButtonEnabled());
        }
        
        [Test]
        public void TryRegisterButton()
        {
            //Arrange
            MainPage.PressRegisterButton();
            
            //Act
            RegisterPage.WaitForPageToLoad();

            //Assert
            Assert.IsFalse(RegisterPage.GetRegisterButtonEnabled());
            Assert.AreEqual(RegisterPage.GetRegisterLabelText(), "Register");
        }
    }
}
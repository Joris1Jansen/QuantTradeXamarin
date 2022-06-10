using NUnit.Framework;
using Xamarin.UITest;

namespace QuantTrade.UITest.Tests
{
    public class RegisterTest : BaseTest
    {
        const string _correctEmail = "test@test.com";
        const string _wrongEmail = "test";
        const string _correctPassword = "password123";
        const string _wrongPassword = "123";
        const string _correctVerifyPassword = "password123";
        const string _wrongVerifyPassword = "123";
        const string _mismatchVerifyPassword = "password321";
        
        public RegisterTest(Platform platform) : base(platform)
        {
        }

        public override void BeforeEachTest()
        {
            base.BeforeEachTest();

            MainPage.WaitForPageToLoad();

            MainPage.PressRegisterButton();
        }

        [Test]
        public void EnterWrongEmail()
        {
            //Arrange
            var wrongEmail = _wrongEmail;
            var emailError = "Invalid Email";

            //Act
            RegisterPage.EnterEmail(wrongEmail);

            //Assert
            Assert.AreEqual(RegisterPage.GetEmailLabelText(), emailError);
            Assert.IsFalse(RegisterPage.GetRegisterButtonEnabled());
        }
        
        [Test]
        public void EnterWrongPassword()
        {
            //Arrange
            var wrongPassword = _wrongPassword;
            var passwordError = "Passwords too short, 6 characters required";

            //Act
            RegisterPage.EnterPassword(wrongPassword);

            //Assert
            Assert.AreEqual(RegisterPage.GetPasswordLabelText(), passwordError);
            Assert.IsFalse(RegisterPage.GetRegisterButtonEnabled());
        }

        [Test]
        public void EnterWrongVerifyPassword()
        {
            //Arrange
            var wrongVerifyPassword = _wrongVerifyPassword;
            var passwordError = "Passwords do not match";

            //Act
            RegisterPage.EnterVerifyPassword(wrongVerifyPassword);

            //Assert
            Assert.AreEqual(RegisterPage.GetPasswordVerifyLabelText(), passwordError);
            Assert.IsFalse(RegisterPage.GetRegisterButtonEnabled());
        }
        
        [Test]
        public void EnterMismatchPasswords()
        {
            //Arrange
            var correctPassword = _wrongPassword;
            var mismatchVerifyPassword = _mismatchVerifyPassword;
            var passwordError = "Passwords do not match";

            //Act
            RegisterPage.EnterPassword(correctPassword);
            RegisterPage.EnterVerifyPassword(mismatchVerifyPassword);

            //Assert
            Assert.AreEqual(RegisterPage.GetPasswordVerifyLabelText(), passwordError);
            Assert.IsFalse(RegisterPage.GetRegisterButtonEnabled());
        }
        
        [Test]
        public void TryRegisterWithoutPassword()
        {
            //Arrange
            var correctEmail = _correctEmail;

            //Act
            RegisterPage.EnterEmail(_correctEmail);
            
            //Assert
            Assert.IsFalse(RegisterPage.GetRegisterButtonEnabled());
        }
        
        [Test]
        public void TryRegisterWithoutEmail()
        {
            //Arrange
            var correctPassword = _correctPassword;
            var correctVerifyPassword = _correctVerifyPassword;

            //Act
            RegisterPage.EnterPassword(correctPassword);
            RegisterPage.EnterVerifyPassword(correctVerifyPassword);

            //Assert
            Assert.IsFalse(RegisterPage.GetRegisterButtonEnabled());
        }
        
        [Test]
        public void TryRegister()
        {
            //Arrange
            var correctEmail = _correctEmail;
            var correctPassword = _correctPassword;
            var correctVerifyPassword = _correctVerifyPassword;
            
            //Act
            RegisterPage.EnterEmail(correctEmail);
            RegisterPage.EnterPassword(correctPassword);
            RegisterPage.EnterVerifyPassword(correctVerifyPassword);

            //Assert
            Assert.IsTrue(RegisterPage.GetRegisterButtonEnabled());
        }
        
        [Test]
        public void TryLoginButton()
        {
            //Arrange
            RegisterPage.PressLoginButton();
            
            //Act
            MainPage.WaitForPageToLoad();

            //Assert
            Assert.AreEqual(MainPage.GetLoginLabelText(), "Login");
        }
    }
}
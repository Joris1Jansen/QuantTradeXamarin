using System.Threading.Tasks;
using NUnit.Framework;
using QuantTrade.ViewModel;

namespace QuantTrade.Test.PageModelTests
{
    [TestFixture]
    public class RegisterVMTest
    {
        RegisterVM _vm;

        [SetUp]
        public void Setup()
        {
            _vm = new RegisterVM();
            
        }
        
        [Test]
        public async Task Register_EmailIsSet_PasswordNot()
        {
            // Arrange             
            _vm.Email = "test@test.com";

            // Act
            await _vm.InitializeAsync();

            // Assert
            Assert.AreEqual(_vm.FormIsValid, false);
        }
        
        [Test]
        public async Task Register_PasswordIsSet_EmailNot()
        {
            // Arrange             
            _vm.Password = "DoKwon123123";

            // Act
            await _vm.InitializeAsync();

            // Assert
            Assert.AreEqual(_vm.FormIsValid, false);
        }
        
        [Test]
        public async Task Register_InvalidEmail()
        {
            // Arrange             
            _vm.Email = "test@test";

            // Act
            await _vm.InitializeAsync();

            // Assert
            Assert.AreEqual(_vm.FormIsValid, false);
            Assert.AreEqual(_vm.EmailError, "Invalid Email");
        }
        
        [Test]
        public async Task Register_ValidEmail_ValidPassword_NoVerifyPassword()
        {
            // Arrange             
            _vm.Email = "test@test.com";
            _vm.Password = "DoKwon123123";

            // Act
            await _vm.InitializeAsync();

            // Assert
            Assert.AreEqual(_vm.FormIsValid, false);
        }
        
        [Test]
        public async Task Register_ValidEmail_ValidPassword_WrongVerifyPassword()
        {
            // Arrange             
            _vm.Email = "test@test.com";
            _vm.Password = "DoKwon123123";
            _vm.VerifyPassword = "test";

            // Act
            await _vm.InitializeAsync();

            // Assert
            Assert.AreEqual(_vm.FormIsValid, false);
            Assert.AreEqual(_vm.VerifyPasswordError, "Passwords do not match");
        }
        
        [Test]
        public async Task CanRegister_ValidEmail_ValidPassword_ValidVerifyPassword()
        {
            // Arrange             
            _vm.Email = "test@test.com";
            _vm.Password = "DoKwon123123";
            _vm.VerifyPassword = "DoKwon123123";

            // Act
            await _vm.InitializeAsync();

            // Assert
            Assert.AreEqual(_vm.FormIsValid, true);
        }
    }
}
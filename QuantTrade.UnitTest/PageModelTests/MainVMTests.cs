using System.Threading.Tasks;
using Autofac.Extras.Moq;
using NUnit.Framework;
using QuantTrade.ViewModel;
using AutoFixture;
using FluentAssertions;


namespace QuantTrade.Test.PageModelTests
{
    [TestFixture]
    public class LoginPageVMTest
    {
        MainVM _vm;

        [SetUp]
        public void Setup()
        {
            _vm = new MainVM();
        }
        
        [Test]
        public async Task Login_EmailIsSet_PasswordNot()
        {
            // Arrange             
            _vm.Email = "test@test.com";

            // Act
            await _vm.InitializeAsync();

            // Assert
            Assert.AreEqual(_vm.FormIsValid, false);
        }
        
        [Test]
        public async Task Login_PasswordIsSet_EmailNot()
        {
            // Arrange             
            _vm.Password = "DoKwon123123";

            // Act
            await _vm.InitializeAsync();

            // Assert
            Assert.AreEqual(_vm.FormIsValid, false);
        }
        
        [Test]
        public async Task Login_InvalidEmail()
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
        public async Task CanLogin_ValidEmail_ValidPassword()
        {
            // Arrange             
            _vm.Email = "test@test.com";
            _vm.Password = "DoKwon123123";

            // Act
            await _vm.InitializeAsync();

            // Assert
            Assert.AreEqual(_vm.FormIsValid, true);
        }
    }
}
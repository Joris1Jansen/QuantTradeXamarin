using System.Threading.Tasks;
using NUnit.Framework;
using QuantTrade.Model;

namespace QuantTrade.Test.PageModelTests
{
    [TestFixture]
    public class CBHoldingTests
    {
        CBHolding _cbHolding;

        [SetUp]
        public void Setup()
        {
            _cbHolding = new CBHolding("123abc123", (decimal)0.05, "BTC", 31550);
        }

        [Test]
        public async Task Holding_GetValue()
        {
            // Arrange
            

            // Act
            var value = _cbHolding.Worth;

            // Assert
            Assert.AreEqual(value, (decimal)0.05 * 31550);
        }
    }
}
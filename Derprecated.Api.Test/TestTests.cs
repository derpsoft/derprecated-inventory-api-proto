using BausCode.Api.Models;
using NUnit.Framework;

namespace Derprecated.Api.Test
{
    [TestFixture]
    public class TestTests
    {
        [Test(Author = TestConstants.Authors.James)]
        public void CanRunTests()
        {
            Assert.True(true);
        }
    }
}
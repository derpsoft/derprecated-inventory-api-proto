using BausCode.Api.Models.Test;
using NUnit.Framework;

namespace Derprecated.Api.Test
{
    [TestFixture]
    public class TestTests
    {
        [Test(Author = Constants.Authors.James)]
        public void CanRunTests()
        {
            Assert.True(true);
        }
    }
}
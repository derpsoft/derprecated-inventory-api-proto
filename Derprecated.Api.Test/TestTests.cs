namespace Derprecated.Api.Test
{
    using BausCode.Api.Models.Test;
    using NUnit.Framework;

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

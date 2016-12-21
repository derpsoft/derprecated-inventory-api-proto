namespace Derprecated.Api.Test.Core
{
    using Models.Test;
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

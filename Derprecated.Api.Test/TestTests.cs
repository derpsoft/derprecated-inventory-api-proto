using BausCode.Api.Models.Test;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;

namespace Derprecated.Api.Test
{
    [TestFixture]
    public class TestTests
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public static ServiceStackHost AppHost;

        public static void Setup()
        {
            AppHost = new BasicAppHost().Init();
        }

        [Test(Author = Constants.Authors.James)]
        public void CanRunTests()
        {
            Assert.True(true);
        }
    }
}
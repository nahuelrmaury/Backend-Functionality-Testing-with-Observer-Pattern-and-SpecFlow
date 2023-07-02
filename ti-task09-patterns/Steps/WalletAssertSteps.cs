using NUnit.Framework;
using System.Net;
using TechTalk.SpecFlow;

namespace BackendTests.Steps
{
    [Binding]
    public sealed class WalletAssertSteps
    {
        private readonly DataContext _context;

        public WalletAssertSteps(DataContext context)
        {
            _context = context;
        }

        [Then(@"Status code is '([^']*)'")]
        public void ThenStatusCodeIs(HttpStatusCode expected)
        {
            Assert.That(_context.ChargeResponse.Status, Is.EqualTo(expected));
        }
    }
}
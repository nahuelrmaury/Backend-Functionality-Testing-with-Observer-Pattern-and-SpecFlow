using BackendTests.Models.Responses.Base;
using NUnit.Framework;
using System.Net;
using TechTalk.SpecFlow;

namespace BackendTests.Steps
{
    [Binding]
    public sealed class UserAssertSteps
    {
        private readonly DataContext _context;

        public UserAssertSteps(DataContext context) 
        {
            _context = context;
        }

        [Then(@"Response code is '([^']*)'")]
        public void ThenResponseCodeIs(HttpStatusCode expected)
        {
            Assert.That(_context.ResponseGetUserCode.Status, Is.EqualTo(expected));
        }

        [Then(@"Content value is autoincremented")]
        public void ThenContentValueIsAutoincremented()
        {
            Assert.That(_context.Result, Is.EqualTo(1));
        }

        [Then(@"Status is '([^']*)'")]
        public void ThenStatusIs(string expected)
        {
            Assert.That(_context.ResponseGetUserCode.Content, Is.EqualTo(expected));
        }


    }
}
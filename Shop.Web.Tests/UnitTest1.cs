using Shop.Web.Controllers;
using Shop.Web.Services.Interfaces;

namespace Shop.Web.Tests
{
    public class Tests
    {
        public IEmailSender EmailSender { get; set; }
        public static bool Result;
        public Tests(IEmailSender emailSender)
        {
            EmailSender = emailSender;
        }
        [SetUp]
        public void Setup()
        {
            if (EmailSender.SendEmail("Teshbkbhhbt"))
            {
                Result = true;
            }
            else
            {
                Result = false;
            }
        }

        [Test]
        public void Test1()
        {
            if (Result)
            {
                Assert.Pass();
            }else
            {
                Assert.Fail();
            }
        }
    }
}
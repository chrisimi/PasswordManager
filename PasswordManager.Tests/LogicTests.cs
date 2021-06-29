using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordManager.Database;
using PasswordManager.Domain;
using PasswordManager.Web;
using PasswordManager.XML;
using System;
using System.Collections.Generic;

namespace PasswordManager.Tests
{
    [TestClass]
    public class LogicTests
    {
        public static IEnumerable<object[]> TestInput
        {
            get
            {
                return new[]
                {
                    new object[] { new TestLogic() },
                    new object[] { new XmlLogic() },
                    new object[] { new DbLogic("localhost", "testpwmg", "root", "") }
                };
            }
        }

        Guid userId = Guid.NewGuid();
        Entry entry = new Entry()
        {
            Key = "amazon",
            Changed = DateTime.Now,
            Email = "test@test.com",
            Notes = "myamazon",
            Password = "amazon221",
            URL = "amazon.com"
        };

        public LogicTests()
        {
            entry.UserId = userId;
        }

        [DynamicData("TestInput")]
        [DataTestMethod]
        public void TestAddAndGet(ILogic logic)
        {
            logic.Add(entry);

            var result = logic.GetFromUser(userId);

            Assert.IsNotNull(result);

            Assert.AreEqual("test@test.com", result[0].Email);
        }

        [DynamicData("TestInput")]
        [DataTestMethod]
        public void TestDelete(ILogic logic)
        {
            logic.Add(entry);

            logic.Remove(entry);

            var result = logic.GetFromUser(userId);

            if (result == null || result.Count != 0)
                Assert.Fail("result is not valid");

            foreach(var entry in result)
            {
                if(entry.Key == this.entry.Key)
                {
                    Assert.Fail("delete not successful");
                }
            }
        }
    }
}

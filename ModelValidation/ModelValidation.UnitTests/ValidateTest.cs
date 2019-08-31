using Meyer.Common.ModelValidation.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Meyer.Common.ModelValidation.UnitTests
{
    [TestClass]
    public class ValidateTest
    {
        [TestMethod]
        public void CanValidateRootPropertiesTrue()
        {
            Assert.IsTrue(new Model
            {
                Phone = "11111111111",
                Email = "aaa@aaa.com",
                MyProperty = "AFDSFSD"
            }
            .ValidateRoot().IsSuccess);
        }

        [TestMethod]
        public void CanValidateRootPropertiesFalse()
        {
            var response = new Model
            {
                Phone = "c",
                Email = "aaaaaa",
                MyProperty = null
            }
            .ValidateRoot();

            Assert.IsFalse(response.IsSuccess);
            Assert.AreEqual(3, response.Results.Count);
        }

        [TestMethod]
        public void GetValidateRootPropertiesResultKeys()
        {
            var response = new Model
            {
                Phone = "c",
                Email = "aaaaaa",
                MyProperty = null
            }
            .ValidateRoot();

            Assert.AreEqual(response.Results[0].Key, "Phone");
            Assert.AreEqual(response.Results[1].Key, "Email");
            Assert.AreEqual(response.Results[2].Key, "MyProperty");
        }

        [TestMethod]
        public void GetValidateRootPropertiesResultMessages()
        {
            var response = new Model
            {
                Phone = "c",
                Email = "aaaaaa",
                MyProperty = null
            }
            .ValidateRoot();

            Assert.AreEqual(response.Results[0].Message, "The  field is not a valid phone number.");
            Assert.AreEqual(response.Results[1].Message, "The  field is not a valid e-mail address.");
            Assert.AreEqual(response.Results[2].Message, "The  field is required.");

            Assert.AreEqual(response.Results[0].Exception.Message, "The  field is not a valid phone number.");
            Assert.AreEqual(response.Results[1].Exception.Message, "The  field is not a valid e-mail address.");
            Assert.AreEqual(response.Results[2].Exception.Message, "The  field is required.");
        }
    }
}
using Meyer.Common.ModelValidation.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Meyer.Common.ModelValidation.UnitTests
{
    [TestClass]
    public class ValidateNestedListTest
    {
        [TestMethod]
        public void CanValidateNestedListPropertiesTrue()
        {
            Assert.IsTrue(new ListModel
            {
                Phone = "11111111111",
                Email = "aaa@aaa.com",
                MyProperty = "AFDSFSD",
                Model = new List<Model>
                {
                    new Model
                    {
                        Phone = "11111111111",
                        Email = "aaa@aaa.com",
                        MyProperty = "AFDSFSD"
                    },
                    new Model
                    {
                        Phone = "11111111111",
                        Email = "aaa@aaa.com",
                        MyProperty = "AFDSFSD"
                    }
                }
            }
            .ValidateNested().IsSuccess);
        }

        [TestMethod]
        public void CanValidateNestedListPropertiesFalse()
        {
            var response = new ListModel
            {
                Phone = "11111111111",
                Email = "aaa@aaa.com",
                MyProperty = "AFDSFSD",
                Model = new List<Model>
                {
                    new Model
                    {
                        Phone = "c",
                        Email = "aaaaaa",
                        MyProperty = null
                    },
                    new Model
                    {
                        Phone = "11111111111",
                        Email = "aaa@aaa.com",
                        MyProperty = "AFDSFSD"
                    }
                }
            }
            .ValidateNested();

            Assert.IsFalse(response.IsSuccess);
            Assert.AreEqual(3, response.Results.Count);
        }

        [TestMethod]
        public void GetValidateNestedListPropertiesResultKeys()
        {
            var response = new ListModel
            {
                Phone = "11111111111",
                Email = "aaa@aaa.com",
                MyProperty = "AFDSFSD",
                Model = new List<Model>
                {
                    new Model
                    {
                        Phone = "11111111111",
                        Email = "aaa@aaa.com",
                        MyProperty = "AFDSFSD"
                    },
                    new Model
                    {
                        Phone = "c",
                        Email = "aaaaaa",
                        MyProperty = null
                    }
                }
            }
            .ValidateNested();

            Assert.AreEqual(response.Results[0].Key, "Phone");
            Assert.AreEqual(response.Results[1].Key, "Email");
            Assert.AreEqual(response.Results[2].Key, "MyProperty");
        }

        [TestMethod]
        public void GetValidateNestedListPropertiesResultMessages()
        {
            var response = new ListModel
            {
                Phone = "11111111111",
                Email = "aaa@aaa.com",
                MyProperty = "AFDSFSD",
                Model = new List<Model>
                {
                    new Model
                    {
                        Phone = "c",
                        Email = "aaaaaa",
                        MyProperty = null
                    },
                    new Model
                    {
                        Phone = "11111111111",
                        Email = "aaa@aaa.com",
                        MyProperty = "AFDSFSD"
                    }
                }
            }
            .ValidateNested();

            Assert.AreEqual(response.Results[0].Message, "The  field is not a valid phone number.");
            Assert.AreEqual(response.Results[1].Message, "The  field is not a valid e-mail address.");
            Assert.AreEqual(response.Results[2].Message, "The  field is required.");

            Assert.AreEqual(response.Results[0].Exception.Message, "The  field is not a valid phone number.");
            Assert.AreEqual(response.Results[1].Exception.Message, "The  field is not a valid e-mail address.");
            Assert.AreEqual(response.Results[2].Exception.Message, "The  field is required.");
        }
    }
}
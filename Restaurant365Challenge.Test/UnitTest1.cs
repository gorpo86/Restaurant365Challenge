using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Restaurant365Challenge.Shared;
using Restaurant365Challenge.Shared.Interfaces;
using System;

namespace Restaurant365Challenge.Test
{
    [TestClass]
    public class StringEvaluatorTests
    {
        [TestMethod]
        public void TestForResult()
        {
            var testExpression = "1,2";
            var expectedResult = 3;
            var logMock = new Mock<ILog>();

            var service = new StringEvaluator(logMock.Object);

            var result = service.EvaluateStringExpression(testExpression);
          
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestForMissingValue()
        {
            var testExpression = "1,";
            var expectedResult = 1;
            var logMock = new Mock<ILog>();

            var service = new StringEvaluator(logMock.Object);

            var result = service.EvaluateStringExpression(testExpression);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestForInvalidValue()
        {
            var testExpression = "yyyy,2";
            var expectedResult = 2;
            var logMock = new Mock<ILog>();

            var service = new StringEvaluator(logMock.Object);

            var result = service.EvaluateStringExpression(testExpression);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Unable to evaluate expression due to too many numbers")]
        public void TestForValueCountException()
        {
            var testExpression = "1,2,3";
            var logMock = new Mock<ILog>();

            var service = new StringEvaluator(logMock.Object);

            //This should throw an exception
            var result = service.EvaluateStringExpression(testExpression);

        }
    }
}
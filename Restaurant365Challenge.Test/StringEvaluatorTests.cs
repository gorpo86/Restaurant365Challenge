using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Restaurant365Challenge.Shared;
using Restaurant365Challenge.Shared.Entities;
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
            var testExpression = "1,2,3,4,5,6,7,8,9,10,11,12";
            var expectedResult = 78;
            var logMock = new Mock<ILog>();

            var service = new StringEvaluator(logMock.Object);

            var result = service.EvaluateStringExpression(testExpression);
          
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestForResultWithSingleDelimiter()
        {
            var testExpression = @"//#\n2#5";
            var expectedResult = 7;
            var logMock = new Mock<ILog>();

            var service = new StringEvaluator(logMock.Object);

            var result = service.EvaluateStringExpression(testExpression);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestForResultWithMultiValuedDelimiter()
        {
            var testExpression = @"//[***]\n11***22***33";
            var expectedResult = 66;
            var logMock = new Mock<ILog>();

            var service = new StringEvaluator(logMock.Object);

            var result = service.EvaluateStringExpression(testExpression);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestForResultWithCommaAndNewLIne()
        {
            var testExpression = "1\n2,3";
            var expectedResult = 6;
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
        public void TestForLargeNumbers()
        {
            var testExpression = "2,1001,6";
            var expectedResult = 8;
            var logMock = new Mock<ILog>();

            var service = new StringEvaluator(logMock.Object);

            var result = service.EvaluateStringExpression(testExpression);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestNumberExpressionExtract()
        {
            var testExpression = @"//#\n2#5";
            var expectedResult = "2#5";
            

            var result = new Expression(testExpression).NumberExpression;

            Assert.AreEqual(expectedResult, result);
        }

        //[TestMethod]
        //public void TestNumberDelimiterExtract()
        //{
        //    var testExpression = @"//#\n2#5";
        //    var expectedResult = '#';


        //    var result = new Expression(testExpression).Delimiter;

        //    Assert.AreEqual(expectedResult, result);
        //}

        //GD - No longer required
        //[TestMethod]
        //[ExpectedException(typeof(Exception), "Unable to evaluate expression due to too many numbers")]
        //public void TestForValueCountException()
        //{
        //    var testExpression = "1,2,3";
        //    var logMock = new Mock<ILog>();

        //    var service = new StringEvaluator(logMock.Object);

        //    //This should throw an exception
        //    var result = service.EvaluateStringExpression(testExpression);

        //}

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestForNegativeValueException()
        {
            var testExpression = "1,2,3,-4";
            var logMock = new Mock<ILog>();

            var service = new StringEvaluator(logMock.Object);

            //This should throw an exception
            var result = service.EvaluateStringExpression(testExpression);

        }
    }
}
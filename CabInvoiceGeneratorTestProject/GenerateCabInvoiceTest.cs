using CabInvoiceGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CabInvoiceGeneratorTestProject
{
    [TestClass]
    public class GenerateCabInvoiceTest
    {
        //Declaring reference variable
        public GenerateCabInvoice generateNormalFare;
        [TestInitialize]
        public void SetUp()
        {
            generateNormalFare = new GenerateCabInvoice(RideType.NORMAL);
        }
        //Test for returning normal cab ride fare(UC1)
        [TestMethod]
        [TestCategory("Calculate Fare")]
        [DataRow(5, 10.6, 111)]
        [DataRow(6, 5, 56)]
        [DataRow(7, 8, 87)]
        public void GivenDistanceAndTimeReturnTotalFare(int time, double distance, double expected)
        {
            //Act
            double actual = generateNormalFare.CalculateFare(time, distance);
            //Assert
            Assert.AreEqual(actual, expected);
        }
        //Test for returning invalid time and invalid distance custom exception(UC1)
        [TestMethod]
        [TestCategory("Calculate Fare")]
        public void GivenTimeAndDistanceReturnCustomException()
        {
            var invalidTimeException = Assert.ThrowsException<CanInvoiceGenertorException>(() => generateNormalFare.CalculateFare(-6, 10));
            Assert.AreEqual(CanInvoiceGenertorException.ExceptionType.INVALID_TIME, invalidTimeException.exceptionType);
            var invalidDistanceException = Assert.ThrowsException<CanInvoiceGenertorException>(() => generateNormalFare.CalculateFare(5, -5));
            Assert.AreEqual(CanInvoiceGenertorException.ExceptionType.INVALID_DISTANCE, invalidDistanceException.exceptionType);
        }
    }
}

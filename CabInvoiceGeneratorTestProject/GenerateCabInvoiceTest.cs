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
        //Test for returning normal cab ride fare(UC1-TC1.1)
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
        //Test for returning invalid time and invalid distance custom exception(UC1-TC1.2)
        [TestMethod]
        [TestCategory("Custom Exception")]
        public void GivenTimeAndDistanceReturnCustomException()
        {
            var invalidTimeException = Assert.ThrowsException<CanInvoiceGenertorException>(() => generateNormalFare.CalculateFare(-6, 10));
            Assert.AreEqual(CanInvoiceGenertorException.ExceptionType.INVALID_TIME, invalidTimeException.exceptionType);
            var invalidDistanceException = Assert.ThrowsException<CanInvoiceGenertorException>(() => generateNormalFare.CalculateFare(5, -5));
            Assert.AreEqual(CanInvoiceGenertorException.ExceptionType.INVALID_DISTANCE, invalidDistanceException.exceptionType);
        }
        //Test for returning aggregate fare when given multiple rides(UC2-TC2.1) 
        [TestMethod]
        [TestCategory("Calculate Aggregate Fare")]
        public void GivenMulRidesShouldReturnAggregateFare()
        {
            //Arrange
            double actual, expected = 248;
            Ride[] cabRides = { new Ride(5, 10.6), new Ride(6, 10.6), new Ride(5, 2.0) };
            //Act
            actual = generateNormalFare.CalculateFare(cabRides);
            //Assert
            Assert.AreEqual(actual, expected);
        }
        //Test for returning null rides exception when given no rides(UC2-TC2.2) 
        [TestMethod]
        [TestCategory("Custom Exception")]
        public void GivenNoRidesReturnCustomException()
        {
            Ride[] cabRides = { };
            var nullRidesException = Assert.ThrowsException<CanInvoiceGenertorException>(() => generateNormalFare.CalculateFare(cabRides));
            Assert.AreEqual(CanInvoiceGenertorException.ExceptionType.NULL_RIDES, nullRidesException.exceptionType);
        }
    }
}

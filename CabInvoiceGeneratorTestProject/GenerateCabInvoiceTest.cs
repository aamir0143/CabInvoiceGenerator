using CabInvoiceGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CabInvoiceGeneratorTestProject
{
    [TestClass]
    public class GenerateCabInvoiceTest
    {
        //Declaring reference variable
        public GenerateCabInvoice generateNormalFare, generatePremiumFare;
        [TestInitialize]
        public void SetUp()
        {
            generateNormalFare = new GenerateCabInvoice(RideType.NORMAL);
            generatePremiumFare = new GenerateCabInvoice(RideType.PREMIUM);
        }
        //Test for returning normal and premium cab ride fare(UC1-TC1.1 && UC4-TC4.1)
        [TestMethod]
        [TestCategory("Calculate Normal And Premium Fare")]
        [DataRow(5, 10.6, 111, 169)]
        [DataRow(6, 5, 56, 87)]
        [DataRow(7, 8, 87, 134)]
        public void GivenDistanceAndTimeReturnTotalFare(int time, double distance, double expectedNormal, double expectedPremium)
        {
            //Act
            double actualNormal = generateNormalFare.CalculateFare(time, distance);
            double actualPremium = generatePremiumFare.CalculateFare(time, distance);
            //Assert
            Assert.AreEqual(actualNormal, expectedNormal);
            Assert.AreEqual(actualPremium, expectedPremium);
        }
        //Test for returning invalid time and invalid distance custom exception(UC1-TC1.2)
        [TestMethod]
        [TestCategory("Custom Exception")]
        public void GivenTimeAndDistanceReturnCustomException()
        {
            var invalidTimeException = Assert.ThrowsException<CabInvoiceGenertorException>(() => generateNormalFare.CalculateFare(-6, 10));
            Assert.AreEqual(CabInvoiceGenertorException.ExceptionType.INVALID_TIME, invalidTimeException.exceptionType);
            var invalidDistanceException = Assert.ThrowsException<CabInvoiceGenertorException>(() => generateNormalFare.CalculateFare(5, -5));
            Assert.AreEqual(CabInvoiceGenertorException.ExceptionType.INVALID_DISTANCE, invalidDistanceException.exceptionType);
        }
        //Refactor The Test for returning invoice summary when given multiple rides(UC2-TC2.1 & UC3-3.1) 
        [TestMethod]
        [TestCategory("Aggregate Fare And Invoice Summary")]
        public void GivenMulRidesShouldReturnInvoiceSummary()
        {
            //Arrange
            Ride[] cabRides = { new Ride(5, 10.6), new Ride(6, 10.6), new Ride(5, 2.0) };
            InvoiceSummary expected = new InvoiceSummary(cabRides.Length, 248);
            //Act
            var actual = generateNormalFare.CalculateFare(cabRides);
            //Assert
            expected.Equals(actual);
        }
        //Test for returning null rides exception when given no rides(UC2-TC2.2) 
        [TestMethod]
        [TestCategory("Custom Exception")]
        public void GivenNoRidesReturnCustomException()
        {
            Ride[] cabRides = { };
            var nullRidesException = Assert.ThrowsException<CabInvoiceGenertorException>(() => generateNormalFare.CalculateFare(cabRides));
            Assert.AreEqual(CabInvoiceGenertorException.ExceptionType.NULL_RIDES, nullRidesException.exceptionType);
        }
        //Test for returning invoice service when queried by UserId(UC5-TC-5.1)
        [TestMethod]
        [TestCategory("Invoice Summary Queried By UserId")]
        [DataRow(1, 2, 60, 5, 2.0, 1, 1.0)]
        [DataRow(2, 2, 136, 7, 5.0, 1, 3.0)]
        [DataRow(3, 2, 191, 8, 4.0, 5, 7.0)]
        public void GivenUserIdReturnInvoiceService(int userId, int cabsRideCount, double totalFare, int time, double distance, int timeTwo, double distaceTwo)
        {
            //Created object for ride repository
            RideRepository rideRepository = new RideRepository();
            //Adding values for different user id
            Ride[] userOne = { new Ride(time, distance), new Ride(timeTwo, distaceTwo) };
            rideRepository.AddUserRidesToRepository(userId, userOne, RideType.PREMIUM);
            //Creating list of userRide for expected value
            List<Ride> list = new List<Ride>();
            list.AddRange(userOne);
            InvoiceSummary userInvoice = new InvoiceSummary(cabsRideCount, totalFare);

            //Using Assert to compare actual and expected value 
            UserCabInvoiceService expectedUserCabInvoice = new UserCabInvoiceService(list, userInvoice);
            UserCabInvoiceService actualUserCabInvoice = rideRepository.ReturnInvoicefromRideRepository(userId);
            Assert.AreEqual(actualUserCabInvoice, expectedUserCabInvoice);
        }
    }
}

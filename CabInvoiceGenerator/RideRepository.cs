using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    /// <summary>
    /// Created Ride Repository Class To Get The Invoice Service For A Particular User(UC5)
    /// </summary>
    public class RideRepository
    {
        //Create dictionary to store userId and invoice Summary
        Dictionary<int, List<Ride>> userCabRides;
        Dictionary<int, InvoiceSummary> userCabInvoice;
        public RideRepository()
        {
            this.userCabRides = new Dictionary<int, List<Ride>>();
            this.userCabInvoice = new Dictionary<int, InvoiceSummary>();
        }
        //Method to store invoice of all rides of a particular user by giving user a id(UC5)
        public void AddUserRidesToRepository(int userId, Ride[] rides, RideType rideType)
        {
            bool rideList = this.userCabRides.ContainsKey(userId);
            try
            {
                if (!rideList)
                {
                    List<Ride> list = new List<Ride>();
                    list.AddRange(rides);
                    this.userCabRides.Add(userId, list);
                    GenerateCabInvoice generateCabInvoice = new GenerateCabInvoice(rideType);
                    InvoiceSummary invoiceSummary = generateCabInvoice.CalculateFare(rides);
                    userCabInvoice.Add(userId, invoiceSummary);
                }
            }
            catch (CabInvoiceGenertorException)
            {
                throw new CabInvoiceGenertorException(CabInvoiceGenertorException.ExceptionType.NULL_RIDES, "No Rides Found");
            }
        }
        //Method to return invoice summary of a particular user by providing id(UC5)
        public UserCabInvoiceService ReturnInvoicefromRideRepository(int userId)
        {
            return new UserCabInvoiceService(userCabRides[userId], userCabInvoice[userId]);
        }
    }
}

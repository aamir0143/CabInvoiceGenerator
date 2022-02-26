using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    /// <summary>
    /// Created The Generate Cab Invoice Class For Calculating Fares(UC1)
    /// </summary>
    public class GenerateCabInvoice
    {
        //Declaring constants
        public readonly int MINIMUM_FARE;
        public readonly int COST_PER_KM;
        public readonly int COST_PER_MINUTE;
        //Declaring Parameterized constructor(UC1)
        public GenerateCabInvoice(RideType type)
        {
            //Initializing the normal ride fare(UC1)
            if (type.Equals(RideType.NORMAL))
            {
                COST_PER_KM = 10;
                MINIMUM_FARE = 5;
                COST_PER_MINUTE = 1;
            }
            //Initializing the premium ride fare(UC4)
            if (type.Equals(RideType.PREMIUM))
            {
                COST_PER_KM = 15;
                MINIMUM_FARE = 20;
                COST_PER_MINUTE = 2;
            }
        }
        //Method to calculate single ride fare(UC1)
        public double CalculateFare(int time, double distance)
        {
            double totalFare = 0;
            try
            {
                if (time <= 0)
                    throw new CanInvoiceGenertorException(CanInvoiceGenertorException.ExceptionType.INVALID_TIME, "Time Is Invalid");
                if (distance <= 0)
                    throw new CanInvoiceGenertorException(CanInvoiceGenertorException.ExceptionType.INVALID_DISTANCE, "Distance Is Invalid");
                //Calculating total fare for single ride
                totalFare = (distance * COST_PER_KM) + (time * COST_PER_MINUTE);
                //Comparing minimum fare and calculated fare to return the maximum fare
                return Math.Max(totalFare, MINIMUM_FARE);
            }
            catch (CanInvoiceGenertorException ex)
            {
                throw ex;
            }
        }
        //Refactor The Method to return invoice summary for multiple rides(UC2 & UC3)
        public InvoiceSummary CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            if (rides.Length == 0)
                throw new CanInvoiceGenertorException(CanInvoiceGenertorException.ExceptionType.NULL_RIDES, "No Rides Found");
            foreach (var ride in rides)
                totalFare += CalculateFare(ride.time, ride.distance);
            double resFare = Math.Max(totalFare, MINIMUM_FARE);
            return new InvoiceSummary(rides.Length, resFare);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    /// <summary>
    /// Creating Class For Initializing Values For Multiple Rides(UC2)
    /// </summary>
    public class Ride
    {
        //Declaring variables
        public int time;
        public double distance;
        //Constructor for setting new distance and time for every ride
        public Ride(int time, double distance)
        {
            this.time = time;
            this.distance = distance;
        }
    }
}

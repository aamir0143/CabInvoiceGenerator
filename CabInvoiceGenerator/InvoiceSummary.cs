using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    /// <summary>
    /// Created The class For Initialize Invoice Summary Values(UC3) 
    /// </summary>
    public class InvoiceSummary
    {
        //Declaring variables
        public int numOfRides;
        public double totalFare, average;
        //Parameterized Constructor to initialize the values(UC3)
        public InvoiceSummary(int numOfRides, double totalFare)
        {
            this.numOfRides = numOfRides;
            this.totalFare = totalFare;
            this.average = totalFare / numOfRides;
        }
        //Method to determine the specified object is equal to the instance(UC3)
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is InvoiceSummary))
                return false;
            InvoiceSummary invoiceSummary = (InvoiceSummary)obj;
            return this.numOfRides == invoiceSummary.numOfRides && this.totalFare == invoiceSummary.totalFare && this.average == invoiceSummary.average;
        }
        public override string ToString()
        {
            return $"Total number of rides : {this.numOfRides} \nTotalFare ={this.totalFare} \nAverageFare = {this.average}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    /// <summary>
    /// Created Custom Exception for Cab Invoice Generator(UC1)
    /// </summary>
    public class CanInvoiceGenertorException : Exception
    {
        //Declaring variables and reference 
        public string message;
        public ExceptionType exceptionType;
        //Enum for holding group of constants
        public enum ExceptionType
        {
            INVALID_TIME, INVALID_DISTANCE
        }
        //Parametrized constructor for custom exception using lambda function
        public CanInvoiceGenertorException(ExceptionType exceptionType, string message) : base(message) => this.exceptionType = exceptionType;
    }
}

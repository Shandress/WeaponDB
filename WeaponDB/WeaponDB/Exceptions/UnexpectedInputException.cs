using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponDB.Exceptions
{
    /// <summary>
    /// Represents a case of invalid user input or invalid source data on from the Internet.
    /// </summary>
    class UnexpectedInputException : Exception
    {
        public UnexpectedInputException(string msg) : base(msg) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPChecker.Service.HandleResponse
{
    public class CustomExceptions : Response
    {
        public CustomExceptions(int statusCode, string? message = null, string? detailes = null)
            : base(statusCode, message)
        {
        }

    }
}

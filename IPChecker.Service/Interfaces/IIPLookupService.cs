using IPChecker.Service.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPChecker.Service.Interfaces
{
    public interface IIPLookupService
    {
        Task<string> GetCountryCodeByIPAsync(string ip);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPChecker.Service.DTO_s
{
    public class IPCheckResultDto
    {
        public string IP { get; set; }
        public bool IsBlocked { get; set; }
        public string CountryCode { get; set; }
    }
}

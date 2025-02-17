using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPChecker.Data.Entities
{
    public class BlockedAttempt
    {
        public string IP { get; set; }
        public string CountryCode { get; set; }
        public DateTime AttemptTime { get; set; }
    }
}

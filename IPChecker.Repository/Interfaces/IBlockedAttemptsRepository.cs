using IPChecker.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPChecker.Repository.Interfaces
{
    public interface IBlockedAttemptsRepository
    {
        void AddBlockedAttempt(string ip, string countryCode);
        List<BlockedAttempt> GetBlockedAttempts();
    }
}

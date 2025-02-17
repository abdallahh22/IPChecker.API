using IPChecker.Data.Entities;
using IPChecker.Repository.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPChecker.Repository.Repositories
{
    public class BlockedAttemptsRepository : IBlockedAttemptsRepository
    {
        private readonly List<BlockedAttempt> _blockedAttempts = new();
        public void AddBlockedAttempt(string ip, string countryCode)
        {
            var existingAttempt = _blockedAttempts.FirstOrDefault(a => a.IP == ip);
            if (existingAttempt != null)
            {
                existingAttempt.CountryCode = countryCode;
                existingAttempt.AttemptTime = DateTime.UtcNow;
            }
            else
            {
                _blockedAttempts.Add(new BlockedAttempt
                {
                    IP = ip,
                    CountryCode = countryCode,
                    AttemptTime = DateTime.UtcNow
                });
            }
        }

        public List<BlockedAttempt> GetBlockedAttempts()
        {
            return _blockedAttempts.ToList();
        }
    }
}

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
        private readonly ConcurrentDictionary<string, BlockedAttempt> _blockedAttempts = new();

        public void AddBlockedAttempt(string ip, string countryCode)
        {
            _blockedAttempts.AddOrUpdate(ip,
                new BlockedAttempt
                {
                    IP = ip,
                    CountryCode = countryCode,
                    AttemptTime = DateTime.UtcNow
                },
                (key, existingAttempt) =>
                {
                    existingAttempt.CountryCode = countryCode;
                    existingAttempt.AttemptTime = DateTime.UtcNow;
                    return existingAttempt;
                });
        }

        public List<BlockedAttempt> GetBlockedAttempts()
        {
            return _blockedAttempts.Values.ToList();
        }
    }
}

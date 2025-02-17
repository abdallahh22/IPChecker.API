using IPChecker.Data.Entities;
using IPChecker.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPChecker.Repository.Repositories
{
    public class BlockedCountryRepository : IBlockedCountryRepository
    {
        private readonly List<BlockedCountry> _blockedCountries = new();

        public void AddBlockedCountry(string countryCode, string countryName)
        {
            if (!_blockedCountries.Any(c => c.CountryCode == countryCode))
            {
                _blockedCountries.Add(new BlockedCountry { CountryCode = countryCode, CountryName = countryName });
            }
        }

        public void RemoveBlockedCountry(string countryCode)
        {
            _blockedCountries.RemoveAll(c => c.CountryCode == countryCode);
        }

        public bool IsCountryBlocked(string countryCode)
        {
            return _blockedCountries.Any(c => c.CountryCode == countryCode);
        }

        public List<BlockedCountry> GetBlockedCountries()
        {
            return _blockedCountries;
        }
    }
}

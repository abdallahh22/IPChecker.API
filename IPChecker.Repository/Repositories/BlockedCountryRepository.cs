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
    public class BlockedCountryRepository : IBlockedCountryRepository
    {
        private readonly ConcurrentDictionary<string, BlockedCountry> _blockedCountries = new();

        public void AddBlockedCountry(string countryCode, string countryName)
        {
            _blockedCountries.TryAdd(countryCode, new BlockedCountry { CountryCode = countryCode, CountryName = countryName });
        }

        public void RemoveBlockedCountry(string countryCode)
        {
            _blockedCountries.TryRemove(countryCode, out _);
        }

        public bool IsCountryBlocked(string countryCode)
        {
            return _blockedCountries.ContainsKey(countryCode);
        }

        public List<BlockedCountry> GetBlockedCountries()
        {
            return _blockedCountries.Values.ToList();
        }

        public List<BlockedCountry> GetBlockedCountries(int page, int pageSize, string? search, string? filter)
        {
            var query = _blockedCountries.Values.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.CountryName.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(c => c.CountryCode.Equals(filter, StringComparison.OrdinalIgnoreCase));
            }

            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public int GetBlockedCountriesCount(string? search, string? filter)
        {
            var query = _blockedCountries.Values.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.CountryName.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(c => c.CountryCode.Equals(filter, StringComparison.OrdinalIgnoreCase));
            }

            return query.Count();
        }

    }

}

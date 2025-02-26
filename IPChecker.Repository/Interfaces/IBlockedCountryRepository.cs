﻿using IPChecker.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPChecker.Repository.Interfaces
{
    public interface IBlockedCountryRepository
    {
        List<BlockedCountry> GetBlockedCountries(int page, int pageSize, string? search, string? filter);
        int GetBlockedCountriesCount(string? search, string? filter);
        bool IsCountryBlocked(string countryCode);
        void RemoveBlockedCountry(string countryCode);
        void AddBlockedCountry(string countryCode, string countryName);

    }
}

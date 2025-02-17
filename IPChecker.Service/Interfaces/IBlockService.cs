using IPChecker.Service.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPChecker.Service.Interfaces
{
    public interface IBlockService
    {
        void TemporarilyBlockCountry(string countryCode, int duration);
        List<BlockedAttemptDto> GetBlockedAttempts();
        List<BlockedCountryDto> GetBlockedCountries(int page, int pageSize, string? search, string? filter, out int totalCount);
        Task<IPCheckResultDto> CheckIfIPIsBlockedAsync(string ip);
        void UnblockCountry(string countryCode);
        void BlockCountry(BlockCountryRequestDto request);
    }
}

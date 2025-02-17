using AutoMapper;
using IPChecker.Data.Entities;
using IPChecker.Repository.Interfaces;
using IPChecker.Repository.Repositories;
using IPChecker.Service.DTO_s;
using IPChecker.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPChecker.Service.Services
{
    public class BlockService : IBlockService
    {
        private readonly IBlockedCountryRepository _blockedCountryRepository;
        private readonly IBlockedAttemptsRepository _blockedAttemptsRepository;
        private readonly IIPLookupService _ipLookupService;
        private readonly IMapper _mapper;

        public BlockService(
            IBlockedCountryRepository blockedCountryRepository,
            IBlockedAttemptsRepository blockedAttemptsRepository,
            IIPLookupService ipLookupService,
            IMapper mapper)
        {
            _blockedCountryRepository = blockedCountryRepository;
            _blockedAttemptsRepository = blockedAttemptsRepository;
            _ipLookupService = ipLookupService;
            _mapper = mapper;
        }

        public void BlockCountry(BlockCountryRequestDto request)
        {
            var entity = _mapper.Map<BlockedCountry>(request);
            _blockedCountryRepository.AddBlockedCountry(entity.CountryCode, entity.CountryName);
        }

        public void UnblockCountry(string countryCode)
        {
            _blockedCountryRepository.RemoveBlockedCountry(countryCode);
        }

        public async Task<IPCheckResultDto> CheckIfIPIsBlockedAsync(string ip)
        {
            var countryCode = await _ipLookupService.GetCountryCodeByIPAsync(ip);
            var isBlocked = _blockedCountryRepository.IsCountryBlocked(countryCode);

            if (isBlocked)
            {
                _blockedAttemptsRepository.AddBlockedAttempt(ip, countryCode);
            }

            return new IPCheckResultDto
            {
                IP = ip,
                IsBlocked = isBlocked,
                CountryCode = isBlocked ? "BLOCKED" : countryCode
            };
        }

        public List<BlockedCountryDto> GetBlockedCountries(int page, int pageSize, string? search, string? filter, out int totalCount)
        {
            var blockedCountries = _blockedCountryRepository.GetBlockedCountries(page, pageSize, search, filter);
            totalCount = _blockedCountryRepository.GetBlockedCountriesCount(search, filter);
            return _mapper.Map<List<BlockedCountryDto>>(blockedCountries);
        }

        public List<BlockedAttemptDto> GetBlockedAttempts()
        {
            var attempts = _blockedAttemptsRepository.GetBlockedAttempts();
            return _mapper.Map<List<BlockedAttemptDto>>(attempts);
        }

        public void TemporarilyBlockCountry(string countryCode, int duration)
        {
            _blockedCountryRepository.AddBlockedCountry(countryCode, $"TEMP-{countryCode}");

            Task.Delay(TimeSpan.FromMinutes(duration)).ContinueWith(_ =>
            {
                _blockedCountryRepository.RemoveBlockedCountry(countryCode);
            });
        }
    }
}

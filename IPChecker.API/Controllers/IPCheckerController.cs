using IPChecker.Service.DTO_s;
using IPChecker.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BlockController : ControllerBase
{
    private readonly IBlockService _blockService;
    private readonly IIPLookupService _iPLookupService;

    public BlockController(IBlockService blockService, IIPLookupService iPLookupService)
    {
        _blockService = blockService;
        _iPLookupService = iPLookupService;
    }

    [HttpPost("BlockCountry")]
    public IActionResult BlockCountry([FromBody] BlockCountryRequestDto request)
    {
        _blockService.BlockCountry(request);
        return Ok(new { message = "Country blocked successfully" });
    }

    [HttpPost("UnBlockCountry")]
    public IActionResult UnblockCountry(string countryCode)
    {
        _blockService.UnblockCountry(countryCode);
        return Ok(new { message = "Country unblocked successfully" });
    }

    [HttpGet("CheckIP")]
    public async Task<IActionResult> CheckIfIPIsBlocked(string ip)
    {
        var result = await _blockService.CheckIfIPIsBlockedAsync(ip);
        return Ok(result);
    }

    [HttpGet("Blocked-Countries")]
    public IActionResult GetBlockedCountries()
    {
        var result = _blockService.GetBlockedCountries();
        return Ok(result);
    }

    [HttpGet("Blocked-attempts")]
    public IActionResult GetBlockedAttempts()
    {
        var result = _blockService.GetBlockedAttempts();
        return Ok(result);
    }

    [HttpPost("Temporary-block")]
    public IActionResult TemporarilyBlockCountry(string countryCode, int duration)
    {
        _blockService.TemporarilyBlockCountry(countryCode, duration);
        return Ok(new { message = $"Country {countryCode} temporarily blocked for {duration} minutes" });
    }

    [HttpGet("GetCountryCodeByIP")]
    public async Task<IActionResult> GetCountryCodeByIP(string ip)
    {
        var countryCode = await _iPLookupService.GetCountryCodeByIPAsync(ip);
        return Ok(new { IP = ip, CountryCode = countryCode });
    }
}

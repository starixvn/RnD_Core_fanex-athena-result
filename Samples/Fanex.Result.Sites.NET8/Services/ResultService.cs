using Fanex.Result.Sites.NET8.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Fanex.Result.Sites.NET8.Services;

public class ResultService : IResultService
{
    private readonly IThirdPartyResult _thirdPartyResult;
    private readonly ILogger<ResultService> _logger;
    private readonly IMemoryCache _cache;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public ResultService(
        IThirdPartyResult thirdPartyResult,
        ILogger<ResultService> logger,
        IMemoryCache cache)
    {
        _thirdPartyResult = thirdPartyResult;
        _logger = logger;
        _cache = cache;
    }

    public async Task<string> GetResultAsync(ResultRequest request)
    {
        var cacheKey = $"circuit_breaker_{request.TranId3rd}";
        
        // Check circuit breaker
        if (_cache.TryGetValue(cacheKey, out bool isOpen) && isOpen)
        {
            _logger.LogWarning("Circuit breaker is open for transaction {TranId}", request.TranId3rd);
            return string.Empty; // Fallback response
        }

        try
        {
            // Reset circuit breaker on success
            _cache.Remove(cacheKey);
            
            return await Task.FromResult(_thirdPartyResult.GetResultPOST(request));
        }
        catch (ResultCoreException ex)
        {
            // Open circuit breaker
            _cache.Set(cacheKey, true, TimeSpan.FromMinutes(5));
            _logger.LogError(ex, "Circuit breaker opened for transaction {TranId}", request.TranId3rd);
            throw;
        }
    }

    public async Task<string> GetResultPOSTAsync(ResultRequest request)
    {
        return await GetResultAsync(request);
    }

    public async Task<string> GetResultGETAsync(ResultRequest request)
    {
        var cacheKey = $"circuit_breaker_{request.TranId3rd}";
        
        // Check circuit breaker
        if (_cache.TryGetValue(cacheKey, out bool isOpen) && isOpen)
        {
            _logger.LogWarning("Circuit breaker is open for transaction {TranId}", request.TranId3rd);
            return string.Empty; // Fallback response
        }

        try
        {
            // Reset circuit breaker on success
            _cache.Remove(cacheKey);
            
            return await Task.FromResult(_thirdPartyResult.GetResultGET(request));
        }
        catch (ResultCoreException ex)
        {
            // Open circuit breaker
            _cache.Set(cacheKey, true, TimeSpan.FromMinutes(5));
            _logger.LogError(ex, "Circuit breaker opened for transaction {TranId}", request.TranId3rd);
            throw;
        }
    }
}

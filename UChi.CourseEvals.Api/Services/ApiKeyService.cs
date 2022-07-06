using Microsoft.EntityFrameworkCore;
using UChi.CourseEvals.Api.Services.Interfaces;
using UChi.CourseEvals.Data;
using UChi.CourseEvals.Domain.Enums;

namespace UChi.CourseEvals.Api.Services;

public class ApiKeyService : IApiKeyService
{
    private readonly IAppDbContext _dbContext;

    public ApiKeyService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsApiKeyValid(string apiKey)
    {
        var dbKey = await _dbContext.ApiKeys
            .FirstOrDefaultAsync(k => k.Key == apiKey);

        return dbKey != null && dbKey.ExpirationDate >= DateTime.Now;
    }
    
    public async Task<bool> IsApiKeyValid(string apiKey, ApiKeyScope scope)
    {
        var dbKey = await _dbContext.ApiKeys
            .FirstOrDefaultAsync(k => k.Key == apiKey);

        return dbKey != null 
               && dbKey.ExpirationDate >= new DateTime()
               && dbKey.Scope == scope;
    }

    public async Task TrackApiKeyRequest(string apiKey)
    {
        var dbKey = await _dbContext.ApiKeys
            .FirstOrDefaultAsync(k => k.Key == apiKey);
        
        if (dbKey != null)
        {
            dbKey.LastUsed = DateTime.Now;
            dbKey.RequestCount++;
        }

        await _dbContext.SaveChangesAsync();
    }
    
}
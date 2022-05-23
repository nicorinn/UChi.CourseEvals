using UChi.CourseEvals.Domain.Enums;

namespace UChi.CourseEvals.Api.Services.Interfaces;

public interface IApiKeyService
{
    public Task<bool> IsApiKeyValid(string apiKey);

    public Task<bool> IsApiKeyValid(string apiKey, ApiKeyScope scope);

    public Task TrackApiKeyRequest(string apiKey);
}
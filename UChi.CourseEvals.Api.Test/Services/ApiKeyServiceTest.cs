using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UChi.CourseEvals.Api.Services;
using UChi.CourseEvals.Data;
using UChi.CourseEvals.Domain.Entities;
using UChi.CourseEvals.Domain.Enums;
using Xunit;

namespace UChi.CourseEvals.Api.Test.Services;

public class ApiKeyDatabaseFixture : IDisposable
{
    public ApiKeyDatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestAppDatabase")
            .Options;

        DbContext = new AppDbContext(options);
        DbContext.AddRange(new ApiKey()
            {
                Id = 1,
                Key = "e279450c-e52c-46b2-96f8-98c0f81760b7",
                Email = "test@test.com",
                Scope = ApiKeyScope.Write,
                CreationDate = new DateTime(2022, 7, 6),
                ExpirationDate = new DateTime(2023, 7, 6),
                LastUsed = new DateTime(),
                RequestCount = 1
            },
            new ApiKey()
            {
                Id = 2,
                Key = "15360391-1eea-46fe-898c-c36e475c4a27",
                Email = "test@test.com",
                Scope = ApiKeyScope.Read,
                CreationDate = new DateTime(2019, 7, 6),
                ExpirationDate = new DateTime(2020, 7, 6),
                LastUsed = new DateTime(2020, 7, 6),
                RequestCount = 1
            },
            new ApiKey()
            {
                Id = 3,
                Key = "391513f3-a898-47b9-b9fb-e2aabf86adc6",
                Email = "test@test.com",
                Scope = ApiKeyScope.Read,
                CreationDate = new DateTime(2022, 7, 6),
                ExpirationDate = new DateTime(2023, 7, 6),
                LastUsed = new DateTime(),
                RequestCount = 1
            });
        DbContext.SaveChanges();
    }

    public void Dispose()
    {
    }

    public AppDbContext DbContext { get; set; }
}
 
public class ApiKeyServiceTest : IClassFixture<ApiKeyDatabaseFixture>
{
    private readonly ApiKeyDatabaseFixture _fixture;

    public ApiKeyServiceTest(ApiKeyDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AcceptsValidApiKey()
    {
        var apiKeyService = new ApiKeyService(_fixture.DbContext);
        var testApiKey = "e279450c-e52c-46b2-96f8-98c0f81760b7";

        Assert.True(await apiKeyService.IsApiKeyValid(testApiKey, ApiKeyScope.Write));
    }

    [Fact]
    public async Task RejectsExpiredApiKey()
    {
        var apiKeyService = new ApiKeyService(_fixture.DbContext);
        var testApiKey = "15360391-1eea-46fe-898c-c36e475c4a27";
    
        Assert.False(await apiKeyService.IsApiKeyValid(testApiKey));
    }
    
    [Fact]
    public async Task RejectsApiKeyWithWrongScope()
    {
        var apiKeyService = new ApiKeyService(_fixture.DbContext);
        var testApiKey = "391513f3-a898-47b9-b9fb-e2aabf86adc6";
    
        Assert.False(await apiKeyService.IsApiKeyValid(testApiKey, ApiKeyScope.Write));
    }
}
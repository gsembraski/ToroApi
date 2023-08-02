using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver.Core.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toro.API.Domain.Repositories;
using Toro.API.Test.Mocks;

namespace Toro.API.Test.Config;

public class TestFixture : WebApplicationFactory<Program>, IDisposable
{
    public IServiceScope Scope { get; }
    public IServiceProvider Services => Scope.ServiceProvider;

    public TestFixture()
    {
        Scope = base.Services.CreateScope();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddTransient<IUserRepository, UserRepositoryMock>();
            services.AddTransient<IPersonRepository, PersonRepositoryMock>();
        });

        // Call the base configuration
        base.ConfigureWebHost(builder);
    }

    public void Dispose()
    {
        // Dispose the service scope
        Scope.Dispose();
    }
}

[CollectionDefinition("MyTestCollection")]
public class TestCollection : ICollectionFixture<TestFixture> { }

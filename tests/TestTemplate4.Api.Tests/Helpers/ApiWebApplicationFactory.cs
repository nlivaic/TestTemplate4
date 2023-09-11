﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestTemplate4.Data;
using Xunit;
using Xunit.Abstractions;

namespace TestTemplate4.Api.Tests.Helpers
{
    public class ApiWebApplicationFactory :
        WebApplicationFactory<Startup>,
        IAsyncLifetime
    {
        private readonly MsSqlContainer _msSqlContainer;
        private MsSqlDbBuilder _msSqlDbBuilder;

        public ApiWebApplicationFactory()
        {
            _msSqlContainer = new MsSqlContainer();
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")))
            {
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            }
            Environment.SetEnvironmentVariable("MessageBroker__Writer__SharedAccessKeyName", "Test");
            Environment.SetEnvironmentVariable("MessageBroker__Writer__SharedAccessKey", "Test");
            Environment.SetEnvironmentVariable("MessageBroker__Reader__SharedAccessKeyName", "Test");
            Environment.SetEnvironmentVariable("MessageBroker__Reader__SharedAccessKey", "Test");
            return base.CreateHostBuilder()
                .ConfigureHostConfiguration(
                    config => config.AddEnvironmentVariables("ASPNETCORE"));
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services
                    .AddAuthentication("Test")
                    .AddScheme<TestAuthenticationOptions, TestAuthenticationHandler>("Test", null);
                services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TestTemplate4DbContext>)));
                services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection)));
                services.AddDbContext<TestTemplate4DbContext>(options =>
                {
                    options.UseSqlServer(_msSqlContainer.ConnectionString);
                });
                services.AddMassTransitTestHarness();
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var ctx = scopedServices.GetRequiredService<TestTemplate4DbContext>();
                ctx.Database.EnsureCreated();
                ctx.Seed();
                ctx.SaveChanges();
            });
        }

        public TestTemplate4DbContext CreateDbContext(ITestOutputHelper testOutput = null)
        {
            _msSqlDbBuilder ??= new MsSqlDbBuilder(testOutput, _msSqlContainer.ConnectionString);
            return _msSqlDbBuilder.BuildContext();
        }

        public async Task InitializeAsync() =>
            await _msSqlContainer.InitializeAsync();

        public async Task DisposeAsync() =>
            await _msSqlContainer.DisposeAsync();


        private class MsSqlDbBuilder
        {
            private readonly DbContextOptions<TestTemplate4DbContext> _options;

            /// <summary>
            /// Creates a new DbContext with an open database connection already set up.
            /// Make sure not to call `context.Database.OpenConnection()` from your code.
            /// </summary>
            public MsSqlDbBuilder(
                ITestOutputHelper testOutput,
                string connection,
                List<string> logs = null)   // This parameter is just for demo purposes, to show you can output logs.
            {
                _options = new DbContextOptionsBuilder<TestTemplate4DbContext>()
                    .UseLoggerFactory(new LoggerFactory(
                        new[] {
                        new TestLoggerProvider(
                            message => testOutput?.WriteLine(message),
                            // message => logs?.Add(message),
                            LogLevel.Information
                        )
                        }
                    ))
                    .UseSqlServer(connection)
                    .Options;
            }

            public TestTemplate4DbContext BuildContext() =>
                new(_options);
        }
    }
}

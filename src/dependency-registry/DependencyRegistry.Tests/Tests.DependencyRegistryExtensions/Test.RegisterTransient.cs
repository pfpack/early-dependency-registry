using System;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PrimeFuncPack.UnitTest;
using Xunit;
using static PrimeFuncPack.UnitTest.TestData;

namespace PrimeFuncPack.DependencyRegistry.Tests;

partial class DependencyRegistryExtensionsTest
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void RegisterTransient_ExpectSourceServices(bool isNotNull)
    {
        var mockServices = MockServiceCollection.CreateMock();
        var sourceServices = mockServices.Object;

        RecordType regService = isNotNull ? ZeroIdNullNameRecord : null!;
        var dependency = Dependency.Of(regService);

        var registrar = dependency.ToRegistrar(sourceServices);

        var actualServices = registrar.RegisterTransient();
        Assert.Same(sourceServices, actualServices);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void RegisterTransient_ExpectCallAddTransientOnce(bool isNotNull)
    {
        string regService = isNotNull ? LowerSomeString : null!;
        var mockServices = MockServiceCollection.CreateMock(
            sd =>
            {
                Assert.Equal(typeof(string), sd.ServiceType);
                Assert.Equal(ServiceLifetime.Transient, sd.Lifetime);
                Assert.NotNull(sd.ImplementationFactory);

                var actualService = sd.ImplementationFactory!.Invoke(Mock.Of<IServiceProvider>());
                Assert.Equal(regService, actualService);
            });

        var sourceServices = mockServices.Object;
        var dependency = Dependency.Of(regService);

        var registrar = dependency.ToRegistrar(sourceServices);
        _ = registrar.RegisterTransient();

        mockServices.Verify(s => s.Add(It.IsAny<ServiceDescriptor>()), Times.Once);
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace PrimeFuncPack
{
    partial class DependencyRegistrar<T>
    {
        public IServiceCollection RegisterSingleton() => services.AddSingleton(resolver);
    }
}
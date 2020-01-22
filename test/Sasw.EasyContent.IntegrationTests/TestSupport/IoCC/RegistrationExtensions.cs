namespace Sasw.EasyContent.IntegrationTests.TestSupport.IoCC
{
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class RegistrationExtensions
    {
        public static IServiceCollection OverrideWith(this IServiceCollection serviceCollection, Func<IServiceCollection, IServiceCollection> overrides)
        {
            var result = overrides.Invoke(serviceCollection);
            return result;
        }
    }
}

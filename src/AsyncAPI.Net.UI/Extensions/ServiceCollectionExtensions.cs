using AsyncApi.Net.Ui.Core;
using AsyncApi.Net.Ui.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AsyncApi.Net.Ui.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAsyncApiDocumentation(this IServiceCollection services, AsyncApiDocInfo asyncApiDocInfo)
    {
        services
            .AddSingleton(asyncApiDocInfo)
            .AddSingleton<IAsyncApiScanner, AsyncApiScanner>()
            .AddSingleton<IAsyncApiDocumentBuilder, AsyncApiDocumentBuilder>()
            .AddSingleton<IAsyncApiServiceGrouper, AsyncApiServiceGrouper>()
            .AddSingleton<IAsyncApiAttributeConverter, AsyncApiAttributeConverter>()
            .AddSingleton<IAsyncApiService, AsyncApiService>();

        return services;
    }
}

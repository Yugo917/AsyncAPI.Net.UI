# 📰 AsyncAPI.Net.UI
A .Net library to generate [AsyncAPI](https://www.asyncapi.com/en) [2.0](https://v2.asyncapi.com/docs/tutorials/getting-started/asyncapi-documents) documentation by code first, to easily share and maintain your event-driven architecture.

# 🧪 Test It :

1. Download the repo

2. Run AsyncAPI.NET.UI.Sample

3. GO  http://localhost:5000/asyncapi

4. And see : 🚧 add screenshoots

# 🏁 Getting Started :
1. Install the standard Nuget package into your ASP.NET Core application.

    ```
    🚧 CLI : dotnet add package --version ?.?.? AsyncAPI.Net.UI
    ```

2. Add import
    ```csharp
    using AsyncApi.Net.Ui.Extensions;
    using AsyncApi.Net.Ui.Models;
    ```

3. In the `ConfigureServices(IServiceCollection services)` method of `Startup.cs`, register the AsyncApiDoc generator, defining one or more AsyncApiDoc documents.


    
    ```csharp
    services.AddMvc();

    services.AddAsyncApiDocumentation(new AsyncApiDocInfo()
        {
            Title = "My title AsyncAPI",
            Description = "The great description of the my AcyncApi",
            ContactName = "Crazy Coder",
            ContactUrl = "https://www.microsoft.com/"
        });
    ```

4. In the `Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)` method of `Startup.cs`, MapRazorPages


    ```csharp
    app.UseRouting();
    app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapRazorPages();
        });
    ```

# 🔗 Links :
- AsyncApi : https://www.asyncapi.com/en
 - AsyncApi Studio : https://www.asyncapi.com/en
 - AsyncAPI.NET : https://github.com/LEGO/AsyncAPI.NET


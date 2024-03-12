using System;
using System.Reflection;
using AsyncApi.Net.Ui.Core;
using AsyncApi.Net.Ui.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AsyncApi.Net.Ui.Controller;

public class AsyncApiDocumentController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IAsyncApiService asyncApiService;

    public AsyncApiDocumentController(IAsyncApiService asyncApiService)
    {
        this.asyncApiService = asyncApiService;
    }

    [Route("asyncapi")]
    public ActionResult AsyncApiUi()
    {
        try
        {
            ViewData["asyncApiJsons"] = GenerateJsFormatedString();

            return View("~/Views/AsyncApiUi.cshtml");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [Route("asyncapi/json")]
    [Produces("text/plain")]
    public ActionResult<string> AsyncApiJson()
    {
        return GenerateJsFormatedString();
    }

    private string GenerateJsFormatedString()
    {
        var asyncApiJsons = asyncApiService.GenerateJsonAsyncApiDocs(Assembly.GetEntryAssembly());
        var asyncApiJsonsString = JsonConvert.SerializeObject(asyncApiJsons, Formatting.Indented);
        var minified = JsonHelper.Minify(asyncApiJsonsString);
        return minified;
    }
}

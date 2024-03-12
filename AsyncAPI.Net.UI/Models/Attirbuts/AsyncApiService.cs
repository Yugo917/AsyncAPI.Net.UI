using System;

namespace AsyncApi.Net.Ui.Models.Attirbuts;

/// <summary>
/// Attibute used to identify documented async api classes.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class AsyncApiServiceAttribute : Attribute
{
    public string Version { get; set; }

    public string ServerProtocol { get; set; }
}

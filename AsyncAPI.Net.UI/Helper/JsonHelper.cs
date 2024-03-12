using Newtonsoft.Json;

namespace AsyncApi.Net.Ui.Helper;

public class JsonHelper
{
    /// <summary>
    /// Returns a 'minified' version of the specified JSON string, stripped of all .
    /// non-essential characters.
    /// </summary>
    /// <param name="json">A valid JSON string.</param>
    /// <returns>A 'minified' version of the specified JSON string.</returns>
    public static string Minify(string json)
        => JsonConvert.SerializeObject(JsonConvert.DeserializeObject(json));
}

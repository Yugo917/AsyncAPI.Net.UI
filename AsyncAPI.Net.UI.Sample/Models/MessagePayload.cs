using System.ComponentModel;

namespace AsyncApi.Net.Ui.Sample.Models;

public class MessagePayload
{
    [Description("PropA1 description.")]
    public string PropA1 { get; set; }

    public int PropWithoutDescriptionA2 { get; set; }

    [Description("PropA2 description.")]
    public int PropA3 { get; set; }
}

public class PayloadComponent
{
    [Description("PropB1 description.")]
    public string PropB1 { get; set; }

    [Description("PropA2 description.")]
    public int PropA2 { get; set; }
}

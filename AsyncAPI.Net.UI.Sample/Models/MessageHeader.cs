using System;
using System.ComponentModel;

namespace AsyncApi.Net.Ui.Sample.Models;

public class MessageHeader
{
    [Description("Type of action performed on the entity.")]
    public EventType EventType { get; set; }

    [Description("Time (in UTC) when the change happened on the source entity.")]
    public DateTime EventTime { get; set; }

    [Description("Unique identifier for the message being sent.")]
    public Guid EventId { get; set; }

    [Description("Name of the schema of the payload + the version.")]
    public string SchemaName { get; set; }

    [Description("Name of the actor who produced the message + the version number.")]
    public string ActorName { get; set; }
}

public enum EventType
{
    Create,
    Update,
    Upsert,
    Delete
}

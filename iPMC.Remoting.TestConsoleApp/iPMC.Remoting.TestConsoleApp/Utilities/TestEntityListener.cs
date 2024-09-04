using System;
using System.Collections.Generic;
using inRiver.Remoting;
using inRiver.Remoting.Extension;
using inRiver.Remoting.Extension.Interface;
using inRiver.Remoting.Objects;

namespace iPMC.Remoting.TestConsoleApp.Utilities;

public class TestEntityListener(
    IinRiverManager manager,
    IExtensionLog logger,
    Dictionary<string, string>? defaultSettings = null)
    : IEntityListener
{
    public string Test()
    {
        Console.WriteLine(nameof(Test));
        return nameof(Test);
    }

    public void EntityCreated(int entityId)
    {
        Console.WriteLine($"{nameof(EntityCreated)} - Entity Id: {entityId}");
    }

    public void EntityUpdated(int entityId, string[] fields)
    {
        Console.WriteLine($"{nameof(EntityUpdated)} - Entity Id: {entityId}, Field: {string.Join(", ", fields)}");
    }

    public void EntityDeleted(Entity deletedEntity)
    {
        Console.WriteLine($"{nameof(EntityDeleted)} - Entity Id: {deletedEntity.Id}");
    }

    public void EntityLocked(int entityId)
    {
        Console.WriteLine($"{nameof(EntityLocked)} - Entity Id: {entityId}");
    }

    public void EntityUnlocked(int entityId)
    {
        Console.WriteLine($"{nameof(EntityUnlocked)} - Entity Id: {entityId}");
    }

    public void EntityFieldSetUpdated(int entityId, string fieldSetId)
    {
        Console.WriteLine($"{nameof(EntityFieldSetUpdated)} - Entity Id: {entityId}, Field Set Id: {fieldSetId}");
    }

    public void EntityCommentAdded(int entityId, int commentId)
    {
        Console.WriteLine($"{nameof(EntityCommentAdded)} - Entity Id: {entityId}, Comment Id: {commentId}");
    }

    public void EntitySpecificationFieldAdded(int entityId, string fieldName)
    {
        Console.WriteLine($"{nameof(EntitySpecificationFieldAdded)} - Entity Id: {entityId}, Field Name: {fieldName}");
    }

    public void EntitySpecificationFieldUpdated(int entityId, string fieldName)
    {
        Console.WriteLine($"{nameof(EntitySpecificationFieldUpdated)} - Entity Id: {entityId}, Field Name: {fieldName}");
    }

    public inRiverContext Context { get; set; } = new(manager, logger);
    public Dictionary<string, string> DefaultSettings { get; } = defaultSettings ?? new Dictionary<string, string>();
}
using System;
using System.Globalization;
using inRiver.Remoting;
using inRiver.Remoting.Extension;
using inRiver.Remoting.Objects;
using iPMC.Remoting.TestConsoleApp.Utilities;

namespace iPMC.Remoting.TestConsoleApp.TestClients;

public class EntityClient(IinRiverManager manager, IExtensionLog logger)
{
    private readonly TestEntityListener _listener = new(manager, logger);

    public void AddEntity()
    {
        var entityTypeKey = "Product";
        var itemIntegerKey = entityTypeKey + "Number";
        var itemDoubleKey = entityTypeKey + "Weight";
        var itemRequiredKey = entityTypeKey + "Industry";
        var itemLocaleStringKey = entityTypeKey + "Name";
        // var itemCvlKey = "ProductCountries";
        // var itemNameKey = entityTypeKey + "Name";

        var props = new[]
        {
            // itemNameKey,
            itemDoubleKey,
            itemIntegerKey,
            itemRequiredKey,
            itemLocaleStringKey
        };

        var enUs = new CultureInfo("en-US");
        var languages = new  List<CultureInfo> {enUs};
        var localeString = new LocaleString(languages);
        localeString[enUs] = $"REMOVE_ME-{Guid.NewGuid().ToString("N")[..6]}";

        var itemEntityType = _listener.Context.ExtensionManager.ModelService.GetEntityType(entityTypeKey);
        var entity = Entity.CreateEntity(itemEntityType);
        entity.GetField(itemIntegerKey).Data = 7; // invalid input
        entity.GetField(itemDoubleKey).Data = "12.5"; // invalid input
        entity.GetField(itemRequiredKey).Data = "bwx";
        entity.GetField(itemLocaleStringKey).Data = localeString;
        // entity.GetField(itemLocaleStringKey).Data = "Invalid Locale String Value";
        
        // entity.GetField(itemCvlKey).Data = "Spain;UK;Sweden";
        // entity.GetField(itemNameKey).Data = $"REMOVE_ME-{Guid.NewGuid().ToString("N")[..6]}";

        // entity.GetField(itemIntegerKey).Data = "7"; // valid input
        // entity.GetField(itemDoubleKey).Data = "12.5"; // valid input

        foreach (var prop in props)
        {
            Console.WriteLine($"{prop} Field.Data type is: {entity.GetField(prop).Data.GetType().Name}");
            Console.WriteLine($"{prop} Field.Data is: {entity.GetField(prop).Data}");
            Console.WriteLine("------------------------------");
        }

        var createdEntity = _listener.Context.ExtensionManager.DataService.AddEntity(entity);
        if (createdEntity.Id > 0)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Entity with ID {createdEntity.Id} created");
            Console.WriteLine("------------------------------");
        }
        
        foreach (var prop in props)
        {
            Console.WriteLine($"{prop} Field.Data type is: {createdEntity.GetField(prop).Data.GetType().Name}");
            Console.WriteLine($"{prop} Field.Data is: {createdEntity.GetField(prop).Data}");
            Console.WriteLine("------------------------------");
        }
    }
}
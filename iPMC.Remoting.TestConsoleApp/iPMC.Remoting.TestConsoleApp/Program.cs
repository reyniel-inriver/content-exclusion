using inRiver.Remoting;
using inRiver.Remoting.Extension;
using inRiver.Remoting.Objects;
using inRiver.Remoting.Query;
using iPMC.Remoting.TestConsoleApp.Utilities;
using TestApp.TestCases;


var url = "https://remoting-dev3a-euw.productmarketingcloud.com";
var apiKey = "061bb9cbd7202a76999e56be68a6d196";

try
{
    var manager = RemoteManager.CreateInstance(url, apiKey);

    var users = manager.UserService.GetAllUsers();
    foreach (var user in users)
    {
        Console.WriteLine($"User :{user.Username}");
        Console.WriteLine("====================");
        // Get all personal workareas of the user
        var workAreas =
            manager.UtilityService.GetAllPersonalWorkAreaFoldersForUser(user.Username, false);

        foreach (var workArea in workAreas)
        {
            // Print the Workarea ID and username
            Console.WriteLine(workArea.Id + " - " + workArea.Username);
        }
        Console.WriteLine("====================");

    }
}
catch (Exception e)
{
    Console.WriteLine("An error occurred.");
    Console.WriteLine(e);
    throw;
}

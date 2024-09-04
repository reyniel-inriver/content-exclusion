using inRiver.Remoting;
using inRiver.Remoting.Extension;
using inRiver.Remoting.Log;

namespace iPMC.Remoting.TestConsoleApp.TestClients;

public class LinkClient(IinRiverManager manager, IExtensionLog logger)
{
    private readonly IinRiverManager _manager = manager;
    private readonly IExtensionLog _logger = logger;

    public void UpdateSortOrder(int linkId, int index)
    {
        _logger.Log(LogLevel.Debug, $"Updating link: {linkId}, index: {index}");
        var updatedLinkSortOrder = _manager.DataService.UpdateLinkSortOrder(linkId, index);
        _logger.Log(LogLevel.Debug, $"Updated link: {updatedLinkSortOrder.Id}, index: {updatedLinkSortOrder.Index}");
    }
}

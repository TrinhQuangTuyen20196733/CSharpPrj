using Microsoft.SharePoint.Client;

namespace BHDStarBooking.Config.SharePointConfig
{
    public interface ISharePointBaseContext
    {
        ClientContext context { get;}
    }
}

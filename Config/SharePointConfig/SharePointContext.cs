using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Microsoft.Extensions.Options;
using Microsoft.SharePoint.Client;
using SPS.MASS.MODEL;
using SPS.MASS.MODEL.Services;

namespace BHDStarBooking.Config.SharePointConfig
{
    public class SharePointContext : ISharePointBaseContext
    {
        public ClientContext context { get; }

        public SharePointContext(IOptions<SharePointSetting> sharePointSettings)
        {
            string Token = "";
            using (ScopeAuthManager au = new ScopeAuthManager(sharePointSettings.Value.tenantId, sharePointSettings.Value.clientId,
                sharePointSettings.Value.userName,sharePointSettings.Value.password,sharePointSettings.Value.connectionString))
            {
                Token = au.GetATokenForGraphAsync().Result;
            }
            Uri webs = new Uri(sharePointSettings.Value.connectionString);
            context = SPTokenServices.GetContext(Token, webs);
        }

       
    }
}

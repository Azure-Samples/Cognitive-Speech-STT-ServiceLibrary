using Microsoft.IdentityModel.Clients.ActiveDirectory;
using SpeechLuisOwin.Src.Static;
using System;
using System.Threading.Tasks;

namespace SpeechLuisOwin.Src.AuthorizationProvider
{
    public class AADTokenProvider
    {
        private AuthenticationContext authenticationContext = null;

        private string clientId = "";

        private string key = "";

        private string authUri = "";

        private string resource = "";

        private ClientCredential clientCred;

        public AADTokenProvider()
        {

            this.clientId = Configurations.aad_ClientId;
            this.key = Configurations.aad_Key;
            this.authUri = Configurations.aad_AuthUri;
            this.resource = Configurations.aad_Resource;
            try
            {
                this.clientCred = new ClientCredential(clientId, key);
                this.authenticationContext = new AuthenticationContext(authUri, false);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<string> GetAccessTokenFromAAD()
        {
            // we get access token for https://graph.windows.net access, 
            AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenAsync(resource, this.clientCred);
            return authenticationResult.AccessToken;
        }
    }
}
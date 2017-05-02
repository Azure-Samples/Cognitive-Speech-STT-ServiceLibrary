using Common.Service.Model;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;

namespace Common.Service.AuthorizationProvider
{
    public class AADTokenProvider
    {
        private AuthenticationContext authenticationContext = null;


        private ClientCredential clientCred;

        private AADModel _AADModel;

        public AADTokenProvider(AADModel model)
        {
            _AADModel = model;

            try
            {
                this.clientCred = new ClientCredential(_AADModel.AAD_ClientId, _AADModel.AAD_Key);
                this.authenticationContext = new AuthenticationContext(_AADModel.AAD_AuthUri, false);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<ServiceAuthenticationResultModel> GetAccessTokenFromAAD()
        {
            // we get access token for https://graph.windows.net access, 
            AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenAsync(_AADModel.AAD_Resource, this.clientCred);
            return new ServiceAuthenticationResultModel
            {
                AccessToken = authenticationResult.AccessToken,
                ExpiresOn = authenticationResult.ExpiresOn
            };
        }
    }
}
using Microsoft.OneDrive.Sdk.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLinker.Helpers
{
    class AuthentificationHelper
    {
        public static MsaAuthenticationProvider GetAuthenticator(string clientId, string returnUrl, string[] scopes)
        {
            return new MsaAuthenticationProvider(
                clientId,
                returnUrl,
                scopes,
                new CredentialVault(clientId)
            );
        }
    }
}

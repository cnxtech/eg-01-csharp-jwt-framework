using DocuSign.eSign.Client;
using DocuSign.eSign.Client.Auth;
using DocuSign.eSign.Model;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using static DocuSign.eSign.Client.Auth.OAuth.UserInfo;

namespace eg_01_csharp_jwt
{
    public class ExampleBase
    {
        private const int TOKEN_EXPIRATION_IN_SECONDS = 3600;
        private const int TOKEN_REPLACEMENT_IN_MILLISECONDS = 10 * 60 * 1000;


        private static string AccessToken { get; set; }
        private static int expiresIn;

        private static Account Account { get; set; }
        protected static ApiClient ApiClient { get; private set; }

        protected static string AccountID
        {
            get { return Account.AccountId(); }
        }

        public ExampleBase(ApiClient client)
        {
            ApiClient = client;
        }

        public void CheckToken()
        {
            if (AccessToken == null 
                || (DateTime.Now.Millisecond + TOKEN_REPLACEMENT_IN_MILLISECONDS) > expiresIn)
            {
                UpdateToken();
            }
        }

        private void UpdateToken()
        {
            ApiClient.SetBasePath(null);

            OAuth.OAuthToken authToken = ApiClient.ConfigureJwtAuthorizationFlowByKey(DSConfig.ClientID,
                            DSConfig.ImpersonatedUserGuid,
                            DSConfig.AuthServer,
                            DSConfig.PrivateKey,
                            1);

            AccessToken = authToken.access_token;

            if (Account == null)
                Account = GetAccountInfo(authToken);

            //TODO: Larry should ask about this lines
            ApiClient = new ApiClient(Account.GetBaseUri() + "/restapi");
            
            //ApiClient.RestClient.BaseUrl = new Uri(Account.GetBaseUri() + "/restapi");
            
            //ApiClient.SetBasePath(Account.GetBaseUri() + "/restapi");

            //notice that expiresIn value is not exposed yet by the SDK so, we will assume it as 1 hours.
            expiresIn = DateTime.Now.Millisecond + (TOKEN_EXPIRATION_IN_SECONDS * 1000);
        }

        private Account GetAccountInfo(OAuth.OAuthToken authToken)
        {
            OAuth.UserInfo userInfo = ApiClient.GetUserInfo(authToken.access_token);
            Account acct = null;

            if (string.IsNullOrEmpty(DSConfig.TargetAccountID) || DSConfig.TargetAccountID.Equals("FALSE"))
            {
                var accounts = userInfo.GetAccounts();

                acct = accounts.FirstOrDefault(a => a.GetIsDefault() == "true") ??
                       accounts.FirstOrDefault(a => a.AccountId() == DSConfig.TargetAccountID);
            }

            return acct;
        }
    }
}

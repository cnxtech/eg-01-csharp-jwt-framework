using System;
using System.Configuration;

namespace eg_01_csharp_jwt
{
    internal class DSConfig
    {
        private const string CLIENT_ID = "DS_CLIENT_ID";
        private const string IMPERSONATED_USER_GUID = "DS_IMPERSONATED_USER_GUID";
        private const string TARGET_ACCOUNT_ID = "DS_TARGET_ACCOUNT_ID";
        private const string SIGNER_1_EMAIL = "DS_SIGNER_1_EMAIL";
        private const string SIGNER_1_NAME = "DS_SIGNER_1_NAME";
        private const string CC_1_EMAIL = "DS_CC_1_EMAIL";
        private const string CC_1_NAME = "DS_CC_1_NAME";
        private const string PRIVATE_KEY_FILE = "DS_PRIVATE_KEY_FILE";
        private const string PRIVATE_KEY = "DS_RPIVATE_KEY";

        static DSConfig()
        {
            ClientID = GetSetting(CLIENT_ID);
            ImpersonatedUserGuid = GetSetting(IMPERSONATED_USER_GUID);
            TargetAccountID = GetSetting(TARGET_ACCOUNT_ID);
            OAuthRedirectURI = "https://www.docusign.com";
            Signer1Email = GetSetting(SIGNER_1_EMAIL);
            Signer1Name = GetSetting(SIGNER_1_NAME);
            Cc1Email = GetSetting(CC_1_EMAIL);
            Cc1Name = GetSetting(CC_1_NAME);
            PrivateKeyFile = GetSetting(PRIVATE_KEY_FILE);
            PrivateKey = GetSetting(PRIVATE_KEY);
            AuthenticationURL = "https://account-d.docusign.com";
            AUD = "account-d.docusign.com";
            API = "restapi/v2";
            PermissionScopes = "signature impersonation";
            JWTScope = "signature";
        }

        private static string GetSetting(string configKey)
        {
            string val = Environment.GetEnvironmentVariable(configKey)
                ?? ConfigurationManager.AppSettings.Get(configKey);

            if ((PRIVATE_KEY_FILE.Equals(configKey) || PRIVATE_KEY.Equals(configKey)) && "FALSE".Equals(val))
                return null;

            return val ?? "";
        }

        public static string ClientID { get; private set; }
        public static string ImpersonatedUserGuid { get; private set; }
        public static string TargetAccountID { get; private set; }
        public static string OAuthRedirectURI { get; private set; }
        public static string Signer1Email { get; private set; }
        public static string Signer1Name { get; private set; }
        public static string Cc1Email { get; private set; }
        public static string Cc1Name { get; private set; }
        public static string PrivateKeyFile { get; private set; }
        public static string PrivateKey { get; private set; }
        public static string AuthenticationURL { get; private set; }
        public static string AUD { get; private set; }
        public static string API { get; private set; }
        public static string PermissionScopes { get; private set;}
        public static string JWTScope { get; private set; }
    }
}
namespace Trailblazor;

public struct Constants
{
    public struct Authentication
    {
        public struct ExternalProviders
        {
            public const string Google = "Google";
        }

        public struct Sections
        {
            public const string ClientId = "ClientId";
            public const string ClientSecret = "ClientSecret";
        }

        public struct ExternalProviderClaimTypes
        {
            public struct Google
            {
                public const string Picture = "urn:google:picture";
                public const string Locale = "urn:google:locale";
            }
        }
    }
}
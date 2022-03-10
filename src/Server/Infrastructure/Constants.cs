namespace Trailblazor;

public static class Constants
{
    public static class Authentication
    {
        public static class ExternalProviders
        {
            public const string Google = "Google";
        }

        public static class Sections
        {
            public const string ClientId = "ClientId";
            public const string ClientSecret = "ClientSecret";
        }
    }

    public static class Authorization
    {
        public static class Policies
        {
            public const string GetListOwner = nameof(GetListOwner);
        }
    }
}
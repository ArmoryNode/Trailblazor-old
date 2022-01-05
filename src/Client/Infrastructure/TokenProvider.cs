namespace Trailblazor.Client.Infrastructure
{
    public sealed class TokenProvider
    {
        public string AntiForgeryToken { get; set; } = string.Empty;
    }
}

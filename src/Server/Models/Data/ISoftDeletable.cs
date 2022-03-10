namespace Trailblazor.Server.Models.Data
{
    public interface ISoftDeletable
    {
        DateTimeOffset? Deleted { get; set; }
    }
}

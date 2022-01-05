namespace Trailblazor.Server.Models.Data
{
    public record GearItem(string Id, Guid OwnerId, string OwnerName) : BaseDocument(Id)
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;

        public bool Favorite { get; set; } = false;
        public bool Consumable { get; set; } = false;
        public bool Wearable { get; set; } = false;
    }
}

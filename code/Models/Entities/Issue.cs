namespace P6_NexaWorks.Models.Entities
{
    public class Issue
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public string? Resolution { get; set; }
        public DateTime? DateResolution { get; set; }
        public required string Statut {  get; set; }

        // FK vers VersionOS
        public int VersionOSId { get; set; }

        // chaque Issue pointe vers 1 seul VersionOS
        public required VersionOS VersionOS { get; set; }
    }
}

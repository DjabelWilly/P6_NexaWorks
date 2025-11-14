namespace P6_NexaWorks.Models.Entities
{
    public class OS
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        // Relation 1,n avec VersionOS
        public List<VersionOS> VersionOSes { get; set; } = new List<VersionOS>();
    }
}

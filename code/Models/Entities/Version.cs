namespace P6_NexaWorks.Models.Entities
{
    public class Version
    {
        public int Id { get; set; }
        public required string Number { get; set; }
        public DateTime DateRelease { get; set; }

        // FK vers Product
        public int ProductId { get; set; }

        // Navigation vers Product (1,1)
        public required Product Product { get; set; }

        // Relation 1,n avec VersionOS
        public List<VersionOS> VersionOSes { get; set; } = new List<VersionOS>();
    }
}

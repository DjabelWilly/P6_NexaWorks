namespace P6_NexaWorks.Models.Entities
{
    public class VersionOS
    {
        public int Id { get; set; }
        // Foreign key vers Version
        public int VersionId { get; set; }
        public required Version Version { get; set; }

        // Foreign key vers OS
        public int OSId { get; set; }
        public required OS OS { get; set; }

        // Relation 1,n avec Issue
        public List<Issue> Issues { get; set; } = new List<Issue>();
    }
}


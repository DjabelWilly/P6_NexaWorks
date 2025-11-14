namespace P6_NexaWorks.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        // Relation 1,n avec Version
        public List<Version> Versions { get; set; } = new List<Version>();
    }
}

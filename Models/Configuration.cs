namespace SeventhGearApi.Models
{
    public class Configuration
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ColorName { get; set; }
        public string InteriorName { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
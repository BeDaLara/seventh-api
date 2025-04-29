namespace SeventhGearApi.Models
{
    public class ContactRequest
    {
        public int Id { get; set; }
        public int? ConfigurationId { get; set; }
        public Configuration? Configuration { get; set; }
        public string DealerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string City { get; set; }
        public string ContactPreference { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsExistingCustomer { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; }
    }
}
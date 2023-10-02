namespace BMWIgnition_API.Model
{
    public class AuditTrail
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; } // You can adjust the data type as needed
        public string Action { get; set; }
        public int Amount { get; set; }
        public int Quantity { get; set; }

        // Additional properties as needed for your specific requirements

        // Constructor to initialize the timestamp
        public AuditTrail()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}

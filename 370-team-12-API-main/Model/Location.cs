namespace BMWIgnition_API.Model
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public ICollection<Help> Helps { get; set; } = new List<Help>();

    }
}

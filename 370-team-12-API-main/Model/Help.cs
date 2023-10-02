namespace BMWIgnition_API.Model
{
    public class Help
    {
        public int HelpId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }

    }
}
namespace BMWIgnition_API.ViewModels
{
    public class PrizeVM
    {
        public string Name { get; set; }
        public string Description { get; set; }       
        public int PrizeTypeId  { get; set; }
        public string Image { get; set; }
        public int Tokens { get; set;  }
    }
}

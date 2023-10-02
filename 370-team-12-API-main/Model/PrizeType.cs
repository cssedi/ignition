namespace BMWIgnition_API.Model
{
    public class PrizeType
    {
        public int PrizeTypeID { get; set; }
        public string Name { get; set; }

        //relationships
        public int PrizeCategoryID { get; set; }
        public PrizeCategory prizeCategory { get; set; }
        public ICollection<Prize> prizes { get; set; }
    }
}
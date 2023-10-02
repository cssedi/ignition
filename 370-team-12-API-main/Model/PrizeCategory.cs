namespace BMWIgnition_API.Model
{
    public class PrizeCategory
    {
        public int PrizeCategoryID { get; set; }
        public string Name { get; set; }

        public ICollection<PrizeType> prizeTypes { get; set; }
    }
}

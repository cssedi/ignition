namespace BMWIgnition_API.ViewModels
{
  public class ChallengeViewModel
  {
    
        public string  name { get; set; }
        public string description { get; set; }
        public int? Tokens { get; set; }
        public DateTime endDate { get; set; }
        public DateTime startDate { get; set; }
        public int ChallengeTypeId { get; set; }
        public int? PrizeId { get; set; }
        public int MedalId { get; set; }
        public string Image { get; set; }



    }
}

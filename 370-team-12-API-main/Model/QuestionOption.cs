using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.Model
{
  public class QuestionOption
  {
    [Key]
    public int OptionId { get; set; }
    public int QuestionId { get; set; }
    public string OptionText { get; set; }
    public bool IsCorrect { get; set; }
    public virtual Question Question { get; set; }
  }
}

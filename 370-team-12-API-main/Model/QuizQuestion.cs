using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.Model
{
  public class QuizQuestion
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int QuizQuestionId { get; set; }
    public int QuizId { get; set; }
    public int QuestionId { get; set; }

    public Quiz Quiz { get; set; }
    public Question Question { get; set; }
  }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BMWIgnition_API.Model
{
  public class QuestionCategory
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }

    public ICollection<Question> Questions { get; set; }
    public ICollection<Quiz> Quizzes { get; set; }
  }
}

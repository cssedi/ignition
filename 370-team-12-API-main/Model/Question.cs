namespace BMWIgnition_API.Model
{
  public class Question
  {
    public int QuestionId { get; set; }
    public string QuestionText { get; set; }
    public int CategoryId { get; set; }

    public QuestionCategory Category { get; set; }
    public ICollection<QuestionOption> Options { get; set; }
    public ICollection<QuizQuestion> QuizQuestions { get; set; }
  }
}

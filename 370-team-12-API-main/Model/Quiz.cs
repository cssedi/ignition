namespace BMWIgnition_API.Model
{
  public class Quiz
  {

    public int QuizId { get; set; }
    public string QuizName { get; set; }
    public int CategoryId { get; set; }

    public QuestionCategory Category { get; set; }
    public ICollection<QuizQuestion> QuizQuestions { get; set; }
  }
}

namespace Questionnaire.Data
{
    public class QuestionEvaluationVM
    {
        public AnswerOption SelectedAnswer { get; set; }
        public bool RightAnswerSelected { get; set; }
        public AnswerOption CorrectAnswer { get; set; }
    }
}

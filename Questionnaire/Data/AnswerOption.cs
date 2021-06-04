namespace Questionnaire.Data
{
    public class AnswerOption
    {
        public string OptionText { get; set; }
        public bool IsCorrectAnswer { get; set; }
        public bool IsSelected { get; set; }
        public bool IsInputOption { get; set; }
    }
}

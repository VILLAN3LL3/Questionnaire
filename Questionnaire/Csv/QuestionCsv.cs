namespace Questionnaire.Csv
{
    public class QuestionCsv
    {
        [CsvConfig("Title", 1)]
        public string Title { get; set; }

        [CsvConfig("Question", 2)]
        public string Question { get; set; }

        [CsvConfig("Answer quality", 3)]
        public string AnswerQuality { get; set; }
    }
}

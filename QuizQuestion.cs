namespace CybersecurityChatbot.Classes
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public string[] Options { get; set; }
        public int CorrectIndex { get; set; }
        public string Category { get; set; }
        public string Explanation { get; set; }

        public bool IsTrueFalse()
        {
            return Options.Length == 2 &&
                   Options[0].ToLower() == "true" &&
                   Options[1].ToLower() == "false";
        }

        public string GetQuestionType()
        {
            return IsTrueFalse() ? "True/False" : "Multiple Choice";
        }
    }
}
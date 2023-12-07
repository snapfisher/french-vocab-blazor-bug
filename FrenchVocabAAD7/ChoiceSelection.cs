
namespace FrenchVocabAAD7
{
    public class ChoiceSelection
    {
        private readonly Random random = new();

        public string Question { get; set; } = string.Empty;

        public int CorrectAnswer { get; set; } = -1;
        public List<int> IncorrectAnswers { get; set; } = new();

        public string[]  Answers { get; set; } = new string[4] { string.Empty, string.Empty, string.Empty, string.Empty };

        public ChoiceSelection(string question, string correctAnswer, string badAnswer1, string badAnswer2, string badAnswer3)
        {
            AddValues(question, correctAnswer, badAnswer1, badAnswer2, badAnswer3);
        }

        public ChoiceSelection() { }

        public void AddValues(string question, string correctAnswer, string badAnswer1, string badAnswer2, string badAnswer3)
        {
            Question = question;
            PlaceAnswer(true, correctAnswer);
            PlaceAnswer(false, badAnswer1);
            PlaceAnswer(false, badAnswer2);
            PlaceAnswer(false, badAnswer3);
        }


        private int freeSlots = 4;
        private bool PlaceAnswer(bool correct, string answer)
        {
            if (freeSlots == 0) return false;

            int free = random.Next(0, freeSlots);
            int f1 = 0;
            for(int f = 0; f < 4; f++)
            {
                if(!string.IsNullOrWhiteSpace(Answers[f]))
                    continue;

                if(f1 != free)
                {
                    f1++;
                    continue;
                }

                Answers[f] = answer;
                if (correct) CorrectAnswer = f;
                else IncorrectAnswers.Add(f);
                break;
            }

            freeSlots--;

            return true;
        }
    }
}

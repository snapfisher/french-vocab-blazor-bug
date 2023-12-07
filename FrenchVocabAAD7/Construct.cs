namespace FrenchVocabAAD7
{
    public enum ConstructType
        { Number, Verb, Noun, Adjective, Adverb, Article, Phrase, Pronoun, Unspecified }


    public class Construct 
    {
        public ConstructType Construct_Type { get; set; } = ConstructType.Unspecified;

        private string type = string.Empty;
        public string Type 
        {   
            get
            {
                return type;
            }
            set
            {
                type = value;
                Construct_Type = (ConstructType)Enum.Parse(typeof(ConstructType), type, true);
            }
        }

        public string Label { get; set; } = string.Empty;
    }
}

namespace FrenchVocabAAD7
{
    public class Number : Construct, IWord
    {
        public Translation[] Cardinal { get; set; } = new Translation[1] { new Translation() };
        public Translation[] Ordinal { get; set; } = new Translation[1] { new Translation() };
        public List<string> BoundingOrdinal { get; set; } = new();
        public List<string> BoundingCardinal { get; set; } = new();

        public Translation[] Infinitive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] IndicatifPresent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Substantif { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Definition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Adjectif { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Art { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Adverbe { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Pronom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

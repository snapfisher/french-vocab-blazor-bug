namespace FrenchVocabAAD7
{
    public class Verb : Construct, IWord
    {
        public Translation[] Infinitive { get; set; } = new Translation[1] { new Translation() };
        public Translation[] IndicatifPresent { get; set; } = new Translation[10] { new Translation(), new Translation(), new Translation(), new Translation(), new Translation(), new Translation(), new Translation(), new Translation(), new Translation(), new Translation() };

        public Translation[] Cardinal { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Ordinal { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Substantif { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Definition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Adjectif { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Art { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Adverbe { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Pronom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

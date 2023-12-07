namespace FrenchVocabAAD7
{
    public class Pronoun : Construct, IWord
    {
        public Translation[] Pronom { get; set; } = new Translation[2] { new Translation(), new Translation() };


        public Translation[] Infinitive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] IndicatifPresent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Cardinal { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Ordinal { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Substantif { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Definition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Adjectif { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Art { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Adverbe { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

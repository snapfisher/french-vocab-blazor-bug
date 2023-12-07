namespace FrenchVocabAAD7
{
    public interface IWord
    {
        public Translation[] Infinitive { get; set; }
        public Translation[] IndicatifPresent { get; set; }

        public Translation[] Cardinal { get; set; }
        public Translation[] Ordinal { get; set; }

        public Translation[] Substantif { get; set; }

        public Translation[] Definition { get; set; }

        public Translation[] Adjectif { get; set; }

        public Translation[] Art { get; set; }

        public Translation[] Adverbe { get; set; }
        public Translation[] Pronom { get; set; }

    }
}

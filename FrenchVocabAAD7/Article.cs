﻿namespace FrenchVocabAAD7
{
    public class Article : Construct, IWord
    {
        public Translation[] Art { get; set; } = new Translation[4] { new Translation(), new Translation(), new Translation(), new Translation() };

        public Translation[] Infinitive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] IndicatifPresent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Cardinal { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Ordinal { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Substantif { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Definition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Adjectif { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Adverbe { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Translation[] Pronom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

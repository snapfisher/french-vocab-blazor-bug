
using Blazorise;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FrenchVocabAAD7
{
    public class Comparer : IComparer<string>
    {
        private readonly CultureInfo _culture;

        public Comparer(CultureInfo culture)
        {
            _culture = culture;
        }

        public int Compare(string? x, string? y)
        {
            //if (double.TryParse(x, out double nx) && double.TryParse(y, out double ny))
            //{
            //    return nx.CompareTo(ny);
            //}

            return string.Compare(x, y, _culture, CompareOptions.IgnoreNonSpace);
        }
    }

    public class WordList
    {
        private readonly Random random = new();
        private List<Construct> wordList = new();

        public int Count { get => wordList.Count; }

        public List<Construct> SortedWordList { get => wordList.OrderBy(x => x.Label, new Comparer(new CultureInfo("fr-FR"))).ToList(); } 

        public List<List<int>> typeIndices = new();

        public WordList() 
        { 
            // -1 for undefined
            for(int i = 0; i < Enum.GetValues(typeof(ConstructType)).Length - 1; ++i)
            {
                typeIndices.Add(new List<int>());
            }
        }

        public (int wordsAdded, int totalWords) AddWords(List<Construct> wordConstructs, bool clear = false) 
        {
            if(clear)
                wordList.Clear();

            if(wordConstructs is null || wordConstructs.Count == 0)
                return (0, wordList.Count);

            // create the number dictionary drop down
            if (wordConstructs[0] is Number)
            {
                List<Number> numberWordConstructs = wordConstructs.OfType<Number>().ToList();

                foreach (var n in numberWordConstructs)
                {
                    foreach (var nn in numberWordConstructs)
                    {
                        if (Math.Abs(double.Parse(nn.Label) - double.Parse(n.Label)) <= 2)
                        {
                            n.BoundingOrdinal.Add($"{nn.Ordinal[0].F} / {nn.Ordinal[0].E}");
                            n.BoundingCardinal.Add($"{nn.Cardinal[0].F} / {nn.Cardinal[0].E}");
                        }
                    }

                }

                foreach (var n in numberWordConstructs)
                {
                    // once we have the bounding arrays we want it labeled in french 
                    // like all the other types
                    n.Label = n.Cardinal[0].F;
                }
            }

            wordList.AddRange(wordConstructs);
            wordList = Shuffle(wordList);

            return (wordConstructs.Count, wordList.Count);
        }

        int currentChoice = -1;

        public ChoiceSelection GetNextChoice()
        {
            ++currentChoice;
            if(currentChoice >= wordList.Count)
            {
                Shuffle(wordList);
                currentChoice = 0;
            }

            CurrentChoice = GenerateAChoice(currentChoice);

            return CurrentChoice;
        }

        private List<Construct> Shuffle(List<Construct> wordList)
        {
            for (int i = 0; i < wordList.Count; i++)
            {
                int r = random.Next(0, wordList.Count);
                (wordList[r], wordList[i]) = (wordList[i], wordList[r]);
            }

            // -1 for undefined
            for (int i = 0; i < Enum.GetValues(typeof(ConstructType)).Length - 1; ++i)
            {
                typeIndices[i].Clear();
            }

            for (int i = 0; i < wordList.Count; ++i)
            {
                typeIndices[(int)wordList[i].Construct_Type].Add(i);
            }

            return wordList;
        }


        public ChoiceSelection CurrentChoice { get; set; } = new();

        private ChoiceSelection GenerateAChoice(int wordIndex)
        {
            string question;
            string answer;
            string ba1;
            string ba2;
            string ba3;

            int randSelection;
            int fOrE = random.Next(0, 2);

            //wordIndex = typeIndices[(int)ConstructType.Phrase][0];


            switch (wordList[wordIndex])
            {
                case Verb:
                    randSelection = random.Next(0, 11);
                    break;
                case Number:
                    randSelection = random.Next(0, 2);
                    break;
                case Phrase:
                    randSelection = 0;
                    break;
                case Adverb:
                    randSelection = 0;
                    break;
                case Noun:
                    randSelection = random.Next(0, 2);
                    break;
                case Adjective:
                    randSelection = random.Next(0, 4);
                    break;
                case Article:
                    randSelection = random.Next(0, 4);
                    break;
                case Pronoun:
                    randSelection = random.Next(0, 2);
                    break;
                default:
                    throw new Exception("undefined");
            }
            try
            {
                // this is for debugging
                //if (wordList[wordIndex].Type == "verb")
                //{
                //    randSelection = 6;
                //    supp = 1;
                //}

                (question, answer) = GetGoodPair(wordList[wordIndex], randSelection, fOrE);
                (ba1, ba2, ba3) = GetBadAnswers(wordList[wordIndex].Construct_Type, wordIndex, randSelection, fOrE);
                return new(question, answer, ba1, ba2, ba3);
            }
            catch //(Exception x)
            {
                throw;
            }
        }


        private static (string ques, string ans) GetGoodPair(Construct construct, int selection, int fOrE)
        {
            switch(construct)
            {
                case Verb verb:
                    if (fOrE == 0)
                    {
                        if (selection == 0)
                            return (verb.Infinitive[0].F, verb.Infinitive[0].E);
                        else 
                            return (verb.IndicatifPresent[selection - 1].F, verb.IndicatifPresent[selection - 1].E);
                    }
                    else
                    {
                        if (selection == 0)
                            return (verb.Infinitive[0].E, verb.Infinitive[0].F);
                        else 
                            return (verb.IndicatifPresent[selection - 1].E, verb.IndicatifPresent[selection - 1].F);
                    }
                case Number number:
                    if (fOrE == 0)
                    {
                        if (selection == 0)
                            return (number.Ordinal[0].F, number.Ordinal[0].E);
                        else
                            return (number.Cardinal[0].F, number.Cardinal[0].E);
                    }
                    else
                    {
                        if (selection == 0)
                            return (number.Ordinal[0].E, number.Ordinal[0].F);
                        else
                            return (number.Cardinal[0].E, number.Cardinal[0].F);
                    }
                case Phrase phrase:
                    if (fOrE == 0)
                    {
                        return (phrase.Definition[0].F, phrase.Definition[0].E);
                    }
                    else
                    {
                        return (phrase.Definition[0].E, phrase.Definition[0].F);
                    }
                case Adverb adverb:
                    if (fOrE == 0)
                    {
                        return (adverb.Adverbe[0].F, adverb.Adverbe[0].E);
                    }
                    else
                    {
                        return (adverb.Adverbe[0].E, adverb.Adverbe[0].F);
                    }
                case Noun noun:
                    if (fOrE == 0)
                    {
                        return (noun.Substantif[selection].F, noun.Substantif[selection].E);
                    }
                    else
                    {
                        return (noun.Substantif[selection].E, noun.Substantif[selection].F);
                    }
                case Adjective adjective:
                    if (fOrE == 0)
                    {
                        return (adjective.Adjectif[selection].F, adjective.Adjectif[selection].E);
                    }
                    else
                    {
                        return (adjective.Adjectif[selection].E, adjective.Adjectif[selection].F);
                    }
                case Article article:
                    if (fOrE == 0)
                    {
                        return (article.Art[selection].F, article.Art[selection].E);
                    }
                    else
                    {
                        return (article.Art[selection].E, article.Art[selection].F);
                    }
                case Pronoun pronoun:
                    if (fOrE == 0)
                    {
                        return (pronoun.Pronom[selection].F, pronoun.Pronom[selection].E);
                    }
                    else
                    {
                        return (pronoun.Pronom[selection].E, pronoun.Pronom[selection].F);
                    }
                default:
                        throw new Exception("undefined");
            }
        }


        private (string bad1, string bad2, string bad3) GetBadAnswers(ConstructType goodType, int goodIndex, int goodSelection, int fOrE)
        {
            List<string> badAnswers = new();
            List<(int,int)> badAnswersIndex = new() { (goodIndex, goodSelection) };

            int lastBadIndex = -1;
            int lastBadSelection = -1;
            int e = 0;

            try
            {

                for (int b = 0; b < 3; ++b)
                {
                    int baRandSelection;
                    int baRandIndex;
                    do
                    {
                        e = 1 + b*100;
                        if (badAnswersIndex.Count == 1)
                        {
                            e = 2+b*200;
                            int ind = random.Next(0, typeIndices[(int)goodType].Count);
                            baRandIndex = typeIndices[(int)goodType][ind];
                            baRandSelection = goodSelection;
                        }
                        else if (badAnswersIndex.Count == 3)
                        {
                            e = 3+b*300;
                            int ind = random.Next(0, typeIndices[(int)wordList[lastBadIndex].Construct_Type].Count);
                            baRandIndex = typeIndices[(int)wordList[lastBadIndex].Construct_Type][ind];
                            baRandSelection = lastBadSelection;
                        }
                        else
                        {
                            e = 4+b*400;
                            baRandIndex = random.Next(0, wordList.Count);
                            baRandSelection = wordList[baRandIndex] switch
                            {
                                Verb => random.Next(0, 11),
                                Number => random.Next(0, 2),
                                Phrase => 0,
                                Adverb => 0,
                                Noun => random.Next(0, 2),
                                Adjective => random.Next(0, 4),
                                Article => random.Next(0, 4),
                                Pronoun => random.Next(0, 2),
                                _ => throw new Exception("undefined"),
                            };
                            lastBadIndex = baRandIndex;
                            lastBadSelection = baRandSelection;
                        }
                    } while (badAnswersIndex.Contains((baRandIndex, baRandSelection)));

                    e = 5;
                    badAnswersIndex.Add((baRandIndex, baRandSelection));

                    switch (wordList[baRandIndex])
                    {
                        case Verb verb:
                            if (fOrE == 0)
                            {
                                if (baRandSelection == 0)
                                    badAnswers.Add(verb.Infinitive[0].E);
                                else 
                                    badAnswers.Add(verb.IndicatifPresent[baRandSelection - 1].E);
                            }
                            else
                            {
                                if (baRandSelection == 0)
                                    badAnswers.Add(verb.Infinitive[0].F);
                                else 
                                    badAnswers.Add(verb.IndicatifPresent[baRandSelection - 1].F);
                            }
                            break;
                        case Number number:
                            if (fOrE == 0)
                            {
                                if (baRandSelection == 0)
                                    badAnswers.Add(number.Ordinal[0].E);
                                else
                                    badAnswers.Add(number.Cardinal[0].E);
                            }
                            else
                            {
                                if (baRandSelection == 0)
                                    badAnswers.Add(number.Ordinal[0].F);
                                else
                                    badAnswers.Add(number.Cardinal[0].F);
                            }
                            break;
                        case Phrase phrase:
                            if (fOrE == 0)
                            {
                                badAnswers.Add(phrase.Definition[0].E);
                            }
                            else
                            {
                                badAnswers.Add(phrase.Definition[0].F);
                            }
                            break;
                        case Adverb adverb:
                            if (fOrE == 0)
                            {
                                badAnswers.Add(adverb.Adverbe[0].E);
                            }
                            else
                            {
                                badAnswers.Add(adverb.Adverbe[0].F);
                            }
                            break;
                        case Noun noun:
                            if (fOrE == 0)
                            {
                                badAnswers.Add(noun.Substantif[baRandSelection].E);
                            }
                            else
                            {
                                badAnswers.Add(noun.Substantif[baRandSelection].F);
                            }
                            break;
                        case Adjective adjective:
                            if (fOrE == 0)
                            {
                                badAnswers.Add(adjective.Adjectif[baRandSelection].E);
                            }
                            else
                            {
                                badAnswers.Add(adjective.Adjectif[baRandSelection].F);
                            }
                            break;
                        case Article article:
                            if (fOrE == 0)
                            {
                                badAnswers.Add(article.Art[baRandSelection].E);
                            }
                            else
                            {
                                badAnswers.Add(article.Art[baRandSelection].F);
                            }
                            break;
                        case Pronoun pronoun:
                            if (fOrE == 0)
                            {
                                badAnswers.Add(pronoun.Pronom[baRandSelection].E);
                            }
                            else
                            {
                                badAnswers.Add(pronoun.Pronom[baRandSelection].F);
                            }
                            break;
                        default:
                            throw new Exception("undefined");
                    }

                }
            }
            catch //(Exception x)
            {
                throw;
            }

            return (badAnswers[0], badAnswers[1], badAnswers[2]);
        }
    }
}

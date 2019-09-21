using System;

namespace CardsCreator.DomainModel
{
    public class Card
    {
        public Card(LanguageType sideOneLang, LanguageType sideTwoLang, string wordOne, string wordTwo)
        {
            SideOne = new Side { Type = sideOneLang, Text = wordOne };
            SideTwo = new Side { Type = sideTwoLang, Text = wordTwo };
        }

        public Side SideOne { get; set; }

        public Side SideTwo { get; set; }

        public bool IsCompleted => !string.IsNullOrWhiteSpace(SideTwo.Text) && !string.IsNullOrWhiteSpace(SideOne.Text);
    }

    public class Side
    {
        public LanguageType Type { get; set; }
        public string Text { get; set; }
    }
}

﻿using System;

namespace CardsCreator.DomainModel
{
    public class Card
    {
        public Card(LanguageType sideOneLang, LanguageType sideTwoLang, string wordOne, string wordTwo)
        {
            SideOne = new Side { LanguageType = sideOneLang, Text = wordOne };
            SideTwo = new Side { LanguageType = sideTwoLang, Text = wordTwo };
        }

        public Side SideOne { get; set; }

        public Side SideTwo { get; set; }

        public bool IsCompleted => !string.IsNullOrWhiteSpace(SideTwo.Text) && !string.IsNullOrWhiteSpace(SideOne.Text) && 
                                    SideOne.LanguageType != LanguageType.Undefined && SideTwo.LanguageType != LanguageType.Undefined;
    }

    public class Side
    {
        public LanguageType LanguageType { get; set; }
        public string Text { get; set; }
    }
}

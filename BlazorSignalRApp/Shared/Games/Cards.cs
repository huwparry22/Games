using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorSignalRApp.Shared.Games
{
    public static class Cards
    {
        public static List<Card> GetCards()
        {
            var cards = new List<Card>();

            foreach(var suit in Enum.GetValues(typeof(Suit)))
            {
                foreach(var face in Enum.GetValues(typeof(Face)))
                {
                    cards.Add(new Card
                    {
                        Suit = (Suit)suit,
                        Face = (Face)face
                    });
                }
            }

            return cards;
        }
    }

    public class Card
    {
        public Suit Suit { get; set; }
        public Face Face { get; set; }
    }

    public enum Suit
    {
        Spades,
        Clubs,
        Diamonds,
        Hearts
    }

    public enum Face
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
}

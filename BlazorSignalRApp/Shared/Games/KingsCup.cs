using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorSignalRApp.Shared.Games
{
    public class KingsCup
    {
        private List<string> _players = new List<string>();
        private List<Card> _cards;

        public KingsCup()
        {
            _cards = Cards.GetCards();
        }

        public void AddPlayer(string playerName)
        {
            _players.Add(playerName);
        }

        public List<string> GetAllPlayers()
        {
            return _players;
        }

        public Card GetNextCard()
        {
            var random = new Random();
            int randomIndex = random.Next(0, _cards.Count - 1);

            var card = _cards[randomIndex];
            _cards.RemoveAt(randomIndex);

            return card;
        }
    }
}

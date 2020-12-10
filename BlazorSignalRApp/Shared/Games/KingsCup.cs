using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazorSignalRApp.Shared.Games
{
    public class KingsCup
    {
        private List<string> _players = new List<string>();
        private List<Card> _cards;
        private int _currentPlayerIndex = 0;
        private Card _currentCard;

        public KingsCup()
        {
            _cards = Cards.GetCards();
        }

        public string CurrentPlayer => _players[_currentPlayerIndex];

        public Card CurrentCard => _currentCard;

        private void MoveToNextPlayer()
        {
            if (_currentPlayerIndex >= _players.Count - 1)
            {
                _currentPlayerIndex = 0;
            }
            else
            {
                _currentPlayerIndex++;
            }
        }

        public string AddPlayer(string playerName)
        {
            if (_players.Any(p => p.Trim().Equals(playerName.Trim(), StringComparison.InvariantCultureIgnoreCase)))
            {
                return $"Player {playerName} already added.";
            }

            _players.Add(playerName);

            return null;
        }

        public string RemovePlayer(string playerName)
        {
            if (!_players.Any(p => p.Trim().Equals(playerName.Trim(), StringComparison.InvariantCultureIgnoreCase)))
            {
                return $"Player {playerName} not in this game.";
            }

            _players.Remove(playerName.Trim());

            MoveToNextPlayer();

            return null;
        }

        public List<string> GetAllPlayers()
        {
            return _players;
        }

        public void SetNextCard()
        {
            var random = new Random();
            int randomIndex = random.Next(0, _cards.Count - 1);

            _currentCard = _cards[randomIndex];
            _cards.RemoveAt(randomIndex);

            MoveToNextPlayer();
        }
    }
}

using BlazorSignalRApp.Shared.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSignalRApp.Shared.Models
{
    public class KingsCupModel
    {
        public string ErrorMessage { get; set; }

        public List<string> Players { get; set; }

        public string CurrentPlayer { get; set; }

        public Card CurrentCard { get; set; }

        public int CardsLeft { get; set; }
    }
}

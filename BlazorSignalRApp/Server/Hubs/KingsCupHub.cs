using BlazorSignalRApp.Shared.Games;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSignalRApp.Server.Hubs
{
    public class KingsCupHub : Hub
    {
        private static KingsCup _kingsCup = new KingsCup();

        public async Task GetAllPlayers()
        {
            await Clients.All.SendAsync("OnGetAllPlayers", _kingsCup.GetAllPlayers()).ConfigureAwait(false);
        }

        public async Task AddPlayer(string playerName)
        {
            _kingsCup.AddPlayer(playerName);

            await Clients.All.SendAsync("OnPlayerAdded", playerName).ConfigureAwait(false);
        }

        public async Task GetNextCard()
        {
            var card = _kingsCup.GetNextCard();

            await Clients.All.SendAsync("OnReceivedNextCard", card).ConfigureAwait(false);
        }
    }
}

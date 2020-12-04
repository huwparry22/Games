using BlazorSignalRApp.Shared.Games;
using BlazorSignalRApp.Shared.Models;
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

        public async Task AddPlayer(string playerName)
        {
            var errorMessage = _kingsCup.AddPlayer(playerName);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                await Clients.All.SendAsync("OnError", errorMessage).ConfigureAwait(false);
            }
            else
            {
                await Clients.Caller.SendAsync("OnPlayerAdded", playerName).ConfigureAwait(false);

                await ReturnKingsCupGame().ConfigureAwait(false);
            }
        }

        public async Task GetNextCard()
        {
            _kingsCup.SetNextCard();

            await ReturnKingsCupGame().ConfigureAwait(false);
        }

        public async Task GetCurrentGameState()
        {
            await ReturnKingsCupGame().ConfigureAwait(false);
        }

        private async Task ReturnKingsCupGame()
        {
            var model = new KingsCupModel
            {
                Players = _kingsCup.GetAllPlayers(),
                CurrentPlayer = _kingsCup.CurrentPlayer,
                CurrentCard = _kingsCup.CurrentCard
            };

            await Clients.All.SendAsync("OnKingsCupUpdated", model).ConfigureAwait(false);
        }
    }
}

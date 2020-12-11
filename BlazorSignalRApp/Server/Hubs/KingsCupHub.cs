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

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            if (exception is null)
            {
                var playerName = Context.Items.First(i => i.Key.ToString() == "PlayerName").Value.ToString();

                await RemovePlayer(playerName).ConfigureAwait(false);

                await SetInfoMessage($"{playerName} has left the game.").ConfigureAwait(false);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task AddPlayer(string playerName)
        {
            var errorMessage = _kingsCup.AddPlayer(playerName);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                await SetErrorMessage(errorMessage).ConfigureAwait(false);
            }
            else
            {
                await Clients.Caller.SendAsync("OnPlayerAdded", playerName).ConfigureAwait(false);

                Context.Items.Add("PlayerName", playerName);

                await SetInfoMessage($"{playerName} has been added to the game.").ConfigureAwait(false);

                await ReturnKingsCupGame().ConfigureAwait(false);
            }
        }

        public async Task RemovePlayer(string playerName)
        {
            var errorMessage = _kingsCup.RemovePlayer(playerName);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                await SetErrorMessage(errorMessage).ConfigureAwait(false);
            }

            await ReturnKingsCupGame().ConfigureAwait(false);
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

        private async Task SetErrorMessage(string errorMessage)
        {
            await Clients.All.SendAsync("OnError", errorMessage).ConfigureAwait(false);
        }

        private async Task SetInfoMessage(string infoMessage)
        {
            await Clients.All.SendAsync("OnInfo", infoMessage).ConfigureAwait(false);
        }
    }
}

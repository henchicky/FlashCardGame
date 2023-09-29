using FlashCardGameBackend.Models;

namespace FlashCardGameBackend.Services;

public interface IGameService
{
    GameDetails GenerateGameDetails(GameDetailsRequest request);
    bool CheckInput(CheckInputRequest request);
}
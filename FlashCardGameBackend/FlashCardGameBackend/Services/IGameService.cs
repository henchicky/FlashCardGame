using FlashCardGameBackend.Models;

namespace FlashCardGameBackend.Services;

public interface IGameService
{
    GameDetails GenerateGameRequest(GameDetailsRequest request);
    bool CheckInput(int input, string user);
}